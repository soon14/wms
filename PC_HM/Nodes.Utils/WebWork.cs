using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml; 
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;

namespace Nodes.Utils
{
    public class WebWork
    {
        #region PC 混合
        #region 郭春PC接口
        /// <summary>
        /// 读取等待到货（也就是没有做到货登记）的单据
        /// </summary>
        public const string URL_QueryNotRelatedBills = "/wms/arrivalRegister/orderInfo";
        public const string URL_QueryNotRelatedBills2 = "/wms/arrivalRegister/searchArrivalOrderInfo";
        /// <summary>
        /// 已经登记，但是收货未完成的数据
        /// </summary>
        public const string URL_GetVehicles = "/wms/arrivalRegister/registered";
        /// <summary>
        /// 到货登记，根据用户角色查询用户信息（查询 清点、复核、上架人员）
        /// </summary>
        public const string URL_GetUserByRole = "/wms/arrivalRegister/userInfo";

        /// <summary>
        /// 到货登记，生成清点、复核、上架任务
        /// </summary>
        public const string URL_CreateAsnPlan = "/wms/arrivalRegister/generateAsnPlans";

        /// <summary>
        /// 到货登记,绑定送货牌与入库单
        /// </summary>
        public const string URL_CreateVechile = "/wms/arrivalRegister/saveVehicle";
        /// <summary>
        /// 送货牌列表，取消登记
        /// </summary>
        public const string URL_CancelVechile = "/wms/arrivalRegister/cancelVehicle";
        /// <summary>
        /// 送货牌列表，查看送货牌使用记录
        /// </summary>
        public const string URL_ListCardHistory = "/wms/arrivalRegister/cardHistory";
        /// <summary>
        /// 托盘状态列表，初始化加载托盘数据列表
        /// </summary>
        public const string URL_ListContainerState = "/wms/arrivalRegister/containerStates";
        /// <summary>
        /// 托盘状态列表，清空托盘
        /// </summary>
        public const string URL_CleanLPN = "/wms/arrivalRegister/releaseContainer";
        /// <summary>
        /// 托盘状态列表，托盘使用记录
        /// </summary>
        public const string URL_GetContainerRecords = "/wms/arrivalRegister/containerRecord";
        /// <summary>
        /// 收货单管理，修改入库方式
        /// </summary>
        public const string URL_UpdateInstoreType = "/wms/billManage/modifyInstoreType";
        /// <summary>
        /// 收货单据管理， 查询入库单明细
        /// </summary>
        public const string URL_GetDetailByBillID = "/wms/billManage/billDetail";
        /// <summary>
        /// 收货单据管理， baseCode信息查询
        /// </summary>
        public const string URL_GetItemList = "/wms/billManage/baseCodeInfo";
        /// <summary>
        /// 收货单据管理，查询托盘记录
        /// </summary>
        public const string URL_GetContainerStateByBillID = "/wms/billManage/containerState";
        /// <summary>
        /// 收货单据管理，查询出库单据日志（收货管理未使用）
        /// </summary>
        public const string URL_SOGetBillLog = "/wms/billManage/soBillLog";
        /// <summary>
        /// 收货单据管理，查询入库单据日志
        /// </summary>
        public const string URL_ASNGetBillLog = "/wms/billManage/asnBillLog";
        /// <summary>
        /// 收货单据管理，查询订单中未复核的托盘
        /// </summary>
        public const string URL_GetContainerNochek = "/wms/billManage/containerNoCheck";
        /// <summary>
        /// 收货单据管理，打印--查询订单主表信息
        /// </summary>
        public const string URL_GetBillHeader = "/wms/billManage/billHeader";
        /// <summary>
        /// 收货单据管理，打印---查询仓库信息
        /// </summary>
        public const string URL_GetWarehouseByCode = "/wms/billManage/wareHouseInfo";
        /// <summary>
        /// 收货单据管理，更新打印次数
        /// </summary>
        public const string URL_UpdatePrinted = "/wms/billManage/updatePrinted";
        /// <summary>
        /// 收货单据管理， 查询供应商信息列表
        /// </summary>
        public const string URL_ListActiveSupplierByPriority = "/wms/billManage/supilers";
        /// <summary>
        /// 收货单据管理，收货完成
        /// </summary>
        public const string URL_ReceivedComplete = "/wms/billManage/confirmReceive";
        /// <summary>
        /// 收货单据管理，查询收货完成后的单据信息
        /// </summary>
        public const string URL_GetBillState = "/wms/billManage/billInfo";
        /// <summary>
        /// 收货单据管理， 多条件查询
        /// </summary>
        public const string URL_QueryBills = "/wms/billManage/queryBills";
        /// <summary>
        /// 退货单管理，所有未完成单据
        /// </summary>
        public const string URL_QueryBillsReturn = "/wms/billManage/queryReturnBills";
        /// <summary>
        /// 根据当前扫描的容器(物流箱或托盘)查出与该容器关联的订单信息
        /// </summary>
        public const string URL_GetBillInfoByContainer = "/wms//getBillInfoByContainer";
        /// <summary>
        /// 获取当前容器的信息
        /// </summary>
        public const string URL_GetCurrentContainerInfo = "/wms/getCurrentContainerInfo";
        /// <summary>
        /// 获取指定订单的称重记录数量，不包含传入的容器编号
        /// </summary>
        public const string URL_GetWeightRecordsCountByBillID = "/wms/getWeightRecordsCountByBillID";
        /// <summary>
        /// 获得系统设置某项的值(如物流箱标准偏差)
        /// </summary>
        public const string URL_GetSystemDiffSet = "/wms/getSystemDiffSet";
        /// <summary>
        /// 向数据库写日志
        /// </summary>
        public const string URL_InsertSoLog = "/wms/insertSoLog";
        /// <summary>
        /// 更新目标订单信息
        /// </summary>
        public const string URL_UpdateCurrentBillState = "/wms/updateCurrentBillState";
        /// <summary>
        /// 越库收货， 查询等待到货单据
        /// </summary>
        public const string URL_QueryOverStockBills = "/wms/crossStock/searchOrders";
        /// <summary>
        /// 更新订单明细的收货量
        /// </summary>
        public const string URL_SaveOverStock = "/wms/crossStock/updateDetailQty";
        /// <summary>
        /// 更新订单状态
        /// </summary>
        public const string URL_SaveOverStockOK = "/wms/crossStock/saveStock";
        /// <summary>
        /// 更新订单状态
        /// </summary>
        public const string URL_BillState_Change = "/wms/crossStock/updateBillState";
        /// <summary>
        /// 越库收货， 获取退库临时收货区
        /// </summary>
        public const string URL_GetTempZone = "/wms/crossStock/getTempZone";
        /// <summary>
        /// 判断单据有指定类型的任务的个数
        /// </summary>
        public const string URL_GetCountOfTaskByCase = "/wms/getCountOfTaskByCase";
        /// <summary>
        /// 判断单据是否有指定的货品
        /// </summary>
        public const string URL_IsHasCase = "/wms/isHasCase";
        /// <summary>
        /// 更新容器状态表信息,写入称重是所用的地牛
        /// </summary>
        public const string URL_UpdateContainerStateSetDiNiu = "/wms/updateContainerStateSetDiNiu";
        /// <summary>
        /// 更新容器状态表信息
        /// </summary>
        public const string URL_UpdateContainerStateInfo = "/wms/updateContainerStateInfo";
        /// <summary>
        /// 显示当前装车编号的车辆（与装车编号 DEBUG时会显示）
        /// </summary>
        public const string URL_GetBillVhNoAndVhTrainNo = "/wms/getBillVhNoAndVhTrainNo";
        /// <summary>
        /// 根据车辆编号获得车辆信息
        /// </summary>
        public const string URL_GetVehicleIDByNO = "/wms/getVehicleIDByNO";
        /// <summary>
        /// 退货单管理, 按照状态（是小于某个状态）的单据查询退货单
        /// </summary>
        public const string URL_QueryBillsQuickly = "/wms/billManage/queryReturnBillsQuickly";
        /// <summary>
        /// 退货单管理,删除一张退货单据及其明细
        /// </summary>
        public const string URL_DeleteReturnBill = "/wms/billManage/deleteReturnBill";
        /// <summary>
        /// 退货单管理, 根据退货单id获取退货单信息
        /// </summary>
        public const string URL_GetHeaderInfoByBillID = "/wms/billManage/getHeaderInfoByBillID";
        /// <summary>
        /// 退货单管理, 获取退货明细
        /// </summary>
        public const string URL_GetReturnDetails = "/wms/billManage/getReturnDetails";
        /// <summary>
        /// 退货单管理, 获取公司信息
        /// </summary>
        public const string URL_GetCompanys = "/wms/billManage/getCompanys";
        /// <summary>
        /// 退货单管理,更新打印标记为已打印
        /// </summary>
        public const string URL_UpdatePrintedFlag = "/wms/billManage/updatePrintedFlag";
        /// <summary>
        /// 退货单管理,修改退货金额
        /// </summary>
        public const string URL_ModifyReturnAmount = "/wms/billManage/modifyReturnAmount";
        /// <summary>
        /// 退货单管理,关闭退货订单
        /// </summary>
        public const string URL_CloseReturn = "/wms/billManage/closeReturn";
        /// <summary>
        /// 记录操作日志
        /// </summary>
        public const string URL_Insert = "/wms/billManage/insetWmLog";
        /// <summary>
        /// 退货单管理, 关联托盘记录的单据
        /// </summary>
        public const string URL_GetRelatingStackInfo = "/wms/billManage/getRelatingStackInfo";
        /// <summary>
        /// 写入称重记录
        /// </summary>
        public const string URL_InsertWeightRecord = "/wms/insertWeightRecord";
        /// <summary>
        /// 新增公司
        /// </summary>
        public const string URL_CreateOrUpdateCompany = "/wms/systemController/insertCompany";
        #endregion

