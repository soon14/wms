using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Shares;
using Nodes.Utils;

namespace Nodes.Instore
{
    public partial class FrmAsnEdit : DevExpress.XtraEditors.XtraForm
    {
        AsnDal asnDal = new AsnDal();
        AsnQueryDal asnQueryDal = new AsnQueryDal();
        AsnBodyEntity EditedAsnBody = null;
        bool IsCopyNew = false;

        public FrmAsnEdit()
        {
            InitializeComponent();
        }

        public FrmAsnEdit(AsnBodyEntity editedAsnBody, bool isCopyNew)
            : this()
        {
            EditedAsnBody = editedAsnBody;
            this.IsCopyNew = isCopyNew;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.add;
            barButtonItem5.ImageIndex = (int)AppResource.EIcons.open;
            barButtonItem4.ImageIndex = (int)AppResource.EIcons.copy;
            barButtonItem6.ImageIndex = (int)AppResource.EIcons.save;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.print;

            //初始化控件的值
            InitUI();
            CustomFields.AppendLotExpFields(gridView1, true);
            CustomFields.AppendMaterialFields(gridView1);
            if (EditedAsnBody != null) ShowAsnDetail(EditedAsnBody);
        }

        /// <summary>
        /// 处理控件及内容的初始化，对于下拉列表默认选中第一个
        /// </summary>
        void InitUI()
        {
            try
            {
                //绑定供应商
                List<SupplierEntity> suppliers = new SupplierDal().ListActiveSupplierByPriority();
                listSupplier.Properties.DataSource = suppliers;
                if (suppliers.Count > 0) listSupplier.EditValue = suppliers[0].SupplierCode;

                //绑定采购业务员
                listRespPerson.Properties.DataSource = new UserDal().ListUsersByRoleAndOrgCode(GlobeSettings.LoginedUser.OrgCode, GlobeSettings.POSalesRoleName);

                //绑定业务类型并默认选中第一个
                List<BaseCodeEntity> billTypes = BaseCodeDal.GetItemList(BaseCodeConstant.PO_TYPE);
                listBillType.Properties.DataSource = billTypes;
                if (billTypes.Count > 0) listBillType.EditValue = billTypes[0].ItemValue;

                //绑定入库方式并默认选中第一个
                List<BaseCodeEntity> instoreTypes = BaseCodeDal.GetItemList(BaseCodeConstant.INSTORE_TYPE);
                listInstoreType.Properties.DataSource = instoreTypes;
                if (instoreTypes.Count > 0) listInstoreType.EditValue = instoreTypes[0].ItemValue;

                //明细表格默认绑定到LineEntity集合
                bindingSource1.DataSource = new List<PODetailEntity>();
                dateEditDelivery.DateTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            DoClickEvent(tag);
        }

        void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "新建":
                    ClearForm();
                    break;
                case "打开":
                    OpenAsnBill();
                    break;
                case "复制":
                    ClearBillID();
                    break;
                case "保存":
                    SaveToDatabase();
                    break;
                default:
                    MsgBox.Warn("未实现。");
                    break;
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            switch (btn.Tag.ToString())
            {
                case "添加":
                    AddRow();
                    break;
                case "删除选中行":
                    gridView1.DeleteSelectedRows();
                    break;
            }
        }

        #region 保存到数据库

