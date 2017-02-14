using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Nodes.Utils;
using Nodes.DBHelper;
using DevExpress.XtraEditors.Repository;
using Nodes.Shares;
using Nodes.Entities;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.Icons;
using Nodes.Controls;

namespace Nodes.Instore
{
    public partial class FrmPoEdit : DevExpress.XtraEditors.XtraForm
    {
        PODal poDal = new PODal();
        POQueryDal poQueryDal = new POQueryDal();
        string BillID = null;
        bool IsCopyNew = false;

        public FrmPoEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开单据以编辑
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="isCopyNew">true：打开并新建，false：仅打开</param>
        public FrmPoEdit(string billID, bool isCopyNew)
            : this()
        {
            this.BillID = billID;
            this.IsCopyNew = isCopyNew;
            //if (!isCopyNew)
            //{
            //    bar1.Visible = false;
            //    layoutControlItem6.Visibility = layoutControlItem11.Visibility = layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = IconHelper.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)IconHelper.Images.add;
            barButtonItem5.ImageIndex = (int)IconHelper.Images.open;
            barButtonItem4.ImageIndex = (int)IconHelper.Images.copy;
            barButtonItem6.ImageIndex = (int)IconHelper.Images.save;
            barButtonItem7.ImageIndex = (int)IconHelper.Images.basedata;
            barButtonItem2.ImageIndex = (int)IconHelper.Images.print;

            //初始化控件的值
            InitUI();
            CustomFields.AppendMaterialFields(gridView1);
            if (!string.IsNullOrEmpty(BillID)) ShowPODetail();
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
                UserDal userDal = new UserDal();
                listSales.Properties.DataSource = userDal.ListUsersByRoleAndOrgCode(GlobeSettings.LoginedUser.OrgCode, GlobeSettings.POSalesRoleName);

                //绑定业务类型并默认选中第一个
                List<BaseCodeEntity> instoreTypes = BaseCodeDal.GetItemList(BaseCodeConstant.PO_TYPE);
                listBillType.Properties.DataSource = instoreTypes;
                if (instoreTypes.Count > 0) listBillType.EditValue = instoreTypes[0].ItemValue;

                //明细表格默认绑定到LineEntity集合
                bindingSource1.DataSource = new List<PODetailEntity>();
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
                    ChoosePO();
                    break;
                case "复制":
                    ClearBillID();
                    break;
                case "存为草稿":
                    SaveAsDraft();
                    break;
                case "提交待审":
                    SaveForApprove();
                    break;
                case "打印采购单":
                    MsgBox.Warn("正在实现...");
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
                case "删除":
                    gridView1.DeleteSelectedRows();
                    break;
                case "缺料查询":
                    MsgBox.Warn("正在实现中...");
                    break;
            }
        }

        #region 保存到数据库
        /// <summary>
        /// 存为草稿
        /// </summary>
        void SaveAsDraft()
        {
            if (!CanSave()) return;

            //存入数据库
            SaveToDatabase(false);
        }

        /// <summary>
        /// 提交待审
        /// </summary>
        void SaveForApprove()
        {
            if (!CanSave()) return;

            if (MsgBox.AskOK("确定要提交吗？提交后将不允许再次修改。") != DialogResult.OK)
                return;

            //存入数据库
            SaveToDatabase(true);
        }