        #region 销货管理 /wms/
        /// <summary>
        /// 设置参数为0时,支持多状态情况（用逗号隔开），例如status可以是100901，也可以是100901,100902或'100901','100902'
        ///         设置参数为1时,查询状态小于61，并且订单信息存储在TMS_DATA_DETAIL表中的数据
        /// </summary>
        public const string URL_QueryBillsByStatus = "/wms/pickTaskManager/getPickTaskInfo";
        /// <summary>
        /// 捡货任务管理-捡货商品列表 /wms/
        /// </summary>
        public const string URL_GetPickPlan = "/wms/pickTaskManager/getPickTaskPlan";
        /// <summary>
        /// 出库单管理，，自定义查询	
        /// </summary>
        public const string URL_selectBillBody = "/wms/outOrder/selectBillBody";
        /// <summary>
        /// 出库单管理：所有未完成,近一周单据，刷新按照库房、收货方式、状态（是小于某个状态）的单据
        /// </summary>
        public const string URL_QueryBillsQuicklyBill = "/wms/outOrder/selectBillsQuickly";
        /// <summary>
        /// 打印销售发货单－获取车辆信息
        /// </summary>
        public const string URL_GetVehicleInfo = "/wms/printSellBill/getVehicleInfo";
        /// <summary>
        /// 打印销售发货单－释放订单相关托盘
        /// </summary>
        public const string URL_UpdateContainerState = "/wms/printSellBill/updateContainerState";
        /// <summary>
        /// 打印销售发货单－生成车次信息
        /// </summary>
        public const string URL_CreateTrain = "/wms/printSellBill/createTrain";
        /// <summary>
        /// 打印销售发货单－根据车次号和关联车辆查询车头信息
        /// </summary>
        public const string URL_GetVhicleHeadersInfoByBillID = "/wms/printSellBill/getVhicleHeadersInfoByBillID";
        /// <summary>
        /// 出库单管理：修改出库方式
        /// </summary>
        public const string URL_UpdateOutstoreStype = "/wms/outOrder/updateOutstoreStype";
        /// <summary>
        /// 出库单管理：订单操作-等待装车
        /// </summary>
        public const string URL_SetBillStatesSend = "/wms/outOrder/updateBillState";
        /// <summary>
        /// 出库单管理：拣货记录
        /// </summary>
        public const string URL_GetPickRecordsByBillID = "/wms/outOrder/getPickRecordsByBillID";
        /// <summary>
        /// 出库单管理：拣货计划 赵龙淼
        /// </summary>
        public const string URL_GetPickPlanLongMiao = "/wms/outOrder/getPickPlan";
        /// <summary>
        /// 出库单管理：称重记录
        /// </summary>
        public const string URL_GetWeighRecordsByBillID = "/wms/outOrder/getWeighRecordsByBillID";
        /// <summary>
        /// 出库单管理：修改备注
        /// </summary>
        public const string URL_UpdateWmsRemark = "/wms/outOrder/updateRemark";
        /// <summary>
        /// 出库单管理:打印出库单
        /// </summary>
        public const string URL_UpdatePrintedFlagLongMiao = "/wms/outOrder/updatePrintedFlag";
        /// <summary>
        /// 出库单管理：容器详情(订单详情)
        /// </summary>
        public const string URL_GetContainerByBillID = "/wms/outOrder/getContainerByBillID";
        /// <summary>
        /// 出库单管理:取消订单
        /// </summary>
        public const string URL_CancelBill = "/wms/outOrder/updateCancelOrder";
        /// <summary>
        /// 出单管理，等待装车--用户授权
        /// </summary>
        public const string URL_TempAuthorize = "/wms/outOrder/selectUserList";
        /// <summary>
        /// 物流箱状态查询－查询物流箱状态
        /// </summary>
        public const string URL_ListContainerStateZhangJinQiao = "/wms/containerStatus/listContainerState";
        /// <summary>
        /// 物流箱状态查询－查询物流箱当前记录
        /// </summary>
        public const string URL_GetContainerRecordsZhangJinQiao = "/wms/containerStatus/getContainerRecords";
        /// <summary>
        /// 出库单管理，查询出库单明细
        /// </summary>
        public const string URL_GetDetails = "/wms/outOrder/selectDetailList";
        /// <summary>
        /// 打印销售发货单－获取一个未选择人员的装车信息
        /// </summary>
        public const string URL_GetLoadingInfoByNonChooseUser = "/wms/printSellBill/getLoadingInfoByNonChooseUser";
        /// <summary>
        /// 生成SO的操作日志信息
        /// </summary>
        public const string URL_InsertSOLog = "/wms/printSellBill/insertSOLog";
        /// <summary>
        /// 获取选择车辆的所有装车编号
        /// </summary>
        public const string URL_GetLoadingHeaderByVehicleID = "/wms/printSellBill/getLoadingHeaderByVehicleID";
        /// <summary>
        /// 列出某个组织下面的某个角色的成员，例如保税库的发货员，状态必须是启用的
        /// </summary>
        public const string URL_ListUsersByRoleAndWarehouseCode = "/wms/printSellBill/listUsersByRoleAndWarehouseCode";
        /// <summary>
        /// 越库出库，确认发货
        /// </summary>
        public const string URL_AcrossOutbound = "/wms/outOrder/updateCrossDock";
        /// <summary>
        /// 查询当前装车编号所有的订单的托盘（理论重量含托盘和地牛自重）
        /// </summary>
        public const string URL_GetCurrentVhNoAllContainers = "/wms/getCurrenContainers";
        /// <summary>
        /// 获取订单指定托盘的记录，如果是散归整，计算整箱的重量
        /// </summary>
        public const string URL_GetPickRecordsByCtCode = "/wms/getPickRecordsByCtCode";
        /// <summary>
        /// 检查当前托盘是否符合预期的 称重装车顺序
        /// </summary>
        public const string URL_CheckTuopanIsExpect = "/wms/updateWeightExpect";
        /// <summary>
        /// 清空托盘位
        /// </summary>
        public const string URL_ClearCtl = "/wms/updateClearCtl";
        /// <summary>
        /// 拣货计划 ：生成拣货计划
        /// </summary>
        public const string URL_CreatePickPlan = "/wms/pickTask/savePickTask";
        /// <summary>
        /// 拣货计划 ：保存拣配结果 之 ： 查询临时表信息 （拣货临时表和缺货临时表）
        /// </summary>
        public const string URL_GetTempPickResult = "/wms/pickTask/selectPickTemp";
        /// <summary>
        /// 获取当前托盘内最轻商品的重量（销售单位）
        /// </summary>
        public const string URL_GetCTCodeMinWeight = "/wms/selectMinWeight";
        /// <summary>
        /// 拣货计划 ： 判断临时表里面是否有生成的记录
        /// </summary>
        public const string URL_JudgeIsNext = "/wms/pickTask/getJudgeIsNext";
        /// <summary>
        /// 当前订单量－当前订单量
        /// </summary>
        public const string URL_GetBillPlans = "/wms/frmShowNeedSKU/getBillPlans";
        /// <summary>
        /// 装车信息--查询所有
        /// </summary>
        public const string URL_GetCarAll = "/wms/outCar/selectCarList";
        /// <summary>
        /// 装车信息--分派装车-获取组别
        /// </summary>
        public const string URL_Select = "/wms/outCar/selectCarGroup";
        /// <summary>
        /// 装车信息--装车记录查询
        /// </summary>
        public const string URL_GetLoadingRecords = "/wms/outCar/getLoadingRecords";
        /// <summary>
        /// 装车信息--完成装车
        /// </summary>
        public const string URL_FinishLoadingInfo = "/wms/outCar/updateCarFinish";
        /// <summary>
        /// 装车信息--完成装车2,再次查询信息
        /// </summary>
        public const string URL_GetHeaderInfoByBillNOS = "/wms/outCar/getSoHeaderList";
        /// <summary>
        /// 装车信息：车辆变更
        /// </summary>
        public const string URL_ChangeVehicle = "/wms/outCar/updateVehicle";
        /// <summary>
        /// 装车信息：编辑装车信息--获取指定装车编号中装车顺序最大的值
        /// </summary>
        public const string URL_GetMaxInVehicleSort = "/wms/outCar/getMaxSort";
        /// <summary>
        /// 装车信息：编辑装车信息--移除
        /// </summary>
        public const string URL_DeleteDetails = "/wms/outCar/deleteLocalDetail";
        /// <summary>
        /// 装车信息：编辑装车信息--移除task
        /// </summary>
        public const string URL_RemoveLoadingTask = "/wms/outCar/deleteTaskBillId";
        /// <summary>
        /// 装车信息：编辑装车信息--插入详细
        /// </summary>
        public const string URL_InsertDetails = "/wms/outCar/insertDetails";
        /// <summary>
        /// 装车信息：编辑装车信息-创建任务
        /// </summary>
        public const string URL_CreateTask = "/wms/outCar/insertTaskCreate";
        /// <summary>
        /// 装车信息：编辑装车信息-保存编辑-删除车辆信息
        /// </summary>
        public const string URL_DeleteVehicleInfo = "/wms/outCar/deleteVehicleInfo";
        /// <summary>
        /// 回款确认－更新发货单的回款确认标记
        /// </summary>
        public const string URL_UpdateConfirmFlag = "/wms/returnMoneyConfirm/updateConfirmFlag";
        /// <summary>
        /// 回款确认－根据车辆信息获取未做回款确认的出库单信息
        /// </summary>
        public const string URL_GetVhicleHeadersByVehicleID = "/wms/returnMoneyConfirm/getVhicleHeadersByVehicleID";
        /// <summary>
        /// 回款确认－根据车辆信息获取确认记录
        /// </summary>
        public const string URL_GetConfirmHistory = "/wms/returnMoneyConfirm/getConfirmHistory";
        /// <summary>
        /// 保存出库单的各种金额 
        /// </summary>
        public const string URL_SaveAmount = "/wms/returnMoneyConfirm/saveAmount";
        /// <summary>
        /// 回款确认－根据单据ID更新状态和标记
        /// </summary>
        public const string URL_UpdateDelayedOrderd = "/wms/returnMoneyConfirm/updateDelayedOrder";
        /// <summary>
        /// 存储排序记录
        /// </summary>
        public const string URL_CreateLoadingInfo = "/wms/outCar/createLoadingInfo";
        /// <summary>
        /// 装车信息：编辑装车信息-添加人员
        /// </summary>
        public const string URL_InsertUser = "/wms/outCar/insertUser";
        /// <summary>
        /// 装车信息：编辑装车信息-移除人员
        /// </summary>
        public const string URL_DeleteUser = "/wms/outCar/deleteUser";
        /// <summary>
        /// 装车信息：编辑装车信息-修改TMS表头的本地状态
        /// </summary>
        public const string URL_UpdateLocState = "/wms/outCar/updateLocState";
        /// <summary>
        /// 根据分组，查询待装车信息
        /// </summary>
        public const string URL_Details = "/wms/outCar/details";
        /// <summary>
        /// 得到车辆类型
        /// </summary>
        public const string URL_GetVHtype = "/wms/outCar/getVHtype";
        /// <summary>
        /// 显示关联的订单和装车员--装车员
        /// </summary>
        public const string URL_GetLoadingUsers = "/wms/outCar/getLoadingUsers";
        /// <summary>
        /// 显示关联的订单和装车员--订单
        /// </summary>
        public const string URL_GetLoadingDetails = "/wms/outCar/getLoadingDetails";
        /// <summary>
        /// 生成叫号信息
        /// </summary>
        public const string URL_CreateCalling = "/wms/outCar/createCalling";
        /// <summary>
        /// 订单线路查询－订单线路查询
        /// </summary>
        public const string URL_QueryBillsQuery = "/wms/orderLine/queryBillsQuery";
        /// <summary>
        /// 获取等待称重并且未生成装车任务的订单
        /// </summary>
        public const string URL_GetUnLoadingBills = "/wms/outCar/getUnLoadingBills";
        /// <summary>
        /// 任务刷新
        /// </summary>
        public const string URL_AutoAssignTask = "/wms/outCar/taskRefresh";
        /// <summary>
        /// 获取活动状态的集合
        /// </summary>
        public const string URL_GetStatusList = "/wms/outCar/getStatusList";
        /// <summary>
        /// 装车信息--删除没有明显的装车表头
        /// </summary>
        public const string URL_DeleteLoading = "/wms/outCar/deleteLoading";
        /// <summary>
        /// 获取所有装车信息表头
        /// </summary>
        public const string URL_GetLoadingHeaders = "/wms/outCar/getLoadingHeaders";
        /// <summary>
        /// 拣货计划 ： 判断货位是否被禁用
        /// </summary>
        public const string URL_QueryNoActiveLocBySku = "/wms/pickTask/getNoActiveLocBySku";
        /// <summary>
        /// 拣货计划 ： 排除缺货的订单，清空缺货信息
        /// </summary>
        public const string URL_DeleteTempPickAll = "/wms/pickTask/deleteTempPickAll";
        /// <summary>
        /// 拣货计划 ： 保存拣配结果
        /// </summary>
        public const string URL_SavePickPlan = "/wms/pickTask/savePickingTaskResult";
        /// <summary>
        /// 获取车次信息
        /// </summary>
        public const string URL_GetTrainSOMsg = "/wms/outCar/getTrainSOMsg";
        /// <summary>
        /// 打印装车单query
        /// </summary>
        public const string URL_Query = "/wms/outCar/query";
        /// <summary>
        /// 打印装车单getTrainSOUserEntity
        /// </summary>
        public const string URL_GetTrainSOUserEntity = "/wms/outCar/getTrainSOUserEntity";
        /// <summary>
        /// 车次信息-打印装车单-获取所有有关联的托盘
        /// </summary>
        public const string URL_GetContainerListByBillIDNoPara = "/wms/outCar/getContainerListByBillID";
        /// <summary>
        /// 车次信息-打印装车单-获取所有有关联的托盘
        /// </summary>
        public const string URL_GetContainerListByBillID = "/wms/outCar/selectContainerListByBillID";
        /// <summary>
        /// 修改订单状态为 693
        /// </summary>
        public const string URL_UpdateBillStatus = "/wms/outCar/updateBillStatus";
        /// <summary>
        /// 获取车次订单明细
        /// </summary>
        public const string URL_GetTrainSODetailMsg = "/wms/outCar/getTrainSODetailMsg";
        /// <summary>
        /// 人员维护--查询人员信息
        /// </summary>
        public const string URL_GetTrainSOUsersMsg = "/wms/outCar/getTrainSOUsersMsg";
        /// <summary>
        /// 车次信息查询
        /// </summary>
        public const string URL_GetLoadingTrainRecords = "/wms/outCar/getLoadingTrainRecords";
        /// <summary>
        /// 车次信息-人员维护--查询当前车次中车次信息的创建时间
        /// </summary>
        public const string URL_GetVHCreateDate = "/wms/outCar/getVHCreateDate";
        /// <summary>
        /// 清除现有的人员数据
        /// </summary>
        public const string URL_ClearUsers = "/wms/outCar/clearUsers";
        /// <summary>
        /// 车次关联司机-助理
        /// </summary>
        public const string URL_CreateUsers = "/wms/outCar/createUsers";
        /// <summary>
        /// 回车确认
        /// </summary>
        public const string URL_GetBulkCargoQty = "/wms/outCar/getBulkCargoQty";
        /// <summary>
        /// 回车确认
        /// </summary>
        public const string URL_ConfirmTrain = "/wms/outCar/confirmTrain";
        /// <summary>
        /// 车次信息---查询所有人员
        /// </summary>
        public const string URL_ListUsers = "/wms/outCar/listUsers";
        /// <summary>
        /// 拣货计划（修改订单状态）
        /// </summary>
        public const string URL_UpdateBillsState = "/wms/pickPlan/updateBillsState";
        /// <summary>
        /// 拣货计划（获得值）
        /// </summary>
        public const string URL_GetValue = "/wms/pickPlan/getValue";
        /// <summary>
        /// 拣货计划（删除拣货计划临时数据 ）
        /// </summary>
        public const string URL_DeletePickTemp = "/wms/pickPlan/deletePickTemp";
        /// <summary>
        /// 拣货计划（获得订单状态 ）
        /// </summary>
        public const string URL_GetBillStatus = "/wms/pickPlan/getBillStatus";
        /// <summary>
        /// 拣货计划（保存发货方式及拣货区域）
        /// </summary>
        public const string URL_SaveStrategy = "/wms/pickPlan/saveStrategy";
        /// <summary>
        /// 拣货计划（删除已有拣配计算的结果，必须是未开始拣货）
        /// </summary>
        public const string URL_DeletePickPlan = "/wms/pickPlan/deletePickPlan";
        /// <summary>
        /// 订单排序查询
        /// </summary>
        public const string URL_QuerySort = "/wms/outCar/querySort";
        /// <summary>
        /// 获取系统设置
        /// </summary>
        public const string URL_GetSysLoadingSetting = "/wms/outCar/getSysLoadingSetting";
        /// <summary>
        /// 查询未分组的订单
        /// </summary>
        public const string URL_QueryBillsSortMap = "/wms/outCar/queryBills";
        /// <summary>
        /// 存储排序记录
        /// </summary>
        public const string URL_SaveSortOrders = "/wms/outCar/saveSortOrders";
        /// <summary>
        /// 存储排序记录
        /// </summary>
        public const string URL_InsertSortBill = "/wms/outCar/insert";
        /// <summary>
        /// 检测订单物流箱是否全部在电子称上称重
        /// </summary>
        public const string URL_IsWeightedAllWLXByBillID = "/wms/IsWeightedAllWLXByBillID";
        #endregion

