using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class InvoiceSearchRepository : ApplicationDbContext,IInvoiceSearchRepository
    {
        public async Task<DataTableAjaxPostModel> GetAllExchangeItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM VEW_EXCHANGE_HISTORY WHERE 1=1  ";

            var query = "Select " +
                        " ROW_NUMBER () OVER (ORDER BY NEWINVOICENUMBER DESC) RN2," +
                        "EXCHANGEID," +
                        "PREVIOUSINVOICENUMBER,PREVIOUSSALEINFOID,PREVIOUSINVOICEDATE,NEWINVOICENUMBER,NEWSALEINFOID," +
                        "NEWINVOICEDATE,CREATEBY,CREATEDATE,SHOPID,WAREHOUSEID " +
                        "from VEW_EXCHANGE_HISTORY WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query += "and ( (lower(PREVIOUSINVOICENUMBER) like lower(:SearchBy)  or upper(PREVIOUSINVOICENUMBER)like upper(:SearchBy) )" +
                       "or (lower(PREVIOUSINVOICEDATE) like lower(:SearchBy)  or upper(PREVIOUSINVOICEDATE)like upper(:SearchBy) )" +
                       "or (lower(NEWINVOICENUMBER) like lower(:SearchBy)  or upper(NEWINVOICENUMBER)like upper(:SearchBy) )" +
                       "or (lower(NEWINVOICEDATE) like lower(:SearchBy)  or upper(NEWINVOICEDATE)like upper(:SearchBy) ) )";

                sql += "and ( (lower(PREVIOUSINVOICENUMBER) like lower(:SearchBy)  or upper(PREVIOUSINVOICENUMBER)like upper(:SearchBy) )" +
                       "or (lower(PREVIOUSINVOICEDATE) like lower(:SearchBy)  or upper(PREVIOUSINVOICEDATE)like upper(:SearchBy) )" +
                       "or (lower(NEWINVOICENUMBER) like lower(:SearchBy)  or upper(NEWINVOICENUMBER)like upper(:SearchBy) )" +
                       "or (lower(NEWINVOICEDATE) like lower(:SearchBy)  or upper(NEWINVOICEDATE)like upper(:SearchBy) ) )";
            }

            if (model.order != null)
            {
                query += " ORDER BY " + model.columns[model.order[0].column].data + " " + model.order[0].dir.ToUpper();
            }
            query = "SELECT * FROM (" + query + ") WHERE RN2 BETWEEN  '" + model.start + "' AND '" + (model.start + model.length) + "' ";
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SearchBy", OracleDbType.Varchar2, !string.IsNullOrWhiteSpace(model.search.value) ? $"%{model.search.value}%" : null, ParameterDirection.Input)
            };

            var count = await GetSingleIntAsync(sql, param);

            var dt = await GetDataThroughDataTableAsync(query, param);

            var returnValue = (dt.Rows).Cast<DataRow>().Select(HistoryModel.ConvertHistoryModel);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;
        }

        //public async Task<DataTableAjaxPostModel> GetAllSaleInfoForDataTable(DataTableAjaxPostModel model)
        //{
        //    string sql = "SELECT COUNT(*) FROM VEW_SEARCH_SALE_INFO WHERE 1=1  ";

        //    var query = "Select " +
        //                "ROWNUM RN, " +
        //                "SaleInfoId," +
        //                "InvoiceNumber," +
        //                "InvoiceDate," +
        //                "SalesManId," +
        //                "SalesManName," +
        //                "TotalItem," +
        //                "TotalAmount," +
        //                "Vat," +
        //                "DiscountPercent," +
        //                "DiscountAmount," +
        //                "SubTotal," +
        //                "CustomerId," +
        //                "CustomerName," +
        //                "CustomerContactNO," +
        //                "CreatedBy," +
        //                "ShopId," +
        //                "ShopName," +
        //                "WareHouseId," +
        //                "NetAmount," +
        //                "HoldInvoiceYN," +
        //                "ExchangeYN," +
        //                "ExchangeShopId," +
        //                "ExchangeShopName, " +
        //                "PAYMENTTYPE, " +
        //                "INVOICESTYLENAME "+
        //                "from VEW_SEARCH_SALE_INFO WHERE 1=1 ";

        //    if (!string.IsNullOrWhiteSpace(model.search.value))
        //    {
        //        query += "and ( (lower(InvoiceNumber) like lower(:SearchBy)  or upper(InvoiceNumber)like upper(:SearchBy) )" +
        //               "or (lower(InvoiceDate) like lower(:SearchBy)  or upper(InvoiceDate)like upper(:SearchBy) )" +
        //               "or (lower(SalesManName) like lower(:SearchBy)  or upper(SalesManName)like upper(:SearchBy) )" +
        //               "or (lower(TotalItem) like lower(:SearchBy)  or upper(TotalItem)like upper(:SearchBy) )" +
        //               "or (lower(TotalAmount) like lower(:SearchBy)  or upper(TotalAmount)like upper(:SearchBy) )" +
        //               "or (lower(VAT) like lower(:SearchBy)  or upper(VAT)like upper(:SearchBy) )" +
        //               "or (lower(DiscountPercent) like lower(:SearchBy)  or upper(DiscountPercent)like upper(:SearchBy) )" +
        //               "or (lower(DiscountAmount) like lower(:SearchBy)  or upper(DiscountAmount)like upper(:SearchBy) )" +
        //               "or (lower(CustomerName) like lower(:SearchBy)  or upper(CustomerName)like upper(:SearchBy) )" +
        //               "or (lower(CustomerContactNO) like lower(:SearchBy)  or upper(CustomerContactNO)like upper(:SearchBy) )" +
        //               "or (lower(ShopName) like lower(:SearchBy)  or upper(ShopName)like upper(:SearchBy) )" +
        //               "or (lower(NetAmount) like lower(:SearchBy)  or upper(NetAmount)like upper(:SearchBy) )" +
        //               "or (lower(ExchangeYN) like lower(:SearchBy)  or upper(ExchangeYN)like upper(:SearchBy) )" +
        //               "or (lower(INVOICESTYLENAME) like lower(:SearchBy)  or upper(INVOICESTYLENAME)like upper(:SearchBy) )" +
        //               "or (lower(ExchangeShopName) like lower(:SearchBy)  or upper(ExchangeShopName)like upper(:SearchBy) ) )";

        //        sql += "and ( (lower(InvoiceNumber) like lower(:SearchBy)  or upper(InvoiceNumber)like upper(:SearchBy) )" +
        //               "or (lower(InvoiceDate) like lower(:SearchBy)  or upper(InvoiceDate)like upper(:SearchBy) )" +
        //               "or (lower(SalesManName) like lower(:SearchBy)  or upper(SalesManName)like upper(:SearchBy) )" +
        //               "or (lower(TotalItem) like lower(:SearchBy)  or upper(TotalItem)like upper(:SearchBy) )" +
        //               "or (lower(TotalAmount) like lower(:SearchBy)  or upper(TotalAmount)like upper(:SearchBy) )" +
        //               "or (lower(VAT) like lower(:SearchBy)  or upper(VAT)like upper(:SearchBy) )" +
        //               "or (lower(DiscountPercent) like lower(:SearchBy)  or upper(DiscountPercent)like upper(:SearchBy) )" +
        //               "or (lower(DiscountAmount) like lower(:SearchBy)  or upper(DiscountAmount)like upper(:SearchBy) )" +
        //               "or (lower(CustomerName) like lower(:SearchBy)  or upper(CustomerName)like upper(:SearchBy) )" +
        //               "or (lower(CustomerContactNO) like lower(:SearchBy)  or upper(CustomerContactNO)like upper(:SearchBy) )" +
        //               "or (lower(ShopName) like lower(:SearchBy)  or upper(ShopName)like upper(:SearchBy) )" +
        //               "or (lower(NetAmount) like lower(:SearchBy)  or upper(NetAmount)like upper(:SearchBy) )" +
        //               "or (lower(ExchangeYN) like lower(:SearchBy)  or upper(ExchangeYN)like upper(:SearchBy) )" +
        //               "or (lower(INVOICESTYLENAME) like lower(:SearchBy)  or upper(INVOICESTYLENAME)like upper(:SearchBy) )" +
        //               "or (lower(ExchangeShopName) like lower(:SearchBy)  or upper(ExchangeShopName)like upper(:SearchBy) ) ) ";
        //    }

        //    if (model.order != null)
        //    {
        //        query += "ORDER BY " + model.columns[model.order[0].column].data + " " + model.order[0].dir.ToUpper();
        //    }

        //    query = "SELECT * FROM (" + query + ") WHERE RN BETWEEN  '" + model.start + "' AND '" + (model.start + model.length) + "' ORDER BY InvoiceNumber DESC  ";

        //    List<OracleParameter> param = new List<OracleParameter>
        //    {
        //        new OracleParameter(":SearchBy", OracleDbType.Varchar2, !string.IsNullOrWhiteSpace(model.search.value) ? $"%{model.search.value}%" : null, ParameterDirection.Input)
        //    };


        //    var count = await GetSingleIntAsync(sql, param);

        //    var dt = await GetDataThroughDataTableAsync(query, param);

        //    var returnValue = (dt.Rows).Cast<DataRow>().Select(SaleInfoModel.ConvertSaleInfoModelForDataTable);
        //    model.ListOfData = returnValue.ToList();
        //    model.recordsFiltered = count;
        //    model.recordsTotal = count;
        //    return model;
        //}

        public async Task<DataTableAjaxPostModel> GetAllSaleInfoForDataTable(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM SEARCH_INVOICE_INFO WHERE 1=1  ";

            var query = "Select " +
                        "RN," +
                        " ROW_NUMBER () OVER (ORDER BY INVOICENUMBER DESC) RN2," +
                        "SALEINFOID," +
                        "INVOICENUMBER," +
                        "INVOICEDATE," +
                        "SALESMANID," +
                        "SALESMANNAME," +
                        "TOTALITEM ," +
                        "TOTALAMOUNT," +
                        "VAT," +
                        "DISCOUNTPERCENT," +
                        "DISCOUNTAMOUNT," +
                        "SUBTOTAL," +
                        "CUSTOMERID ," +
                        "CUSTOMERNAME," +
                        "CUSTOMERCONTACTNO," +
                        "CREATEDBY," +
                        "SHOPID," +
                        "SHOPNAME," +
                        "NETAMOUNT ," +
                        "HOLDINVOICEYN ," +
                        "EXCHANGEYN ," +
                        "EXCHANGESHOPID ," +
                        "EXCHANGESHOPNAME ," +
                        "PAYMENTTYPE ," +
                        "INVOICESTYLENAME ," +
                        "WAREHOUSEID," +
                        "TOTALROWCOUNT " +
                        "from SEARCH_INVOICE_INFO WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query += "and ( (lower(INVOICENUMBER) like lower(:SearchBy)  or upper(INVOICENUMBER)like upper(:SearchBy) )" +
                       "or (lower(INVOICEDATE) like lower(:SearchBy)  or upper(INVOICEDATE)like upper(:SearchBy) )" +
                       "or (lower(SALESMANNAME) like lower(:SearchBy)  or upper(SALESMANNAME)like upper(:SearchBy) )" +
                       "or (lower(TOTALITEM) like lower(:SearchBy)  or upper(TOTALITEM)like upper(:SearchBy) )" +
                       "or (lower(TOTALAMOUNT) like lower(:SearchBy)  or upper(TOTALAMOUNT)like upper(:SearchBy) )" +
                       "or (lower(VAT) like lower(:SearchBy)  or upper(VAT)like upper(:SearchBy) )" +
                       "or (lower(DISCOUNTPERCENT) like lower(:SearchBy)  or upper(DISCOUNTPERCENT)like upper(:SearchBy) )" +
                       "or (lower(DISCOUNTAMOUNT) like lower(:SearchBy)  or upper(DISCOUNTAMOUNT)like upper(:SearchBy) )" +
                       "or (lower(CUSTOMERNAME) like lower(:SearchBy)  or upper(CUSTOMERNAME)like upper(:SearchBy) )" +
                       "or (lower(CUSTOMERCONTACTNO) like lower(:SearchBy)  or upper(CUSTOMERCONTACTNO)like upper(:SearchBy) )" +
                       "or (lower(SHOPNAME) like lower(:SearchBy)  or upper(SHOPNAME)like upper(:SearchBy) )" +
                       "or (lower(NETAMOUNT) like lower(:SearchBy)  or upper(NETAMOUNT)like upper(:SearchBy) )" +
                       "or (lower(EXCHANGEYN) like lower(:SearchBy)  or upper(EXCHANGEYN)like upper(:SearchBy) )" +
                       "or (lower(INVOICESTYLENAME) like lower(:SearchBy)  or upper(INVOICESTYLENAME)like upper(:SearchBy) )" +
                       "or (lower(EXCHANGESHOPNAME) like lower(:SearchBy)  or upper(EXCHANGESHOPNAME)like upper(:SearchBy) ) )";

                sql += "and ( (lower(INVOICENUMBER) like lower(:SearchBy)  or upper(INVOICENUMBER)like upper(:SearchBy) )" +
                       "or (lower(INVOICEDATE) like lower(:SearchBy)  or upper(INVOICEDATE)like upper(:SearchBy) )" +
                       "or (lower(SALESMANNAME) like lower(:SearchBy)  or upper(SALESMANNAME)like upper(:SearchBy) )" +
                       "or (lower(TOTALITEM) like lower(:SearchBy)  or upper(TOTALITEM)like upper(:SearchBy) )" +
                       "or (lower(TOTALAMOUNT) like lower(:SearchBy)  or upper(TOTALAMOUNT)like upper(:SearchBy) )" +
                       "or (lower(VAT) like lower(:SearchBy)  or upper(VAT)like upper(:SearchBy) )" +
                       "or (lower(DISCOUNTPERCENT) like lower(:SearchBy)  or upper(DISCOUNTPERCENT)like upper(:SearchBy) )" +
                       "or (lower(DISCOUNTAMOUNT) like lower(:SearchBy)  or upper(DISCOUNTAMOUNT)like upper(:SearchBy) )" +
                       "or (lower(CUSTOMERNAME) like lower(:SearchBy)  or upper(CUSTOMERNAME)like upper(:SearchBy) )" +
                       "or (lower(CUSTOMERCONTACTNO) like lower(:SearchBy)  or upper(CUSTOMERCONTACTNO)like upper(:SearchBy) )" +
                       "or (lower(SHOPNAME) like lower(:SearchBy)  or upper(SHOPNAME)like upper(:SearchBy) )" +
                       "or (lower(NETAMOUNT) like lower(:SearchBy)  or upper(NETAMOUNT)like upper(:SearchBy) )" +
                       "or (lower(EXCHANGEYN) like lower(:SearchBy)  or upper(EXCHANGEYN)like upper(:SearchBy) )" +
                       "or (lower(INVOICESTYLENAME) like lower(:SearchBy)  or upper(INVOICESTYLENAME)like upper(:SearchBy) )" +
                       "or (lower(EXCHANGESHOPNAME) like lower(:SearchBy)  or upper(EXCHANGESHOPNAME)like upper(:SearchBy) ) )";
            }
          
            //query += " AND  RN BETWEEN  " + model.start + " AND " + (model.start + model.length) + " ";
            if (model.order != null)
            {
                query += " ORDER BY " + model.columns[model.order[0].column].data + " " + model.order[0].dir.ToUpper();
            }
            query = "SELECT * FROM (" + query + ") WHERE RN2 BETWEEN  '" + model.start + "' AND '" + (model.start + model.length) + "' ";
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SearchBy", OracleDbType.Varchar2, !string.IsNullOrWhiteSpace(model.search.value) ? $"%{model.search.value}%" : null, ParameterDirection.Input)
            };
            
            var count = await GetSingleIntAsync(sql, param);

            var dt = await GetDataThroughDataTableAsync(query, param);

            var returnValue = (dt.Rows).Cast<DataRow>().Select(SaleInfoModel.ConvertSaleInfoModelForDataTable);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;
        }

        public async Task<string> SaveAllSaleInfoForDataTable()
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, "", ParameterDirection.Input),
                new OracleParameter("p_message", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("pro_INVOICE_SEARCH");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}