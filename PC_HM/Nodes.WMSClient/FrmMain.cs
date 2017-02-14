using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Nodes.BaseData;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.SystemManage;
using Nodes.Utils;
using Nodes.Instore;
using Nodes.Outstore;
using Nodes.Stock;
using Nodes.CycleCount;
using Reports;
using System.IO;
using Nodes.Reports;
using Nodes.Resources;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;
using DevExpress.Utils;

namespace Nodes.WMSClient
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        //UserDal userDal = null;
        List<ModuleEntity> menus = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载菜单模块数据
        /// </summary>
        /// <param name="loginedUserCode"></param>
        /// <returns></returns>
        public List<ModuleEntity> ListSystemMenus(string loginedUserCode)
        {
            List<ModuleEntity> list = new List<ModuleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(loginedUserCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListSystemMenus);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListSystemMenus bill = JsonConvert.DeserializeObject<JsonListSystemMenus>(jsonQuery);
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

                foreach (JsonListSystemMenusResult tm in bill.result)
                {
                    ModuleEntity sku = new ModuleEntity();
                    sku.DEEP = Convert.ToInt32(tm.deep);
                    sku.FormName = tm.formName;
                    sku.MenuName = tm.menuName;
                    sku.ModuleID = tm.moduleId;
                    sku.ModuleType = Convert.ToInt32(tm.moduleType);
                    sku.ParentID = tm.parentId;
                    //tm.sortOrder;
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

        void LoadMenu()
        {
            //ModuleDal moduleDal = new ModuleDal();
            if (GlobeSettings.LoginedUser.UserCode != "000999")
                menus = ListSystemMenus(GlobeSettings.LoginedUser.UserCode);
            else
                menus = ListSystemMenus("000999");

            //这是测试，上面是正式的
            //menus = moduleDal.SystemMenuLists();
            treeMenu.DataSource = menus;
            treeMenu.ExpandAll();

            barLoginInfo.Caption = string.Format("当前用户: {0}({1})", GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.WarehouseName);
            barIPAddress.Caption = string.Format("IP: {0}", GlobeSettings.LoginedUser.IPAddress);
        }

        void OpenForm(string tag)
        {
            bool found = false;
            foreach (Form page in this.MdiChildren)
            {
                if (page.Text == tag)
                {
                    found = true;
                    page.Close();
                    break;
                }
            }

            //if (!found)
            //{
            Form frm = NewForm(tag);
            if (frm != null)
            {
                frm.MdiParent = this;
                frm.Show();
            }
            //}
        }

        #region 
        public void Test()
        {
            System.Text.StringBuilder loStr = new System.Text.StringBuilder();
            loStr.Append("WarehouseType=").Append("2").Append("&");
            loStr.Append("UserName=").Append("admin").Append("&");
            #region list 转 json
            string pickPlanEntity = "\"billids\":[{\"BillID\":\"4326649\",\"BillNO\":\"PM231050299799642126347\"}]";
            string pickPlanEntity1 = "\"details\":[{\"BillID\":\"4326649\",\"DetailID\":\"46320774\",\"STOCK_ID\":\"2229397\",\"Qty\":\"12\",\"IsCase\":\"0\",\"BillNO\":\"PM231050299799642126347\",\"MaterialName\":\"伊利畅意100%乳酸菌330ml*12\"}]";
                string jsons = "{" + pickPlanEntity + "," + pickPlanEntity1 + "}";
            loStr.Append("pickJson=").Append(jsons);
            #endregion

            string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SavePickPlan);
            //string url = "http://" + "192.168.91.199" + WebWork.URL_SavePickPlan;
            //string jsonQuery = WebWork.testwhc("25@", "ad&min", jsons, WebWork.URL_SavePickPlan);


            MsgBox.Err(WebWork.RESULT_NULL);
        }
        #endregion

        Form NewForm(string tag)
        {
            Form frm = null;
            switch (tag)
            {
                case "本机设置":
                    FrmOptions frmOptions = new FrmOptions();
                    frmOptions.ShowDialog();
                    break;
               

                #region 收货管理
                case "到货登记":
                    frm = new FrmVehicle();
                    break;
                case "送货牌列表":
                    frm = new FrmListCard();
                    break;
                case "托盘状态表":
                    frm = new FrmContainerState();
                    break; 
                case "收货单管理":
                    frm = new FrmAsnManage();
                    break;
                case "越库收货":
                    frm = new FrmCrossInstore();
                    break;
                case "越库收货确认":
                    frm = new FrmCrossInstoreConfirm();
                    break;
                case "退货单管理"://Add by ZXQ 20150525
                    frm = new FrmReturnManage();
                    break;
                #endregion

                #region 销货管理
                case "出库单管理":
                    frm = new FrmSOManage();
                    break;
                case "订单排序":
                    frm = new FrmSOSort();
                    break;
                case "订单排序(地图)":
                    frm = new Nodes.Outstore.FrmSOSortMap();
                    break;
                case "订单排序查询":
                    frm = new FrmSOSortMapQuery();
                    break;
                case "拣货计划":
                    frm = new FrmPickPlans();
                    break;
                case "拣货任务管理":
                    frm = new FrmPickTaskManager();
                    break;
                case "物流箱状态表":
                    frm = new FrmSOLPNState();
                    break;
                case "散货称重":
                    frm = new FrmSOWeight();
                    break;
                case "装车称重":
                    frm = new FrmSOWeightLoading();
                    break;
                case "打印销售发货单":
                    frm = new FrmSOLoading();
                    break;
                case "打印销售发货单(新)":
                    frm = new FrmSOLoadingNew();
                    break;
                case "回款确认":
                    frm = new FrmBackConfirm();
                    break;
                case "车次信息":
                    frm = new FrmLoadingTrain();
                    break;
                case "装车信息":
                    frm = new FrmLoadingInfo();
                    break;
                case "整货称重[简化]":
                    frm = new FrmSOWeightLoading_Simple();
                    break;
                case "越库出库":
                    frm = new FrmAcrossOutbound();
                    break;
                case "订单线路查询":
                    frm = new FrmSOSortQuery();
                    break;
                case "当前订单量":
                    frm = new FrmShowNeedSKU();
                    break;
                #endregion

                #region 库内管理
                case "触发补货任务":
                    frm = new FrmCreateReplenishBill();
                    break;
                case "库存查询":
                    frm = new FrmStockQuery();
                    break;
                case "待称重集货区查询":
                    frm = new FrmStockTemp();
                    break;
                case "库存转移":
                    frm = new FrmStockTransfer();
                    break;
                case "移库记录表":
                    frm = new FrmStockTransferQuery();
                    break;
                #endregion

                #region 盘点管理
                case "创建盘点单":
                    frm = new FrmCreateCC();
                    break;
                case "盘点差异报告":
                    frm = new FrmCountManager();
                    break;
                case "盘点单管理":
                    frm = new FrmCountManager();
                    break;
                case "盘点差异调整":
                    frm = new FrmCountExecute();
                    break;
                case "盘点任务分派":
                    frm = new FrmCountTask();
                    break;
                #endregion

                #region 系统管理
                case "用户管理":
                    frm = new FrmUserManager();
                    break;
                case "角色管理":
                    frm = new FrmRoleManager();
                    break;
                case "公司信息":
                    frm = new FrmCompanyManager();
                    break;
                case "条码规则定义":
                    frm = new FrmBarcodeDefine();
                    break;
                case "系统设置":
                    frm = new FrmSetting();
                    break;
                case "任务池管理":
                    frm = new FrmTaskManager();
                    break;
                case "任务池管理(新)":
                    frm = new FrmTaskManagerNew();
                    break;
                case "人员状态表":
                    frm = new FrmUserState();
                    break;
                case "叫号面板":
                    frm = new FrmCallingScreen();
                    break;
                #endregion

                #region 查询统计
                case "库存对账查询":
                    frm = new FrmStockAccount();
                    break;
                case "库存流水查询":
                    frm = new FrmStockLog();
                    break;
                case "装车记录查询":
                    frm = new FrmLoadRecords();
                    break;
                case "任务调度统计":
                    frm = new FrmTaskReport();
                    break;
                case "商品销量统计":
                    frm = new Reports.FrmSaleSort();
                    break;
                case "拣货记录表":
                    frm = new FrmQueryPickRecords();
                    break;
                case "销货明细":
                    frm = new FrmQuerySoDetails();
                    break;
                case "叉车司机任务统计":
                    frm = new FrmQueryTransRecord();
                    break;
                case "收货绩效考核":
                    frm = new FrmQueryAsnRecord();
                    break;
                case "装车绩效考核":
                    frm = new FrmQueryLoadingReports2();
                    break;
                case "库房人员绩效汇总":
                    frm = new FrmSummaryRecords2();
                    break;
                case "容器位查询":
                    frm = new FrmContainerQuery();
                    break;
                case "容器位维护":
                    frm = new FrmContainerLocatiomManager();
                    break;
                #endregion

                #region 基础管理
                case "送货牌维护":
                    frm = new FrmDriverCardManager();
                    break;
                case "客户信息":
                    frm = new FrmCustomerManager();
                    break;
                case "仓库信息":
                    frm = new FrmWarehouseManager();
                    break;
                case "货区信息":
                    frm = new FrmZoneManager();
                    break;
                case "货位信息":
                    frm = new FrmLocationManager();
                    break;
                case "物料分类":
                    frm = new FrmMaterialTypeManager();
                    break;
                case "包装关系":
                    frm = new FrmUnitGroupManager();
                    break;
                case "物料信息":
                    frm = new FrmMaterialManager();
                    break;
                case "区域信息":
                    frm = new FrmAreaManager();
                    break;
                case "品牌信息":
                    frm = new FrmBrandManager();
                    break;
                case "不合格原因":
                    frm = new FrmBugReasonManager();
                    break;
                case "容器信息":
                    frm = new FrmContainerManager();
                    break;
                case "送货路线":
                    frm = new FrmRouteManager();
                    break;
                case "叉车信息":
                    frm = new FrmForkManager();
                    break;
                case "车辆信息":
                    frm = new FrmVehicleManager();
                    break;
                case "推荐货位":
                    frm = new FrmReclocationManager();
                    break;
                case "本库物料":
                    frm = new FrmSkuWarehouseQuery();
                    break;
                #endregion
               
                
                case "权限管理":
                    frm = new FrmModulesManager();
                    break;
                case "登录日志":
                    frm = new FrmLoginLogs();
                    break;
                case "计量单位信息":
                    frm = new FrmUnitManager();
                    break;
                case "供应商信息":
                    frm = new FrmSupplierManager();
                    break;
                
                case "省份信息":
                    frm = new FrmProvinceManager();
                    break;
                case "组织信息":
                    frm = new FrmOrgManager();
                    break;
               
                case "温控信息":
                    frm = new FrmTemperatureManager();
                    break;
                
                case "手持机维护":
                    frm = new FrmPDAManager();
                    break; 
                case "关联物流箱":
                    frm = new FrmScanContainer();
                    break;
                case "修改密码":
                    FrmChangePwd frmChangePwd = new FrmChangePwd();
                    frmChangePwd.ShowDialog();
                    break;
                case "商品销量":
                    frm = new Nodes.Outstore.FrmSaleSort();
                    break;
                
               
                
                
                case "上架记录查询":
                    frm = new FrmReportPutAway();
                    break;
                
                case "拣货员任务统计":
                    frm = new FrmPickSort();
                    break;
                case "绩效考核指标":
                    frm = new FrmPerformanceAppraisal();
                    break;
                
               
                case "配送绩效考核":
                    frm = new FrmQueryDriverRecords();
                    break;
                
                
                //case "库存占用管理":
                //    frm = new FrmStockOccupyManager();
                //    break;
                case "退出系统":
                    this.Close();
                    break;
                case "帮助文档":
                    string helpfile = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\使用手册.chm";
                    if (File.Exists(helpfile))
                    {
                        Help.ShowHelp(this, helpfile);
                    }
                    else
                    {
                        MsgBox.Warn("无法找到相关文件，请联系技术人员！");
                    }
                    break;
                
                
                
                case "货位盘点记录":
                    frm = new FrmStockReviseRecords();
                    break;
                case "货位盘点":
                    frm = new FrmStockReviseRecords();
                    break;
            }

            return frm;
        }

        /// <summary>
        /// 拣货计划（删除拣货计划临时数据 ）
        /// </summary>
        /// <returns></returns>
        public bool DeletePickTemp()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_DeletePickTemp);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonJudgeIsNext bill = JsonConvert.DeserializeObject<JsonJudgeIsNext>(jsonQuery);
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
                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].flag;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (MsgBox.AskOK("确定要退出系统吗？") != DialogResult.OK)
                e.Cancel = true;
            DeletePickTemp();
        }

        /// <summary>
        /// 保存系统设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SaveSettings(string key, string value)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("key=").Append(key).Append("&");
                loStr.Append("value=").Append(value);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveSettings);
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

        private void OnFormLoad(object sender, EventArgs e)
        {
            this.Text = this.Text + string.Format("({0})", GlobeSettings.LocalVersion.VER);
            //userDal = new UserDal();
            LoadMenu();
            // 加载系统必要资源
            // 加载地图资源
            string mapDir = Path.Combine(PathUtil.ApplicationStartupPath, "Map");
            if (!Directory.Exists(mapDir))
                Directory.CreateDirectory(mapDir);
            string[] mapFileNames = new string[] 
            {
                "amap.htm",
                "jquery-1.4.1.min.js"
            };
            string[] mapFileAssembly = new string[]
            {
                "Htmls",
                "Javascripts"
            };
            for (int i = 0; i < mapFileNames.Length; i++)
            {
                string fileName = mapFileNames[i];
                string filePath = Path.Combine(mapDir, fileName);
                if (!File.Exists(filePath)) // 资源文件不存在
                {
                    string assemblyPath = string.Format("{0}.{1}", mapFileAssembly[i], fileName);
                    using (Stream stream = AppResource.GetStream(assemblyPath))
                    {
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, data.Length);
                        File.WriteAllBytes(filePath, data);
                    }
                }
                else   // 文件存在，对比本地版本与数据库中的版本是否一致
                {
                    const string MAP_KEY = "地图版本";
                    Dictionary<string, object> settings = GlobeSettings.SystemSettings;
                    if (!settings.ContainsKey(MAP_KEY))
                        return;
                    int mapVer = ConvertUtil.ToInt(settings[MAP_KEY]);
                    if (mapVer != ResourceVersion.HTMLS_AMAP_HTM)
                    {
                        File.Delete(filePath);
                        string assemblyPath = string.Format("{0}.{1}", mapFileAssembly[i], fileName);
                        using (Stream stream = AppResource.GetStream(assemblyPath))
                        {
                            byte[] data = new byte[stream.Length];
                            stream.Read(data, 0, data.Length);
                            File.WriteAllBytes(filePath, data);
                        }
                        SaveSettings(MAP_KEY, ResourceVersion.HTMLS_AMAP_HTM.ToString());
                    }
                }
            }
        }

        private void OnSystemMenuButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Button.Tag);
            OpenForm(tag);
        }

        private void OnTreeMenuMouseClick(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hInfo = treeMenu.CalcHitInfo(new System.Drawing.Point(e.X, e.Y));
            TreeListNode focusedNode = hInfo.Node;
            if (focusedNode != null && focusedNode.ParentNode != null)
            {
                ModuleEntity focusedMenu = menus.Find(m => m.ModuleID == ConvertUtil.ToString(focusedNode.GetValue("ModuleID")));
                if (focusedMenu == null || string.IsNullOrEmpty(focusedMenu.FormName)) return;

                OpenForm(focusedMenu.FormName);
            }
        }

        private void btnSearchMenu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMenu.Text.Trim()))
                    return;

                TreeListNode n = treeMenu.FindNodeByFieldValue("FormName", txtMenu.Text.Trim());
                if (n != null)
                {
                    treeMenu.SetFocusedNode(n);
                    OpenForm(txtMenu.Text.Trim());
                }
            }
        }

        private void txtMenu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                txtMenu.Text = null;
        }
    }
}