        #region 库内管理
        /// <summary>
        /// 查看商品库存信息
        /// </summary>
        public const string URL_GetStockSKU = "/wms/storeStock/getStockSKU";
        /// <summary>
        /// 台账记录
        /// </summary>
        public const string URL_QuerySkuLog = "/wms/storeStock/querySkuLog";
        /// <summary>
        /// 删除0库存行
        /// </summary>
        public const string URL_DeleteStock = "/wms/storeStock/deleteStock";
        /// <summary>
        /// 查看库存占用货主
        /// </summary>
        public const string URL_GetPickingScan = "/wms/storeStock/getPickingScan";
        /// <summary>
        /// 实时库存查询
        /// </summary>
        public const string URL_QueryStock = "/wms/storeStock/queryStock";
        /// <summary>
        /// 标记--添加商品质量
        /// </summary>
        public const string URL_UpdateSkuQuality = "/wms/storeStock/updateSkuQuality";
        /// <summary>
        /// 待称重集货区查询--按照SKU统计
        /// </summary>
        public const string URL_GetTempStockBySKU = "/wms/storeStock/getTempStockBySKU";
        /// <summary>
        /// 待称重集货区查询--按照托盘统计
        /// </summary>
        public const string URL_GetTempStockByCTCode = "/wms/storeStock/getTempStockByCTCode";
        /// <summary>
        /// 待称重集货区查询--按照订单统计
        /// </summary>
        public const string URL_GetTempStockByBill = "/wms/storeStock/getTempStockByBill";
        /// <summary>
        /// 库存转移--查询
        /// </summary>
        public const string URL_QueryStockRemove = "/wms/storeStock/queryStockRemove";
        /// <summary>
        /// 库存转移--保存编辑的采购单
        /// </summary>
        public const string URL_SaveBill = "/wms/storeStock/saveBill";
        /// <summary>
        /// 库存转移--分派任务
        /// </summary>
        public const string URL_Schedule = "/wms/storeStock/schedule";
        /// <summary>
        /// 库存转移---库存转移
        /// </summary>
        public const string URL_GetAllLocation = "/wms/storeStock/getAllLocation";
        /// <summary>
        /// 移库记录表--查询移库记录
        /// </summary>
        public const string URL_QueryTransRecords = "/wms/storeStock/queryTransRecords";
        /// <summary>
        /// 触发补货任务---开始计算
        /// </summary>
        public const string URL_InquiryStock = "/wms/storeStock/inquiryStock";
        /// <summary>
        /// 查询所有物料，用于物料维护，如果是填充其他界面，请调用GetActiveMaterials()函数
        /// </summary>
        public const string URL_GetAll = "/wms/storeStock/getAll";
        /// <summary>
        /// 删除临时补货
        /// </summary>
        public const string URL_DeleteTempReplenish = "/wms/cargoAdd/updateReplenishTmp";
        /// <summary>
        /// 当前订单量（获取结果集）
        /// </summary>
        public const string URL_GetResultByGID = "/wms/frmShowNeedSKU/getResultByGID";
        #endregion


