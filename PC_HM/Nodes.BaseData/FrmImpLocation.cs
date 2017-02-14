using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;
using DevExpress.Utils;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmImpLocation : DevExpress.XtraEditors.XtraForm
    {
        private DataTable tHeader = null; //记录WMS需要的表头字段

        public FrmImpLocation()
        {
            InitializeComponent();
        }

        private string[] tHeaderColumns = null;
        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog1.FileName);
            }
        }

        private void OpenFile(string fileName)
        {
            try
            {
                gridView1.Columns.Clear();

                bindingSource1.DataSource = null;

                //先把数据读取到表中
                DataTable data = TextFileToDataTable.ReadTextFile(fileName, "Bill", "\t");

                this.tHeaderColumns = new string[] { "货位编号", "货位描述", "所属货区" };

                //查看其它字段是否符合模板定义
                if (!IsColumnExist(data))
                    return;

                //绑定到界面
                tHeader = data;

                //先添加一列执行结果，记录导入成功、略过等信息
                tHeader.Columns.Add("导入结果", typeof(string));
                bindingSource1.DataSource = data;
                gridView1.ViewCaption = string.Format("文件：{0}", fileName);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        bool IsColumnExist(DataTable data)
        {
            foreach (string colName in tHeaderColumns)
            {
                if (!data.Columns.Contains(colName))
                {
                    MsgBox.Warn(string.Format("未找到列“{0}”，请检查模板文件。", colName));
                    return false;
                }
            }

            return true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                MsgBox.Warn("请先打开数据文件。");
                return;
            }

            if (MsgBox.AskOK("确定要导入到系统吗？") == DialogResult.OK)
            {
                try
                {
                    ImportFromExcelDal dal = new ImportFromExcelDal();
                    using (new WaitDialogForm("请稍等...", "正在解析数据并导入"))
                    {
                        dal.ImportLocation(tHeader, GlobeSettings.LoginedUser.UserName);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            }
        }

    }
}