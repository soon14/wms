using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nodes.Dapper;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    public class ImportFromExcelDal
    {
        public void ImportLocation( DataTable tHeader, string userName)
        {
            string LocationCode, LocationDesc,ZoneCode = string.Empty;

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            DynamicParameters parmsHeader = new DynamicParameters();
            parmsHeader.Add("LOCATION_CODE");
            parmsHeader.Add("LOCATION_DESC");
            parmsHeader.Add("ZONE_CODE");
            parmsHeader.AddOut("RET", DbType.Int32);

            try
            {
                foreach (DataRow row in tHeader.Rows)
                {
                    LocationCode = ConvertUtil.ToString(row["货位编号"]);
                    LocationDesc = ConvertUtil.ToString(row["货位描述"]);
                    ZoneCode = ConvertUtil.ToString(row["所属货区"]);
                    if (string.IsNullOrEmpty(LocationCode))
                        throw new Exception("货位编号不能为空。");

                    if (string.IsNullOrEmpty(ZoneCode))
                        throw new Exception("所属货区不能为空。");

                    parmsHeader.Set("LOCATION_CODE", LocationCode);
                    parmsHeader.Set("LOCATION_DESC", LocationDesc);
                    parmsHeader.Set("ZONE_CODE", ZoneCode);

                    map.Execute("P_LOCATION_IMPORT", parmsHeader, trans, CommandType.StoredProcedure);
                    int billID = parmsHeader.Get<int>("RET");
                    if (billID == -1)
                    {
                        row["导入结果"] = "略过";
                    }
                    else
                    {
                        row["导入结果"] = "成功";
                    }
                }

                trans.Commit();
            }
            catch(Exception ex)
            {
                trans.Rollback();
                throw ex;
            }

        }
        /// <summary>
        /// 导入Excel数据，生成ASN
        /// </summary>
        /// <param name="tOriginal"></param>
        /// <param name="tHeader"></param>
        /// <param name="asnType"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public void ImportASN(DataTable tOriginal, DataTable tHeader, string userName)
        {
            //写原始表、主表和明细
            string billNO = string.Empty, supplier, supplierName, remark, contractNO,
                salesMan, material, materialName, batchNO, dueDate, warehouse,
                docType, docNO, Fonumber, unit, baseLine;
            DateTime createDate;
            decimal qty = 0, quan_qty, unqual_qty, price;
            int rowNO = 0;
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();

            DynamicParameters parmsHeader = new DynamicParameters();
            parmsHeader.Add("BILL_NO");
            parmsHeader.Add("WAREHOUSE");
            parmsHeader.Add("DOC_NO");
            parmsHeader.Add("DOC_TYPE");
            parmsHeader.Add("SALES_MAN");
            parmsHeader.Add("SUPPLIER");
            parmsHeader.Add("SUPPLIER_NAME");
            parmsHeader.Add("CONTRACT_NO"); //明细行有个合同编号，表头有个合同号
            parmsHeader.Add("BASE_LINE"); //基础凭证行号
            parmsHeader.Add("CREATE_DATE");
            parmsHeader.Add("REMARK");
            parmsHeader.Add("USER_NAME", userName);
            parmsHeader.AddOut("BILL_ID", DbType.Int32);

            DynamicParameters parmsDetail = new DynamicParameters();
            parmsDetail.Add("BILL_ID");
            parmsDetail.Add("BILL_NO");
            parmsDetail.Add("ROW_NO");
            parmsDetail.Add("MATERIAL");
            parmsDetail.Add("MATERIAL_NAME");
            parmsDetail.Add("UNIT");
            parmsDetail.Add("SUPPLIER");
            parmsDetail.Add("BATCH_NO");
            parmsDetail.Add("DUE_DATE");
            parmsDetail.Add("QTY");
            parmsDetail.Add("QUAL_QTY");
            parmsDetail.Add("UNQUAL_QTY");
            parmsDetail.Add("PRICE");
            parmsDetail.Add("CONTRACT_NO"); //明细行有个合同编号，表头有个合同号
            parmsDetail.Add("FO_NUMBER"); //外商单据编号
            parmsDetail.AddOut("RET_VAL", DbType.Int32);

            try
            {
                foreach (DataRow row in tHeader.Rows)
                {
                    billNO = ConvertUtil.ToString(row["DocEntry"]);
                    warehouse = ConvertUtil.ToString(row["Warehouse"]);
                    supplier = ConvertUtil.ToString(row["CardCode"]);
                    supplierName = ConvertUtil.ToString(row["CardName"]);
                    contractNO = ConvertUtil.ToString(row["NumAtCard"]);
                    remark = ConvertUtil.ToString(row["Comments"]);
                    salesMan = ConvertUtil.ToString(row["RprtName"]);
                    createDate = ConvertUtil.ToDatetime(row["DocDate"]);
                    docType = ConvertUtil.ToString(row["Object"]);
                    docNO = ConvertUtil.ToString(row["BaseEntry"]);
                    baseLine = ConvertUtil.ToString(row["BaseLine"]);

                    //单据编号、实体库、供应商、名称、过账日期、基础凭证类型、基础凭证单号均不能为空
                    if (string.IsNullOrEmpty(billNO))
                        throw new Exception("单据编号不能为空。");

                    if (string.IsNullOrEmpty(warehouse))
                        throw new Exception("实体库不能为空。");

                    if (string.IsNullOrEmpty(supplier))
                        throw new Exception("供应商不能为空。");

                    if (string.IsNullOrEmpty(supplierName))
                        throw new Exception("名称不能为空。");

                    if (string.IsNullOrEmpty(docType))
                        throw new Exception("基础凭证类型不能为空。");

                    if (string.IsNullOrEmpty(docNO))
                        throw new Exception("基础凭证单号不能为空。");

                    parmsHeader.Set("BILL_NO", billNO);
                    parmsHeader.Set("WAREHOUSE", warehouse);
                    parmsHeader.Set("DOC_NO", docNO);
                    parmsHeader.Set("DOC_TYPE", docType);
                    parmsHeader.Set("SALES_MAN", salesMan);
                    parmsHeader.Set("SUPPLIER", supplier);
                    parmsHeader.Set("SUPPLIER_NAME", supplierName);
                    parmsHeader.Set("CONTRACT_NO", contractNO);
                    parmsHeader.Set("BASE_LINE", baseLine);
                    parmsHeader.Set("CREATE_DATE", createDate);
                    parmsHeader.Set("REMARK", remark);

                    map.Execute("P_ASN_IMPORT_HEADER", parmsHeader, trans, CommandType.StoredProcedure);
                    int billID = parmsHeader.Get<int>("BILL_ID");
                    if (billID == -1)
                    {
                        row["导入结果"] = "略过";
                        continue;
                    }
                    else if (billID == -2)
                    {
                        throw new Exception("基础凭证类型在WMS中未找到匹配，导入取消。");
                    }
                    else if (billID == -3)
                    {
                        throw new Exception("实体库在WMS中未找到匹配，导入取消。");
                    }

                    DataRow[] rows = tOriginal.Select(string.Format("DocEntry = '{0}'", billNO));
                    foreach (DataRow rowDetail in rows)
                    {
                        rowNO = ConvertUtil.ToInt(rowDetail["LineNum"]);
                        material = ConvertUtil.ToString(rowDetail["ItemValue"]);
                        materialName = ConvertUtil.ToString(rowDetail["Dscription"]);
                        batchNO = ConvertUtil.ToString(rowDetail["SupplierNum"]);
                        dueDate = ConvertUtil.ToString(rowDetail["EffectDate"]);
                        qty = ConvertUtil.ToDecimal(rowDetail["Quantity"]);
                        quan_qty = 0;
                        unqual_qty = 0;
                        contractNO = ConvertUtil.ToString(rowDetail["NumAtCardLine"]);
                        Fonumber = ConvertUtil.ToString(rowDetail["Fonumber"]);
                        unit = "";

                        if (string.IsNullOrEmpty(material))
                            throw new Exception(string.Format("单据编号为{0}的行物料号为空。", billNO));

                        if (qty <= 0)
                            throw new Exception(string.Format("单据编号为{0}的行数量小于等于0。", billNO));

                        //有效日期需要去除.
                        if (!string.IsNullOrEmpty(dueDate))
                            dueDate = dueDate.Replace(".", "");

                        parmsDetail.Set("BILL_ID", billID);
                        parmsDetail.Set("BILL_NO", billNO);
                        parmsDetail.Set("ROW_NO", rowNO);
                        parmsDetail.Set("MATERIAL", material);
                        parmsDetail.Set("MATERIAL_NAME", materialName);
                        parmsDetail.Set("UNIT", unit);
                        parmsDetail.Set("SUPPLIER", supplier);
                        parmsDetail.Set("BATCH_NO", batchNO);
                        parmsDetail.Set("DUE_DATE", dueDate);
                        parmsDetail.Set("QTY", qty);
                        parmsDetail.Set("QUAL_QTY", quan_qty);
                        parmsDetail.Set("UNQUAL_QTY", unqual_qty);
                        parmsDetail.Set("PRICE", null); //excel文件中没有给价格，先用0填充
                        parmsDetail.Set("CONTRACT_NO", contractNO);
                        parmsDetail.Set("FO_NUMBER", Fonumber);

                        map.Execute("P_ASN_IMPORT_DETAIL", parmsDetail, trans, CommandType.StoredProcedure);
                        int result = parmsDetail.Get<int>("RET_VAL");
                        if (result == -1)
                        {
                            trans.Rollback();
                            throw new Exception(string.Format("未找到单据的表头信息，无法导入明细，单据编号为“{0}”，导入取消。", billNO));
                        }
                    }

                    row["导入结果"] = "成功";
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 导入销售单据，
        /// </summary>
        /// <param name="tOriginal"></param>
        /// <param name="tHeader"></param>
        /// <param name="asnType"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public void ImportSO(DataTable tOriginal, DataTable tHeader, string userName)
        {
            //写原始表、主表和明细
            string billNO = string.Empty, supplier, supplierName, remark, contractNO,
                salesMan, material, materialName, batchNO, dueDate, warehouse, brand,
                billType, baseEntry, baseLine, baseType, QCNumber, ShCompany, ShTel,
                Consignee, ShAddress, shipNO, creator, cardBZ, dlryRequire, foNumber;
            DateTime createDate;
            decimal qty = 0, price = 0;
            int rowNO = 0;
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();

            //"DocEntry1", "Object", "SlpCode1", "CardCode1", "CardName1", "QCNumber",  
            //"shcompany", "consignee", "shtel", "shaddress", "WhsCode1", "Warehouse", "DocDate1", "BaseType1", "BaseEntry1", "BaseLine1"};

            DynamicParameters parmsHeader = new DynamicParameters();
            parmsHeader.Add("BILL_NO");
            parmsHeader.Add("WAREHOUSE");
            parmsHeader.Add("DOC_NO"); //BaseEntry1
            parmsHeader.Add("DOC_TYPE"); //Object
            parmsHeader.Add("SALES_MAN"); //SlpCode1
            parmsHeader.Add("CUSTOMER"); //CardCode1
            parmsHeader.Add("CUSTOMER_NAME"); //CardName1
            parmsHeader.Add("CONTRACT_NO"); //NumAtCard1
            parmsHeader.Add("BASE_TYPE"); //基础凭证类型BaseType1
            parmsHeader.Add("BASE_ENTRY"); //基础凭证单号BaseEntry1
            parmsHeader.Add("BASE_LINE"); //基础凭证行号BaseLine1
            parmsHeader.Add("SH_ADDRESS"); //收货人地址shaddress
            parmsHeader.Add("SH_TEL"); //收货电话shtel
            parmsHeader.Add("CONSIGNEE"); //收货人consignee
            parmsHeader.Add("SH_COMPANY"); //收货单位shcompany
            parmsHeader.Add("CREATE_DATE");//凭证日期（建单日期）DocDate1
            parmsHeader.Add("QC_NUMBER"); //QCNumber
            parmsHeader.Add("REMARK"); //Comments
            parmsHeader.Add("SHIP_NO"); //DHLnumber
            parmsHeader.Add("CREATOR"); //USER
            parmsHeader.Add("CARDBZ"); //CARDBZ，客户备注
            parmsHeader.Add("DELIVERY_REQUIRE"); //Deliveryrequire，发货要求
            parmsHeader.Add("USER_NAME", userName);
            parmsHeader.AddOut("BILL_ID", DbType.Int32);

            //"DocEntry1",    "LineNum1", "ItemCode1", "Dscription1",   "Brand", "Quantity1", "PriceAfVAT", "DistNumber1", "ExpDate1"

            DynamicParameters parmsDetail = new DynamicParameters();
            parmsDetail.Add("BILL_ID");
            parmsDetail.Add("BILL_NO");
            parmsDetail.Add("ROW_NO"); //LineNum1
            parmsDetail.Add("MATERIAL");//ItemCode1
            parmsDetail.Add("MATERIAL_NAME");//Dscription1
            parmsDetail.Add("BRAND");//Brand
            parmsDetail.Add("BATCH_NO");//DistNumber1，需要转换
            parmsDetail.Add("DUE_DATE");//ExpDate1，需要去除.
            parmsDetail.Add("QTY");//Quantity1
            parmsDetail.Add("PRICE");//PriceAfVAT
            parmsDetail.Add("DOC_TYPE");//这是表头的单据类型，也要传递到明细的存储过程中，用来确定明细是入或是出
            parmsDetail.Add("CONTRACT_NO");//SaleContNo
            parmsDetail.Add("FO_NUM");//Fonumber
            parmsDetail.AddOut("RET_VAL", DbType.Int32);

            try
            {
                foreach (DataRow row in tHeader.Rows)
                {
                    billNO = ConvertUtil.ToString(row["DocEntry1"]);
                    billType = ConvertUtil.ToString(row["Object"]);
                    warehouse = ConvertUtil.ToString(row["Warehouse"]);
                    supplier = ConvertUtil.ToString(row["CardCode1"]);
                    supplierName = ConvertUtil.ToString(row["CardName1"]);
                    contractNO = ConvertUtil.ToString(row["NumAtCard1"]);
                    QCNumber = ConvertUtil.ToString(row["QCNumber"]);
                    salesMan = ConvertUtil.ToString(row["SlpCode1"]);
                    createDate = ConvertUtil.ToDatetime(row["DocDate1"]);
                    baseEntry = ConvertUtil.ToString(row["BaseEntry1"]);
                    baseLine = ConvertUtil.ToString(row["BaseLine1"]);
                    baseType = ConvertUtil.ToString(row["BaseType1"]);
                    ShCompany = ConvertUtil.ToString(row["shcompany"]);
                    Consignee = ConvertUtil.ToString(row["consignee"]);
                    ShTel = ConvertUtil.ToString(row["shtel"]);
                    ShAddress = ConvertUtil.ToString(row["shaddress"]);

                    remark = ConvertUtil.ToString(row["Comments1"]);
                    shipNO = ConvertUtil.ToString(row["DHLnumber"]);
                    creator = ConvertUtil.ToString(row["USER"]);
                    cardBZ = ConvertUtil.ToString(row["CARDBZ"]);
                    dlryRequire = ConvertUtil.ToString(row["Deliveryrequire"]);

                    //单据编号、实体库、供应商、名称、过账日期、基础凭证类型、基础凭证单号均不能为空
                    if (string.IsNullOrEmpty(billNO))
                        throw new Exception("单据编号不能为空。");

                    if (string.IsNullOrEmpty(warehouse))
                        throw new Exception("实体库不能为空。");

                    if (string.IsNullOrEmpty(supplier))
                        throw new Exception("客户/供应商不能为空。");

                    if (string.IsNullOrEmpty(supplierName))
                        throw new Exception("名称不能为空。");

                    if (string.IsNullOrEmpty(billType))
                        throw new Exception("基础凭证类型不能为空。");

                    if (string.IsNullOrEmpty(baseEntry))
                        throw new Exception("基础凭证单号不能为空。");

                    if (string.IsNullOrEmpty(baseLine))
                        baseLine = "0";

                    parmsHeader.Set("BILL_NO", billNO);
                    parmsHeader.Set("WAREHOUSE", warehouse);
                    parmsHeader.Set("DOC_NO", baseEntry);
                    parmsHeader.Set("DOC_TYPE", billType);
                    parmsHeader.Set("SALES_MAN", salesMan);
                    parmsHeader.Set("CUSTOMER", supplier);
                    parmsHeader.Set("CUSTOMER_NAME", supplierName);
                    parmsHeader.Set("CONTRACT_NO", contractNO);
                    parmsHeader.Set("BASE_LINE", baseLine);
                    parmsHeader.Set("BASE_TYPE", baseType);
                    parmsHeader.Set("BASE_ENTRY", baseEntry);
                    parmsHeader.Set("SH_COMPANY", ShCompany);
                    parmsHeader.Set("SH_ADDRESS", ShAddress);
                    parmsHeader.Set("SH_TEL", ShTel);
                    parmsHeader.Set("CONSIGNEE", Consignee);
                    parmsHeader.Set("CREATE_DATE", createDate);
                    parmsHeader.Set("QC_NUMBER", QCNumber);

                    parmsHeader.Set("REMARK", remark);
                    parmsHeader.Set("SHIP_NO", shipNO);
                    parmsHeader.Set("CREATOR", creator);
                    parmsHeader.Set("CARDBZ", cardBZ);
                    parmsHeader.Set("DELIVERY_REQUIRE", dlryRequire);

                    map.Execute("P_EXCEL_IMPORT_BILL", parmsHeader, trans, CommandType.StoredProcedure);
                    int billID = parmsHeader.Get<int>("BILL_ID");
                    if (billID == -1)
                    {
                        row["导入结果"] = "略过";
                        continue;
                    }
                    else if (billID == -2)
                    {
                        throw new Exception("基础凭证类型在WMS中未找到匹配，导入取消。");
                    }
                    else if (billID == -3)
                    {
                        throw new Exception("实体库在WMS中未找到匹配，导入取消。");
                    }

                    DataRow[] rows = tOriginal.Select(string.Format("DocEntry1 = '{0}'", billNO));
                    foreach (DataRow rowDetail in rows)
                    {
                        //行号需要转换，如果为空，转换为0
                        string _rowNO = ConvertUtil.ToString(rowDetail["LineNum1"]);
                        if (string.IsNullOrEmpty(_rowNO))
                            rowNO = 0;
                        else
                            rowNO = ConvertUtil.ToInt(_rowNO);

                        material = ConvertUtil.ToString(rowDetail["ItemCode1"]);
                        materialName = ConvertUtil.ToString(rowDetail["Dscription1"]);
                        brand = ConvertUtil.ToString(rowDetail["Brand"]);
                        batchNO = ConvertUtil.ToString(rowDetail["DistNumber1"]);
                        dueDate = ConvertUtil.ToString(rowDetail["ExpDate1"]);
                        string _price = ConvertUtil.ToString(rowDetail["PriceAfVAT"]);
                        if (string.IsNullOrEmpty(_price))
                            price = 0;
                        else
                            price = ConvertUtil.ToDecimal(_price.Replace("\"", ""));

                        qty = ConvertUtil.ToDecimal(rowDetail["Quantity1"]);
                        contractNO = ConvertUtil.ToString(rowDetail["SaleContNo"]);
                        foNumber = ConvertUtil.ToString(rowDetail["Fonumber"]);

                        if (string.IsNullOrEmpty(material))
                            throw new Exception(string.Format("单据编号为{0}的行物料号为空。", billNO));

                        if (qty <= 0)
                            throw new Exception(string.Format("单据编号为{0}的行数量小于等于0。", billNO));

                        //批次需要转换为批号，批次有三种形式：LOTNO[]2014ABC0405[]赵峰，3254361[]2014BD-05，空，转为批号应该为：空，3254361，空
                        if (!string.IsNullOrEmpty(batchNO))
                        {
                            if (batchNO.StartsWith("LOTNO"))
                                batchNO = "";
                            else if (batchNO.IndexOf('[') > 0)
                                batchNO = batchNO.Substring(0, batchNO.IndexOf('['));
                            else if (batchNO.IndexOf('-') > 0)
                                batchNO = batchNO.Substring(0, batchNO.IndexOf('-'));
                            else
                                batchNO = "";
                        }

                        //有效日期需要去除.
                        if (!string.IsNullOrEmpty(dueDate))
                            dueDate = dueDate.Replace(".", "");

                        parmsDetail.Set("BILL_ID", billID);
                        parmsDetail.Set("BILL_NO", billNO);
                        parmsDetail.Set("ROW_NO", rowNO);
                        parmsDetail.Set("DOC_TYPE", billType);
                        parmsDetail.Set("MATERIAL", material);
                        parmsDetail.Set("MATERIAL_NAME", materialName);
                        parmsDetail.Set("BRAND", brand);
                        parmsDetail.Set("BATCH_NO", batchNO);
                        parmsDetail.Set("DUE_DATE", dueDate);
                        parmsDetail.Set("QTY", qty);
                        parmsDetail.Set("CONTRACT_NO", contractNO);
                        parmsDetail.Set("FO_NUM", foNumber);
                        parmsDetail.Set("PRICE", price.ToString()); //excel文件中没有给价格，先用0填充

                        map.Execute("P_EXCEL_IMPORT_DETAIL", parmsDetail, trans, CommandType.StoredProcedure);
                        int result = parmsDetail.Get<int>("RET_VAL");
                        if (result == -1)
                        {
                            trans.Rollback();
                            throw new Exception(string.Format("未找到单据的表头信息，无法导入明细，单据编号为“{0}”，导入取消。", billNO));
                        }
                    }

                    row["导入结果"] = "成功";
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }
    }
}