        #region 盘点管理
        /// <summary>
        /// 创建盘点单--获取存储区货位
        /// </summary>
        public const string URL_GetStockLocation = "/wms/takeStock/getStockLocation";
        /// <summary>
        /// 创建盘点单--列出今天库存发生变动的货位
        /// </summary>
        public const string URL_ListChangedLocations = "/wms/takeStock/listChangedLocations";
        /// <summary>
        /// 创建盘点单---保存盘点单
        /// </summary>
        public const string URL_SaveCountBill = "/wms/takeStock/saveCountBill";
        /// <summary>
        /// 盘点单管理---根据条件查询盘点单
        /// </summary>
        public const string URL_QueryBills_PanDian = "/wms/takeStock/queryBills";
        /// <summary>
        /// 盘点单管理--落放位
        /// </summary>
        public const string URL_GetCountLocation = "/wms/takeStock/getCountLocation";
        /// <summary>
        /// 盘点单管理--盘点记录
        /// </summary>
        public const string URL_GetCountRecords = "/wms/takeStock/getCountRecords";
        /// <summary>
        /// 盘点单管理---跟库存实时比对，显示报告
        /// </summary>
        public const string URL_GetReportVsStock = "/wms/takeStock/getReportVsStock";
        /// <summary>
        /// 盘点单管理---完成订单
        /// </summary>
        public const string URL_CompleteBill = "/wms/takeStock/completeBill";
        /// <summary>
        /// 盘点单管理---报告上传
        /// </summary>
        public const string URL_GetBillInfo = "/wms/takeStock/getBillInfo";
        /// <summary>
        /// 盘点单管理---报告上传
        /// </summary>
        public const string URL_GetReportOnlyDiff = "/wms/takeStock/getReportOnlyDiff";
        /// <summary>
        /// 盘点差异调整--更新状态
        /// </summary>
        public const string URL_UpdateBillState = "/wms/takeStock/updateBillState";
        /// <summary>
        /// 盘点单管理---复盘============获取当前盘点差异单据的明细
        /// </summary>
        public const string URL_ListGetLocations = "/wms/takeStock/listGetLocations";
        /// <summary>
        /// 盘点任务分派--根据角色查询人员
        /// </summary>
        public const string URL_ListUsersByRoleAndWarehouseCodeForCount = "/wms/takeStock/listUsersByRoleAndWarehouseCodeForCount";
        /// <summary>
        /// 盘点任务分派---编辑查看
        /// </summary>
        public const string URL_GetCountLocations = "/wms/takeStock/getCountLocations";
        /// <summary>
        /// 盘点任务分派--保存任务分派
        /// </summary>
        public const string URL_SaveCountTask = "/wms/takeStock/saveCountTask";
        /// <summary>
        /// 盘点差异调整---读取差异调整单
        /// </summary>
        public const string URL_GetBills = "/wms/takeStock/getBills";
        /// <summary>
        /// 盘点差异调整
        /// </summary>
        public const string URL_ExecuteStock = "/wms/takeStock/executeStock";

        /// <summary>
        /// 盘点差异调整
        /// </summary>
        public const string URL_CycleCountStockExecute = "/wms/takeStock/executeStockNew";
        /// <summary>
        /// 盘点任务分派--同步上传状态
        /// </summary>
        public const string URL_UpdateBillSyncState = "/wms/takeStock/updateBillSyncState";
        /// <summary>
        /// 盘点单管理---保存
        /// </summary>
        public const string URL_SaveReportDetail = "/wms/takeStock/saveReportDetail";
        #endregion

        #region 查询统计
        /// <summary>
        /// 查询统计（商品销量统计）
        /// </summary>
        public const string URL_GetSKUSaleSort = "/wms/merchandiseSales/getSKUSaleSort";
        /// <summary>
        /// 查询统计（装车记录查询-查询指定车辆的装车记录）
        /// </summary>
        public const string URL_GetLoadRecordsByWhCode = "/wms/loadingCarRecord/getLoadRecordsByWhCode";
        /// <summary>
        /// 查询统计（拣货记录表－查询拣货记录）
        /// </summary>
        public const string URL_QueryPickRecords = "/wms/pickRecord/queryPickRecords";
        /// <summary>
        /// 查询统计（销货明细）
        /// </summary>
        public const string URL_QuerySoDetails = "/wms/salesDetail/querySoDetails";
        /// <summary>
        /// 查询统计（装车绩效考核）
        /// </summary>
        public const string URL_GetLoadingReport2 = "/wms/loadingMerit/getLoadingReport2";
        /// <summary>
        /// 查询统计（任务调度统计－获取某个库房下面的所有的拥有任务角色的人员
        /// </summary>
        public const string URL_ListUsersByWarehouseCodeAndTask = "/wms/taskSchedule/listUsersByWarehouseCodeAndTask";
        /// <summary>
        /// 查询统计（任务调度统计－根据userCode查询任务调度）
        /// </summary>
        public const string URL_GetReport = "/wms/taskSchedule/getReport";
        /// <summary>
        /// 查询统计（容器位查询）
        /// </summary>
        public const string URL_GetContainerInfo = "/wms/containerPosition/getContainerInfo";
        /// <summary>
        /// 查询统计（叉车司机任务统计－查询移货记录）
        /// </summary>
        public const string URL_QueryTransRecordsChaChe = "/wms/forkliftDriverTask/queryTransRecords";
        /// <summary>
        /// 查询统计（叉车司机任务统计－获取上架记录）
        /// </summary>
        public const string URL_GetPutawayRecords = "/wms/forkliftDriverTask/getPutawayRecords";
        /// <summary>
        /// 查询统计（叉车司机任务统计－获取上架记录条数）
        /// </summary>
        public const string URL_GetPutawayRecordsCount = "/wms/forkliftDriverTask/getPutawayRecordsCount";
        /// <summary>
        /// 查询统计（收货绩效考核－获取人员入库清点记录）
        /// </summary>
        public const string URL_GetAsnRecords = "/wms/receiveMerit/getAsnRecords";
        /// <summary>
        /// 查询统计（收货绩效考核－获取crn记录）
        /// </summary>
        public const string URL_GetCrnRecords = "/wms/receiveMerit/getCrnRecords";
        /// <summary>
        /// 查询统计（库房人员绩效汇总）
        /// </summary>
        public const string URL_SummaryByPersonnel = "/wms/storeEmpMerit/summaryByPersonnel";
        /// <summary>
        /// 当前订单量（查询补货库存）
        /// </summary>
        public const string URL_QueryReplenishStock = "/wms/frmShowNeedSKU/queryReplenishStock";
        /// <summary>
        /// 通过商品编码审计
        /// </summary>
        public const string URL_InquiryBySku = "/wms/frmShowNeedSKU/insertCargo";
        /// <summary>
        /// 加载菜单模块数据
        /// </summary>
        public const string URL_ListSystemMenus = "/wms/systemController/listSystemMenus";
        /// <summary>
        /// 登录--获取一个用户的详细信息
        /// </summary>
        public const string URL_GetUserInfo = "/wms/systemController/getUserInfo";
        /// <summary>
        /// 登录--考勤登记
        /// </summary>
        public const string URL_LoginRegister = "/wms/systemController/loginRegister";
        /// <summary>
        /// 保存系统设置
        /// </summary>
        public const string URL_SaveSettings = "/wms/systemController/saveSettings";
        #endregion

