using System;
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
    public class StoreHouseRequestRepository : ApplicationDbContext, IStoreHouseRequestRepository
    {
        public async Task<string> SaveAllData(StoreHouseRequestMain objStoreHouseRequestMain)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUEST_ID", OracleDbType.Varchar2, objStoreHouseRequestMain.RequestId, ParameterDirection.Input),
                new OracleParameter(":P_FLOOR_NO", OracleDbType.Varchar2, objStoreHouseRequestMain.FloorNo, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objStoreHouseRequestMain.CreatedBy, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objStoreHouseRequestMain.ShopId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_REQUEST_MAIN_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllRequestItem(StoreHouseRequestMainItem obStoreHouseRequestMainItem)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUEST_MAIN_ITEM_ID", OracleDbType.Varchar2, obStoreHouseRequestMainItem.RequestMainItemId, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_NO", OracleDbType.Varchar2, obStoreHouseRequestMainItem.RequisitionNo, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, obStoreHouseRequestMainItem.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, obStoreHouseRequestMainItem.Quantity, ParameterDirection.Input),
                new OracleParameter(":P_PRICE", OracleDbType.Varchar2, obStoreHouseRequestMainItem.Price, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_REQUEST_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<StoreHouseRequestMain>> StoreHouseRequestList()
        {
            var query = "Select * from VEW_STORE_HOUSE_MAIN WHERE SUBMIT_YN ='N'";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(StoreHouseRequestMain.ConvertStoreHouseRequestMain);
        }
       

        public async Task<IEnumerable<StoreHouseRequestMainItem>> GetAllItemInfoDataByRequisitionNo(string requisitionNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":REQUISITION_NO", OracleDbType.Varchar2, requisitionNo, ParameterDirection.Input)
            };
            var query = "Select * from VEW_STORE_HOUSE_MAIN_ITEM WHERE REQUISITION_NO = :REQUISITION_NO  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(StoreHouseRequestMainItem.ConvertStoreHouseRequestMainItem));
        }

        public async Task<string> SubmitData(StoreHouseRequestMain objStoreHouseRequestMain)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_NO", OracleDbType.Varchar2, objStoreHouseRequestMain.RequisitionNo, ParameterDirection.Input),
                new OracleParameter(":P_UPDATED_BY", OracleDbType.Varchar2, objStoreHouseRequestMain.UpdatedBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_REQUEST_MAIN_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<StoreHouseItemModel>> GetAllProductInfoByProductId(int productId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":PRODUCT_ID", OracleDbType.Varchar2, productId, ParameterDirection.Input)
            };
            var query = "Select  ITEM_ID, PRODUCT_ID, ITEM_NAME, BARCODE, SALE_PRICE, CATEGORY_NAME, " +
                        "SUB_CATEGORY_NAME from VEW_STORE_HOUSE_INFO WHERE PRODUCT_ID = :PRODUCT_ID " +
                        "GROUP BY ITEM_ID, PRODUCT_ID, ITEM_NAME, BARCODE, SALE_PRICE, CATEGORY_NAME, SUB_CATEGORY_NAME " +
                        "ORDER BY ITEM_ID ASC  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(StoreHouseItemModel.ConvertStoreHouseItemModel);
        }

        public  async Task<string> SaveAllStoreHouseMainData(StoreHouseModel objStoreHouseModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STORE_HOUSE_ID", OracleDbType.Varchar2, objStoreHouseModel.StoreHouseId, ParameterDirection.Input),
                new OracleParameter(":P_LINE_NO", OracleDbType.Varchar2, objStoreHouseModel.LineNo, ParameterDirection.Input),
                new OracleParameter(":P_ROPE_NO", OracleDbType.Varchar2, objStoreHouseModel.RopeNo, ParameterDirection.Input),
                new OracleParameter(":P_RACK_NO", OracleDbType.Varchar2, objStoreHouseModel.RackNo, ParameterDirection.Input),
                new OracleParameter(":P_REMARKS", OracleDbType.Varchar2, objStoreHouseModel.Remarks, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objStoreHouseModel.CreatedBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_HOUSE_PRODUCT_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveStoreHouseItemData(StoreHouseItemModel objStoreHouseItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STORE_HOUSE_PRODUCT_ITEM_ID", OracleDbType.Varchar2, objStoreHouseItemModel.StoreHouseItemId, ParameterDirection.Input),
                new OracleParameter(":P_STORE_HOUSE_ID", OracleDbType.Varchar2, objStoreHouseItemModel.StoreHouseId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objStoreHouseItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objStoreHouseItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objStoreHouseItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objStoreHouseItemModel.Quantity, ParameterDirection.Input),
                new OracleParameter(":P_CATEGORY_NAME", OracleDbType.Varchar2, objStoreHouseItemModel.Category, ParameterDirection.Input),
                new OracleParameter(":P_SUB_CATEGORY_NAME", OracleDbType.Varchar2, objStoreHouseItemModel.SubCategory, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_HO_PRODUCT_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<DataTableAjaxPostModel> StoreHouseProductSearch(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM VEW_STORE_HOUSE_SEARCH WHERE 1=1  ";

            var query = "Select " +
                        " ROW_NUMBER () OVER (ORDER BY LINENO DESC) RN2," +
                        "STOREHOUSEID," +
                        "LINENO," +
                        "ROPENO," +
                        "RACKNO," +
                        "PRODUCTID," +
                        "ITEMID," +
                        "ITEMNAME," +
                        "BARCODE," +
                        "QUANTITY," +
                        "CATEGORYNAME," +
                        "SUBCATEGORYNAME," +
                        "TOTALROWCOUNT " +
                        "from VEW_STORE_HOUSE_SEARCH WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query += "and ( (lower(LINENO) like lower(:SearchBy)  or upper(LINENO)like upper(:SearchBy) )" +
                       "or (lower(ROPENO) like lower(:SearchBy)  or upper(ROPENO)like upper(:SearchBy) )" +
                       "or (lower(RACKNO) like lower(:SearchBy)  or upper(RACKNO)like upper(:SearchBy) )" +
                       "or (lower(ITEMNAME) like lower(:SearchBy)  or upper(ITEMNAME)like upper(:SearchBy) )" +
                       "or (lower(BARCODE) like lower(:SearchBy)  or upper(BARCODE)like upper(:SearchBy) )" +
                       "or (lower(QUANTITY) like lower(:SearchBy)  or upper(QUANTITY)like upper(:SearchBy) )" +
                       "or (lower(CATEGORYNAME) like lower(:SearchBy)  or upper(CATEGORYNAME)like upper(:SearchBy) )" +
                       "or (lower(SUBCATEGORYNAME) like lower(:SearchBy)  or upper(SUBCATEGORYNAME)like upper(:SearchBy) ) )";

                sql += "and ( (lower(LINENO) like lower(:SearchBy)  or upper(LINENO)like upper(:SearchBy) )" +
                       "or (lower(ROPENO) like lower(:SearchBy)  or upper(ROPENO)like upper(:SearchBy) )" +
                       "or (lower(RACKNO) like lower(:SearchBy)  or upper(RACKNO)like upper(:SearchBy) )" +
                       "or (lower(ITEMNAME) like lower(:SearchBy)  or upper(ITEMNAME)like upper(:SearchBy) )" +
                       "or (lower(BARCODE) like lower(:SearchBy)  or upper(BARCODE)like upper(:SearchBy) )" +
                       "or (lower(QUANTITY) like lower(:SearchBy)  or upper(QUANTITY)like upper(:SearchBy) )" +
                       "or (lower(CATEGORYNAME) like lower(:SearchBy)  or upper(CATEGORYNAME)like upper(:SearchBy) )" +
                       "or (lower(SUBCATEGORYNAME) like lower(:SearchBy)  or upper(SUBCATEGORYNAME)like upper(:SearchBy) ) )";
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

            var returnValue = (dt.Rows).Cast<DataRow>().Select(StoreHouseProductSearchModel.ConvertStoreHouseProductSearchModel);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;
        }

        public async Task<StoreHouseItemModel> GetAllInfoByBarcode(string barcode)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":BARCODE", OracleDbType.Varchar2, barcode, ParameterDirection.Input)
            };
            var query = "SELECT ITEM_ID,PRODUCT_ID,ITEM_NAME,BARCODE,SALE_PRICE,CATEGORY_NAME," +
                        "SUB_CATEGORY_NAME " +
                        "FROM VEW_STORE_HOUSE_INFO WHERE BARCODE = :BARCODE AND ROWNUM = 1  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return StoreHouseItemModel.ConvertStoreHouseItemModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<StoreHouseRequestItemModelSubmit>> GetAllInfoByRequisitionNo(string requisitionNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":REQUISITION_NO", OracleDbType.Varchar2, requisitionNo, ParameterDirection.Input)
            };
            var query = "Select * from VEW_STORE_HOUSE_MAIN_ITEM WHERE REQUISITION_NO = :REQUISITION_NO  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(StoreHouseRequestItemModelSubmit.ConvertStoreHouseRequestItemModelSubmit));
        }

        public async Task<IEnumerable<StoreHouseModel>> GetAllLineRopeRackList(string barcode)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":BARCODE", OracleDbType.Varchar2, barcode, ParameterDirection.Input)
            };
            var query = "Select * from VEW_STORE_HOU_PRODUCT_INFO WHERE BARCODE = :BARCODE  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(StoreHouseModel.ConvertStoreHouseModel));
        }

        public async Task<string> SubmitRequestItemData(StoreHouseRequestMainItemConfirm objStoreHouseRequestMainItemConfirm)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_NO", OracleDbType.Varchar2, objStoreHouseRequestMainItemConfirm.RequisitionNo, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objStoreHouseRequestMainItemConfirm.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_LINE_NO", OracleDbType.Varchar2, objStoreHouseRequestMainItemConfirm.LineNo, ParameterDirection.Input),
                new OracleParameter(":P_ROPE_NO", OracleDbType.Varchar2, objStoreHouseRequestMainItemConfirm.RopeNo, ParameterDirection.Input),
                new OracleParameter(":P_RACK_NO", OracleDbType.Varchar2, objStoreHouseRequestMainItemConfirm.RackNo, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_REQUEST_ITEM_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