        /// <summary>
        /// 处理写入数据库
        /// </summary>
        void SaveToDatabase(bool commitNow)
        {
            try
            {
                //提交，通过BillID可以得知是否新建
                POBodyEntity po = ReadyEntityToSave();
                string errMsg = string.Empty;
                string result = poDal.SaveBill(po, commitNow, GlobeSettings.LoginedUser.UserName, out errMsg);
                switch (result)
                {
                    case "1":
                        txtBillID.Text = po.BillID;
                        txtBillState.Text = po.BillStateDesc;
                        MsgBox.OK("保存成功。");
                        break;
                    case "2":
                    case "4":
                        MsgBox.Warn("更新单据信息失败，可能该单据已经被其他人删除。");
                        break;
                    case "3":
                        MsgBox.Warn("该单据当前状态不允许编辑。");
                        break;
                    case "11":
                        MsgBox.Warn(string.Format("保存失败，物料‘{0}’不存在。", errMsg));
                        break;
                    case "12":
                        MsgBox.Warn(string.Format("保存失败，计量单位‘{0}’不存在。", errMsg));
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
        POBodyEntity ReadyEntityToSave()
        {
            POBodyEntity po = new POBodyEntity();
            po.Details = bindingSource1.DataSource as List<PODetailEntity>;

            //组织表头，不要填写BillID，业务员直接保存名称
            po.BillID = txtBillID.Text;
            po.OrgCode = GlobeSettings.LoginedUser.OrgCode;
            po.SourceBillID = txtSourceBillID.Text.Trim();
            po.Sales = listSales.Text;
            po.Supplier = ConvertUtil.ToString(listSupplier.EditValue);
            po.BillType = ConvertUtil.ToString(listBillType.EditValue);
            po.ContractNO = txtContractNO.Text.Trim();
            po.Remark = txtRemark.Text.Trim();
            po.Creator = GlobeSettings.LoginedUser.UserName;

            //下面是可扩展的字段
            //po.PO_STR1 = ...
            //...
            //
            //po.PO_DATE2 = ...

            return po;
        }

        /// <summary>
        /// 查看是否可以保存了
        /// 检查必填项
        /// </summary>
        /// <returns></returns>
        bool CanSave()
        {
            //
            if (listSales.EditValue == null)
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
                    MsgBox.Warn(string.Format("第“{0}”行数量填写有误，必须为大于0的数值。", i+1));
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
            txtSourceBillID.Text = "";
            listSales.EditValue = null;
            txtRemark.Text = txtContractNO.Text = "";
        }

        void ClearDetail()
        {
            bindingSource1.Clear();
            bindingSource1.ResetBindings(false);
        }
        #endregion

        void ChoosePO()
        {
            FrmPoQuery frmQuery = new FrmPoQuery();
            if (frmQuery.ShowDialog() == DialogResult.OK)
            {
                ShowPODetail(frmQuery.FocusedHeader);
            }
        }

        void ShowPODetail(POBodyEntity po)
        {
            txtBillID.Text = po.BillID;
            txtSourceBillID.Text = po.SourceBillID;
            listSales.Text = po.Sales;
            listSales.ClosePopup();
            listSupplier.EditValue = po.Supplier;
            listBillType.EditValue = po.BillType;
            txtContractNO.Text = po.ContractNO;
            txtRemark.Text = po.Remark;
            txtBillState.Text = po.BillStateDesc;

            bindingSource1.DataSource = po.Details;
        }

        void ShowPODetail()
        {
            if (!string.IsNullOrEmpty(this.BillID))
            {
                POBodyEntity header = poQueryDal.GetBillHeader(this.BillID);
                if (header != null)
                {
                    header.Details = poQueryDal.GetDetailByBillID(this.BillID);
                    ShowPODetail(header);

                    //复制并创建新单据
                    if (this.IsCopyNew)
                        ClearBillID();
                }
            }
        }

        void AddRow()
        {
            FrmChooseMaterial frmItemEdit = new FrmChooseMaterial(ConvertUtil.ObjectToNull(listSupplier.EditValue));
            frmItemEdit.OnInsertItem += delegate(MaterialEntity material, decimal qty)
            {
                List<PODetailEntity> lines = bindingSource1.DataSource as List<PODetailEntity>;
                PODetailEntity line = lines.Find(d => d.MaterialCode == material.MaterialCode && d.Price == material.Price);
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

        private void OnGridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                gridView1.DeleteSelectedRows();
        }
    }
}