using System;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities;

namespace Nodes.WMS.Inbound
{
    public partial class FrmPrintLabel : DevExpress.XtraEditors.XtraForm
    {
        SeedsDal seedsDal = null;
        private string sequenceBarcodePattern = null;

        public FrmPrintLabel()
        {
            InitializeComponent();
        }

        public FrmPrintLabel(int qty) : this()
        {
            spSequenceQty.Value = qty;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            seedsDal = new SeedsDal();

            int seedType = 1;
            long currSequenceNO = seedsDal.GetCurrentSeed(seedType, GlobeSettings.LoginedUser.WarehouseCode);

            //从服务器获取流水号的编码方式
            SettingEntity setting = SettingDal.Get("SeqBarcodePattern", GlobeSettings.LoginedUser.WarehouseCode);
            sequenceBarcodePattern = setting.Value2 + "{0:D" + setting.Value1 + "}";
            txtSequenceBarcode.Text = string.Format(sequenceBarcodePattern, currSequenceNO + 1);
        }

        #region 打印流水号标签
        private void btnPrintMBarcode_Click(object sender, EventArgs e)
        {
            int qty = (int)spSequenceQty.Value;
            if (MsgBox.AskOK(string.Format("确定要开始打印吗？打印张数：{0}。", qty)) != DialogResult.OK)
                return;

            btnPrintMaterialLabel.Enabled = false;

            try
            {
                // 1、先从数据库获取最新的流水号，确保并发时获取的种子不重复，
                // 2、然后更新种子，
                // 3、最后保存打印日志
                int seedType = 1;
                long currSequenceNO = seedsDal.GetCurrentSeed(seedType, GlobeSettings.LoginedUser.WarehouseCode);
                seedsDal.IncreaseSeed(seedType, qty, GlobeSettings.LoginedUser.WarehouseCode);

                //流水号加1，并保存打印日志
                long startFrom = currSequenceNO + 1;
                new BillLogDal().SavePrintLog(startFrom.ToString(), qty, "打印流水号标签", GlobeSettings.LoginedUser.UserName);

                //使用打印报表
                for (int i = 1; i <= qty; i++)
                {
                    //使用保存的流水号编制方式，生成流水号条码字符，并发送到打印机驱动
                    RepSequenceLabel repMaterialCard = new RepSequenceLabel();
                    repMaterialCard.PrintedData = string.Format(sequenceBarcodePattern, startFrom);
                    repMaterialCard.Print();

                    //序号加1
                    startFrom += 1;

                    //暂停片刻，防止打印机内存不足
                    System.Threading.Thread.Sleep(20);
                }

                //更新界面显示，多台同时打印时，考虑并发，
                //有可能流水号又被更新了，所以重新从数据库读取一下
                txtSequenceBarcode.Text = string.Format(sequenceBarcodePattern, seedsDal.GetCurrentSeed(seedType, GlobeSettings.LoginedUser.WarehouseCode) + 1);
                btnPrintMaterialLabel.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 补打流水号标签

        /// <summary>
        /// 与正常打印的区别：
        /// 补打不读取种子表，也不增加种子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReprintSequenceLabel(object sender, EventArgs e)
        {
            int qty = (int)spReprintSequenceQty.Value;
            if (MsgBox.AskOK(string.Format("确定要开始打印吗？打印张数：{0}。", qty)) != DialogResult.OK)
                return;

            btnReprintSequence.Enabled = false;

            try
            {
                //获取起始流水号，并保存打印日志
                long startFrom = (int)spReprintBarcodeFrom.Value;
                new BillLogDal().SavePrintLog(startFrom.ToString(), qty, "打印流水号标签", GlobeSettings.LoginedUser.UserName);

                //使用打印报表
                RepSequenceLabel repMaterialCard = new RepSequenceLabel();
                for (int i = 1; i <= qty; i++)
                {
                    //使用保存的流水号编制方式，生成流水号条码字符，并发送到打印机驱动
                    repMaterialCard.PrintedData = string.Format(sequenceBarcodePattern, startFrom);
                    repMaterialCard.Print();

                    //序号加1
                    startFrom += 1;

                    //暂停片刻，防止打印机内存不足
                    System.Threading.Thread.Sleep(20);
                }

                btnReprintSequence.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        private void btnDesignMaterialLabel_Click(object sender, EventArgs e)
        {
            RibbonReportDesigner.MainForm designForm = new RibbonReportDesigner.MainForm();
            RepSequenceLabel rep = new RepSequenceLabel();
            try
            {
                designForm.OpenReport(rep, rep.RepFileName);
                designForm.ShowDialog();
                designForm.Dispose();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim().Length == 0)
            {
                MsgBox.Warn("请输入要打印的内容。");
                return;
            }

            RepSequenceLabel repMaterialCard = new RepSequenceLabel();
            repMaterialCard.PrintedData = textEdit1.Text.Trim();
            repMaterialCard.Print();
        }        
    }
}