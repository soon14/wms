using System;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using System.Collections.Generic;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;


namespace Nodes.BaseData
{
    public partial class FrmContainerManager : DevExpress.XtraEditors.XtraForm
    {
        //private ContainerDal containerDal = null;

        public FrmContainerManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolPrint.ImageIndex = (int)AppResource.EIcons.print;
            toolDesign.ImageIndex = (int)AppResource.EIcons.tree;
            toolWeight.ImageIndex = (int)AppResource.EIcons.download;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.back;
            //containerDal = new ContainerDal();
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// string转换decimal 得到2位小数点 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private decimal GetTwoDecimal(string num)
        {
            string ret = num;
            if (ret.Contains("."))
                ret = ret.Insert(ret.Length, "00");
            else
                ret = ret.Insert(ret.Length, ".00");
            return Math.Round(Convert.ToDecimal(ret), 2);
        }

        ///<summary>
        ///查询所有托盘
        ///</summary>
        ///<returns></returns>
        public List<ContainerEntity> GetAllContainer(string warehouseCode, string state)
        {
            List<ContainerEntity> list = new List<ContainerEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(warehouseCode).Append("&");
                loStr.Append("state=").Append(state);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAllContainer);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllContainer bill = JsonConvert.DeserializeObject<JsonGetAllContainer>(jsonQuery);
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
                foreach (JsonGetAllContainerResult jbr in bill.result)
                {
                    ContainerEntity asnEntity = new ContainerEntity();
                    #region
                    asnEntity.ContainerCode = jbr.ctCode;
                    asnEntity.ContainerName = jbr.ctName;
                    asnEntity.ContainerType = jbr.ctType;
                    asnEntity.ContainerTypeDesc = jbr.ctTypeDesc;
                    asnEntity.ContainerWeight = StringToDecimal.GetTwoDecimal(jbr.ctWeight);
                    asnEntity.IsDelete = Convert.ToInt32(jbr.isDeleted);
                    #endregion
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message+ex.StackTrace);
            }
            return list;
        }

        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = GetAllContainer(GlobeSettings.LoginedUser.WarehouseCode, "Y");

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "新增":
                    DoCreateBrands();
                    break;
                case "修改":
                    ShowEditBrands();
                    break;
                case "删除":
                    DoDeleteSelectedBrands();
                    break;
                case "打印":
                    DoPrint();
                    break;
                case "设计":
                    RibbonReportDesigner.MainForm designForm = new RibbonReportDesigner.MainForm();
                    RepContianer rep = new RepContianer();
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
                    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
                case "重量维护":
                    using (FrmContainerMaintain frmContainer = new FrmContainerMaintain())
                    {
                        frmContainer.ShowDialog();
                    }
                    this.ReLoad();
                    break;
                case "已删除容器":
                    bindingSource1.DataSource = GetAllContainer(GlobeSettings.LoginedUser.WarehouseCode, "N");
                    break;
                case "恢复":
                    this.DoRestoreContainer();
                    break;
            }
        }

        private void DoRestoreContainer()
        {
            ContainerEntity removeEntity = SelectedBrandsRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要恢复的数据！");
                return;
            }
            else if (removeEntity.IsDelete == 0)
            {
                MsgBox.Warn("当前容器不需要恢复！");
                return;
            }
            if (MsgBox.AskOK(string.Format("确定要恢复“({0}){1}”吗？", removeEntity.ContainerCode, removeEntity.ContainerName)) == DialogResult.OK)
            {
                bool ret = DeleteCt(removeEntity.ContainerCode, 0);
                if (ret)
                {
                    Insert(ELogType.容器状态, GlobeSettings.LoginedUser.UserCode, removeEntity.ContainerCode,
                        "容器信息-恢复");
                    bindingSource1.DataSource = GetAllContainer(GlobeSettings.LoginedUser.WarehouseCode, "N");
                }
                //else
                //{
                //    MsgBox.Warn("恢复失败。");
                //}
            }
        }

        private void DoPrint()
        {
            int[] selectedIndex = gridView1.GetSelectedRows();
            if (selectedIndex.Length == 0)
            {
                MsgBox.Warn("请选中要打印的行。");
                return;
            }

            if (MsgBox.AskOK("确定开始打印吗？") == DialogResult.OK)
            {
                List<ContainerEntity> containers = new List<ContainerEntity>();
                foreach (int i in selectedIndex)
                {
                    if (i >= 0)
                        containers.Add(gridView1.GetRow(i) as ContainerEntity);
                }
                RepContianer repContianer = new RepContianer(containers, 1);
                repContianer.Print();
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditBrands();
        }
        /// <summary>
        /// 获得选中数据
        /// </summary>
        ContainerEntity SelectedBrandsRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as ContainerEntity;
            }
        }


        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void DoCreateBrands()
        {
            FrmContainerEdit frmcontainerEdit = new FrmContainerEdit();
            frmcontainerEdit.DataSourceChanged += OnCreateChanage;
            frmcontainerEdit.ShowDialog();
            LoadDataAndBindGrid();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            ContainerEntity newEntity = (ContainerEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditBrands()
        {
            ContainerEntity editEntity = SelectedBrandsRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmContainerEdit frmcontainerEdit = new FrmContainerEdit(editEntity);
            frmcontainerEdit.DataSourceChanged += OnEditChanage;
            frmcontainerEdit.ShowDialog();
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 删除托盘
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public bool DeleteCt(string ContainerCode, int deleteFlag)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(ContainerCode).Append("&");
                loStr.Append("isDeleted=").Append(deleteFlag);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteCt);
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

        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteSelectedBrands()
        {
            ContainerEntity removeEntity = SelectedBrandsRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据！");
                return;
            }
            else if (removeEntity.IsDelete != 0)
            {
                MsgBox.Warn("已经删除的容器不允许再次删除！");
                return;
            }
            if (MsgBox.AskOK(string.Format("确定要删除“({0}){1}”吗？", removeEntity.ContainerCode, removeEntity.ContainerName)) == DialogResult.OK)
            {
                bool ret = DeleteCt(removeEntity.ContainerCode, 1);
                if (ret)
                {
                    Insert(ELogType.容器状态, GlobeSettings.LoginedUser.UserCode, removeEntity.ContainerCode,
                        "容器信息-删除");
                    bindingSource1.Remove(removeEntity);
                }
                //else
                //    MsgBox.Warn("删除失败。");
            }
        }
    }
}
