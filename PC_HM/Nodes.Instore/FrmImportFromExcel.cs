using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;

namespace Nodes.WMS.Inbound
{
    public partial class FrmImportFromExcel : DevExpress.XtraEditors.XtraForm
    {
        private DataTable tOriginal = null; //记录原始单据
        private DataTable tHeader = null; //记录WMS需要的表头字段
        private DataTable tDetail = null; //记录WMS需要的明细字段
        private DataSet dsASN = null; //包含表tHeader和表tDetail，中间有单据编号关联

        private bool IsAsnFile = true;
        private string BillNOColumnName = "DocEntry";

        #region 数据文件及对应的中文描述，格式为文本文件
        //到货通知单的
        //private string[] tHeaderDisplayName = new string[] { "单据编号", "单据类型", "供应商", "名称", "实体库", "过账日期", "备注", "报检员", "报检员名称", "合同号", "基础凭证类型", "基础凭证单号", "基础凭证行号" };
        //private string[] tDetailDisplayName = new string[] { "单据编号", "行号", "物料号", "物料描述", "报检数量", "供应商批号(SupplierNum)", "有效日期(EffectDate)", "合同编号", "外商单据编号(Fonumber)" };

        //private string[] tHeaderColumns = new string[] { "DocEntry", "Object", "CardCode", "CardName", "Warehouse", "DocDate", "Comments", "Reporter", "RprtName", "NumAtCard", "BaseType", "BaseEntry", "BaseLine" };
        //private string[] tDetailColumns = new string[] { "DocEntry", "LineNum", "ItemCode", "Dscription", "Quantity", "SupplierNum", "EffectDate", "NumAtCardLine", "Fonumber" };
        
        //以下是销货单字段格式
        //private string[] tHeaderColumns = new string[] { "DocEntry1", "Object", "SlpCode1", "CardCode1", "CardName1", "NumAtCard1", "QCNumber", 
        //    "shcompany", "consignee", "shtel", "shaddress", "WhsCode1", "Warehouse", "DocDate1", "BaseType1", "BaseEntry1", "BaseLine1",
        //    "Comments1", "DHLnumber", "USER", "CARDBZ", "Deliveryrequire"};
        //private string[] tDetailColumns = new string[] { "DocEntry1", "LineNum1", "ItemCode1", "Dscription1", "Brand", "Quantity1", "PriceAfVAT", "DistNumber1", "ExpDate1", "SaleContNo", "Fonumber" };

        //private string[] tHeaderDisplayName = new string[] { "单据编号", "单据类型", "业务员", "客户/供应商代码", "客户/供应商名称", "合同号", "QC合同号", 
        //    "收货单位", "收货人", "收货人电话", "收货地址", "虚拟仓库", "实体库", "过账日期", "基础凭证类型", "基础凭证单号", "基础凭证行号",
        //    "备注", "运单号", "制单人", "客户备注", "发货要求"};
        //private string[] tDetailDisplayName = new string[] { "单据编号", "行号", "物料编号", "物料/服务描述", "品牌", "数量", "单价", "批次", "效期", "销售合同号", "外商单据编号" };

        private string[] tHeaderColumns = null, tDetailColumns = null;
        #endregion

