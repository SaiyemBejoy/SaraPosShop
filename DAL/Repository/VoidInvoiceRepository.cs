using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class VoidInvoiceRepository : ApplicationDbContext, IVoidInvoiceRepository
    {
        public async Task<DataTableAjaxPostModel> GetAllVoidInvoiceDataTable(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM VOID_SALE_INFO WHERE 1=1  ";

            var query = "Select " +
                        "ROW_NUMBER() OVER(ORDER BY INVOICE_NUMBER) AS RN," +
                        "SALE_INFO_ID, " +
                        "INVOICE_NUMBER, " +
                        "INVOICE_DATE," +
                        "SALESMAN_ID," +
                        "TOTAL_ITEM," +
                        "TOTAL_AMOUNT," +
                        "VAT," +
                        "DISCOUNT_P," +
                        "DISCOUNT_A," +
                        "SUB_TOTAL," +
                        "CUSTOMER_ID," +
                        "CREATED_BY," +
                        "NET_AMOUNT " +
                        "from VOID_SALE_INFO WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query += "and ( (lower(INVOICE_NUMBER) like lower(:SearchBy)  or upper(INVOICE_NUMBER)like upper(:SearchBy) )" +
                       "or (lower(INVOICE_DATE) like lower(:SearchBy)  or upper(INVOICE_DATE)like upper(:SearchBy) )" +
                       "or (lower(SALESMAN_ID) like lower(:SearchBy)  or upper(SALESMAN_ID)like upper(:SearchBy) )" +
                       "or (lower(DISCOUNT_P) like lower(:SearchBy)  or upper(DISCOUNT_P)like upper(:SearchBy) )" +
                       "or (lower(NET_AMOUNT) like lower(:SearchBy)  or upper(NET_AMOUNT)like upper(:SearchBy) ) )";

                sql += "and ( (lower(INVOICE_NUMBER) like lower(:SearchBy)  or upper(INVOICE_NUMBER)like upper(:SearchBy) )" +
                       "or (lower(INVOICE_DATE) like lower(:SearchBy)  or upper(INVOICE_DATE)like upper(:SearchBy) )" +
                       "or (lower(SALESMAN_ID) like lower(:SearchBy)  or upper(SALESMAN_ID)like upper(:SearchBy) )" +
                       "or (lower(DISCOUNT_P) like lower(:SearchBy)  or upper(DISCOUNT_P)like upper(:SearchBy) )" +
                       "or (lower(NET_AMOUNT) like lower(:SearchBy)  or upper(NET_AMOUNT)like upper(:SearchBy) ) )";
            }

            if (model.order != null)
            {
                query += "ORDER BY " + model.columns[model.order[0].column].data + " " + model.order[0].dir.ToUpper();
            }

            query = "SELECT * FROM (" + query + ") WHERE RN BETWEEN  '" + model.start + "' AND '" + (model.start + model.length) + "' ORDER BY SALE_INFO_ID DESC  ";

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SearchBy", OracleDbType.Varchar2, !string.IsNullOrWhiteSpace(model.search.value) ? $"%{model.search.value}%" : null, ParameterDirection.Input)
            };


            var count = await GetSingleIntAsync(sql, param);

            var dt = await GetDataThroughDataTableAsync(query, param);

            var returnValue = (dt.Rows).Cast<DataRow>().Select(VoidInvoiceModel.ConvertVoidInvoiceModelForDataTable);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;
        }

        public async Task<IEnumerable<VoidInvoiceItem>> ViewAllVoidInvoiceItemBySaleInfoId(string saleInfoId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SALE_INFO_ID", OracleDbType.Varchar2, saleInfoId, ParameterDirection.Input)
            };
            var query = "Select * from VOID_SALE_ITEM WHERE SALE_INFO_ID = :SALE_INFO_ID  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(VoidInvoiceItem.ConvertVoidInvoiceItem);
        }
    }
}
