using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.UI;
using DevExpress.XtraBars;
using Newtonsoft.Json;
using Nodes.Common;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;

namespace Nodes.Stock
{
    public partial class FrmStockQuery : DevExpress.XtraEditors.XtraForm
    {
        //StockDal stockDal = new StockDal();

        public FrmStockQuery()
        {
            InitializeComponent();
        }

        private void OnQueryClick(object sender, EventArgs e)
        {
            Query();
        }

        public List<StockRecordEntity> QueryStock(string location, string materialName, bool withZeroQty, int nums, int begin, out int total)
        {
            List<StockRecordEntity> list = new List<StockRecordEntity>();
            total = 0;
            try
            {
                // watch.Start();

                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("location=").Append(location).Append("&");
                loStr.Append("materialName=").Append(materialName).Append("&");
                if (begin == 0)
                    loStr.Append("startPage=").Append("&");
                else
                    loStr.Append("startPage=").Append(begin).Append("&");

                if (nums == 0)
                    loStr.Append("pageSize=").Append("&");
                else
                    loStr.Append("pageSize=").Append(nums).Append("&");
                loStr.Append("withZeroQty=").Append(withZeroQty);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryStock, 20000);

                //watch.Stop();

                //labSH.Text = string.Format("查询完成：耗时{0}秒", watch.ElapsedMilliseconds / 1000f);
                //watch.Reset();

                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryStock bill = JsonConvert.DeserializeObject<JsonQueryStock>(jsonQuery);
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
                foreach (JsonQueryStockResult jbr in bill.result)
                {
                    StockRecordEntity asnEntity = new StockRecordEntity();
                    #region 0- 10
                    asnEntity.ExpDays = Convert.ToInt32(jbr.expDays);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.LocationIsActive = jbr.active;
                    asnEntity.Material = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.OccupyQty = Convert.ToDecimal(jbr.occupyQty);
                    asnEntity.PickingQty = Convert.ToDecimal(jbr.pickingQty);
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.SkuQuality = jbr.skuQuality;
                    asnEntity.Spec = jbr.spec;
                    #endregion
                    asnEntity.StockID = Convert.ToInt32(jbr.id);
                    asnEntity.UnitName = jbr.umName;
                    asnEntity.ZoneName = jbr.znName;

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.expDate))
                            asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.latestIn))
                            asnEntity.LastInDate = Convert.ToDateTime(jbr.latestIn);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion


                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.latestOut))
                            asnEntity.LastOutDate = Convert.ToDateTime(jbr.latestOut);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    list.Add(asnEntity);
                }

                total = bill.total;

                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }


        private void Query()
        {
            int total;
            string location = null, skuName = null;
            if (txtLocation.Text.Trim().Length > 0)
                location = txtLocation.Text.Trim();

            if (txtMC.Text.Trim().Length > 0)
                skuName = txtMC.Text.Trim();

            try
            {
                //StockDal stockDal = new StockDal();
                List<StockRecordEntity> data = data = QueryStock(location, skuName, checkWithZero.Checked, 0, 0, out total);

                bindingSource1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private StockRecordEntity SelectedStockRecord
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as StockRecordEntity;
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockRecordEntity s = SelectedStockRecord;
            string tag = ConvertUtil.ToString(e.Item.Tag);
            if (tag == "查看商品库存信息")
            {
                FrmStockSKU frmStockSKU = new FrmStockSKU();
                frmStockSKU.Show();
                return;
            }
            else if (tag == "台账记录")
            {
                FrmSkuLog frmLog = null;
                if (s == null)
                {
                    frmLog = new FrmSkuLog();
                }
                else
                {
                    frmLog = new FrmSkuLog(s.StockID);
                }
                frmLog.ShowDialog();
                return;
            }
            
            if (s == null)
            {
                MsgBox.Warn("请选中要查看的行。");
                return;
            }
            switch (tag)
            {
                case "台账记录":
                    
                    break;
                case "占用记录":
                    FrmPickingScan frmPickingScan = new FrmPickingScan(s.StockID);
                    frmPickingScan.ShowDialog();
                    break;
                case "删除0库存行":
                    DeleteZeroRow();
                    break;
            }
        }

        /// <summary>
        /// 删除0库存行
        /// </summary>
        /// <param name="stockID"></param>
        /// <returns></returns>
        public bool DeleteStock(int stockID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("stockId=").Append(stockID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteStock);
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

        private void DeleteZeroRow()
        {
            if (gridView1.GetSelectedRows().Length > 1)
            {
                MsgBox.Warn("只允许逐行删除。");
                return;
            }

            //为了减少出错，只允许一行一行删除
            StockRecordEntity s = SelectedStockRecord;
            if (s == null)
            {
                MsgBox.Warn("请选中要删除的行。");
                return;
            }

            //必须是库存为0，占用为0
            if (s.Qty != 0 || s.PickingQty != 0 || s.OccupyQty != 0)
            {
                MsgBox.Warn("总库存、拣货中与占用中必须全部为0，才可以删除。");
                return;
            }

            try
            {
                bool result = DeleteStock(s.StockID);
                //if (result == -1)
                //    MsgBox.Warn("该行库存未查到，可能已经被其他人删除。");
                //else if (result == -2)
                //    MsgBox.Warn("总库存、拣货中与占用中必须全部为0，才可以删除。");
                //else
                if (result)
                {
                    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                    MsgBox.OK("删除成功。");
                }
                //int result = DeleteStock(s.StockID);
                //if (result == -1)
                //    MsgBox.Warn("该行库存未查到，可能已经被其他人删除。");
                //else if (result == -2)
                //    MsgBox.Warn("总库存、拣货中与占用中必须全部为0，才可以删除。");
                //else
                //{
                //    gridView1.DeleteRow(gridView1.FocusedRowHandle);
                //    MsgBox.OK("删除成功。");
                //}
                Insert(ELogType.库存, GlobeSettings.LoginedUser.UserName, ConvertUtil.ToString(s.StockID), ConvertUtil.ToString(result), "库存查询", JsonConvert.SerializeObject(s));
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void FrmStockQuery_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 标记--添加商品质量
        /// </summary>
        /// <param name="skuQuatity"></param>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public bool UpdateSkuQuality(int skuQuatity, int stockId)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("stockId=").Append(stockId).Append("&");
                loStr.Append("skuQuatity=").Append(skuQuatity);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateSkuQuality);
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

        private void barBtnSkuFlag_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmChooseBaseCode frm = new FrmChooseBaseCode("130", "商品标记");

            if(gridView1.SelectedRowsCount < 1 )
            {
                MsgBox.Warn("请要修改的行！");
                return;
            }
            if (frm.ShowDialog() == DialogResult.OK) 
            {
                int[] index_array = gridView1.GetSelectedRows();
                BaseCodeEntity _selectedBaseCode = frm.cboVehicle.EditValue as BaseCodeEntity;
                foreach (int index in index_array)
                {
                    StockRecordEntity stock = gridView1.GetRow(index) as StockRecordEntity;
                    int code = ConvertUtil.ToInt(_selectedBaseCode.ItemValue.ToString());
                    UpdateSkuQuality(code,stock.StockID);
                }

                Query();
            }
        }           
    }
}