        #region 基础管理
        /// <summary>
        /// 基础管理（车辆信息-新增）
        /// </summary>
        public const string URL_InsertSave = "/wms/vehicleInfo/insert";
        /// <summary>
        /// 基础管理（车辆信息-更改）
        /// </summary>
        public const string URL_UpdateSave = "/wms/vehicleInfo/update";
        /// <summary>
        /// 基础管理（车辆信息-删除）
        /// </summary>
        public const string URL_Delete = "/wms/vehicleInfo/delete";
        /// <summary>
        /// 基础管理（送货路线-查询所有）
        /// </summary>
        public const string URL_GetAllRoute = "/wms/sendRoute/getAllRoute";
        /// <summary>
        /// 基础管理（送货路线-添加）
        /// </summary>
        public const string URL_InsertSaveRoute = "/wms/sendRoute/insert";
        /// <summary>
        /// 基础管理（送货路线-更改）
        /// </summary>
        public const string URL_UpdateSaveRoute = "/wms/sendRoute/update";
        /// <summary>
        /// 基础管理（送货路线-删除）
        /// </summary>
        public const string URL_DeleteUnit = "/wms/sendRoute/delete";
        /// <summary>
        /// 基础管理（仓库信息-查询所有仓库信息）
        /// </summary>
        public const string URL_GetAllWarehouse = "/wms/warehouseInfo/getAllWarehouseInfo";
        /// <summary>
        /// 基础管理（仓库信息-查询所有组织）
        /// </summary>
        public const string URL_GetAllOrganization = "/wms/warehouseInfo/getAllOrganization";
        /// <summary>
        /// 基础管理（仓库信息-更改仓库信息）
        /// </summary>
        public const string URL_WarehouseAddAndUpdate = "/wms/warehouseInfo/updateWarehouseInfo";
        /// <summary>
        /// 基础管理（仓库信息-根据仓库查询所有货区和货位）
        /// </summary>
        public const string URL_GetZoneByWarehouseCode = "/wms/warehouseInfo/getZoneByWarehouseCode";
        /// <summary>
        /// 基础管理（货区信息-查询所有货区）
        /// </summary>
        public const string URL_GetAllZone = "/wms/goodsZoneInfo/getAllZone";
        /// <summary>
        /// 基础管理（货区信息-添加货区）
        /// </summary>
        public const string URL_SaveAddZone = "/wms/goodsZoneInfo/addZone";
        /// <summary>
        /// 基础管理（货区信息-更改货区信息）
        /// </summary>
        public const string URL_SaveUpdateZone = "/wms/goodsZoneInfo/updateZone";
        /// <summary>
        /// 基础管理（货区信息-删除货区信息）
        /// </summary>
        public const string URL_DeleteZone = "/wms/goodsZoneInfo/deleteZone";
        /// <summary>
        /// 基础管理（货区信息-根据所选货区查询所有货位）
        /// </summary>
        public const string URL_GetAllLocationByZone = "/wms/goodsZoneInfo/getAllLocationByZone";
        /// <summary>
        /// 基础管理（货区信息-查询所有温控信息）
        /// </summary>
        public const string URL_GetAllTemperature = "/wms/goodsZoneInfo/getAllTemperature";
        /// <summary>
        /// 基础管理（货位信息-查询所有货位）
        /// </summary>
        public const string URL_GetAllLocationZJQ = "/wms/goodsLocationInfo/getAllLocation";
        /// <summary>
        /// 基础管理（货位信息-添加货位）
        /// </summary>
        public const string URL_SaveAddLocationInfo = "/wms/goodsLocationInfo/addLocationInfo";
        /// <summary>
        /// 基础管理（货位信息-编辑货位）
        /// </summary>
        public const string URL_UpdateLocationInfo = "/wms/goodsLocationInfo/updateLocationInfo";
        /// <summary>
        /// 基础管理（货位信息-删除货位）
        /// </summary>
        public const string URL_DeleteLocation = "/wms/goodsLocationInfo/deleteLocation";
        /// <summary>
        /// 基础管理（推荐货位-查询所有推荐货位）
        /// </summary>
        public const string URL_GetAllRecLocation = "/wms/recommendLocation/getAllRecLocation";
        /// <summary>
        /// 基础管理（推荐货位-添加或编辑库存货位）
        /// </summary>
        public const string URL_Save = "/wms/recommendLocation/saveSkuLocation";
        /// <summary>
        /// 基础管理（推荐货位-清空货位商品）
        /// </summary>
        public const string URL_DeleteRecLoc = "/wms/recommendLocation/deleteSkuLocation";
        /// <summary>
        /// 基础管理（物料信息-获取本库物料信息）
        /// </summary>
        public const string URL_GetLocalAll = "/wms/suppliesInfo/getLocalAll";
        /// <summary>
        /// 基础管理（物料信息-编辑物料信息）
        /// </summary>
        public const string URL_UpdateSkuInfo = "/wms/suppliesInfo/updateSkuInfo";
        /// <summary>
        /// 基础管理（包装关系-查询所有计量单位和组）
        /// </summary>
        public const string URL_GetAllZJQ = "/wms/packRelation/getAll";
        /// <summary>
        /// 基础管理（包装关系-编辑包装关系）
        /// </summary>
        public const string URL_SaveUpdateUmSku = "/wms/packRelation/updateUmSku";
        /// <summary>
        /// 基础管理（包装关系-查询所有计量单位）
        /// </summary>
        public const string URL_GetAllUnit = "/wms/packRelation/getAllUnit";
        /// <summary>
        /// 基础管理（本库物料-查询所有本库物料）
        /// </summary>
        public const string URL_GetAllSkuWarehouse = "/wms/thisSupplies/getAllSkuWarehouse";
        /// <summary>
        /// 基础管理（本库物料-更新本库物料）
        /// </summary>
        public const string URL_SaveUpdateSkuWarehouse = "/wms/thisSupplies/updateSkuWarehouse";
        /// <summary>
        /// 基础管理（客户信息-查询所有客户及默认地址）
        /// </summary>
        public const string URL_GetAllCustomer = "/wms/clientInfo/getAllCustomer";
        /// <summary>
        /// 基础管理（物料分类-查询所有分类）
        /// </summary>
        public const string URL_GetMaterialTypeAll = "/wms/suppliesClassify/getAll";
        /// <summary>
        /// 基础管理（物料分类-添加物料分类）
        /// </summary>
        public const string URL_SaveAddSkuType = "/wms/suppliesClassify/addSkuType";
        /// <summary>
        /// 基础管理（物料分类-更新物料分类）
        /// </summary>
        public const string URL_SaveUpdateSkuType = "/wms/suppliesClassify/updateSkuType";
        /// <summary>
        /// 基础管理（物料分类-删除物料分类）
        /// </summary>
        public const string URL_DeleteSkuType = "/wms/suppliesClassify/deleteSkuType";
        /// <summary>
        /// 基础管理（计量单位信息-添加计量单位）
        /// </summary>
        public const string URL_SaveAddWmUm = "/wms/measureUnit/addWmUm";
        /// <summary>
        /// 基础管理（计量单位信息-编辑计量单位）
        /// </summary>
        public const string URL_SaveUpdateWmUm = "/wms/measureUnit/updateWmUm";
        /// <summary>
        /// 基础管理（计量单位信息-删除计量单位）
        /// </summary>
        public const string URL_DeleteUnitZQJ = "/wms/measureUnit/deleteWmUm";
        /// <summary>
        /// 基础管理（区域信息-查询所有区域信息）
        /// </summary>
        public const string URL_GetAreaAll = "/wms/areaInfo/getAll";
        /// <summary>
        /// 基础管理（区域信息-编辑区域信息）
        /// </summary>
        public const string URL_SaveUpdateArea = "/wms/areaInfo/updateArea";
        /// <summary>
        /// 基础管理（品牌信息-获取所有的品牌）
        /// </summary>
        public const string URL_GetAllBrands = "/wms/brandInfo/getAllBrands";
        /// <summary>
        /// 基础管理（品牌信息-更新品牌信息）
        /// </summary>
        public const string URL_SaveUpdateBrandInfo = "/wms/brandInfo/updateBrandInfo";
        /// <summary>
        /// 基础管理（品牌信息-按照次序排序的供应商列表）
        /// </summary>
        public const string URL_ListActiveSupplierByPriorityZJQ = "/wms/brandInfo/listActiveSupplierByPriority";
        /// <summary>
        /// 基础管理（品牌信息-存入数据库，排除已经关联的）
        /// </summary>
        public const string URL_CreateRelationWithSupplier = "/wms/brandInfo/createRelationWithSupplier";
        /// <summary>
        /// 基础管理（品牌信息-重新绑定关联的供应商）
        /// </summary>
        public const string URL_ListRelationSuppliers = "/wms/brandInfo/listRelationSuppliers";
        /// <summary>
        /// 基础管理（品牌信息-断开品牌与供应商关联）
        /// </summary>
        public const string URL_DeleteRelationSupplier = "/wms/brandInfo/deleteRelationSupplier";
        /// <summary>
        /// 基础管理（不合格原因-查询所有不合格原因）
        /// </summary>
        public const string URL_GetAllNotHeGe = "/wms/unqualifiedCause/getAll";
        /// <summary>
        /// 基础管理（不合格原因-添加不合格原因）
        /// </summary>
        public const string URL_SaveAddBugReason = "/wms/unqualifiedCause/addBugReason";
        /// <summary>
        /// 基础管理（不合格原因-编辑不合格原因）
        /// </summary>
        public const string URL_SaveUpdateBugReason = "/wms/unqualifiedCause/updateBugReason";
        /// <summary>
        /// 基础管理（不合格原因-删除不合格原因）
        /// </summary>
        public const string URL_DeleteUnitZJQ = "/wms/unqualifiedCause/deleteBugReason";
        /// <summary>
        /// 基础管理（容器信息-查询所有容器信息）
        /// </summary>
        public const string URL_GetAllContainer = "/wms/containerInfo/getAllContainer";
        /// <summary>
        /// 基础管理（容器信息-新增容器信息）
        /// </summary>
        public const string URL_SaveAddContainerInfo = "/wms/containerInfo/addContainerInfo";
        /// <summary>
        /// 基础管理（容器信息-更改容器删除状态）
        /// </summary>
        public const string URL_SaveUpdateContainerInfo = "/wms/containerInfo/updateContainerInfo";
        /// <summary>
        /// 基础管理（容器信息-更改容器删除状态）
        /// </summary>
        public const string URL_DeleteCt = "/wms/containerInfo/delete";
        /// <summary>
        /// 基础管理（容器信息-根据托盘编号找到对应的托盘）
        /// </summary>
        public const string URL_GetContainerByCode = "/wms/containerInfo/getContainerByCode";
        /// <summary>
        /// 基础管理（容器信息-更新容器重量）
        /// </summary>
        public const string URL_UpdateWeight = "/wms/containerInfo/updateWeight";
        /// <summary>
        /// 基础管理（叉车信息-查询所有叉车）
        /// </summary>
        public const string URL_GetAllFork = "/wms/forkCarInfo/getAllFork";
        /// <summary>
        /// 基础管理（叉车信息-添加叉车信息）
        /// </summary>
        public const string URL_SaveAddForkInfo = "/wms/forkCarInfo/addForkInfo";
        /// <summary>
        /// 基础管理（叉车信息-编辑叉车信息）
        /// </summary>
        public const string URL_SaveUpdateForkInfo = "/wms/forkCarInfo/updateForkInfo";
        /// <summary>
        /// 基础管理（叉车信息-删除叉车信息）
        /// </summary>
        public const string URL_DeleteForkInfo = "/wms/forkCarInfo/deleteForkInfo";
        /// <summary>
        /// 基础管理（送货牌维护-查询所有送货牌）
        /// </summary>
        public const string URL_GetAllCardState = "/wms/driverCard/getAllCardState";
        /// <summary>
        /// 基础管理（送货牌维护-添加送货牌）
        /// </summary>
        public const string URL_SaveAddCardState = "/wms/driverCard/addCardState";
        /// <summary>
        /// 基础管理（容器位维护-查询所有容器位）
        /// </summary>
        public const string URL_QeryCTL = "/wms/containerLocation/queryCTL";
        /// <summary>
        /// 基础管理（容器位维护-添加容器位）
        /// </summary>
        public const string URL_SaveAddCTLInfo = "/wms/containerLocation/addCTLInfo";
        /// <summary>
        /// 基础管理（容器位维护-编辑容器位）
        /// </summary>
        public const string URL_SaveUpdateCTLInfo = "/wms/containerLocation/updateCTLInfo";
        /// <summary>
        /// 基础管理（容器位维护-getMaxName）
        /// </summary>
        public const string URL_GetMaxName = "/wms/containerLocation/getMaxName";
        /// <summary>
        /// 基础管理（容器位维护-删除容器位）
        /// </summary>
        public const string URL_DeleteCTL = "/wms/containerLocation/deleteCTLInfo";
        #endregion
        