        public FrmImportFromExcel()
        {
            InitializeComponent();
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

            //原来为Excel文件，现在变成了文本文件，所以下面的函数不再使用
            //使用NPOI方法
            //tOriginal = NPOIHandler.ExcelToDataTable(excelFile, 0, 1);

            //使用Excel.interop
            //Nodes.Util.ExcelPrinter.ExcelAccess excel = new Nodes.Util.ExcelPrinter.ExcelAccess();
            //excel.Open(excelFile);
            //tOriginal = excel.ExcelToDataTableCore(1, 2);
            //excel.Close();
            try
            {
                gridView1.Columns.Clear();
                gridView2.Columns.Clear();

                bindingSource1.DataSource = null;
                gridControl2.DataSource = null;

                //先把数据读取到表中
                DataTable data = TextFileToDataTable.ReadTextFile(fileName, "Bill", "\t");

                //查看数据文件是到货通知单还是销货单（销货单文本文件格式包含好几种单据类型，既有出库也有入库的单据；
                //到货通知单文件只包含到货通知单）
                //区分文件类型方式是到货通知单单据编号是DocEntry，销货单是DocEntry1
                if (data.Columns.Contains("DocEntry"))
                {
                    this.BillNOColumnName = "DocEntry";
                    this.IsAsnFile = true;

                    this.tHeaderColumns = new string[] { "DocEntry", "Object", "CardCode", "CardName", "Warehouse", "DocDate", "Comments", "Reporter", "RprtName", "NumAtCard", "BaseType", "BaseEntry", "BaseLine" };
                    this.tDetailColumns = new string[] { "DocEntry", "LineNum", "ItemCode", "Dscription", "Quantity", "SupplierNum", "EffectDate", "NumAtCardLine", "Fonumber" };
                }
                else if (data.Columns.Contains("DocEntry1"))
                {
                    this.BillNOColumnName = "DocEntry1";
                    this.IsAsnFile = false;

                    this.tHeaderColumns = new string[] { "DocEntry1", "Object", "SlpCode1", "CardCode1", "CardName1", "NumAtCard1", "QCNumber",
                            "shcompany", "consignee", "shtel", "shaddress", "WhsCode1", "Warehouse", "DocDate1", "BaseType1", "BaseEntry1", "BaseLine1",
                            "Comments1", "DHLnumber", "USER", "CARDBZ", "Deliveryrequire" };
                    this.tDetailColumns = new string[] { "DocEntry1", "LineNum1", "ItemCode1", "Dscription1", "Brand", "Quantity1", "PriceAfVAT", "DistNumber1", "ExpDate1", "SaleContNo", "Fonumber" };
                }
                else
                    throw new Exception("文件格式错误，无法解析。");

                //查看其它字段是否符合模板定义
                if (!IsColumnExist(data))
                    return;

                //根据文件类型初始化表结构
                InitTable();

                //绑定到界面
                tOriginal = data;
                bindingSource1.DataSource = data;
                gridView1.ViewCaption = string.Format("文件：{0}", fileName);

                //将原始表分割为表头和明细，显示到下面的表格中
                SplitDataTable(data);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 创建主从表以及数据集、关系
        /// </summary>
        private void InitTable()
        {
            tHeader = new DataTable();
            tDetail = new DataTable();

            //先添加一列执行结果，记录导入成功、略过等信息
            tHeader.Columns.Add("导入结果", typeof(string));

            //构造表头字段
            foreach (string colName in tHeaderColumns) tHeader.Columns.Add(colName, typeof(string));

            //构造明细字段
            foreach (string colName in tDetailColumns) tDetail.Columns.Add(colName, typeof(string));

            //设置表头主键
            tHeader.PrimaryKey = new DataColumn[] { tHeader.Columns[this.BillNOColumnName] };

            //构造数据集，并设置主表与明细表的关联
            dsASN = new DataSet();
            dsASN.Tables.AddRange(new DataTable[] { tHeader, tDetail });
            dsASN.Relations.Add(new DataRelation("明细行", tHeader.Columns[this.BillNOColumnName], tDetail.Columns[this.BillNOColumnName]));
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
        /// 将一个DataTable分割为主表和明细表
        /// </summary>
        /// <param name="data"></param>
        private void SplitDataTable(DataTable data)
        {
            //将数据导入到主从表中
            foreach (DataRow row in data.Rows)
            {
                string billID = ConvertUtil.ToString(row[this.BillNOColumnName]);
                if (tHeader.Rows.Find(billID) == null)
                    tHeader.ImportRow(row);

                tDetail.ImportRow(row);
            }

            gridControl2.DataSource = tHeader;
            labelControl1.Text = string.Format("共有单据{0}个，明细{1}行。", tHeader.Rows.Count, tDetail.Rows.Count);
        }

        //导入数据库
        private void OnImportClick(object sender, EventArgs e)
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
                        // 到货通知单和销货单对应不同的函数
                        if (IsAsnFile)
                            dal.ImportASN(tOriginal, tHeader, GlobeSettings.LoginedUser.UserName);
                        else
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