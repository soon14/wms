using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.Utils;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmChooseSuppliers : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 选中的货位
        /// </summary>
        public List<SupplierEntity> Suppliers
        {
            get;
            set;
        }

        public FrmChooseSuppliers()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 按照次序排序的供应商列表
        /// </summary>
        /// <returns></returns>
        public List<SupplierEntity> ListActiveSupplierByPriority()
        {
            List<SupplierEntity> list = new List<SupplierEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_ListActiveSupplierByPriorityZJQ);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonSupplier bill = JsonConvert.DeserializeObject<JsonSupplier>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonSupplierResult jbr in bill.result)
                {
                    SupplierEntity asnEntity = new SupplierEntity();
                    #region 
                    asnEntity.Address = jbr.address;
                    asnEntity.AreaID = jbr.areaId;
                    asnEntity.AreaName = jbr.arName;
                    asnEntity.ContactName = jbr.contact;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.IsOwn = jbr.isOwn;
                    asnEntity.Phone = jbr.phone;
                    asnEntity.Postcode = jbr.postCode;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.SortOrder = Convert.ToInt32(jbr.sortOrder);
                    asnEntity.SupplierCode = jbr.sCode;
                    asnEntity.SupplierName = jbr.sName;
                    asnEntity.SupplierNameS = jbr.nameS;
                    asnEntity.UpdateBy = jbr.updateBy;
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.updateDate))
                            asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("UcAsnQueryEngine+ListActiveSupplierByPriority", msg);
                    }
                    #endregion
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                //绑定供应商
                bindingSource1.DataSource = ListActiveSupplierByPriority();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            int[] selectedIndexs = gridView1.GetSelectedRows();
            if (selectedIndexs.Length == 0)
                MsgBox.Warn("未选中任何行。");
            else
            {
                //转换为集合
                Suppliers = new List<SupplierEntity>();
                foreach (int handler in selectedIndexs)
                {
                    if (handler >= 0)
                    {
                        Suppliers.Add(gridView1.GetRow(handler) as SupplierEntity);
                    }
                }

                if (Suppliers.Count > 0)
                    this.DialogResult = DialogResult.OK;
                else
                    MsgBox.Warn("未选中任何数据行。");
            }
        }
    }
}