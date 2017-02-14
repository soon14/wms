using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.WMS.Outbound
{
    public partial class FrmImportSOFromExcel : DevExpress.XtraEditors.XtraForm
    {
        private DataSet dsASN = null;
        private DataTable tOriginal = null;
        private DataTable tHeader = null;
        private DataTable tDetail = null;

        private string[] tHeaderColumns = new string[] { "DocEntry1", "Object", "SlpCode1", "CardCode1", "CardName1", "NumAtCard1", "QCNumber", 
            "shcompany", "consignee", "shtel", "shaddress", "WhsCode1", "Warehouse", "DocDate1", "BaseType1", "BaseEntry1", "BaseLine1",
            "Comments1", "DHLnumber", "USER", "CARDBZ", "Deliveryrequire"};
        private string[] tDetailColumns = new string[] { "DocEntry1", "LineNum1", "ItemCode1", "Dscription1", "Brand", "Quantity1", "PriceAfVAT", "DistNumber1", "ExpDate1", "SaleContNo", "Fonumber" };
        
        private string[] tHeaderDisplayName = new string[] { "单据编号", "单据类型", "业务员", "客户/供应商代码", "客户/供应商名称", "合同号", "QC合同号", 
            "收货单位", "收货人", "收货人电话", "收货地址", "虚拟仓库", "实体库", "过账日期", "基础凭证类型", "基础凭证单号", "基础凭证行号",
            "备注", "运单号", "制单人", "客户备注", "发货要求"};
        private string[] tDetailDisplayName = new string[] { "单据编号", "行号", "物料编号", "物料/服务描述", "品牌", "数量", "单价", "批次", "效期", "销售合同号", "外商单据编号" };


        public FrmImportSOFromExcel()
        {
            InitializeComponent();
            InitTable();
        }

        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog1.FileName);
            }
        }

        private void OpenFile(string fileName)
        {
            dsASN.Clear();

            if (tOriginal != null)
                tOriginal.Rows.Clear();

            using (new WaitDialogForm("请稍等...", "正在读取数据"))
            {
                //tOriginal = NPOIHandler.ExcelToDataTable(excelFile, 0, 1);
                //Nodes.Util.ExcelPrinter.ExcelAccess excel = new Nodes.Util.ExcelPrinter.ExcelAccess();
                //excel.Open(excelFile);
                //tOriginal = excel.ExcelToDataTableCore(1, 2);
                //excel.Close();
                try
                {
                    tOriginal = TextFileToDataTable.ReadTextFile(fileName, "SO", "\t");
                    bindingSource1.DataSource = null;

                    if (IsHeaderColumnExist(tOriginal))
                    {
                        bindingSource1.DataSource = tOriginal;
                        gridView1.ViewCaption = string.Format("文件：{0}", fileName);

                        SplitDataTable(tOriginal);
                    }
                    else
                        gridControl2.DataSource = null;
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            }
        }

        /// <summary>
        /// 创建主从表以及数据集、关系
        /// </summary>
        private void InitTable()
        {
            tHeader = new DataTable();
            tDetail = new DataTable();

            //先添加一列执行结果，记录成功、略过等信息
            tHeader.Columns.Add("导入结果", typeof(string));
            foreach (string colName in tHeaderColumns)
                tHeader.Columns.Add(colName, typeof(string));

            foreach (string colName in tDetailColumns)
                tDetail.Columns.Add(colName, typeof(string));

            tHeader.PrimaryKey = new DataColumn[] { tHeader.Columns["DocEntry1"] };

            dsASN = new DataSet();
            dsASN.Tables.AddRange(new DataTable[] { tHeader, tDetail });
            dsASN.Relations.Add(new DataRelation("明细行", tHeader.Columns["DocEntry1"], tDetail.Columns["DocEntry1"]));
        }

        bool IsHeaderColumnExist(DataTable data)
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

        bool IsDetailColumnExist(DataTable data)
        {
            foreach (string colName in tDetailColumns)
            {
                if (!data.Columns.Contains(colName))
                {
                    MsgBox.Warn(string.Format("未找到列“{0}”，请检查模板文件。", colName));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 将一个DataTable分割为
        /// </summary>
        /// <param name="data"></param>
        private void SplitDataTable(DataTable data)
        {
            //将数据导入到主从表中
            foreach (DataRow row in data.Rows)
            {
                string billID = ConvertUtil.ToString(row["DocEntry1"]);
                if (tHeader.Rows.Find(billID) == null)
                    tHeader.ImportRow(row);

                tDetail.ImportRow(row);
            }

            gridControl2.DataSource = tHeader;
            labelControl1.Text = string.Format("共有单据{0}个，明细{1}行。", tHeader.Rows.Count, tDetail.Rows.Count);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (tHeader.Rows.Count == 0 || tDetail.Rows.Count == 0)
            {
                MsgBox.Warn("请先打开文本文件");
                return;
            }

            if (MsgBox.AskOK("确定要导入到系统吗？") == DialogResult.OK)
            {
                try
                {
                    ImportFromExcelDal dal = new ImportFromExcelDal();

                    using (new WaitDialogForm("请稍等...", "正在解析数据并导入"))
                    {
                        // 单据类型暂定为正常采购单
                        dal.ImportSO(tOriginal, tHeader, GlobeSettings.LoginedUser.UserName);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            }
        }

        #region 处理拖放文件
        private void OnFormDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        private void OnFormDragDrop(object sender, DragEventArgs e)
        {
            string fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (File.Exists(fileName))
            {
                if (Path.GetExtension(fileName).ToLower() == ".txt")
                {
                    OpenFile(fileName);
                }
            }
        }
        #endregion
    }
}