        /// <summary>
        /// 处理写入数据库
        /// </summary>
        void SaveToDatabase()
        {
            if (!CanSave()) return;

            try
            {
                //提交，通过BillID可以得知是否新建
                AsnBodyEntity asn = PrepareData();
                string errMsg = string.Empty;
                string result = asnDal.SaveBill(asn, GlobeSettings.LoginedUser.UserName, out errMsg);
                switch (result)
                {
                    case "1":
                        txtBillID.Text = asn.BillID;
                        txtBillState.Text = asn.BillStateDesc;
                        MsgBox.OK("保存成功。");
                        break;
                    case "2":
                    case "4":
                        MsgBox.Warn("更新单据信息失败，可能该单据已经被其他人删除。");
                        break;
                    case "3":
                        MsgBox.Warn("该单据当前状态不允许修改，只有“尚未到货确认”的单据才能修改。");
                        break;
                    case "11":
                        MsgBox.Warn(string.Format("保存失败，物料‘{0}’不存在。", errMsg));
                        break;
                    default:
                        MsgBox.Err("保存失败，错误信息未知，请记下操作轨迹，联系管理员协助解决。");
                        break;
                }                
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 用窗体的内容初始化实体对象
        /// </summary>
        AsnBodyEntity PrepareData()
        {
            AsnBodyEntity asn = new AsnBodyEntity();
            asn.Details = bindingSource1.DataSource as List<PODetailEntity>;

            //组织表头，不要填写BillID，业务员直接保存名称
            asn.BillID = txtBillID.Text;
            asn.OrgCode = GlobeSettings.LoginedUser.OrgCode;
            asn.PoNO = txtPO.Text.Trim();
            asn.Sales = listRespPerson.Text;
            asn.Supplier = ConvertUtil.ToString(listSupplier.EditValue);
            asn.InstoreType = ConvertUtil.ToString(listInstoreType.EditValue);
            asn.BillType = ConvertUtil.ToString(listBillType.EditValue);
            asn.Remark = txtRemark.Text.Trim();
            asn.Creator = GlobeSettings.LoginedUser.UserName;
            asn.ContractNO = txtContractNO.Text.Trim();
            asn.DeliveryDate = dateEditDelivery.DateTime;

            return asn;
        }

        /// <summary>
        /// 查看是否可以保存了
        /// 检查必填项
        /// </summary>
        /// <returns></returns>
        bool CanSave()
        {
            //采购单是否必须选择？
            if (string.IsNullOrEmpty(txtPO.Text))
            {
                MsgBox.Warn("请选择采购单。");
                return false;
            }

            //
            if (listRespPerson.EditValue == null)
            {
                MsgBox.Warn("请选择业务员。");
                return false;
            }

            if (listSupplier.EditValue == null)
            {
                MsgBox.Warn("请选择供应商。");
                return false;
            }

            gridView1.CloseEditor();

            //至少有一行明细
            if (bindingSource1.Count == 0)
            {
                MsgBox.Warn("请填写明细行。");
                return false;
            }

            //查看明细是否填充完整：物料编码与数量
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                PODetailEntity line = gridView1.GetRow(i) as PODetailEntity;
                if (!line.PlanQty.HasValue || line.PlanQty.Value <= 0)
                {
                    MsgBox.Warn(string.Format("第{0}行数量填写有误，必须为大于0的数值。", i+1));
                    return false;
                }
            }

            //查看是否有相同的物料，提示合并，让用户自己合并
            if (HasSameMaterial())
            {
                if (MsgBox.AskYes("明细中存在相同的物料编码行，建议先合并再保存？选择“是”将继续，“否”将返回。") == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 检查是否有相同的物料编码行
        /// </summary>
        /// <returns></returns>
        bool HasSameMaterial()
        {
            List<PODetailEntity> lines = bindingSource1.DataSource as List<PODetailEntity>;

            //查看是否有相同的物料行
            bool found = false;
            for (int i = 1; i < lines.Count; i++)
            {
                int exist = lines.FindIndex(i, r => r.MaterialCode == lines[i - 1].MaterialCode);
                if (exist > 0)
                {
                    found = true;
                    break;
                }
            }

            return found;
        }
        #endregion

        #region 清空界面内容
        void ClearForm()
        {
            if (MsgBox.AskOK("确定要清空界面内容吗？") != DialogResult.OK)
                return;

            ClearBillID();
            ClearHeader();
            ClearDetail();
        }

        void ClearBillID()
        {
            txtBillID.Text = txtBillState.Text = null;
        }

        /// <summary>
        /// 清空单据头信息
        /// 注意：不会经常变动的不要清空，例如采购类型、入库类型
        /// </summary>
        void ClearHeader()
        {
            txtPO.Text = txtContractNO.Text = txtRemark.Text = "";
            listRespPerson.EditValue = null;
        }

        void ClearDetail()
        {
            bindingSource1.Clear();
            bindingSource1.ResetBindings(false);
        }
        #endregion

        #region 打开与编辑选中行
        void OpenAsnBill()
        {
            FrmAsnQuery frmQuery = new FrmAsnQuery();
            if (frmQuery.ShowDialog() == DialogResult.OK)
            {
                ShowAsnDetail(frmQuery.FocusedHeader);
            }
        }

        void ShowAsnDetail(AsnBodyEntity asn)
        {
            if (asn != null)
            {
                txtBillID.Text = asn.BillID;
                txtPO.Text = asn.PoNO;
                listRespPerson.Text = asn.Sales;
                listRespPerson.ClosePopup();
                listSupplier.EditValue = asn.Supplier;
                listBillType.EditValue = asn.BillType;
                txtRemark.Text = asn.Remark;
                txtBillState.Text = asn.BillStateDesc;

                bindingSource1.DataSource = asn.Details;

                //复制并创建新单据
                if (this.IsCopyNew)
                    ClearBillID();
            }
        }

        void ShowAsnDetail(string billID)
        {
            if (!string.IsNullOrEmpty(billID))
            {
                AsnBodyEntity header = asnQueryDal.GetBillHeader(billID);
                if (header != null)
                {
                    header.Details = asnQueryDal.GetDetailByBillID(billID);
                    ShowAsnDetail(header);

                    //复制并创建新单据
                    if (this.IsCopyNew)
                        ClearBillID();
                }
            }
        }
        #endregion

        void AddRow()
        {
            FrmChooseMaterial frmItemEdit = new FrmChooseMaterial(null);
            frmItemEdit.OnInsertItem += delegate(MaterialEntity material, decimal qty)
            {
                List<PODetailEntity> lines = bindingSource1.DataSource as List<PODetailEntity>;
                PODetailEntity line = lines.Find(d => 
                    d.MaterialCode == material.MaterialCode 
                    && d.Price == material.Price);
                if (line != null)
                {
                    line.PlanQty = line.PlanQty + qty;
                }
                else
                {
                    line = new PODetailEntity();
                    line.PlanQty = qty;
                    line.Copy(material);
                    bindingSource1.Add(line);
                }

                bindingSource1.ResetBindings(false);
            };
            frmItemEdit.ShowDialog();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                gridView1.DeleteSelectedRows();
        }

        // 从采购单创建收货单，一个采购单可以创建多个收货单，正常说应该以实收数量来限制收货单入库总量不能大于采购订单量
        private void OnChoosePOButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                using (FrmPoQuery frmQuery = new FrmPoQuery())
                {
                    //如果只启用一审，则传参一审
                    string billState = string.Concat(BillStateConst.PO_STATE_CODE_SECOND_APPROVED, ",", BillStateConst.PO_STATE_CODE_RECEIVING);
                    if (CustomFields.ApproveType.ItemValue == "1")
                        billState = string.Concat(BillStateConst.PO_STATE_CODE_FIRST_APPROVED, ",", BillStateConst.PO_STATE_CODE_RECEIVING);

                    frmQuery.LockThisState(billState);

                    if (frmQuery.ShowDialog() == DialogResult.OK)
                    {
                        POBodyEntity header = frmQuery.FocusedHeader;
                        txtPO.Text = header.BillID;
                        listBillType.EditValue = header.BillType;
                        listRespPerson.EditValue = header.Sales;
                        listSupplier.EditValue = header.Supplier;
                        txtContractNO.Text = header.ContractNO;

                        bindingSource1.DataSource = header.Details;
                    }
                }
            }
            else
            {
                ClearHeader();
            }
        }
    }
}