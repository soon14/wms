using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmBarcodeDefine : DevExpress.XtraEditors.XtraForm
    {
        public FrmBarcodeDefine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取条码规范定义表
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public  DataTable GetBarcodeRule(string warehouse)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("BARCODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("RULE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouse=").Append(warehouse);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBarcodeRule);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetBarcodeRule bill = JsonConvert.DeserializeObject<JsonGetBarcodeRule>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tblDatas;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tblDatas;
                }
                #endregion

                #region 赋值
                foreach (JsonGetBarcodeRuleResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ID"] = tm.id;
                    newRow["BARCODE"] = tm.barCode;
                    newRow["RULE"] = tm.rule;
                    tblDatas.Rows.Add(newRow);
                }
                return tblDatas;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tblDatas;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                gridControl1.DataSource = GetBarcodeRule(GlobeSettings.LoginedUser.WarehouseCode);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 条码规则定义--保存
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="id"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public bool SaveBarcodeRule(string rule,string id,string barcode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("id=").Append(id).Append("&");
                loStr.Append("rule=").Append(rule).Append("&");
                loStr.Append("barcode=").Append(barcode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveBarcodeRule);
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

        public bool SaveBarcodeRuleFor(DataTable data)
        {
            foreach (DataRow field in data.Rows)
            {
                if (!SaveBarcodeRule(ConvertUtil.ToString(field["RULE"]), ConvertUtil.ToString(field["ID"]),
                    ConvertUtil.ToString(field["BARCODE"])))
                    return false;
            }
            return true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            DataTable fields = gridControl1.DataSource as DataTable;

            //不允许显示为空字符，没有意义
            if (MsgBox.AskOK("确定要保存吗？") != DialogResult.OK)
                return;

            try
            {
                //保存到数据库
                SaveBarcodeRuleFor(fields);

                //更新内存
                splashLabel1.Show(true);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}