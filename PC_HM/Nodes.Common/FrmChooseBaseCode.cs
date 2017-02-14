using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Common
{
    public partial class FrmChooseBaseCode : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        private BaseCodeEntity _selectedBaseCode = null;
        private string _groupID = string.Empty;
        #endregion

        #region 构造函数

        public FrmChooseBaseCode()
        {
            InitializeComponent();
        }
        public FrmChooseBaseCode(string groupID)
            : this()
        {
            this._groupID = groupID;
        }
        public FrmChooseBaseCode(string groupID, string title)
            : this(groupID)
        {
            this.labelControl1.Text = title;
        }

        #endregion

        #region Override Methods
        /// <summary>
        /// 收货单据管理， baseCode信息查询(用于业务类型和单据状态筛选条件)
        /// 获取活动状态的集合
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public  List<BaseCodeEntity> GetItemList(string groupCode)
        {
            List<BaseCodeEntity> list = new List<BaseCodeEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetItemList);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBaseCodeInfo bill = JsonConvert.DeserializeObject<JsonBaseCodeInfo>(jsonQuery);
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
                foreach (JsonBaseCodeInfoResult jbr in bill.result)
                {
                    BaseCodeEntity asnEntity = new BaseCodeEntity();
                    asnEntity.GroupCode = jbr.groupCode;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.ItemDesc = jbr.itemDesc;
                    asnEntity.ItemValue = jbr.itemValue;
                    asnEntity.Remark = jbr.remark;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                List<BaseCodeEntity> list = GetItemList(this._groupID);
                this.bindingSource1.DataSource = list;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 属性
        public BaseCodeEntity SelectedBaseCode
        {
            get { return this._selectedBaseCode; }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 确认
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this._selectedBaseCode = this.cboVehicle.EditValue as BaseCodeEntity;
            if (this._selectedBaseCode == null)
            {
                MsgBox.Warn("请选择...");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
