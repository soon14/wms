using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Instore
{
    public partial class FrmCardHistory : DevExpress.XtraEditors.XtraForm
    {
        private string cardNO;
        private AsnQueryDal asnDal = new AsnQueryDal();

        public FrmCardHistory(string cardNO)
        {
            InitializeComponent();

            this.cardNO = cardNO;
            this.Text = string.Format("{0}-{1}", this.Text, cardNO);
        }

        
        private List<JsonCardHistoryEntity> ListCardHistory(string cardNO)
        {
            List<JsonCardHistoryEntity> list = new List<JsonCardHistoryEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("cardNo=").Append(cardNO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListCardHistory);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonCardHistory bill = JsonConvert.DeserializeObject<JsonCardHistory>(jsonQuery);
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
                foreach (JsonCardHistoryResult jbr in bill.result)
                {
                    JsonCardHistoryEntity he = new JsonCardHistoryEntity();
                    he.BILL_NO = jbr.billNo;
                    he.BILL_STATE_DESC = jbr.billStateDesc;
                    he.cardNo = jbr.cardNo;
                    he.contact = jbr.contact;
                    he.CREATE_DATE = jbr.createDate;
                    he.creator = jbr.creator;
                    he.driver = jbr.driver;
                    he.S_NAME = jbr.cName;
                    he.vehicleNo = jbr.vehicleNo;
                    list.Add(he);
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
                bindingSource1.DataSource = ListCardHistory(this.cardNO);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}