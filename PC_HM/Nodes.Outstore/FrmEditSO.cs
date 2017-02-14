using System;
using System.Drawing;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;


namespace Nodes.Outstore
{
    public partial class FrmEditSO : DevExpress.XtraEditors.XtraForm
    {
        public int? SelectedColor { get; set; }
        public string Remark { get; set; }

        SOHeaderEntity Header = null;
        public FrmEditSO(SOHeaderEntity header)
        {
            InitializeComponent();
            Header = header;

            this.Text = string.Format("填写备注(单号：{0})", Header.BillNO);
        }

        /// <summary>
        /// 出库单管理：修改备注
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="remark"></param>
        /// <param name="colorArgb"></param>
        /// <returns></returns>
        public bool UpdateWmsRemark(int billID, string remark, int? colorArgb)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("colorArgb=").Append(colorArgb).Append("&");
                loStr.Append("remark=").Append(remark);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateWmsRemark);
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

        private void OnOKClick(object sender, EventArgs e)
        {
            if (colorBack.Color != Color.Empty)
                SelectedColor = colorBack.Color.ToArgb();

            Remark = txtRemark.Text.Trim();
            UpdateWmsRemark(Header.BillID, Remark, SelectedColor);
            this.DialogResult = DialogResult.OK;
        }

        private void colorBack_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                colorBack.Color = Color.Empty;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            txtRemark.Text = Header.WmsRemark;
            if (Header.RowForeColor != null)
                colorBack.Color = Color.FromArgb(Header.RowForeColor.Value);
        }
    }
}