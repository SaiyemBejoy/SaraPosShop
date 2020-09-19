using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.AllModel.Report;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class ReportRepository : ApplicationDbContext, IReportRepository
    {

        #region "Oracle Connection Check"
        private OracleConnection GetConnection()
        {

            string connection = ConnectionString;
            OracleConnection con = new OracleConnection(connection);
            return con;
        }
        #endregion

        public async Task<DataSet> InvoicePrintAsync(string invoice)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_NUMBER", OracleDbType.Varchar2, invoice, ParameterDirection.Input)
            };

            var query = "SELECT * from VEW_RPT_INVOICE_PRINT where INVOICE_NUMBER = :INVOICE_NUMBER ";

            var ds = await GetDataThroughDataSetAsync(query, param);
            return ds;
        }

        public async Task<DataSet> InvoicePrint(string invoice)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_NUMBER", OracleDbType.Varchar2, invoice, ParameterDirection.Input)
            };

            var query = "SELECT * from VEW_RPT_INVOICE_PRINT where INVOICE_NUMBER = :INVOICE_NUMBER ";

            var ds = await GetDataThroughDataSetAsync(query, param);
            return ds;
        }

        public async Task<DataSet> SaleSummary(SaleReportModel objSaleReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_FROM_DATE", OracleDbType.Varchar2,objSaleReportModel.FromDate , ParameterDirection.Input),
                new OracleParameter(":INVOICE_TO_DATE", OracleDbType.Varchar2, objSaleReportModel.ToDate, ParameterDirection.Input)
            };

            var query = " SELECT " +
                        " ' Sale Summary  between ' || to_date( '" + objSaleReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objSaleReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                        "HEAD_OFFICE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "INVOICE_NUMBER," +
                        "INVOICE_DATE," +
                        "CASHIER_ID," +
                        "CASHIER_NAME," +
                        "TOTAL_ITEM," +
                        "TOTAL_AMOUNT," +
                        "VAT," +
                        "DISCOUNT_P," +
                        "DISCOUNT_A," +
                        "SUB_TOTAL," +
                        "SHOP_ID," +
                        "WAREHOUSE_ID," +
                        "NET_AMOUNT, " +
                        "BAG_PRICE, "+
                        "PAYMENT_TYPE, " +
                        "CASHAMOUNT, " +
                        "BKASHAMOUNT, " +
                        "BRACKBANKAMOUNT, " +
                        "ROCKETAMOUNT, " +
                        "DBBLAMOUNT, " +
                        "SCBAMOUNT, " +
                        "SIBLAMOUNT, " +
                        "CITYAMOUNT, " +
                        "EBLAMOUNT, " +
                        "GIFTVOUCHER, " +
                         "SHOPARU," +
                         "BAGDOOM," +
                         "PRIYOSHOP," +
                         "KABLEWALA," +
                         "DARAZ," +
                         "OTHOBA," +
                         "DELIGRAM," +
                         "BESHIDESHI," +
                         "AJKERDEAL," +
                         "AADI," +
                         "EORANGE," +
                         "CLOTHO," +
                         "EVALY," +
                         "DBBLGATEWAY," +
                         "CITYGATEWAY," +
                         "REDX," +
                        "TOTAL_PAYMENT_TYPE_AMOUNT, " +
                        "PAYMENT_AMOUNT_ALL  " +


            //"(select nvl(sum(CASHAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )CASHAMOUNT, " +
            //"(select nvl(sum(BKASHAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )BKASHAMOUNT, " +
            //"(select nvl(sum(BRACKBANKAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )BRACKBANKAMOUNT, " +
            //"(select nvl(sum(ROCKETAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )ROCKETAMOUNT, " +
            //"(select nvl(sum(DBBLAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )DBBLAMOUNT, " +
            //"(select nvl(sum(SCBAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )SCBAMOUNT, " +
            //"(select nvl(sum(SIBLAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )SIBLAMOUNT, " +
            //"(select nvl(sum(CITYAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )CITYAMOUNT, " +
            //"(select nvl(sum(EBLAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )EBLAMOUNT, " +
            //"(select nvl(sum(GIFTVOUCHER),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )GIFTVOUCHER, " +
            //"total_payment_type_amount, " +
            //"(select nvl(sum(total_payment_type_amount),0) from VEW_PAYMENT_AMOUNT_TOTAL where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )payment_amount_all " +

            "from VEW_RPT_SALE_SUMMARY where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') ";

            ds =  await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_SALE_SUMMARY");
            return ds;
        }

        public async Task<DataSet> TimeSaleSummary(SaleReportModel objSaleReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_FROM_DATE", OracleDbType.Varchar2,objSaleReportModel.FromDateAndTime , ParameterDirection.Input),
                new OracleParameter(":INVOICE_TO_DATE", OracleDbType.Varchar2, objSaleReportModel.ToDateAndTime, ParameterDirection.Input)
            };

            var query = " SELECT " +
                        " 'Time Wise Sale Summary'  RPT_TITLE, " +
                        "HEAD_OFFICE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "INVOICE_NUMBER," +
                        "INVOICE_DATE," +
                        "CASHIER_ID," +
                        "CASHIER_NAME," +
                        "TOTAL_ITEM," +
                        "TOTAL_AMOUNT," +
                        "VAT," +
                        "DISCOUNT_P," +
                        "DISCOUNT_A," +
                        "SUB_TOTAL," +
                        "SHOP_ID," +
                        "WAREHOUSE_ID," +
                        "NET_AMOUNT, " +
                        "BAG_PRICE, " +
                        "PAYMENT_TYPE, " +
                        "CASHAMOUNT, " +
                        "BKASHAMOUNT, " +
                        "BRACKBANKAMOUNT, " +
                        "ROCKETAMOUNT, " +
                        "DBBLAMOUNT, " +
                        "SCBAMOUNT, " +
                        "SIBLAMOUNT, " +
                        "CITYAMOUNT, " +
                        "EBLAMOUNT, " +
                        "GIFTVOUCHER, " +
                         "SHOPARU," +
                         "BAGDOOM," +
                         "PRIYOSHOP," +
                         "KABLEWALA," +
                         "DARAZ," +
                         "OTHOBA," +
                         "DELIGRAM," +
                         "BESHIDESHI," +
                         "AJKERDEAL," +
                         "AADI," +
                         "EORANGE," +
                         "CLOTHO," +
                         "EVALY," +
                         "DBBLGATEWAY," +
                         "CITYGATEWAY," +
                         "REDX," +
                        "TOTAL_PAYMENT_TYPE_AMOUNT, " +
                        "PAYMENT_AMOUNT_ALL  " +
                        "from VEW_RPT_SALE_SUMMARY where INVOICE_DATE  BETWEEN  to_date(:INVOICE_FROM_DATE, 'MM/DD/YYYY HH:MI:SS AM') AND  to_date(:INVOICE_TO_DATE , 'MM/DD/YYYY HH:MI:SS AM') ";

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_SALE_SUMMARY");
            return ds;
        }

        public async Task<DataSet> SaleDetails(SaleReportModel objSaleReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>();


            var query = "SELECT " +
                          " '  Sale Details Report between ' || to_date( '" + objSaleReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objSaleReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                          "HEAD_OFFICE," +
                          "SHOP_NAME," +
                            "SHOP_ADDRESS," +
                            "INVOICE_NUMBER," +
                            "INVOICE_DATE," +
                            "BARCODE," +
                            "STYLE_NAME," +
                            "PRODUCT_CATEGORY," +
                            "PRODUCT_SUB_CATEGORY," +
                            "UNIT_PRICE," +
                            "QUANTITY," +
                            "SUB_TOTAL," +
                            "DISCOUNT_PERCENT," +
                            "DISCOUNT_AMOUNT," +
                            "NET_AMOUNT," +
                            "VAT_AMOUNT " +
                          " From VEW_RPT_INV_WISE_SALE_ITEM where TRUNC (INVOICE_DATE) BETWEEN  to_date('" + objSaleReportModel.FromDate.Trim() + "', 'dd/mm/yy') AND  to_date('" + objSaleReportModel.ToDate.Trim() + "' , 'dd/mm/yy')  ";

            if (!string.IsNullOrEmpty(objSaleReportModel.CategoryName))
            {
                query = query + "and PRODUCT_CATEGORY = '" + objSaleReportModel.CategoryName + "' ";
            }
            if (!string.IsNullOrEmpty(objSaleReportModel.SubCategoryName))
            {
                query = query + "and PRODUCT_SUB_CATEGORY = '" + objSaleReportModel.SubCategoryName + "' ";
            }

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_INV_WISE_SALE_ITEM");

            return ds;

            //try
            //{
            //    DataSet ds = null;
            //    DataTable dt = new DataTable();
            //    try
            //    {
            //        var sql = "SELECT " +
            //                  " '  Sale Details Report between ' || to_date( '" + objSaleReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objSaleReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
            //                  "SHOP_NAME," +
            //                    "SHOP_ADDRESS," +
            //                    "INVOICE_NUMBER," +
            //                    "INVOICE_DATE," +
            //                    "SALESMAN_ID," +
            //                    "SALE_ITEM_ID," +
            //                    "SALE_INFO_ID," +
            //                    "BARCODE," +
            //                    "STYLE_NAME," +
            //                    "PRICE," +
            //                    "QUANTITY," +
            //                    "VAT," +
            //                    "TOTAL," +
            //                    "DISCOUNT_P," +
            //                    "DISCOUNT_A," +
            //                    "SUB_TOTAL," +
            //                    "NET_AMOUNT," +
            //                    "TOTAL_VAT," +
            //                    "ITEM_ID," +
            //                    "PRODUCT_ID," +
            //                    "PRODUCT_CATEGORY," +
            //                    "PRODUCT_SUB_CATEGORY " +
            //                  " From VEW_RPT_SALE_DETAIL where TRUNC (INVOICE_DATE) BETWEEN  to_date('" + objSaleReportModel.FromDate.Trim() + "', 'dd/mm/yy') AND  to_date('" + objSaleReportModel.ToDate.Trim() + "' , 'dd/mm/yy')  ";

            //        if (!string.IsNullOrEmpty(objSaleReportModel.CategoryName))
            //        {
            //            sql = sql + "and PRODUCT_CATEGORY = '" + objSaleReportModel.CategoryName + "' ";
            //        }
            //        if (!string.IsNullOrEmpty(objSaleReportModel.SubCategoryName))
            //        {
            //            sql = sql + "and PRODUCT_SUB_CATEGORY = '" + objSaleReportModel.SubCategoryName + "' ";
            //        }
            //        OracleCommand objOracleCommand = new OracleCommand(sql);
            //        using (OracleConnection strConn = GetConnection())
            //        {
            //            try
            //            {
            //                objOracleCommand.Connection = strConn;
            //                strConn.Open();
            //                var objDataAdapter = new OracleDataAdapter(objOracleCommand);
            //                dt.Clear();
            //                ds = new System.Data.DataSet();
            //                objDataAdapter.Fill(ds, "VEW_RPT_SALE_DETAIL");
            //                objDataAdapter.Dispose();
            //                objOracleCommand.Dispose();
            //            }
            //            catch (Exception ex)
            //            {
            //                throw new Exception("Error : " + ex.Message);
            //            }

            //            finally
            //            {
            //                strConn.Close();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //    return ds;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

        public async Task<DataSet> CurrentStockDetails(StockReportModel objStockReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>();

            var query = " SELECT " +
                        " 'Current Stock' RPT_TITLE, " +
                        " HEAD_OFFICE, " +
                        " SHOP_NAME, " +
                        " SHOP_ADDRESS, " +
                        " ITEM_ID, " +
                        " PRODUCT_ID, " +
                        " STYLE_NAME, " +
                        " ITEM_NAME, " +
                        " BARCODE, " +
                        " RECEIVE_QUANTITY, " +
                        " SALE_QUANTITY, " +
                        " DAMAGE_QUANTITY, " +
                        " SHOP_TO_SHOP_RECEIVE, " +
                        " SHOP_TO_SHOP_DELIVERY, " +
                        " TRANSFER_QUANTITY, " +
                        " QUANTITY, " +
                        "TRANSIT_QUANTITY," +
                        " SALE_PRICE, " +
                        " UMO, " +
                        " VAT, " +
                        " PRODUCT_CATEGORY, " +
                        " PRODUCT_SUB_CATEGORY  " +
                        "from VEW_RPT_STOCK_DETAILS WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(objStockReportModel.CategoryName))
            {
                query += "AND  PRODUCT_CATEGORY = '" + objStockReportModel.CategoryName + "' ";

            }
            if (!string.IsNullOrWhiteSpace(objStockReportModel.SubCategoryName))
            {
                query += "AND  PRODUCT_SUB_CATEGORY = '" + objStockReportModel.SubCategoryName + "' ";

            }
            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STOCK_DETAILS");
            return ds;
        }

        public async Task<DataSet> GenerateSaleSummaryByCategoryAndSubCtgy(SaleReportModel objSaleReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_FROM_DATE", OracleDbType.Varchar2,objSaleReportModel.FromDate , ParameterDirection.Input),
                new OracleParameter(":INVOICE_TO_DATE", OracleDbType.Varchar2, objSaleReportModel.ToDate, ParameterDirection.Input)

            };

            var query = " SELECT " +
                        " ' Sale Summary By Category And SubCategory  between ' || to_date( '" + objSaleReportModel.FromDate + "', 'dd/mm/yy') || ' to '|| to_date( '" + objSaleReportModel.ToDate + "', 'dd/mm/yy')  RPT_TITLE, " +
                        "HEAD_OFFICE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "PRODUCT_CATEGORY," +
                        "PRODUCT_SUB_CATEGORY," +
                        "INVOICE_DATE," +
                        "QUANTITY," +
                        "SUB_TOTAL," +
                        "DISCOUNT_AMOUNT," +
                        "NET_AMOUNT," +
                        "VAT_AMOUNT " +
                        "from VEW_RPT_CTGY_WISE_SALE_ITEM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yy') ";

            if (!string.IsNullOrEmpty(objSaleReportModel.CategoryName))
            {
                query = query + "and PRODUCT_CATEGORY = '" + objSaleReportModel.CategoryName + "' ";
            }
            if (!string.IsNullOrEmpty(objSaleReportModel.SubCategoryName))
            {
                query = query + "and PRODUCT_SUB_CATEGORY = '" + objSaleReportModel.SubCategoryName + "' ";
            }

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_CTGY_WISE_SALE_ITEM");
            return ds;
        }

        public async Task<DataSet> GetallShopReceiveItem(string storeReceiveChallanNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":STORE_RECEIVE_CHALLAN_NO", OracleDbType.Varchar2, storeReceiveChallanNo, ParameterDirection.Input)
            };

            var query = "SELECT " +
                        "'Shop Product Receive Report 'RPT_TITLE," +
                        "SHOP_ID," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "STORE_RECEIVE_ITEM_ID," +
                        "STORE_RECEIVE_ID," +
                        "STORE_RECEIVE_CHALLAN_NO," +
                        "ITEM_ID," +
                        "ITEM_NAME," +
                        "PRODUCT_ID," +
                        "PRODUCT_STYLE_NAME, " +
                        "BARCODE," +
                        "RECEIVE_QUANTITY," +
                        "SALE_PRICE," +
                        "CATEGORY_NAME," +
                        "SUB_CATEGORY_NAME," +
                        "BRAND_NAME," +
                        "UMO," +
                        "VAT," +
                        "RECEIVE_FROM," +
                        "RECEIVE_FROM_NAME," +
                        "RECEIVE_DATE," +
                        "SEASON_NAME " +
                        "from VEW_RPT_STORE_RECEIVE_DETAILS where STORE_RECEIVE_CHALLAN_NO = :STORE_RECEIVE_CHALLAN_NO ";

            var ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STORE_RECEIVE_DETAILS");
            return ds;
        }

        public async Task<DataSet> GetallRequisitionItem(string requisitionNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":REQUISITION_NO", OracleDbType.Varchar2, requisitionNo, ParameterDirection.Input)
            };

            var query = "SELECT " +
                        "' Product Requisition Report 'RPT_TITLE," +
                        "SHOP_ID," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "REQUISITION_MAIN_ITEM_ID," +
                        "REQUISITION_ID," +
                        "REQUISITION_NO," +
                        "ITEM_NAME," +
                        "BARCODE," +
                        "PRICE," +
                        "REQUISITION_DATE," +
                        "CREATED_BY " +
                        "from VEW_RPT_REQUISITION_DC_DETAILS where REQUISITION_NO = :REQUISITION_NO ";

            var ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_REQUISITION_DC_DETAILS");
            return ds;
        }

        public async Task<DataSet> GetallShopTransferItem(string transferChallanNo)
        {

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":STOCK_TRANSFER_CHALLAN_NUM", OracleDbType.Varchar2, transferChallanNo, ParameterDirection.Input)
            };

            var query = "SELECT " +
                        "'Shop Product Transfer Report 'RPT_TITLE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "STOCK_TRANSFER_ITEM_ID," +
                        "STOCK_TRANSFER_ID," +
                        "STOCK_TRANSFER_CHALLAN_NUM," +
                        "ITEM_ID," +
                        "STYLE_NAME, "+
                        "ITEM_NAME," +
                        "PRODUCT_ID," +
                        "BARCODE," +
                        "TRANSFER_QUANTITY," +
                        "SALE_PRICE," +
                        "VAT," +
                        "REQUISITION_NUM," +
                        "TRANSFER_SHOPID_TO," +
                        "TRANSFER_SHOPID_TO_NAME," +
                        "TRANSFER_SHOPID_FROM," +
                        "TRANSFERED_BY," +
                        "EMPLOYEE_NAME," +
                        "TRANSFER_DATE " +
                        "from VEW_RPT_STORE_TRANSFER_DETAILS where STOCK_TRANSFER_CHALLAN_NUM = :STOCK_TRANSFER_CHALLAN_NUM ";

            var ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STORE_TRANSFER_DETAILS");
            return ds;
        }

        public async Task<DataSet> SalesManSaleSummary(SalesManReportModel objSalesManReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_FROM_DATE", OracleDbType.Varchar2,objSalesManReportModel.FromDate , ParameterDirection.Input),
                new OracleParameter(":INVOICE_TO_DATE", OracleDbType.Varchar2, objSalesManReportModel.ToDate, ParameterDirection.Input),
            };

            var query = " SELECT " +
                        " ' Sales Man Wise Sale Report Summary  between ' || to_date( '" + objSalesManReportModel.FromDate + "', 'dd/mm/yy') || ' to '|| to_date( '" + objSalesManReportModel.ToDate + "', 'dd/mm/yy')  RPT_TITLE, " +
                        "HEAD_OFFICE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "SALESMAN_NAME," +
                        "SUM(QUANTITY) QUANTITY," +
                        "SUM(TOTAL) TOTAL," +
                        "SUM(DISCOUNT_AMOUNT) DISCOUNT_AMOUNT," +
                        "SUM(NET_AMOUNT) NET_AMOUNT," +
                        "SUM(VAT_AMOUNT) VAT_AMOUNT " +
                        "from VEW_RPT_SALES_MAN_WISE_SALE where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') ";

            if (!string.IsNullOrWhiteSpace(objSalesManReportModel.SalesMan))
            {
                query += "AND  SALESMAN_ID = '" + objSalesManReportModel.SalesMan + "' ";

            }
            query += " GROUP BY RPT_TITLE," +
                     "HEAD_OFFICE, " +
                     "SHOP_NAME," +
                     "SHOP_ADDRESS," +
                     "SALESMAN_NAME ";
            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_SALES_MAN_WISE_SALE");
            return ds;
        }

        public async Task<DataSet> CashierSaleSummary(SaleReportModel objSaleReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_FROM_DATE", OracleDbType.Varchar2,objSaleReportModel.FromDate , ParameterDirection.Input),
                new OracleParameter(":INVOICE_TO_DATE", OracleDbType.Varchar2, objSaleReportModel.ToDate, ParameterDirection.Input)
            };

            var query = " SELECT " +
                        " ' Cashier Sale Summary  between ' || to_date( '" + objSaleReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objSaleReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                        " INVOICE_DATE," +
                        " SUB_TOTAL," +
                        " CREATED_BY," +
                        " EMPLOYEE_NAME," +
                        " SHOP_ID," +
                        " HEAD_OFFICE," +
                        " SHOP_NAME," +
                        " SHOP_ADDRESS, " +
                        "CASHAMOUNT," +
                        "BKASHAMOUNT," +
                        "BRACKBANKAMOUNT," +
                        "ROCKETAMOUNT," +
                        "DBBLAMOUNT," +
                        "SCBAMOUNT," +
                        "SIBLAMOUNT," +
                        "CITYAMOUNT," +
                        "EBLAMOUNT," +
                        "GIFTVOUCHER," +
                        "TOTAL_PAYMENT_TYPE_AMOUNT," +
                        "PAYMENT_AMOUNT_ALL " +
            //"(select nvl(sum(CASHAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )CASHAMOUNT, " +
            //"(select nvl(sum(BKASHAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )BKASHAMOUNT, " +
            //"(select nvl(sum(BRACKBANKAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )BRACKBANKAMOUNT, " +
            //"(select nvl(sum(ROCKETAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )ROCKETAMOUNT, " +
            //"(select nvl(sum(DBBLAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )DBBLAMOUNT, " +
            //"(select nvl(sum(SCBAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )SCBAMOUNT, " +
            //"(select nvl(sum(SIBLAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )SIBLAMOUNT, " +
            //"(select nvl(sum(CITYAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )CITYAMOUNT, " +
            //"(select nvl(sum(EBLAMOUNT),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )EBLAMOUNT, " +
            //"(select nvl(sum(GIFTVOUCHER),0) from VEW_PAYMENT_TYPE_SUM where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )GIFTVOUCHER, " +
            //"total_payment_type_amount, " +
            //"(select nvl(sum(total_payment_type_amount),0) from VEW_PAYMENT_AMOUNT_TOTAL where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') )payment_amount_all " +

                        "from VEW_RPT_CASHIER_SALE_SUMMARY where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') ";

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_CASHIER_SALE_SUMMARY");
            return ds;
        }

        public async Task<DataSet> GenerateCashierSaleDetails(SaleReportModel objSaleReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_FROM_DATE", OracleDbType.Varchar2,objSaleReportModel.FromDate , ParameterDirection.Input),
                new OracleParameter(":INVOICE_TO_DATE", OracleDbType.Varchar2, objSaleReportModel.ToDate, ParameterDirection.Input)
            };

            var query = " SELECT " +
                        " ' Cashier Sale Details  between ' || to_date( '" + objSaleReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objSaleReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                        "Head_Office," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "EMPLOYEE_NAME," +
                        "CREATED_BY," +
                        "INVOICE_DATE," +
                        "GRAND_TOTAL," +
                        "CASH_AMOUNT," +
                        "BKASH_AMOUNT," +
                        "BRACK_AMOUNT," +
                        "ROCKET_AMOUNT," +
                        "DBBL_AMOUNT," +
                        "SCB_AMOUNT," +
                        "SIBL_AMOUNT," +
                        "CITY_AMOUNT," +
                        "EBL_AMOUNT," +
                        "GIFTVOUCHER " +
                        "from VEW_RPT_INDIV_CHASHIER_SALE where TRUNC (INVOICE_DATE) BETWEEN  to_date(:INVOICE_FROM_DATE, 'dd/mm/yyyy') AND  to_date(:INVOICE_TO_DATE , 'dd/mm/yyyy') ";

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_INDIV_CHASHIER_SALE");
            return ds;
        }

        public async Task<string> SaleSummarySave(SaleReportModel objSaleReportModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_FROM_DATE", OracleDbType.Varchar2, objSaleReportModel.FromDate, ParameterDirection.Input),
                new OracleParameter(":P_TO_DATE", OracleDbType.Varchar2, objSaleReportModel.ToDate, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("pro_rpt_sale_summary");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<DataSet> GetallDamageProductForRpt(string challanNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":DAMAGE_CHALLAN_NO", OracleDbType.Varchar2, challanNo, ParameterDirection.Input)
            };
            var query = "SELECT " +
                        "'Damage Product Report 'RPT_TITLE," +
                        "SHOP_ID," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "DAMAGE_CHALLAN_NO," +
                        "CREATED_DATE," +
                        "CREATED_BY," +
                        "CREATED_BY_NAME," +
                        "TRANSFER_YN," +
                        "TRANSFER_DATE," +
                        "RECEIVED_YN," +
                        "BARCODE," +
                        "ITEM_NAME," +
                        "PRODUCT_STYLE_NAME,"+
                        "PRICE," +
                        "QUANTITY," +
                        "REMARKS," +
                        "ITEM_ID," +
                        "PRODUCT_ID, " +
                        "TRANSFER_TO_NAME "+
                        "from VEW_RPT_DAMAGE_PRODUCT where DAMAGE_CHALLAN_NO = :DAMAGE_CHALLAN_NO ";

            var ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_DAMAGE_PRODUCT");
            return ds;
        }

        public async Task<DataSet> GetallShopTransferItemForGvtFormate(string transferChallanNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":STOCK_TRANSFER_CHALLAN_NUM", OracleDbType.Varchar2, transferChallanNo, ParameterDirection.Input)
            };

            var query = "SELECT " +
                        "'Shop Product Transfer Report 'RPT_TITLE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "Central_BIN, " +
                        "STOCK_TRANSFER_ITEM_ID," +
                        "STOCK_TRANSFER_ID," +
                        "STOCK_TRANSFER_CHALLAN_NUM," +
                        "ITEM_ID," +
                        "ITEM_NAME," +
                        "PRODUCT_ID," +
                        "BARCODE," +
                        "TRANSFER_QUANTITY," +
                        "ACTUAL_PRICE," +
                        "SALE_PRICE," +
                        "VAT," +
                        "REQUISITION_NUM," +
                        "TRANSFER_SHOPID_TO," +
                        "TRANSFER_SHOPID_TO_NAME," +
                        "TRANSFER_SHOPID_FROM," +
                        "TRANSFERED_BY," +
                        "TRANSFER_DATE " +
                        "from VEW_RPT_STORE_TRANSFER_GOVTFOR where STOCK_TRANSFER_CHALLAN_NUM = :STOCK_TRANSFER_CHALLAN_NUM ";

            var ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STORE_TRANSFER_GOVTFOR");
            return ds;
        }

        public async Task<DataSet> StyleWiseCurrentStockDetails(StockReportModel objStockReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>();

            var query = " SELECT " +
                        " 'Style Wise Current Stock' RPT_TITLE, " +
                        " HEAD_OFFICE, " +
                        " SHOP_NAME, " +
                        " SHOP_ADDRESS, " +
                        " ITEM_ID, " +
                        " PRODUCT_ID, " +
                        " STYLE_NAME, " +
                        " ITEM_NAME, " +
                        " BARCODE, " +
                        " RECEIVE_QUANTITY, " +
                        " SALE_QUANTITY, " +
                        " DAMAGE_QUANTITY, " +
                        " SHOP_TO_SHOP_RECEIVE, " +
                        " SHOP_TO_SHOP_DELIVERY, " +
                        " TRANSFER_QUANTITY, " +
                        " QUANTITY, " +
                        " SALE_PRICE, " +
                        " UMO, " +
                        " VAT, " +
                        " PRODUCT_CATEGORY, " +
                        " PRODUCT_SUB_CATEGORY  " +
                        "from VEW_RPT_STOCK_DETAILS WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(objStockReportModel.ProductStyle))
            {
                query += "AND  PRODUCT_ID = '" + objStockReportModel.ProductStyle + "' ";

            }
            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STOCK_DETAILS");
            return ds;
        }

        public async Task<DataSet> LowStockDetails(StockReportModel objStockReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>();

            var query = " SELECT " +
                        " 'Current Low Stock' RPT_TITLE, " +
                        " HEAD_OFFICE, " +
                        " SHOP_NAME, " +
                        " SHOP_ADDRESS, " +
                        " ITEM_ID, " +
                        " PRODUCT_ID, " +
                        " STYLE_NAME, " +
                        " ITEM_NAME, " +
                        " BARCODE, " +
                        " RECEIVE_QUANTITY, " +
                        " SALE_QUANTITY, " +
                        " DAMAGE_QUANTITY, " +
                        " SHOP_TO_SHOP_RECEIVE, " +
                        " SHOP_TO_SHOP_DELIVERY, " +
                        " TRANSFER_QUANTITY, " +
                        " QUANTITY, " +
                        " SALE_PRICE, " +
                        " UMO, " +
                        " VAT, " +
                        " PRODUCT_CATEGORY, " +
                        " PRODUCT_SUB_CATEGORY  " +
                        "from VEW_RPT_STOCK_DETAILS WHERE QUANTITY <= 2 ";

            if (!string.IsNullOrWhiteSpace(objStockReportModel.CategoryName))
            {
                query += "AND  PRODUCT_CATEGORY = '" + objStockReportModel.CategoryName + "' ";

            }
            if (!string.IsNullOrWhiteSpace(objStockReportModel.SubCategoryName))
            {
                query += "AND  PRODUCT_SUB_CATEGORY = '" + objStockReportModel.SubCategoryName + "' ";

            }

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STOCK_DETAILS");
            return ds;
        }

        public async Task<DataSet> StyleWiseStockSummary(StockReportModel objStockReportModel)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>();

            var query = " SELECT " +
                        " 'Style Wise Stock Summary' RPT_TITLE, " +
                        " HEAD_OFFICE, " +
                        " SHOP_NAME, " +
                        " SHOP_ADDRESS, " +
                        " PRODUCT_ID, " +
                        " STYLE_NAME, " +
                        " RECEIVE_QUANTITY, " +
                        " SALE_QUANTITY, " +
                        " DAMAGE_QUANTITY, " +
                        " SHOP_TO_SHOP_RECEIVE, " +
                        " SHOP_TO_SHOP_DELIVERY, " +
                        " TRANSFER_QUANTITY, " +
                        " QUANTITY, " +
                        " PRODUCT_CATEGORY, " +
                        " PRODUCT_SUB_CATEGORY  " +
                        "from VEW_RPT_STOCK_SUMMARY WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(objStockReportModel.CategoryName))
            {
                query += "AND  PRODUCT_CATEGORY = '" + objStockReportModel.CategoryName + "' ";

            }
            if (!string.IsNullOrWhiteSpace(objStockReportModel.SubCategoryName))
            {
                query += "AND  PRODUCT_SUB_CATEGORY = '" + objStockReportModel.SubCategoryName + "' ";

            }

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STOCK_SUMMARY");
            return ds;
        }

        public async Task<DataSet> GetallShopTransitItem(string challanNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":TRANSIT_CHALLAN_NUM", OracleDbType.Varchar2, challanNo, ParameterDirection.Input)
            };

            var query = "SELECT " +
                        "'Transit Product Challan Details 'RPT_TITLE," +
                        "RPT_TITLE," +
                        "TRANSIT_CHALLAN_NUM," +
                        "MARKET_PLACE_ID," +
                        "MARKETPLACE_NAME," +
                        "BRAND_NAME," +
                        "CENTRAL_BIN," +
                        "SHOP_ID," +
                        "FROM_SHOP_NAME," +
                        "FROM_SHOP_ADDRESS," +
                        "CREATED_BY," +
                        "CREATED_BY_NAME," +
                        "CREATED_DATE," +
                        "UPDATED_BY," +
                        "UPDATED_BY_NAME," +
                        "UPDATED_DATE," +
                        "TRANSIT_PRODUCT_ITEM_ID," +
                        "PRODUCT_ID," +
                        "ITEM_ID," +
                        "BARCODE," +
                        "ITEM_NAME," +
                        "STYLE_NAME," +
                        "SALE_PRICE," +
                        "ACTUAL_PRICE," +
                        "GROWING_PERCENT," +
                        "GROWING_PRICE," +
                        "QUANTITY," +
                        "DOLLAR_RATE," +
                        "US_DOLLAR," +
                        "TOTAL_GROWING_PRICE," +
                        "TOTAL_US_DOLLAR " +
                        "from RPT_TRANSIT_PROD_DETAILS where TRANSIT_CHALLAN_NUM = :TRANSIT_CHALLAN_NUM ";

            var ds = await GetDataThroughDataSetRptAsync(query, param, "RPT_TRANSIT_PROD_DETAILS");
            return ds;
        }

        public async Task<DataSet> StyleWiseProductHistory(StockReportModel objStockReportMod)
        {
            DataSet ds = null;
            List<OracleParameter> param = new List<OracleParameter>();

            var query = " SELECT " +
                        " 'Style Wise History' RPT_TITLE, " +
                        "HEAD_OFFICE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "STORE_RECEIVE_ID," +
                        "STORE_RECEIVE_CHALLAN_NO," +
                        "ITEM_ID," +
                        "ITEM_NAME," +
                        "PRODUCT_ID," +
                        "PRODUCT_STYLE_NAME," +
                        "BARCODE," +
                        "RECEIVE_QUANTITY," +
                        "SALE_PRICE," +
                        "CATEGORY_NAME," +
                        "SUB_CATEGORY_NAME," +
                        "RECEIVE_DATE," +
                        "RECEIVE_FROM," +
                        "RECEIVE_FROM_NAME," +
                        "FIRST_SALE_DATE," +
                        "LAST_SALE_DATE," +
                        "LAST_SALE_INVOICE_NUMBER," +
                        "FIRST_SALE_INVOICE_NUMBER " +
                        "from VEW_RPT_STYLE_HISTORY WHERE PRODUCT_ID = '" + objStockReportMod.ProductStyle + "'  ";

            ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_STYLE_HISTORY");
            return ds;
        }

        public async Task<DataSet> GetallShopRequisitionItem(string shopRequisitionNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":REQUISITION_NUMNER", OracleDbType.Varchar2, shopRequisitionNo, ParameterDirection.Input)
            };

            var query = "SELECT " +
                        "'Shop To Shop Requisition Details'RPT_TITLE," +
                        "SHOP_NAME," +
                        "SHOP_ADDRESS," +
                        "REQUISITION_ITEM_ID," +
                        "ITEM_ID," +
                        "PRODUCT_ID," +
                        "BARCODE," +
                        "ITEM_NAME," +
                        "STYLE_NAME," +
                        "PRICE," +
                        "QUANTITY," +
                        "REQUISITION_ID," +
                        "REQUISITION_NUMNER," +
                        "REQUISITION_SHOPID_TO," +
                        "REQUISITION_SHOPID_FROM," +
                        "FROM_SHOP_NAME," +
                        "REQUISITION_DATE," +
                        "CREATED_BY," +
                        "CREATED_BY_NAME," +
                        "DELIVERY_YN," +
                        "TRANSFER_CHALLAN_NUMBER," +
                        "EMPLOYEE_NAME " +
                        "from VEW_RPT_SHOP_SHOP_REQUISITION where REQUISITION_NUMNER = :REQUISITION_NUMNER ";

            var ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_SHOP_SHOP_REQUISITION");
            return ds;
        }

        public async Task<DataSet> GetallExchangeProductDetails(string newInvoiceNumber)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_NUMBER", OracleDbType.Varchar2, newInvoiceNumber, ParameterDirection.Input)
            };

            var query = "SELECT * from VEW_RPT_EXCHANGE_PRODUCT where INVOICE_NUMBER = :INVOICE_NUMBER ";
           
            var ds = await GetDataThroughDataSetRptAsync(query, param, "VEW_RPT_EXCHANGE_PRODUCT");
            return ds;
        }
    }
}
