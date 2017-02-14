using System;
using Nodes.Entities;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Newtonsoft.Json;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing;

namespace Nodes.Outstore
{
    /// <summary>
    /// 2015-07-17 惠民确认的版本  （销售发货单）
    /// </summary>
    public partial class RepSOLoading : DevExpress.XtraReports.UI.XtraReport
    {
        #region 变量

        private OrderSortPrintEntity _dataSource = null;
        public readonly string RepFileName = "RepSOLoading.repx";
        public short copies = 1;
        private string _module = string.Empty;
        #endregion

        #region 构造函数

        public RepSOLoading()
        {
            InitializeComponent();
            //string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            //if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSOLoading(OrderSortPrintEntity dataSource, string module)
            : this()
        {
            var codeParams = CodeDescriptor.Init(ErrorCorrectionLevel.H, dataSource.RandomCode.Trim(), QuietZoneModules.Two, 5);

            codeParams.TryEncode();

            // Render the QR code as an image  
            using (var ms = new MemoryStream())
            {
                codeParams.Render(ms);

                Image image = Image.FromStream(ms);
                xrPictureBox.Image = image;
                //if (image != null)
                //    xrPictureBox.SizeMode = image.Height > xrPictureBox.Height ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
            } 
            this._dataSource = dataSource;
            this._module = module;
        }
        
        #endregion

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Collate = true;
            e.PrintDocument.PrinterSettings.Copies = this.copies;
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = _dataSource;
            this.DataMember = "Details";
        }

        #region 插入日志记录
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="creator">当前操作人</param>
        /// <param name="billNo">订单编号</param>
        /// <param name="description">操作描述</param>
        /// <param name="module">模块</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, DateTime createTime, string remark)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("billNo=").Append(billNo).Append("&");
                loStr.Append("description=").Append(description).Append("&");
                loStr.Append("module=").Append(module).Append("&");
                loStr.Append("remark=").Append(remark);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Insert);
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
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        private void RepSO_AfterPrint(object sender, EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            //new BillLogDal().SavePrintLog(this._dataSource.VehicleNO, pageCount, "打印装车单", GlobeSettings.LoginedUser.UserName);
            string billIDs = string.Empty;
            foreach (var item in this._dataSource.Details)
            {
                billIDs += (item.BillID + ",");
            }
            Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, this._dataSource.VehicleNO, "发车单", this._module + "-RepSOLoading", billIDs);
        }
    }


    /// <summary>  
    /// Class containing the description of the QR code and wrapping encoding and rendering.  
    /// </summary>  
    internal class CodeDescriptor
    {
        public ErrorCorrectionLevel Ecl;
        public string Content;
        public QuietZoneModules QuietZones;
        public int ModuleSize;
        public BitMatrix Matrix;
        public string ContentType;

        /// <summary>  
        /// Parse QueryString that define the QR code properties  
        /// </summary>  
        /// <param name="request">HttpRequest containing HTTP GET data</param>  
        /// <returns>A QR code descriptor object</returns>  
        public static CodeDescriptor Init(ErrorCorrectionLevel level, string content, QuietZoneModules qzModules, int moduleSize)
        {
            var cp = new CodeDescriptor();

            //// Error correction level  
            cp.Ecl = level;
            //// Code content to encode  
            cp.Content = content;
            //// Size of the quiet zone  
            cp.QuietZones = qzModules;
            //// Module size  
            cp.ModuleSize = moduleSize;
            return cp;
        }

        /// <summary>  
        /// Encode the content with desired parameters and save the generated Matrix  
        /// </summary>  
        /// <returns>True if the encoding succeeded, false if the content is empty or too large to fit in a QR code</returns>  
        public bool TryEncode()
        {
            var encoder = new QrEncoder(Ecl);
            QrCode qr;
            if (!encoder.TryEncode(Content, out qr))
                return false;

            Matrix = qr.Matrix;
            return true;
        }

        /// <summary>  
        /// Render the Matrix as a PNG image  
        /// </summary>  
        /// <param name="ms">MemoryStream to store the image bytes into</param>  
        internal void Render(MemoryStream ms)
        {
            var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
            render.WriteToStream(Matrix, System.Drawing.Imaging.ImageFormat.Png, ms);
            ContentType = "image/png";
        }
    }  
}
