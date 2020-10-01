using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class StockTransferRepository : ApplicationDbContext, IStockTransferRepository
    {
        public async Task<string> GetMaxChallanNo()
        {
            string query = "SELECT LPAD (TO_CHAR (SYSDATE, 'dd'), 2, 0)" +
                           "|| LPAD (TO_CHAR (SYSDATE, 'MM'), 2, 0)" +
                           "|| TO_CHAR (SYSDATE, 'yy')" +
                           "|| LPAD ( (SELECT SHOP_ID FROM Shop), 2, 0)" +
                           "|| (SELECT LPAD ( NVL (MAX (SUBSTR ( (STOCK_TRANSFER_CHALLAN_NUM), 9, 8)),0)+ 1, 8,0)" +
                           " FROM STOCK_TRANSFER_MAIN) STOCK_TRANSFER_CHALLAN_NUM FROM DUAL ";
            var getMaxChallanNo = await GetSingleStringAsync(query, null);
            return getMaxChallanNo;
        }

        public async Task<ItemInfoModel> GetProductInfoByBarcode(string barcode)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":BARCODE", OracleDbType.Varchar2, barcode, ParameterDirection.Input)
            };

            var query = "Select * from VEW_ITEM_INFO WHERE BARCODE = :BARCODE ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ItemInfoModel.ConvertItemInfoModel(dt.Rows[0]);
        }

        public async Task<TransitItemInfoModel> GetProductReturnInfoByBarcode(string barcode, int marketPlaceId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":BARCODE", OracleDbType.Varchar2, barcode, ParameterDirection.Input),
                new OracleParameter(":MARKET_PLACE_ID", OracleDbType.Varchar2, marketPlaceId, ParameterDirection.Input)
            };

            var query = "Select * from VEW_ALL_TRANSIT_ITEM_INFO WHERE BARCODE = :BARCODE AND MARKET_PLACE_ID = :MARKET_PLACE_ID";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return TransitItemInfoModel.ConvertTransitItemInfoModel(dt.Rows[0]);
        }

        public async Task<string> GetRequisitionNo()
        {
            string query = " SELECT 'ST' || (SELECT LPAD (NVL (MAX (SUBSTR ( (REQUISITION_NUM), 9, 8)), 0) + 1,8,0)FROM STOCK_TRANSFER_MAIN) REQUISITION_NUM  FROM DUAL ";
            var getRequisition = await GetSingleStringAsync(query, null);
            return getRequisition;
        }


        public async Task<string> UpdateDamageTable(string challanNo)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {

                new OracleParameter(":P_DAMAGE_CHALLAN_NO", OracleDbType.Varchar2, challanNo, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_DAMAGE_MAIN_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllTransferDataInWarehouse(StockTransferModel objStockTransferModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "StockTransfer", objStockTransferModel);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var message = response.Content.ReadAsStringAsync().Result;
                    string result = Regex.Match(message, @"\d+").Value; 
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<string> SaveAllTransferDataInShop(StockTransferModel objStockTransferModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STOCK_TRANSFER_ID", OracleDbType.Varchar2, objStockTransferModel.StockTransferId, ParameterDirection.Input),
                new OracleParameter(":P_STOCK_TRANSFER_CHALLAN_NO", OracleDbType.Varchar2, objStockTransferModel.StockTransferChallanNumber, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_NUMNER", OracleDbType.Varchar2, objStockTransferModel.RequisitionNumber, ParameterDirection.Input),
                new OracleParameter(":P_TRANSFER_SHOPID_TO", OracleDbType.Varchar2, objStockTransferModel.TransferToShopId, ParameterDirection.Input),
                new OracleParameter(":P_TRANSFER_SHOPID_NAME", OracleDbType.Varchar2, objStockTransferModel.TransferToShopName, ParameterDirection.Input),
                new OracleParameter(":P_TRANSFER_SHOPID_FROM", OracleDbType.Varchar2, objStockTransferModel.TransferFromShopId, ParameterDirection.Input),
                new OracleParameter(":P_TRANSFER_BY", OracleDbType.Varchar2, objStockTransferModel.TransferedBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STOCK_TRANSFER_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllTransferDataItemInShop(StockTransferItemModel objStockTransferItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STOCK_TRANSFER_ITEM_ID", OracleDbType.Varchar2, objStockTransferItemModel.StockTransferItemId, ParameterDirection.Input),
                new OracleParameter(":P_STOCK_TRANSFER_ID", OracleDbType.Varchar2, objStockTransferItemModel.StockTransferId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objStockTransferItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objStockTransferItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objStockTransferItemModel.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objStockTransferItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_TRANSFER_QUANTITY", OracleDbType.Varchar2, objStockTransferItemModel.TransferQuantity, ParameterDirection.Input),
                new OracleParameter(":P_SALE_PRICE", OracleDbType.Varchar2, objStockTransferItemModel.SalePrice, ParameterDirection.Input),
                new OracleParameter(":P_VAT", OracleDbType.Varchar2, objStockTransferItemModel.Vat, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STOCK_TRANSFER_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> UpdateStockTransferModel(string challanNo)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {

                new OracleParameter(":P_STOCK_TRANSFER_CHALLAN_NO", OracleDbType.Varchar2, challanNo, ParameterDirection.Input),
                   new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STOCK_TR_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveDmgTransferDataInWarehouse(DamageTransferMain objDamageTransferMainModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);

                    HttpResponseMessage response = await client.PostAsJsonAsync(
                        "DamageTransfer", objDamageTransferMainModel);
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
            catch (Exception )
            {
                return null;
            }
        }

        public async Task<string> UpdateDmgStockTransferModel(string damageChallanNo)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {

                new OracleParameter(":P_DAMAGE_CHALLAN_NO", OracleDbType.Varchar2, damageChallanNo, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_DAMAGE_FOR_REC_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> UpdateStockTransferTable(TransferReturnProduct objTransferReturnProduct)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {

                new OracleParameter(":P_TRANSFER_CHALLAN_NO", OracleDbType.Varchar2, objTransferReturnProduct.StockTranferChallanNo, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_TRANSFER_FOR_REC_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<DataTableAjaxPostModel> GetAllTransferChallanForDataTable(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM VEW_STOCK_TR_DATA_TABLE WHERE 1=1  ";

            var query = "Select " +
                        "ROW_NUMBER() OVER(ORDER BY  STOCKTRANSFERID DESC) AS RN," +
                        "STOCKTRANSFERID," +
                        "STOCKTRANSFERCHALLANNUMBER," +
                        "REQUISITIONNUM," +
                        "TRANSFERSHOPIDTO," +
                        "TRANSFERSHOPIDFROM," +
                        "TRANSFEREDBY," +
                        "TRANSFEREDBYNAME," +
                        "TRANSFERDATE," +
                        "RECEIVEYN," +
                        "TRANSFERSHOPIDTONAME," +
                        "TOTALROWCOUNT " +
                        "from VEW_STOCK_TR_DATA_TABLE WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query += "and ( (lower(STOCKTRANSFERCHALLANNUMBER) like lower(:SearchBy)  or upper(STOCKTRANSFERCHALLANNUMBER)like upper(:SearchBy) )" +
                       "or (lower(TRANSFERDATE) like lower(:SearchBy)  or upper(TRANSFERDATE)like upper(:SearchBy) )" +
                       "or (lower(TRANSFEREDBYNAME) like lower(:SearchBy)  or upper(TRANSFEREDBYNAME)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEYN) like lower(:SearchBy)  or upper(RECEIVEYN)like upper(:SearchBy) )" +
                       "or (lower(TRANSFERSHOPIDTONAME) like lower(:SearchBy)  or upper(TRANSFERSHOPIDTONAME)like upper(:SearchBy) ) )";

                sql += "and ( (lower(STOCKTRANSFERCHALLANNUMBER) like lower(:SearchBy)  or upper(STOCKTRANSFERCHALLANNUMBER)like upper(:SearchBy) )" +
                       "or (lower(TRANSFERDATE) like lower(:SearchBy)  or upper(TRANSFERDATE)like upper(:SearchBy) )" +
                       "or (lower(TRANSFEREDBYNAME) like lower(:SearchBy)  or upper(TRANSFEREDBYNAME)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEYN) like lower(:SearchBy)  or upper(RECEIVEYN)like upper(:SearchBy) )" +
                       "or (lower(TRANSFERSHOPIDTONAME) like lower(:SearchBy)  or upper(TRANSFERSHOPIDTONAME)like upper(:SearchBy) ) )";
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

            var returnValue = (dt.Rows).Cast<DataRow>().Select(StockTransferModel.ConvertStockTransferModelForDataTable);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;
        }

        public async Task<IEnumerable<StockTransferItemModel>> ViewAllTransferInfoByStoreReceiveChallanNo(string transferId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":STOCK_TRANSFER_ID", OracleDbType.Varchar2, transferId, ParameterDirection.Input)
            };
            var query = "Select * from STOCK_TRANSFER_ITEM WHERE STOCK_TRANSFER_ID = :STOCK_TRANSFER_ID  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(StockTransferItemModel.ConvertStockTransferItemModel);
        }
    }
}