        #region 系统管理
        /// <summary>
        /// 系统管理--角色管理--查询
        /// </summary>
        public const string URL_ListRoles = "/wms/systemController/listRoles";
        /// <summary>
        /// 角色管理---列出某个角色下面的所有用户
        /// </summary>
        public const string URL_ListUsersByRoleID = "/wms/systemController/listUsersByRoleId";
        /// <summary>
        /// 角色管理---角色新增或者更新
        /// </summary>
        public const string URL_SaveRole = "/wms/systemController/saveRole";
        /// <summary>
        /// 角色管理---列出某个角色下面的所有用户model
        /// </summary>
        public const string URL_ListModulesByRoleID = "/wms/systemController/listModulesByRoleId";
        /// <summary>
        /// 角色管理--查询所有modules
        /// </summary>
        public const string URL_ListModules = "/wms/systemController/listModules";
        /// <summary>
        /// 角色管理---根据roleid删除角色
        /// </summary>
        public const string URL_DeleteRole = "/wms/systemController/deleteRole";
        /// <summary>
        /// 角色管理---列出某个权限下的所有关联角色
        /// </summary>
        public const string URL_ListRolesByModuleID = "/wms/systemController/listRolesByModuleId";
        /// <summary>
        /// 角色管理--列出某个权限下的所有用户
        /// </summary>
        public const string URL_ListUsersByModuleID = "/wms/systemController/listUsersByModuleId";
        /// <summary>
        /// 任务池管理
        /// </summary>
        public const string URL_GetCurrentTask = "/wms/taskPoolController/getCurrentTask";
        /// <summary>
        /// 获取任务对应的订单状态
        /// </summary>
        public const string URL_TaskState = "/wms/taskPoolController/taskState";
        /// <summary>
        /// 关闭任务---
        /// </summary>
        public const string URL_CloseTask = "/wms/taskPoolController/closeTask";
        /// <summary>
        /// 人员状态表
        /// </summary>
        public const string URL_ListUserState = "/wms/taskPoolController/listUserState";
        /// <summary>
        /// 任务池管理--查询1
        /// </summary>
        public const string URL_GetUserByTasks = "/wms/taskPoolController/getUsgerByTasks";
        /// <summary>
        /// 获取任务池当前状态信息
        /// </summary>
        public const string URL_GetCurrentTaskNew = "/wms/taskPoolController/getCurrentTaskNew";
        /// <summary>
        /// 获取当前符合任务角色的所有用户
        /// </summary>
        public const string URL_GetAllUsers = "/wms/taskPoolController/getAllUsers";
        /// <summary>
        /// 查询等待分配任务的单据
        /// </summary>
        public const string URL_GetTask62 = "/wms/taskPoolController/getTask62";
        /// <summary>
        /// 任务优先级-
        /// </summary>
        public const string URL_SelectZLM = "/wms/taskPoolController/select";
        /// <summary>
        /// 清空数据
        /// </summary>
        public const string URL_DeleteZLM = "/wms/taskPoolController/delete";
        /// <summary>
        /// 删除装车任务
        /// </summary>
        public const string URL_DeleteLoadingTask = "/wms/taskPoolController/deleteLoadingTask";
        /// <summary>
        /// 改变用户的任务优先级
        /// </summary>
        public const string URL_ChangeUserTaskLevel = "/wms/taskPoolController/changeUserTaskLevel";
        /// <summary>
        /// 根据用户编号获取该用户可执行的任务,优先级等
        /// </summary>
        public const string URL_GetTaskByUserCode = "/wms/taskPoolController/getTaskByUserCode";
        /// <summary>
        /// 查询可执行任务人员
        /// </summary>
        public const string URL_ListUserRolesByPick = "/wms/taskPoolController/listUserRolesByPick";
        /// <summary>
        /// 查询任务名称
        /// </summary>
        public const string URL_GetRoleNameByTaskType = "/wms/taskPoolController/getRoleNameByTaskType";
        /// <summary>
        /// 判断是否存在已分配某个人
        /// </summary>
        public const string URL_CanAdd = "/wms/taskPoolController/CanAdd";
        /// <summary>
        /// 变更人员
        /// </summary>
        public const string URL_ChangeInstoreTask = "/wms/taskPoolController/changeInstoreTask";
        /// <summary>
        /// 添加人员
        /// </summary>
        public const string URL_AddInstoreTaskPerson = "/wms/taskPoolController/addInstoreTaskPerson";
        /// <summary>
        /// 系统设置--获取系统设置
        /// </summary>
        public const string URL_GetSysSetting = "/wms/systemController/getSysSetting";
        /// <summary>
        /// 系统设置--保存
        /// </summary>
        public const string URL_SaveSysSetting = "/wms/systemController/saveSysSetting";
        /// <summary>
        /// 用户管理---根据userCode查询角色信息
        /// </summary>
        public const string URL_ListUserRoles = "/wms/systemController/listUserRoles";
        /// <summary>
        /// 用户管理---根据库房编号获取用户编号最大值（自动生成用户编号）
        /// </summary>
        public const string URL_GetMaxUserCode = "/wms/systemController/getMaxUserCode";
        /// <summary>
        /// 用户管理--获取某个用户的角色信息
        /// </summary>
        public const string URL_ListMyRoles = "/wms/systemController/listMyRoles";
        /// <summary>
        /// 用户管理---新增
        /// </summary>
        public const string URL_SaveInsertUsers = "/wms/systemController/insertUsers";
        /// <summary>
        /// 用户管理---删除
        /// </summary>
        public const string URL_DeleteUserZLM = "/wms/systemController/deleteUser";
        /// <summary>
        /// 用户管理---重置密码
        /// </summary>
        public const string URL_ChangePassword = "/wms/systemController/changePassword";
        /// <summary>
        /// 条码规则定义--保存
        /// </summary>
        public const string URL_SaveBarcodeRule = "/wms/systemController/saveBarcodeRule";
        /// <summary>
        /// 删除公司信息
        /// </summary>
        public const string URL_DeleteCompany = "/wms/systemController/deleteCompany";
        /// <summary>
        /// 读取条码规范定义表
        /// </summary>
        public const string URL_GetBarcodeRule = "/wms/systemController/getBarcodeRule";
        /// <summary>
        /// 选择任务---根据任务类型，获取指定任务列表
        /// </summary>
        public const string URL_GetTasksByType = "/wms/taskPoolController/getTasksByType";
        /// <summary>
        /// 任务优先--stickTask
        /// </summary>
        public const string URL_StickTask = "/wms/taskPoolController/stickTask";
        /// <summary>
        /// 任务池管理(新)--根据任务ID 获取订单明细
        /// </summary>
        public const string URL_GetDetailsByTaskID = "/wms/taskPoolController/getDetailsByTaskID";
        /// <summary>
        /// 改变任务
        /// </summary>
        public const string URL_TaskChange = "/wms/taskPoolController/taskChange";
        /// <summary>
        /// 任务池管理(新)--任务优先级管理--保存insert
        /// </summary>
        public const string URL_InsertZLM = "/wms/taskPoolController/insert";
        /// <summary>
        /// 任务池管理(新)--获取任务详情
        /// </summary>
        public const string URL_IGetTaskDetail = "/wms/taskPoolController/getTaskDetail";
        /// <summary>
        /// 根据任务id--得到任务类型
        /// </summary>
        public const string URL_GetTaskType = "/wms/taskPoolController/getTaskType";
        /// <summary>
        /// 当前订单量（加载报警窗体）
        /// </summary>
        public const string URL_QueryStockWarm = "/wms/frmShowNeedSKU/stockWarm";
        #endregion
        #endregion

        #region C02接口

        #region 收货管理
        /// <summary>
        /// 到货登记,绑定送货牌与入库单
        /// </summary>
        public static string URL_CreateVechileC02 = "/wms/arrivalRegister/createVechile";
        /// <summary>
        /// 查询送货牌状态
        /// </summary>
        public static string URL_CarNoIsExit = "/wms/arrivalRegister/getCardNoState";
        #endregion

        #region 销货管理
        /// <summary>
        /// 关联物流箱--物流箱查询
        /// </summary>
        public static string URL_QueryBillsByStatusZLMC02 = "/wms/logisticsBox/QueryBillsByStatus";
        /// <summary>
        /// C02销货管理（称重异常处理-获取物流箱绑定的订单信息）
        /// </summary>
        public static string URL_GetSOBillMsg = "/wms/weighExceptionC02/getSOBillMsg";
        /// <summary>
        /// C02销货管理（称重异常处理-查询该订单是否已存在称重记录）
        /// </summary>
        public static string URL_GetWeightRecordsCountByBillIDZJQ = "/wms/weighExceptionC02/getWeightRecordsCountByBillID";
        /// <summary>
        /// C02销货管理（称重异常处理-判断指定订单是否已经拣货完成（以物流箱为单位））
        /// </summary>
        public static string URL_IsPickCompleted = "/wms/weighExceptionC02/isPickCompleted";
        /// <summary>
        /// C02销货管理（称重异常处理-更新订单状态）
        /// </summary>
        public static string URL_UpdateBillStateZJQC02 = "/wms/weighExceptionC02/updateBillState";
        /// <summary>
        /// C02销货管理（称重异常处理-获取物流箱重量，通过billId）
        /// </summary>
        public static string URL_GetContainerWeightByBillID = "/wms/weighExceptionC02/getContainerWeightByBillID";
        /// <summary>
        /// C02销货管理（称重异常处理-判断当前应该打印第几箱）
        /// </summary>
        public static string URL_GetBillBoxIndex = "/wms/weighExceptionC02/getBillBoxIndex";
        /// <summary>
        /// C02销货管理（称重异常处理-获取到订单一共是多少箱）
        /// </summary>
        public static string URL_GetBillBoxIndexTotal = "/wms/weighExceptionC02/getBillBoxIndexTotal";
        /// <summary>
        /// C02销货管理（称重异常处理-P_SO_CONTAINER_WEIGHT_BOX过程）
        /// </summary>
        public static string URL_SaveCheckWeightBox = "/wms/weighExceptionC02/saveCheckWeightBox";
        /// <summary>
        /// C02销货管理（称重异常处理-获取箱贴打印记录）
        /// </summary>
        public static string URL_GetXTPrintRecord = "/wms/weighExceptionC02/getXTPrintRecord";
        /// <summary>
        /// C02销货管理（称重异常处理-生成打印记录）
        /// </summary>
        public static string URL_InsertPringtXTRcord = "/wms/weighExceptionC02/insertPringtXTRcord";
        /// <summary>
        /// C02销货管理（称重异常处理-获取物流箱绑定的订单信息）
        /// </summary>
        public static string URL_GetContainerSKUMIX = "/wms/weighExceptionC02/getContainerSKUMIX";
        /// <summary>
        /// 通过商品编码审计
        /// </summary>
       // public const string URL_InquiryBySku = "/wms/frmShowNeedSKU/insertCargo";
        /// <summary>
        /// 打印箱贴---获取物流箱实际重量
        /// </summary>
        public const string URL_GetContainerWeightActual = "/wms/printBoxStick/getContainerWeightActual";
        /// <summary>
        /// 拣货信息---获取通道拣货情况
        /// </summary>
        public const string URL_GetChannelInfo_Picking = "/wms/pickPlanC02/getChannelInfo_Picking";
        /// <summary>
        /// 拣货信息---获取通道分箱情况
        /// </summary>
        public const string URL_GetChannelInfo_Box = "/wms/pickPlanC02/getChannelInfo_Box";
        /// <summary>
        /// 拣货信息---计算自动线上物流箱总是
        /// </summary>
        public const string URL_GetBoxTotalAuto = "/wms/pickPlanC02/getBoxTotalAuto";
        /// <summary>
        /// 笼车状态表--笼车信息状态查询
        /// </summary>
        public const string URL_GetLCStateQuery = "/wms/cageCarState/getLCStateQuery";
        /// <summary>
        /// 笼车状态表---根据笼车查询 物流箱详细信息
        /// </summary>
        public const string URL_ListContainerStateC02 = "/wms/cageCarState/listContainerState";
        /// <summary>
        /// 笼车状态表---清楚物流箱占用笼车位的信息，清楚UNIQUE_CODE 值
        /// </summary>
        public const string URL_EmptyLogisticsBoxInOccupy = "/wms/cageCarState/emptyLogisticsBoxInOccupy";
        /// <summary>
        /// 订单排序：保存订单
        /// </summary>
        public const string URL_SaveSortOrdersC02 = "/wms/orderSortC02/saveOrderSort";
        /// <summary>
        /// 订单排序：查询出订单信息
        /// </summary>
        public const string URL_QueryBillsC02 = "/wms/orderSortC02/getOrderSortInfo";
        /// <summary>
        /// 拣货计划：更新订单状态
        /// </summary>
        public const string URL_UpdateBillsStateC02 = "/wms/pickPlanC02/updateBillsState";
        /// <summary>
        /// 拣货计划：验证订单是否存在临时表中
        /// </summary>
        public const string URL_JudgeIsNextC02 = "/wms/pickPlanC02/judgeIsNext";
        /// <summary>
        /// 拣货计划：查询订单详细信息
        /// </summary>
        public const string URL_QueryBillsByStatusC02 = "/wms/pickPlanC02/queryBillsByStatus";
        /// <summary>
        /// 拣货计划：查询以保存的拣货计划
        /// </summary>
        public const string URL_GetPickPlanC02 = "/wms/pickPlanC02/getPickPlan";
        /// <summary>
        /// 拣货计划：根据订单ID查询订单详细信息
        /// </summary>
        public const string URL_GetDetailsC02 = "/wms/pickPlanC02/getDetails";

