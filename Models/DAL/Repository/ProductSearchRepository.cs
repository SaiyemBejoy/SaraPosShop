using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class ProductSearchRepository : ApplicationDbContext, IProductSearchRepository
    {
        public async Task<DataTableAjaxPostModel> GetAllItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM VEW_ITEM_INFO WHERE 1=1 ";

            var query = "Select " +
                        "ROWNUM RN, " +
                        "ITEMID," +
                        "PRODUCTID," +
                        "ITEMNAME," +
                        "BARCODE," +
                        "RECEIVEQUANTITY," +
                        "SALEQUANTITY," +
                        "DAMAGEQUANTITY," +
                        "SHOPTOSHOPRECEIVEQTY," +
                        "SHOPTOSHOPDELIVERYQTY," +
                        "WAREHOUSETRANSFERQTY," +
                        "PENDINGTRANSFERQTY," +
                        "QUANTITY," +
                        "SALEPRICE," +
                        "VAT " +
                        "from VEW_ITEM_INFO WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query +=
                    "and ( (lower(ITEMNAME) like lower(:SearchBy)  or upper(ITEMNAME)like upper(:SearchBy) )" +
                    "or (lower(BARCODE) like lower(:SearchBy)  or upper(BARCODE)like upper(:SearchBy) )" +
                    "or (lower(RECEIVEQUANTITY) like lower(:SearchBy)  or upper(RECEIVEQUANTITY)like upper(:SearchBy) )" +
                    "or (lower(SALEQUANTITY) like lower(:SearchBy)  or upper(SALEQUANTITY)like upper(:SearchBy) )" +
                    "or (lower(DAMAGEQUANTITY) like lower(:SearchBy)  or upper(DAMAGEQUANTITY)like upper(:SearchBy) )" +
                    "or (lower(SHOPTOSHOPRECEIVEQTY) like lower(:SearchBy)  or upper(SHOPTOSHOPRECEIVEQTY)like upper(:SearchBy) )" +
                    "or (lower(SHOPTOSHOPDELIVERYQTY) like lower(:SearchBy)  or upper(SHOPTOSHOPDELIVERYQTY)like upper(:SearchBy) )" +
                    "or (lower(WAREHOUSETRANSFERQTY) like lower(:SearchBy)  or upper(WAREHOUSETRANSFERQTY)like upper(:SearchBy) )" +
                    "or (lower(PENDINGTRANSFERQTY) like lower(:SearchBy)  or upper(PENDINGTRANSFERQTY)like upper(:SearchBy) )" +
                    "or (lower(QUANTITY) like lower(:SearchBy)  or upper(QUANTITY)like upper(:SearchBy) )" +
                    "or (lower(SALEPRICE) like lower(:SearchBy)  or upper(SALEPRICE)like upper(:SearchBy) )" +
                    "or (lower(VAT) like lower(:SearchBy)  or upper(VAT)like upper(:SearchBy) ) )";

                sql +=
                    "and ( (lower(ITEMNAME) like lower(:SearchBy)  or upper(ITEMNAME)like upper(:SearchBy) )" +
                    "or (lower(BARCODE) like lower(:SearchBy)  or upper(BARCODE)like upper(:SearchBy) )" +
                    "or (lower(RECEIVEQUANTITY) like lower(:SearchBy)  or upper(RECEIVEQUANTITY)like upper(:SearchBy) )" +
                    "or (lower(SALEQUANTITY) like lower(:SearchBy)  or upper(SALEQUANTITY)like upper(:SearchBy) )" +
                    "or (lower(DAMAGEQUANTITY) like lower(:SearchBy)  or upper(DAMAGEQUANTITY)like upper(:SearchBy) )" +
                    "or (lower(SHOPTOSHOPRECEIVEQTY) like lower(:SearchBy)  or upper(SHOPTOSHOPRECEIVEQTY)like upper(:SearchBy) )" +
                    "or (lower(SHOPTOSHOPDELIVERYQTY) like lower(:SearchBy)  or upper(SHOPTOSHOPDELIVERYQTY)like upper(:SearchBy) )" +
                    "or (lower(WAREHOUSETRANSFERQTY) like lower(:SearchBy)  or upper(WAREHOUSETRANSFERQTY)like upper(:SearchBy) )" +
                    "or (lower(PENDINGTRANSFERQTY) like lower(:SearchBy)  or upper(PENDINGTRANSFERQTY)like upper(:SearchBy) )" +
                    "or (lower(QUANTITY) like lower(:SearchBy)  or upper(QUANTITY)like upper(:SearchBy) )" +
                    "or (lower(SALEPRICE) like lower(:SearchBy)  or upper(SALEPRICE)like upper(:SearchBy) )" +
                    "or (lower(VAT) like lower(:SearchBy)  or upper(VAT)like upper(:SearchBy) ) )";
            }

            if (model.order != null)
            {
                query += "ORDER BY " + model.columns[model.order[0].column].data + " " +
                         model.order[0].dir.ToUpper();
            }

            query = "SELECT * FROM (" + query + ") WHERE RN BETWEEN  '" + model.start + "' AND '" +
                    (model.start + model.length) + "' ";

            List<OracleParameter> param = new List<OracleParameter>
                {
                    new OracleParameter(":SearchBy", OracleDbType.Varchar2,
                        !string.IsNullOrWhiteSpace(model.search.value) ? $"%{model.search.value}%" : null,
                        ParameterDirection.Input)
            };


            var count = await GetSingleIntAsync(sql, param);

            var dt = await GetDataThroughDataTableAsync(query, param);

            var returnValue = (dt.Rows).Cast<DataRow>().Select(ProductSearchModel.ConvertProductSearchModel);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;

        }

        public async Task<string> GetTotalItemCount()
        {
            string query = " SELECT NVL (SUM (QUANTITY), 0)QUANTITY FROM VEW_ITEM_INFO ";
            var totalItemCount = await GetSingleStringAsync(query, null);
            return totalItemCount;
        }
        public async Task<string> GetSaleItemCount()
        {
            string query = " SELECT NVL (SUM (SALEQUANTITY), 0)SALEQUANTITY FROM VEW_ITEM_INFO ";
            var totalItemCount = await GetSingleStringAsync(query, null);
            return totalItemCount;
        }

        public async Task<string> GetReceiveItemCount()
        {
            string query = " SELECT NVL (SUM (RECEIVEQUANTITY), 0)RECEIVEQUANTITY FROM VEW_ITEM_INFO ";
            var totalReceiveItemCount = await GetSingleStringAsync(query, null);
            return totalReceiveItemCount;
        }

        public async Task<IEnumerable<ProductSearchModel>> GetAllProductInfoByProductId(int productId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":PRODUCT_ID", OracleDbType.Varchar2, productId, ParameterDirection.Input)
            };
            var query = "Select * FROM  VEW_ITEM_INFO  WHERE PRODUCTID = :PRODUCT_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(ProductSearchModel.ConvertProductSearchModel);
        }

        public async Task<StockCalculateSummary> GetAllCalculateSummaryInfo()
        {
            var query = "SELECT NVL (SUM (RECEIVEQUANTITY), 0) RECEIVEQUANTITY, " +
                        "NVL(SUM(SALEQUANTITY), 0) SALEQUANTITY, " +
                        "NVL(SUM(DAMAGEQUANTITY), 0) DAMAGEQUANTITY, " +
                        "NVL(SUM(WAREHOUSETRANSFERQTY), 0) WAREHOUSETRANSFERQTY, " +
                        "NVL(SUM(PENDINGTRANSFERQTY), 0) PENDINGTRANSFERQTY," +
                        "NVL(SUM(QUANTITY), 0) QUANTITY  " +
                        "FROM VEW_ITEM_INFO ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return StockCalculateSummary.ConvertStockCalculateSummary(dt.Rows[0]);
        }

        public async Task<IEnumerable<ProductSearchModel>> GetAllInfoBySearchTextBoxValue(string searchValue)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SearchBy", OracleDbType.Varchar2, !string.IsNullOrWhiteSpace(searchValue) ? $"%{searchValue}%" : null,                        ParameterDirection.Input)
            };
            var query = "SELECT *   FROM VEW_ITEM_INFO WHERE LOWER(ITEMNAME) LIKE LOWER(:SearchBy)" +
                " OR UPPER(ITEMNAME) LIKE UPPER(:SearchBy)" +
                " OR LOWER(BARCODE) LIKE LOWER(:SearchBy)" +
                " OR UPPER(BARCODE) LIKE UPPER(:SearchBy) ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(ProductSearchModel.ConvertProductSearchModel);
        }
    }
}