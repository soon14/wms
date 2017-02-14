using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.Utils;
using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;


namespace Nodes.Outstore
{
    public partial class FrmBillContainerInfo : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数

        public FrmBillContainerInfo()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// 订单落放明细,查找商品明细
        /// </summary>
        /// <returns></returns>
        public List<Nodes.DBHelper.Print.SOFindGoodsDetail> GetFindGoodsDetail()
        {
            List<Nodes.DBHelper.Print.SOFindGoodsDetail> list = new List<Nodes.DBHelper.Print.SOFindGoodsDetail>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();

                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetFindGoodsDetail, 300000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetFindGoodsDetail bill = JsonConvert.DeserializeObject<JsonGetFindGoodsDetail>(jsonQuery);
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
                #region 赋值

                foreach (JsonGetFindGoodsDetailResult tm in bill.result)
                {
                    Nodes.DBHelper.Print.SOFindGoodsDetail sku = new Nodes.DBHelper.Print.SOFindGoodsDetail();
                    #region 0-7
                    sku.CustomerAddress = tm.address;
                    sku.BillID = Convert.ToInt32(tm.billId);
                    sku.BillNo = tm.billNo;
                    sku.CustomerName = tm.cName;
                    sku.DelayMark = Convert.ToInt32(tm.delayMark);
                    sku.SanNum = Convert.ToInt32(tm.sanNum);
                    sku.ZhengNum = Convert.ToInt32(tm.zhengNum);
                    #endregion

                    list.Add(sku);
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

        #region Override Methods
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try
            {
                using (WaitDialogForm waiting = new WaitDialogForm())
                {
                    List<Nodes.DBHelper.Print.SOFindGoodsDetail> list = GetFindGoodsDetail();
                    SOFindGoodsDetailList dataSource = new SOFindGoodsDetailList();
                    if (list != null && list.Count > 0)
                    {
                        foreach (Nodes.DBHelper.Print.SOFindGoodsDetail item in list)
                        {
                            dataSource.Details.Add(Nodes.Outstore.SOFindGoodsDetail.Convert(item));
                        }
                    }
                    this.gridControl1.DataSource = dataSource.Details;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion
    }
}
