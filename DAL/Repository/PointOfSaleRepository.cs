using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class PointOfSaleRepository : ApplicationDbContext, IPointOfSaleRepository
    {
        public async Task<ItemInfoModel> GetAllInfoByBarcode(string barcode)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":BARCODE", OracleDbType.Varchar2, barcode.ToUpper(), ParameterDirection.Input)
            };

            var query = "Select * from VEW_ITEM_INFO WHERE BARCODE = :BARCODE ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ItemInfoModel.ConvertItemInfoModel(dt.Rows[0]);
        }

        public async Task<CustomerInfoModel> GetAllInfoByCustomerCode(string customerCode)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":CUSTOMER_CODE", OracleDbType.Varchar2, customerCode, ParameterDirection.Input)
            };

            var query = " Select * from VEW_CUSTOMER_INFO WHERE CUSTOMER_CODE = :CUSTOMER_CODE ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return CustomerInfoModel.ConvertCustomerInfoModel(dt.Rows[0]);
        }

        public async Task<string> SaveDataCustomer(CustomerSaleModel objCustomerSaleModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_CUSTOMER_ID", OracleDbType.Varchar2, objCustomerSaleModel.CustomerId, ParameterDirection.Input),
                 new OracleParameter(":P_CUSTOMER_CODE", OracleDbType.Varchar2, objCustomerSaleModel.CustomerCode, ParameterDirection.Input),
                new OracleParameter(":P_CUSTOMER_NAME", OracleDbType.Varchar2, objCustomerSaleModel.CustomerName, ParameterDirection.Input),
                new OracleParameter(":P_CONTACT_NO", OracleDbType.Varchar2, objCustomerSaleModel.ContactNo, ParameterDirection.Input),
                new OracleParameter(":P_EMAIL", OracleDbType.Varchar2, objCustomerSaleModel.Email, ParameterDirection.Input),
                new OracleParameter(":P_ADDRESS", OracleDbType.Varchar2, objCustomerSaleModel.Address, ParameterDirection.Input),
                new OracleParameter(":P_CUSTOMER_MEDIUM", OracleDbType.Varchar2, objCustomerSaleModel.CustomerMedium, ParameterDirection.Input),
                new OracleParameter(":P_DISCOUNT", OracleDbType.Varchar2, objCustomerSaleModel.Discount, ParameterDirection.Input),
                new OracleParameter(":P_CUSTOMER_INFO_ID", OracleDbType.Varchar2, objCustomerSaleModel.CustomerInfoId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_CUSTOMER_SALE_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveSaleInfo(SaleInfoModel objSaleInfoModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, objSaleInfoModel.SaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, objSaleInfoModel.InvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_SALESMAN_ID", OracleDbType.Varchar2, objSaleInfoModel.SalesManId, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL_ITEM", OracleDbType.Varchar2, objSaleInfoModel.TotalItem, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.TotalAmount, ParameterDirection.Input),
                new OracleParameter(":P_VAT", OracleDbType.Varchar2, objSaleInfoModel.Vat, ParameterDirection.Input),
                new OracleParameter(":P_DISCOUNT_P", OracleDbType.Varchar2, objSaleInfoModel.DiscountPercent, ParameterDirection.Input),
                new OracleParameter(":P_DISCOUNT_A", OracleDbType.Varchar2, objSaleInfoModel.DiscountAmount, ParameterDirection.Input),
                new OracleParameter(":P_BAG_PRICE", OracleDbType.Varchar2, objSaleInfoModel.BagPrice, ParameterDirection.Input),
                new OracleParameter(":P_PAYMENT_TYPE", OracleDbType.Varchar2, objSaleInfoModel.PaymentType, ParameterDirection.Input),
                new OracleParameter(":P_CASH_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.CashAmount, ParameterDirection.Input),
                new OracleParameter(":P_CARD_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.CardAmount, ParameterDirection.Input),
                new OracleParameter(":P_SUB_TOTAL", OracleDbType.Varchar2, objSaleInfoModel.SubTotal, ParameterDirection.Input),
                new OracleParameter(":P_NET_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.NetAmount, ParameterDirection.Input),
                new OracleParameter(":P_CUSTOMER_ID", OracleDbType.Varchar2, objSaleInfoModel.CustomerId, ParameterDirection.Input),
                new OracleParameter(":P_HOLD_INVOICE_YN", OracleDbType.Varchar2, objSaleInfoModel.HoldInvoiceYN, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objSaleInfoModel.CreatedBy, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objSaleInfoModel.ShopId, ParameterDirection.Input),
                new OracleParameter(":P_WAREHOUSE_ID", OracleDbType.Varchar2, objSaleInfoModel.WareHouseId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SALE_INFO_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveSaleItem(SaleItemModel objSaleItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_ITEM_ID", OracleDbType.Varchar2, objSaleItemModel.SaleItemId, ParameterDirection.Input),
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, objSaleItemModel.SaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objSaleItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objSaleItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objSaleItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_STYLE_NAME", OracleDbType.Varchar2, objSaleItemModel.StyleName, ParameterDirection.Input),
                new OracleParameter(":P_PRICE", OracleDbType.Varchar2, objSaleItemModel.Price, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objSaleItemModel.Quantity, ParameterDirection.Input),
                new OracleParameter(":P_VAT", OracleDbType.Varchar2, objSaleItemModel.Vat, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL", OracleDbType.Varchar2, objSaleItemModel.Total, ParameterDirection.Input),
                new OracleParameter(":P_SALES_MAN_ID", OracleDbType.Varchar2, objSaleItemModel.SalesManId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_TO_SHOP_EX_YN", OracleDbType.Varchar2, objSaleItemModel.ShopToShopExYn, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };

            query.Append("PRO_SALE_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllPaymentInfo(SalePaymentInfoModel objSalePaymentInfoModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_PAYMENT_TYPE_ID", OracleDbType.Varchar2, objSalePaymentInfoModel.PaymentTypeId, ParameterDirection.Input),
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, objSalePaymentInfoModel.SaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_DISCOUNT_P", OracleDbType.Varchar2, objSalePaymentInfoModel.DiscountPercent, ParameterDirection.Input),
                new OracleParameter(":P_DISCOUNT_A", OracleDbType.Varchar2, objSalePaymentInfoModel.DiscountAmount, ParameterDirection.Input),
                new OracleParameter(":P_PAYMENT_TYPE", OracleDbType.Varchar2, objSalePaymentInfoModel.PaymentType, ParameterDirection.Input),
                new OracleParameter(":P_AMOUNT", OracleDbType.Varchar2, objSalePaymentInfoModel.Amount, ParameterDirection.Input),
                new OracleParameter(":P_SUB_TOTAL", OracleDbType.Varchar2, objSalePaymentInfoModel.SubTotal, ParameterDirection.Input),
                new OracleParameter(":P_PAID_AMOUNT", OracleDbType.Varchar2, objSalePaymentInfoModel.PaidAmount, ParameterDirection.Input),
                new OracleParameter(":P_RETURN_AMOUNT", OracleDbType.Varchar2, objSalePaymentInfoModel.ReturnAmount, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SALE_PAYMENT_INFO_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveSaleInfoForExchange(SaleInfoModel objSaleInfoModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, objSaleInfoModel.SaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, objSaleInfoModel.InvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_SALESMAN_ID", OracleDbType.Varchar2, objSaleInfoModel.SalesManId, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL_ITEM", OracleDbType.Varchar2, objSaleInfoModel.TotalItem, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.TotalAmount, ParameterDirection.Input),
                new OracleParameter(":P_VAT", OracleDbType.Varchar2, objSaleInfoModel.Vat, ParameterDirection.Input),
                new OracleParameter(":P_DISCOUNT_P", OracleDbType.Varchar2, objSaleInfoModel.DiscountPercent, ParameterDirection.Input),
                new OracleParameter(":P_DISCOUNT_A", OracleDbType.Varchar2, objSaleInfoModel.DiscountAmount, ParameterDirection.Input),
                new OracleParameter(":P_PAYMENT_TYPE", OracleDbType.Varchar2, objSaleInfoModel.PaymentType, ParameterDirection.Input),
                new OracleParameter(":P_CASH_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.CashAmount, ParameterDirection.Input),
                new OracleParameter(":P_CARD_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.CardAmount, ParameterDirection.Input),
                new OracleParameter(":P_SUB_TOTAL", OracleDbType.Varchar2, objSaleInfoModel.SubTotal, ParameterDirection.Input),
                new OracleParameter(":P_NET_AMOUNT", OracleDbType.Varchar2, objSaleInfoModel.NetAmount, ParameterDirection.Input),
                new OracleParameter(":P_CUSTOMER_ID", OracleDbType.Varchar2, objSaleInfoModel.CustomerId, ParameterDirection.Input),
                new OracleParameter(":P_HOLD_INVOICE_YN", OracleDbType.Varchar2, objSaleInfoModel.HoldInvoiceYN, ParameterDirection.Input),
                new OracleParameter(":P_EXCHANGE_YN", OracleDbType.Varchar2, objSaleInfoModel.ExchangeYN, ParameterDirection.Input),
                new OracleParameter(":P_EXCHANGE_SHOP_ID", OracleDbType.Varchar2, objSaleInfoModel.ExchangeShopId, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objSaleInfoModel.CreatedBy, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objSaleInfoModel.ShopId, ParameterDirection.Input),
                new OracleParameter(":P_WAREHOUSE_ID", OracleDbType.Varchar2, objSaleInfoModel.WareHouseId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_EXCHANGE_SALE_INFO_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveSaleItemForExchange(int saleInfoId)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_EXCHANGE_SALE_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllPaymentInfoForExchange(int saleInfoId)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                 new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input),
                 new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_EXCHANGE_PAYMENT_INFO_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> GetMaxInvoiceNumber()
        {
            string query = " SELECT  TO_CHAR (SYSDATE, 'yy')|| LPAD (TO_CHAR (SYSDATE, 'MM'), 2, 0)||" +
                           " LPAD (TO_CHAR (SYSDATE, 'dd'), 2, 0)|| LPAD ( (SELECT SHOP_ID FROM Shop), 2, 0)|| " +
                           " (SELECT LPAD (NVL (MAX (SUBSTR ( (INVOICE_NUMBER), 9, 8)), 0) + 1,8,0)FROM SALE_INFO)" +
                           " invoice_no FROM DUAL ";
            var maxInvoiceNUmber = await GetSingleStringAsync(query, null);
            return maxInvoiceNUmber;
        }
        public async Task<string> GetLastInvoiceNumber()
        {
            string query = " SELECT  TO_CHAR (SYSDATE, 'yy')|| LPAD (TO_CHAR (SYSDATE, 'MM'), 2, 0)||" +
                           " LPAD (TO_CHAR (SYSDATE, 'dd'), 2, 0)|| LPAD ( (SELECT SHOP_ID FROM Shop), 2, 0)|| " +
                           " (SELECT LPAD (NVL (MAX (SUBSTR ( (INVOICE_NUMBER), 9, 8)), 0),8,0)FROM SALE_INFO)" +
                           " invoice_no FROM DUAL ";
            var maxInvoiceNUmber = await GetSingleStringAsync(query, null);
            return maxInvoiceNUmber;
        }


        public async Task<IEnumerable<SaleInfoModel>> GetAllHoldInvoice()
        {
            var query = "Select * from VEW_HOLD_INVOICE_INFO ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(SaleInfoModel.ConvertSaleInfoModel);
        }

        public async Task<DataTableAjaxPostModel> GetAllItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM VEW_ITEM_INFO WHERE 1=1 ";

            var query = "Select " +
                        "ROWNUM RN, " +
                        "ItemId," +
                        "ProductId," +
                        "ItemName," +
                        "Barcode," +
                        "PENDINGTRANSFERQTY," +
                        "Quantity," +
                        "ROUNDQTY, " +
                        "SalePrice," +
                        "Umo," +
                        "Vat " +
                        "from VEW_ITEM_INFO WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query += "and ( (lower(ItemName) like lower(:SearchBy)  or upper(ItemName)like upper(:SearchBy) )" +
                       "or (lower(Barcode) like lower(:SearchBy)  or upper(Barcode)like upper(:SearchBy) )" +
                       "or (lower(Quantity) like lower(:SearchBy)  or upper(Quantity)like upper(:SearchBy) )" +
                       "or (lower(ROUNDQTY) like lower(:SearchBy)  or upper(ROUNDQTY)like upper(:SearchBy) )" +
                       "or (lower(SalePrice) like lower(:SearchBy)  or upper(SalePrice)like upper(:SearchBy) )" +
                       "or (lower(Umo) like lower(:SearchBy)  or upper(Umo)like upper(:SearchBy) )" +
                       "or (lower(Vat) like lower(:SearchBy)  or upper(Vat)like upper(:SearchBy) ) )";

                sql += "and ( (lower(ItemName) like lower(:SearchBy)  or upper(ItemName)like upper(:SearchBy) )" +
                         "or (lower(Barcode) like lower(:SearchBy)  or upper(Barcode)like upper(:SearchBy) )" +
                         "or (lower(Quantity) like lower(:SearchBy)  or upper(Quantity)like upper(:SearchBy) )" +
                         "or (lower(ROUNDQTY) like lower(:SearchBy)  or upper(ROUNDQTY)like upper(:SearchBy) )" +
                         "or (lower(SalePrice) like lower(:SearchBy)  or upper(SalePrice)like upper(:SearchBy) )" +
                         "or (lower(Umo) like lower(:SearchBy)  or upper(Umo)like upper(:SearchBy) )" +
                         "or (lower(Vat) like lower(:SearchBy)  or upper(Vat)like upper(:SearchBy) ) )";
            }

            if (model.order != null)
            {
                query += "ORDER BY " + model.columns[model.order[0].column].data + " " + model.order[0].dir.ToUpper();
            }

            query = "SELECT * FROM (" + query + ") WHERE RN BETWEEN  '" + model.start + "' AND '" + (model.start + model.length) + "' ";

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SearchBy", OracleDbType.Varchar2, !string.IsNullOrWhiteSpace(model.search.value) ? $"%{model.search.value}%" : null, ParameterDirection.Input)
            };


            var count = await GetSingleIntAsync(sql, param);

            var dt = await GetDataThroughDataTableAsync(query, param);

            var returnValue = (dt.Rows).Cast<DataRow>().Select(ItemInfoModel.ConvertItemInfoModel);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;
        }

        public async Task<SaleInfoModel> GetAllDataByInvoiceNumber(string invoiceNumber)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_NUMBER", OracleDbType.Varchar2, invoiceNumber, ParameterDirection.Input)
            };

            var query = "Select * from VEW_HOLD_INVOICE_INFO WHERE INVOICE_NUMBER = :INVOICE_NUMBER ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return SaleInfoModel.ConvertSaleInfoModel(dt.Rows[0]);
        }

        public async Task<SaleInfoModel> GetAllDataByInvoiceNumberForExchange(string invoiceNumber)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_NUMBER", OracleDbType.Varchar2, invoiceNumber, ParameterDirection.Input)
            };
            var query = "Select * from VEW_SALE_INFO WHERE INVOICE_NUMBER = :INVOICE_NUMBER ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return SaleInfoModel.ConvertSaleInfoModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<SaleItemModel>> GetAllItemInfoDataBySaleInfoId(int saleInfoId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input)
            };
            var query = "Select * from VEW_SALE_ITEM WHERE SALE_INFO_ID = :SALE_INFO_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(SaleItemModel.ConvertSaleItemModel));
        }
        public async Task<IEnumerable<SaleItemModel>> GetAllItemInfoDataByInvoiceNum(string invoiceNum)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":INVOICE_NUMBER", OracleDbType.Varchar2, invoiceNum, ParameterDirection.Input)
            };
            var query = "Select * from VEW_SALE_ITEM WHERE INVOICE_NUMBER = :INVOICE_NUMBER ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(SaleItemModel.ConvertSaleItemModel));
        }
        public async Task<IEnumerable<SalePaymentInfoModel>> GetAllPaymentInfoDataBySaleInfoId(int saleInfoId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input)
            };
            var query = "Select * from VEW_PAYMENT_INFO WHERE SALE_INFO_ID = :SALE_INFO_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(SalePaymentInfoModel.ConvertSalePaymentInfoModel));
        }

        public async Task<string> DeleteSaleInfoItemForHoldInvoiceSave(int saleInfoId)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SALE_ITEM_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> DeleteSaleInfo(int saleInfoId)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SALE_INFO_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<CustomerSaleModel> GetAllSaleCustomerByInvoiceNumber(int customerId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":CUSTOMER_ID", OracleDbType.Varchar2, customerId, ParameterDirection.Input)
            };
            var query = "Select * from CUSTOMER_SALE WHERE CUSTOMER_ID = :CUSTOMER_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return CustomerSaleModel.ConvertCustomerSaleModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<SaleInfoModel>> GetAllSaleInfo()
        {
            var query = "Select * from VEW_SALE_INFO_API WHERE WAREHOUSE_REC_YN = 'N' AND HOLD_INVOICE_YN = 'N' ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(SaleInfoModel.ConvertSaleInfoModel);
        }

        public async Task<string> UpdateSaleInfo(SaleInfoWarehouseUpdate model)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, model.SaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, model.InvoiceNumber, ParameterDirection.Input),

                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SALE_INFO_WH_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<CustomerSaleModel>> GetAllSaleCustomerInfo()
        {
            var query = "Select * from VEW_CUSTOMER_SALE_INFO ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(CustomerSaleModel.ConvertCustomerSaleModel);
        }

        public async Task<string> SaveReturnSaleInfoData(ReturnSaleInfoModel objReturnSaleInfoModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, objReturnSaleInfoModel.InvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_RETURN_AMOUNT", OracleDbType.Varchar2, objReturnSaleInfoModel.ReturnAmount, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL_ITEM", OracleDbType.Varchar2, objReturnSaleInfoModel.TotalItem, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL_AMOUNT", OracleDbType.Varchar2, objReturnSaleInfoModel.TotalAmount, ParameterDirection.Input),
                new OracleParameter(":P_SUB_TOTAL", OracleDbType.Varchar2, objReturnSaleInfoModel.SubTotal, ParameterDirection.Input),
                new OracleParameter(":P_NET_AMOUNT", OracleDbType.Varchar2, objReturnSaleInfoModel.NetAmount, ParameterDirection.Input),
                new OracleParameter(":P_RETURN_ITEM", OracleDbType.Varchar2, objReturnSaleInfoModel.ReturnItem, ParameterDirection.Input),
                new OracleParameter(":P_RETURN_TOTAL_AMOUNT", OracleDbType.Varchar2, objReturnSaleInfoModel.ReturnTotalAmount, ParameterDirection.Input),
                new OracleParameter(":P_RETURN_VAT", OracleDbType.Varchar2, objReturnSaleInfoModel.ReturnVat, ParameterDirection.Input),
                new OracleParameter(":P_VAT", OracleDbType.Varchar2, objReturnSaleInfoModel.Vat, ParameterDirection.Input),

                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_RETURN_SALE_INFO_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveReturnSaleItemData(SaleItemModel objSaleItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_ITEM_ID", OracleDbType.Varchar2, objSaleItemModel.SaleItemId, ParameterDirection.Input),
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, objSaleItemModel.SaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objSaleItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objSaleItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objSaleItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_STYLE_NAME", OracleDbType.Varchar2, objSaleItemModel.StyleName, ParameterDirection.Input),
                new OracleParameter(":P_PRICE", OracleDbType.Varchar2, objSaleItemModel.Price, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objSaleItemModel.Quantity, ParameterDirection.Input),
                new OracleParameter(":P_VAT", OracleDbType.Varchar2, objSaleItemModel.Vat, ParameterDirection.Input),
                new OracleParameter(":P_TOTAL", OracleDbType.Varchar2, objSaleItemModel.Total, ParameterDirection.Input),
                new OracleParameter(":P_SALES_MAN_ID", OracleDbType.Varchar2, objSaleItemModel.SalesManId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_RETURN_SALE_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }


        public async Task<string> SaveReturnSaleInfoDataAndDelete(ReturnSaleInfoModel objReturnSaleInfoModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, objReturnSaleInfoModel.InvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_RETURN_AMOUNT", OracleDbType.Varchar2, objReturnSaleInfoModel.ReturnAmount, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_RETURN_SALE_INFO_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<int> GetStockByItemId(int itemId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":ITEMID", OracleDbType.Varchar2, itemId, ParameterDirection.Input)
            };
            var query = "Select QUANTITY from VEW_ITEM_INFO WHERE ITEMID = :ITEMID ";
            return await GetSingleIntAsync(query, param);
        }

        public async Task<string> DeleteAllHoleInfo(string invoiceNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, invoiceNumber, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_HOLDINVOICE_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllPaymentInfoForExchangePayZero(int saleInfoId)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_EXCHANGE_PAYZERO_INFO_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveExchangeHistory(HistoryModel model)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_EXCHANGE_ID", OracleDbType.Varchar2, model.HistoryId, ParameterDirection.Input),
                new OracleParameter(":P_PREVIOUS_INVOICE_NUMBER", OracleDbType.Varchar2, model.PreviousInvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_PREVIOUS_SALE_INFO_ID", OracleDbType.Varchar2, model.PreviousSaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_PREVIOUS_INVOICE_DATE", OracleDbType.Varchar2, model.PreviousInvoiceDate, ParameterDirection.Input),
                new OracleParameter(":P_NEW_INVOICE_NUMBER", OracleDbType.Varchar2, model.NewInvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_NEW_SALE_INFO_ID", OracleDbType.Varchar2, model.NewSaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_NEW_INVOICE_DATE", OracleDbType.Varchar2, model.NewInvoiceDate, ParameterDirection.Input),
                new OracleParameter(":P_CREATE_BY", OracleDbType.Varchar2, model.CreateBy, ParameterDirection.Input),
                new OracleParameter(":P_CREATE_DATE", OracleDbType.Varchar2, model.CreateDate, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, model.ShopId, ParameterDirection.Input),
                new OracleParameter(":P_WAREHOUSE_ID", OracleDbType.Varchar2, model.WarehouseId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_EXCHANGE_HISTORY_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> VoidInvoice(string invoiceNumber)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, invoiceNumber, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_VOID_INVOICE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> VoidInvoiceWarehouse(string invoiceNumber, string shopId)
        {
            var model = new
            {
                InvoiceNumber = invoiceNumber,
                ShopId = shopId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "VoidInvoice", model);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<string> SaveReturnHistory(HistoryModel model)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_RETURN_ID", OracleDbType.Varchar2, model.HistoryId, ParameterDirection.Input),
                new OracleParameter(":P_PREVIOUS_INVOICE_NUMBER", OracleDbType.Varchar2, model.PreviousInvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_PREVIOUS_SALE_INFO_ID", OracleDbType.Varchar2, model.PreviousSaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_PREVIOUS_INVOICE_DATE", OracleDbType.Varchar2, model.PreviousInvoiceDate, ParameterDirection.Input),
                new OracleParameter(":P_NEW_INVOICE_NUMBER", OracleDbType.Varchar2, model.NewInvoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_NEW_SALE_INFO_ID", OracleDbType.Varchar2, model.NewSaleInfoId, ParameterDirection.Input),
                new OracleParameter(":P_NEW_INVOICE_DATE", OracleDbType.Varchar2, model.NewInvoiceDate, ParameterDirection.Input),
                new OracleParameter(":P_CREATE_BY", OracleDbType.Varchar2, model.CreateBy, ParameterDirection.Input),
                new OracleParameter(":P_CREATE_DATE", OracleDbType.Varchar2, model.CreateDate, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, model.ShopId, ParameterDirection.Input),
                new OracleParameter(":P_WAREHOUSE_ID", OracleDbType.Varchar2, model.WarehouseId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_RETURN_HISTORY_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<SaleInfoModel> GetAllDataFromDcByInvoiceNumberForExchange(string invoiceNumber, int shopId)
        {
            SaleInfoModel saleInfo = new SaleInfoModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);
                    //HTTP GET
                    var responseTask = client.GetAsync("InvoiceInfo?invoiceNumber=" + invoiceNumber + "&shopId=" + shopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<SaleInfoModel>();
                        readTask.Wait();
                        await Task.Run(() => saleInfo = readTask.Result);

                        return saleInfo;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ItemInfoShopReceiveModel> GetAllDataFromDcByInvoiceNumberForDirectReceive(int productId, int itemId)
        {
            ItemInfoShopReceiveModel itemInfo = new ItemInfoShopReceiveModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);
                    //HTTP GET
                    var responseTask = client.GetAsync("ItemInfoForDirectShopReceive?productId=" + productId + "&itemId=" + itemId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ItemInfoShopReceiveModel>();
                        readTask.Wait();
                        await Task.Run(() => itemInfo = readTask.Result);

                        return itemInfo;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GiftVoucherModel> GetAllInfoByGiftVoucherCode(string giftVoucherCode)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":GIFT_VOUCHER_CODE", OracleDbType.Varchar2, giftVoucherCode, ParameterDirection.Input)
            };
            var query = " Select * from GIFT_VOUCHER_DELIVERY WHERE GIFT_VOUCHER_CODE = :GIFT_VOUCHER_CODE AND DEPOSIT_YN = 'Y'";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return GiftVoucherModel.ConvertGiftVoucherModel(dt.Rows[0]);
        }

        public async Task<string> SaveGiftVoucherData(string invoiceNumber, string giftVoucherCode, double giftVoucherOldBalance, double giftVoucherNewBalance, string createdBy)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_INVOICE_NUMBER", OracleDbType.Varchar2, invoiceNumber, ParameterDirection.Input),
                new OracleParameter(":P_GIFT_VOUCHER_CODE", OracleDbType.Varchar2, giftVoucherCode, ParameterDirection.Input),
                new OracleParameter(":P_GIFT_VOUCHER_OLD_BAL", OracleDbType.Varchar2, giftVoucherOldBalance, ParameterDirection.Input),
                new OracleParameter(":P_GIFT_VOUCHER_NEW_BAL", OracleDbType.Varchar2, giftVoucherNewBalance, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, createdBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_GIFTVOUCHER_SALE_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<CustomerInfoModel> GetAllInfoByCustomerPhone(string customerPhone)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":CONTACT_NO", OracleDbType.Varchar2, customerPhone, ParameterDirection.Input)
            };
            var query = " Select * from VEW_CUSTOMER_INFO WHERE CONTACT_NO = :CONTACT_NO ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return CustomerInfoModel.ConvertCustomerInfoModel(dt.Rows[0]);
        }

        public async Task<CustomerInfoModel> GetAllInfoByCustomerPhoneFromSale(string customerPhone)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":CONTACT_NO", OracleDbType.Varchar2, customerPhone, ParameterDirection.Input)
            };
            var query = " Select CUSTOMER_CODE,CUSTOMER_NAME,CONTACT_NO, ADDRESS from CUSTOMER_SALE WHERE lower(CONTACT_NO) = lower(:CONTACT_NO)  AND ROWNUM=1 ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return CustomerInfoModel.ConvertCustomerInfoModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<SearchHintsModel>> GetProductGridsForHints(string model)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SEARCH_NAME", OracleDbType.Varchar2, $"%{model}%", ParameterDirection.Input)
            };
            var query = "SELECT DISTINCT STYLE_NAME   FROM VEW_STORE_HOUSE_INFO   WHERE LOWER (STYLE_NAME) LIKE LOWER (:SEARCH_NAME) AND ROWNUM <= 10 ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(SearchHintsModel.ConvertSearchHintsModel));
        }
    }
}
