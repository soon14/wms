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
    public partial class FrmSetting : DevExpress.XtraEditors.XtraForm
    {
        public FrmSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        public DataTable GetSysSetting()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("SET_ITEM", Type.GetType("System.String"));
            tblDatas.Columns.Add("SET_VALUE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SET_GROUP", Type.GetType("System.String"));
            tblDatas.Columns.Add("REMARK", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetSysSetting);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetSysSetting bill = JsonConvert.DeserializeObject<JsonGetSysSetting>(jsonQuery);
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
                foreach (JsonGetSysSettingResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ID"] = tm.id;
                    newRow["SET_ITEM"] = tm.setItem;
                    newRow["SET_VALUE"] = tm.setValue;
                    newRow["SET_GROUP"] = tm.setGroup;
                    newRow["REMARK"] = tm.remark;
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
                gridControl1.DataSource = GetSysSetting();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #region List转换成Json
        private string GetRes<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }
            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + curname + "\"" + strb.ToString();
            ret = ret.Insert(0, "{");
            ret = ret.Insert(ret.Length, "}");
            return ret;
        }


        private string GetResList<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            //strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            //string ret = "\""+ curname + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return string.Join(",", result.ToArray());
        }

        private string GetResList<T>(string josnName, List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + josnName + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 系统设置--保存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SaveSysSetting(string id, string SET_VALUE, string SET_ITEM)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("id=").Append(id).Append("&");
                loStr.Append("setValue=").Append(SET_VALUE).Append("&");
                loStr.Append("setItem=").Append(SET_ITEM);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveSysSetting);
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

        public bool SaveSysSettingFor(DataTable data)
        {
            foreach (DataRow field in data.Rows)
            {
                if (!SaveSysSetting(ConvertUtil.ToString(field["ID"]), ConvertUtil.ToString(field["SET_VALUE"]),
                    ConvertUtil.ToString(field["SET_ITEM"])))
                {
                    return false;
                }
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
                SaveSysSettingFor(fields);

                //更新内存
                splashLabel1.Show(true);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}