using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.OutBound;
using Nodes.DBHelper.Outbound;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmContainerLocatiomManager : DevExpress.XtraEditors.XtraForm
    {
        //SOCtlDal soQueryDal = new SOCtlDal();
        public FrmContainerLocatiomManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 基础管理（容器位维护-查询所有容器位）
        /// </summary>
        /// <returns></returns>
        public List<SoContainerLocation> QeryCTL()
        {
            List<SoContainerLocation> list = new List<SoContainerLocation>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("whCode=").Append(warehouseCode).Append("&");
                //loStr.Append("state=").Append(state);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_QeryCTL);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQeryCTL bill = JsonConvert.DeserializeObject<JsonQeryCTL>(jsonQuery);
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
                foreach (JsonQeryCTLResult jbr in bill.result)
                {
                    SoContainerLocation asnEntity = new SoContainerLocation();
                    #region
                    asnEntity.CTLName = jbr.ctlName;
                    asnEntity.CTLState = jbr.itemDesc1;
                    asnEntity.CTlType = jbr.itemDesc2;
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

        private void Reload()
        {
            try
            {
                bindingSource1.DataSource = QeryCTL();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }


        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            switch (tag)
            {
                case "刷新":
                    Reload();
                    break;
                case "新增":
                    DoCreateCtl();
                    break;
                case "删除":
                    DoDeleteCtl();
                    break;
                case "修改":
                    DoEditCtl();
                    break;
            }
        }
        /// <summary>
        /// 获取选择的行值
        /// </summary>
        SoContainerLocation SelectedRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as SoContainerLocation;
            }
        }

        private void DoEditCtl()
        {
            SoContainerLocation changeEntity = SelectedRow;

            if (changeEntity == null)
            {
                MsgBox.Warn("请选择要修改的托盘位！");
                return;
            }

            FrmCtlEdit frmCtlEdit = new FrmCtlEdit(changeEntity);
            frmCtlEdit.DataSourceChanged += OnCreateChanage;
            frmCtlEdit.ShowDialog();
        }

        /// <summary>
        /// 基础管理（容器位维护-删除容器位）
        /// </summary>
        /// <param name="ctlName"></param>
        /// <param name="ctlType"></param>
        /// <returns></returns>
        public bool DeleteCTL(string ctlName, string ctlType)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctlName=").Append(ctlName).Append("&");
                loStr.Append("ctlType=").Append(ctlType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteCTL);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteCtl()
        {
            //soQueryDal.DeleteCTL();
            SoContainerLocation removeEntity = SelectedRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (removeEntity.CTLState == "占用")
            {
                MsgBox.Warn("不能删除被占用的托盘位");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除托盘位编号“({0}){1}”吗？", removeEntity.CTLName, removeEntity.CTLState)) == DialogResult.OK)
            {
                bool ret = DeleteCTL(removeEntity.CTLName, removeEntity.CTlType);
                if (ret)
                {
                    bindingSource1.Remove(removeEntity);
                }
                //else
                //{
                //    MsgBox.Warn("删除失败。");
                //    return;
                //}
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        private void DoCreateCtl()
        {
            FrmCtlEdit frmCtlEdit = new FrmCtlEdit();
            frmCtlEdit.DataSourceChanged += OnCreateChanage;
            frmCtlEdit.ShowDialog();
        }
        private void OnCreateChanage(object sender, EventArgs e)
        {
            SoContainerLocation newEntity = (SoContainerLocation)sender;
            Reload();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditChanage(object sender, EventArgs e)
        {
            DoEditCtl();
            bindingSource1.ResetBindings(false);
        }

        private void FrmContainerLocatiomManager_Load(object sender, EventArgs e)
        {
            Reload();
        }
    }
}