        #endregion

        #region 查询统计
        /// <summary>
        /// C02查询统计（每日发车量-查询发车记录）
        /// </summary>
        public static string URL_QueryLoadingRecords = "/wms/driveEverydayC02/queryLoadingRecords";
        /// <summary>
        /// C02查询统计（托盘使用记录-根据托盘编号查询类似编号的所有托盘使用记录）
        /// </summary>
        public static string URL_GetContainerRecordsByCode = "/wms/ctUseRecordC02/getContainerRecordsByCode";
        /// <summary>
        /// C02查询统计（拣出物流箱-查询拣出物流箱）
        /// </summary>
        public static string URL_QueryPickBoxes = "/wms/pickedCtC02/queryPickBoxes";
        /// <summary>
        /// 当前订单量（查询补货库存）
        /// </summary>
       // public const string URL_QueryReplenishStock = "/wms/frmShowNeedSKU/queryReplenishStock";
        /// <summary>
        /// 当前订单量（加载报警窗体）
        /// </summary>
       // public const string URL_QueryStockWarm = "/wms/frmShowNeedSKU/stockWarm";
        /// <summary>
        /// 删除临时补货
        /// </summary>
      //  public const string URL_DeleteTempReplenish = "/wms/cargoAdd/updateReplenishTmp";
        /// <summary>
        /// 当前订单量（获取结果集）
        /// </summary>
      //  public const string URL_GetResultByGID = "/wms/frmShowNeedSKU/getResultByGID";
        /// 加载菜单模块数据
        /// </summary>
       // public const string URL_ListSystemMenus = "/wms/systemController/listSystemMenus";
        /// <summary>
        /// 登录--获取一个用户的详细信息
        /// </summary>
       // public const string URL_GetUserInfo = "/wms/systemController/getUserInfo";
        /// <summary>
        /// 登录--考勤登记
        /// </summary>
      //  public const string URL_LoginRegister = "/wms/systemController/loginRegister";
        /// <summary>
        /// 保存系统设置
        /// </summary>
      //  public const string URL_SaveSettings = "/wms/systemController/saveSettings";
        /// <summary>
        /// 销货明细表--查询
        /// </summary>
        public const string URL_QuerySoDetailsC02 = "/wms/pickPlanC02/querySoDetails";
        /// <summary>
        /// C02--拣货记录表--查询拣货记录
        /// </summary>
        public const string URL_QueryPickRecordsC02 = "/wms/pickPlanC02/queryPickRecords";
        /// <summary>
        /// C02--叉车司机任务统计--列出某个组织下面的某个角色的成员，例如保税库的发货员，状态必须是启用的
        /// </summary>
        public const string URL_ListUsersByRoleAndWarehouseCodeC02 = "/wms/forkCarInfo/listUsersByRoleAndWarehouseCode";
        /// <summary>
        /// C02--叉车司机任务统计--获取上架记录
        /// </summary>
        public const string URL_GetPutawayRecordsC02 = "/wms/forkCarInfo/getPutawayRecords";
        #endregion

        #region 基础管理
        /// <summary>
        /// C02通道管理（添加通道信息）
        /// </summary>
        public static string URL_SaveAddChannel = "/wms/channelManageC02/addChannel";
        /// <summary>
        /// C02通道管理（更新通道信息）
        /// </summary>
        public static string URL_SaveUpdateChannelInfo = "/wms/channelManageC02/updateChannelInfo";
        /// <summary>
        /// C02通道管理（获取所有通道数据）
        /// </summary>
        public static string URL_GetAllChannel = "/wms/channelManageC02/getAllChannel";
        /// <summary>
        /// C02通道管理（获取通道编号和通道名称）
        /// </summary>
        public static string URL_GetAllChannelName = "/wms/channelManageC02/getAllChannelName";
        /// <summary>
        /// C02通道管理（根据通道编号删除通道）
        /// </summary>
        public static string URL_DeleteChannel = "/wms/channelManageC02/deleteChannel";
        /// <summary>
        /// 基础管理（货位信息-添加货位）
        /// </summary>
        public static string URL_SaveAddLocationInfoC02 = "/wms/goodsLocationInfo/addC02LocationInfo";
        /// <summary>
        /// 基础管理（货位信息-编辑货位）
        /// </summary>
        public static string URL_UpdateLocationInfoC02 = "/wms/goodsLocationInfo/updateC02LocationInfo";
        /// <summary>
        /// 查询所有货位
        /// </summary>
        public static string URL_GetAllLocationC02 = "/wms/goodsLocationInfo/getC02AllLocation";
        /// <summary>
        /// C02通道管理（返回通道的编码、名称、备用编码、是否启用）
        /// </summary>
        public static string URL_GetChannel = "/wms/channelManageC02/getChannel";

        #endregion

        #region 系统管理
        /// <summary>
        /// C02任务池管理（查询任务池管理信息）
        /// </summary>
        public const string URL_GetTaskDetailC02 = "/wms/taskPoolController/getC02TaskDetail";
        #endregion


        #endregion

        #region 补充接口
        /// <summary>
        /// 分派装车，查看是否有同一个客户，是否还有其他订单不是当前状态的
        /// </summary>
        public static string URL_IsHaveOtherStatus = "/wms/outCar/getSameBillNo";
        /// <summary>
        /// 打印销售发货单，得到未同步到物流箱的订单，检测
        /// </summary>
        public static string URL_GetSyncCodeCanT = "/wms/printSellBill/getSyncCodeCanT";
        /// <summary>
        /// 查询库存对账
        /// </summary>
        public static string URL_SearchStockAccount = "/wms/stockCheck/getStockCheckInfo";
        /// <summary>
        /// 查询物料流水日志
        /// </summary>
        public static string URL_FindStockFlow = "/wms/wmProLog/findStockFlow";
        /// <summary>
        /// 查询总库存新
        /// </summary>
        public static string URL_StockTotalFlow = "/wms/wmProLog/findTotalFlow";
        /// <summary>
        /// 根据派单id 32位（组别）删除数据
        /// </summary>
        public static string URL_DelleteGp = "/wms/outCar/delleteGp";
        /// <summary>
        /// 根据LoadingNo查询车次信息
        /// </summary>
        public static string URL_GetLoadingNOUnSelected = "/wms/outCar/getLoadingNOUnSelected";
        /// <summary>
        /// 新增分组
        /// </summary>
        public static string URL_RequestPlanBillsInsert = "/wms/pickTask/insert";
        /// <summary>
        /// 库存修正记录表
        /// </summary>
        public static string URL_CheckQtyPutQty = "/wms/billManage/checkQtyPutQty";
        /// <summary>
        /// 库存修正记录表
        /// </summary>
        public static string URL_GetStockReviseRecords = "/wms/stock/getStockReviseRecords";
        /// <summary>
        /// sql:查询散货任务关闭，并且物流箱都验证了
        /// </summary>
        public static string URL_JudgetContainerReversed = "/wms/judgetContainerReversed";
        /// <summary>
        /// 当前订单量--双击，查看明细
        /// </summary>
        public static string URL_GetSKULocation = "/wms/frmShowNeedSKU/getSKULocation";
        /// <summary>
        /// 获取叫号内同
        /// </summary>
        public static string URL_GetCallingData = "/wms/systemController/getCallingData";
        /// <summary>
        /// 重复叫号
        /// </summary>
        public static string URL_ReCall = "/wms/systemController/reCall";
        /// <summary>
        /// 更新叫号状态
        /// </summary>
        public static string URL_UpdateCallState = "/wms/systemController/updateCallState";
        /// <summary>
        /// 生成叫号信息
        /// </summary>
        public static string URL_createCalling = "/wms/systemController/createCalling";
        /// <summary>
        /// 物料信息编辑
        /// </summary>
        public static string URL_GetUmName = "/wms/suppliesInfo/getUmName";
        /// <summary>
        /// 获取容器最大值（批量新增）
        /// </summary>
        public static string URL_GetMaxContainerCode = "/wms/containerInfo/getMaxContainerCode";
        /// <summary>
        /// 拣货计划---全整订单
        /// </summary>
        public static string URL_QueryAllCaseBill = "/wms/pickTaskManager/queryAllCaseBill";
        /// <summary>
        /// 拣货计划-----是否含有
        /// </summary>
        public static string URL_HasThisRight = "/wms/pickTaskManager/hasThisRight";
        /// <summary>
        /// 订单落放明细 
        /// </summary>
        public static string URL_GetFindGoodsDetail = "/wms/outOrder/getFindGoodsDetail";
        /// <summary>
        /// 获取本组物流箱重量
        /// </summary>
        public static string URL_GetWLXsGrossWeight = "/wms/getWLXsGrossWeight";
        /// <summary>
        /// 获取指定类型、非指定状态的容器个数
        /// </summary>
        public static string URL_GetNumOfContainer = "/wms/getNumOfContainer";
        /// <summary>
        /// 更新目标物流箱的状态
        /// </summary>
        public static string URL_UpdateWuliuxiangState = "/wms/updateWuliuxiangState";
        /// <summary>
        /// 获取没有分派的补货任务
        /// </summary>
        public static string URL_GetSupQty = "/wms/frmShowNeedSKU/getSupQty";
        /// <summary>
        /// 印销售发货单--获取订单号
        /// </summary>
        public static string URL_GetBillNOS = "/wms/printSellBill/getBillNOS";
        /// <summary>
        /// 得到容器状态
        /// </summary>
        public static string URL_GetCtCodeCanT = "/wms/printSellBill/getCtCodeCanT";
        /// <summary>
        /// 物流箱信息
        /// </summary>
        public static string URL_GetWuLiuXiangInfo = "/wms/getWuLiuXiangInfo";
        /// <summary>
        /// 物流箱信息
        /// </summary>
        public static string URL_GetWuLiuXiangInfo2 = "/wms/getWuLiuXiangInfo2";
        /// <summary>
        /// 托盘信息
        /// </summary>
        public static string URL_GetTuoPanInfo = "/wms/getTuoPanInfo";
        /// <summary>
        /// 根据订单ID查看关联笼车是否都已接收
        /// </summary>
        public static string URL_IsReceiveContainer = "/wms/isReceiveContainer";
        /// <summary>
        /// 当前订单量	查询安全库存
        /// </summary>
        public static string URL_GetNotSafeStock = "/wms/frmShowNeedSKU/getNotSafeStock";
        /// <summary>
        /// 订单落放明细,查找商品明细
        /// </summary>
        public static string URL_GetFindGoodsDetailVhCode = "/wms/getFindGoodsDetail";
        /// <summary>
        /// 1:整货称重，合并 1：更新托盘状态为87  2：清空托盘 3：写入称重记录
        /// </summary>
        public static string URL_UpdateContainerStateInfoRecord = "/wms/updateContainerStateInfoRecord";
        #endregion

