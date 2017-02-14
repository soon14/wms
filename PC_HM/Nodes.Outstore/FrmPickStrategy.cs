using System;
using System.Windows.Forms;
using System.Collections.Generic;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmPickStrategy : DevExpress.XtraEditors.XtraForm
    {
        SOHeaderEntity selectedBill = null;
        //private SODal soDal;
        public event EventHandler dataSourceChanged = null;

        public FrmPickStrategy(SOHeaderEntity selectedBill)
        {
            InitializeComponent();

            this.selectedBill = selectedBill;
            txtBillNO.Text = selectedBill.BillNO;
        }

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

        private void OnFormLoad(object sender, EventArgs e)
        {
            //soDal = new SODal();

            listPickType.Properties.DataSource = GetItemList(BaseCodeConstant.OUTSTORE_TYPE);
            listZnType.Properties.DataSource = GetItemList(BaseCodeConstant.ZONE_TYPE);

            listPickType.EditValue = selectedBill.OutstoreType;
            listZnType.EditValue = selectedBill.PickZnType;
        }


        /// <summary>
        /// 拣货计划（保存发货方式及拣货区域）
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="pickType"></param>
        /// <param name="znType"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SaveStrategy(int billID, string pickType, string znType, string userCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("pickType=").Append(pickType).Append("&");
                loStr.Append("znType=").Append(znType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveStrategy);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return -7;
                }
                #endregion

                #region 正常错误处理

                JsonSaveStrategy bill = JsonConvert.DeserializeObject<JsonSaveStrategy>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return -7;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return -7;
                }
                #endregion
                if (bill.result != null && bill.result.Length > 0)
                    return Convert.ToInt32(bill.result[0].vResult);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return -7;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listPickType.EditValue == null)
            {
                MsgBox.Warn("请选择拣货方式。");
                return;
            }

            if (listZnType.EditValue == null)
            {
                MsgBox.Warn("请选择拣货区域。");
                return;
            }

            if (MsgBox.AskOK("确定要保存拣货方式吗？") != DialogResult.OK)
                return;

            string pickType = ConvertUtil.ToString(listPickType.EditValue);
            string znType = ConvertUtil.ToString(listZnType.EditValue);

            try
            {
                int ret = SaveStrategy(selectedBill.BillID, pickType, znType, GlobeSettings.LoginedUser.UserCode);
                if (ret == -1)
                    MsgBox.Warn(string.Format("未找到单据“{0}”，可能已经被删除。", selectedBill.BillNO));
                else if (ret == -2)
                    MsgBox.Warn(string.Format("单据“{0}”的状态不是等待拣配计算，提交更新失败。", selectedBill.BillNO));
                else
                {
                    selectedBill.OutstoreType = pickType;
                    selectedBill.OutstoreTypeName = listPickType.Text;
                    selectedBill.PickZnType = znType;
                    selectedBill.PickZnTypeName = listZnType.Text;

                    if (dataSourceChanged != null)
                        dataSourceChanged(null, null);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
        }

        private void listPickType_EditValueChanged(object sender, EventArgs e)
        {
            //string type = ConvertUtil.ToString(listPickType.EditValue);
            //txtAsnBillNO.Enabled = type == SysCodeConstant.PICK_TYPE_ACROSS;
        }
    }
}