        #region 常量请求路径

        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string URL_ADDRESS = XmlBaseClass.ReadConfigValue("WebServiceAddress", "Value");
        /// <summary>
        /// 服务器端口
        /// </summary>
        public static string URL_PORT = XmlBaseClass.ReadConfigValue("WebServicePort", "Value");
        /// <summary>
        /// 版本号
        /// </summary>
        public static string VARSION = null;
        /// <summary>
        /// 账号
        /// </summary>
        public static string USER_CODE = "";
        /// <summary>
        /// 用户登陆
        /// </summary>
        public const string USER_LOGIN = "/wms/user/userLogin";
        #endregion

        /// <summary>
        /// 服务器url地址IP
        /// </summary>
        private static string url = null;
        public static string urlAddress
        {
            get
            {
                if (string.IsNullOrEmpty(url))
                {
                    url = XmlBaseClass.ReadConfigValue("WebServiceAddress", "Value");
                }
                return url;
            }
        }

        private static string port = null;
        public static string WebPort
        {
            get
            {
                if (string.IsNullOrEmpty(port))
                {
                    port = XmlBaseClass.ReadConfigValue("WebServicePort", "Value");

                }
                return port;
            }
        }

        #region 请求返回值
        public static string RESULT_NULL = "返回的数据是空值，服务器系统异常。问题严重，请联系技术人员！！！";
        public static string JSON_DATA_NULL = "Json数据无效！";
        public static string DATA_NULL = "没有查询到任何数据";

        #endregion

        /// <summary>
        /// 读取本地版本,读取xml没有属性值的方法
        /// </summary>
        private static string _version = null;
        public static string ReadAttributeVersion(string element)
        {
            if (string.IsNullOrEmpty(_version))
            {
                XmlDocument xml = new XmlDocument();
                string AppPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                string configFile = Path.Combine(AppPath, "manifest.xml"); //获取manifest.xml文件的路径
                xml.Load(configFile);
                _version = xml["VersionInfo"][element].InnerText;
            }
            return _version;
        }


        /// <summary>
        /// 超时时间设置单位秒
        /// </summary>
        private static string timeOut = null;
        public static string TimeOut
        {
            get
            {
                if (string.IsNullOrEmpty(timeOut))
                {
                    timeOut = XmlBaseClass.ReadConfigValue("TimeOut", "Value");
                    
                }
                return timeOut;
            }
        }

        

        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="json">post方式发送的数据</param>
        /// <param name="url">请求的地址</param>
        /// <param name="token">jwt方式的token</param>
        /// <returns>请求的结果，数据</returns>
        /*public static string SendRequestJWT(string json, string url, string token)
        {
            string jsons = "";
            try
            {
                WebClient web = new WebClient();
                if (token.Equals(""))
                    web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                else
                {
                    web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    string headBody = "Bearer:";
                    headBody += token;
                    web.Headers.Add("Authorization", headBody);
                }
                byte[] postData = Encoding.UTF8.GetBytes(json);
                byte[] responseData = web.UploadData(url, "POST", postData);

                jsons = Encoding.UTF8.GetString(responseData);
            }
            catch (Exception err)
            {
                //jsons = err.ToString();
                //LogHelper.errorLog("", err);
            }

            return jsons;
        }
        
        public static string SendRequest(string json, string url)
        {
            string jsons = "";
            try
            {
                WebClient web = new WebClient();
                web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                
                byte[] postData = Encoding.UTF8.GetBytes(json);
                byte[] responseData = web.UploadData(url, "POST", postData);

                jsons = Encoding.UTF8.GetString(responseData);
            }
            catch (Exception err)
            {
                //jsons = err.ToString();
                //LogHelper.errorLog("", err);
            }

            return jsons;
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postDataStr">接口参数</param>
        /// <param name="voPath">接口地址名称</param>
        /// <param name="timeOut">请求超时时间，单位毫秒</param>
        /// <returns></returns>
        public static string SendRequest(string postDataStr, string voPath,int timeOut=10000)
        {
            string loData = string.Empty;
            string sendData = DeleteSpecialStr.DeleteStr(postDataStr);
            byte[] postBytes = Encoding.UTF8.GetBytes(sendData);
            string url = "http://" + urlAddress + ":" + WebPort + voPath;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);/*Uri.EscapeUriString(url)*/
            #region
            req.Timeout = Convert.ToInt32(TimeOut) * 1000;// timeOut;
            req.Headers.Add("userCode:" + USER_CODE);
            req.Headers.Add("client:PC_HM");
            req.Headers.Add("version:" + ReadAttributeVersion("VER"));
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";//"application/x-www-form-urlencoded"; ;//"application/json";//
            req.ContentLength = postBytes.Length;
            req.Proxy = null;
            #endregion

            #region
            try
            {
                using (Stream reqStream = req.GetRequestStream())
                {
                    if (postBytes.Length > 0)
                    {
                        reqStream.Write(postBytes, 0, postBytes.Length);
                        reqStream.Close();
                    }
                }
                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader reader = new StreamReader(wr.GetResponseStream());
                    loData = reader.ReadToEnd();
                    if (loData.IndexOf("\"flag\":1,\"status\":\"fail\"") > 0)// if (loData.IndexOf("fail") > 0)
                    {
                        if (string.IsNullOrEmpty(sendData))
                            sendData = "请求接口参数不需要参数";
                        voPath = voPath + "============" + sendData + "============" + loData;
                        ServerResultError bill = JsonConvert.DeserializeObject<ServerResultError>(loData);
                        if(bill != null)
                            RESULT_NULL = "服务器返回:" + bill.fail;
                        LogerHelper.CreateLogTxt(voPath);
                        loData = string.Empty;
                    }
                }
                return loData;
            }
            catch (Exception ex)
            {
                throw ex;
                //return loData;
            }
            #endregion
        }
        
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }

        public static string testwhc(string m,string b,string c,string url)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("WarehouseType", m);
            parameters.Add("UserName", b);
            parameters.Add("pickJson", Uri.EscapeDataString(c));
            HttpWebResponse response = CreatePostHttpResponse(url, parameters, encoding);
            //打印返回值  
            Stream stream = response.GetResponseStream();   //获取响应的字符串流  
            StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
            return sr.ReadToEnd();   //从头读到尾，放到字符串html
        }
    
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding charset)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求  
            string Sendurl = "http://" + urlAddress + ":" + WebPort + url;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(Sendurl) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version11;//Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";// "application/json"; //"multipart/form-data";
            request.UserAgent = DefaultUserAgent;
            //如果需要POST数据     
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = charset.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        public string HttpGet(string Url, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);

                request.Method = "GET";

                request.ContentType = "text/html;charset=UTF-8";



                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();

                myResponseStream.Close();
                return retString;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public static string GetIPPort()
        {
            return "http://" + urlAddress + ":" + WebPort + "/wms/";
        }

        public static string ReadAttribute(string element, string attribute)
        {
            XmlDocument xml = new XmlDocument();
            string AppPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string configFile = Path.Combine(AppPath, "config.xml"); //获取config.xml文件的路径
            xml.Load(configFile);
            return xml["configuration"][element].GetAttribute(attribute);
        }
    }

    /// <summary>
    /// 处理服务器错误
    /// </summary>
    [Serializable]
    public class ServerResultError
    {

        /// <summary>
        ///  标记 0  成功，1 系统错误, 2 错误信息
        /// </summary>
        public int flag
        {
            get;
            set;
        }

        /// <suummary>
        /// 状态
        /// </summary>
        public string status
        {
            get;
            set;
        }


        /// <summary>
        /// 错误内容
        /// </summary>
        public string fail
        {
            get;
            set;
        }

    }
}
