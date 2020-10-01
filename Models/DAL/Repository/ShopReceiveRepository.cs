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
    public class ShopReceiveRepository : ApplicationDbContext, IShopReceiveRepository
    {
        public async Task<string> SaveStoreReceive(StoreReceiveModel objStoreReceiveModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STORE_RECEIVE_ID", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceiveId, ParameterDirection.Input),
                new OracleParameter(":P_STORE_RECEIVE_CHALLAN_NO", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceiveChallanNo, ParameterDirection.Input),
                new OracleParameter(":P_SEASON_NAME", OracleDbType.Varchar2, objStoreReceiveModel.SeasonName, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_FROM", OracleDbType.Varchar2, objStoreReceiveModel.ReceiveFrom, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVED_BY", OracleDbType.Varchar2, objStoreReceiveModel.ReceivedBy, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_DATE", OracleDbType.Varchar2, objStoreReceiveModel.ReceivedDate, ParameterDirection.Input),
                new OracleParameter(":P_STORE_RECEIVE_YN", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceive_YN, ParameterDirection.Input),
                new OracleParameter(":P_WARE_HOUSE_ID", OracleDbType.Varchar2, objStoreReceiveModel.WareHouseId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objStoreReceiveModel.ShopId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_RECEIVE_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveStoreReceiveEX(StoreReceiveModel objStoreReceiveModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STORE_RECEIVE_ID", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceiveId, ParameterDirection.Input),
                new OracleParameter(":P_STORE_RECEIVE_CHALLAN_NO", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceiveChallanNo, ParameterDirection.Input),
                new OracleParameter(":P_SEASON_NAME", OracleDbType.Varchar2, objStoreReceiveModel.SeasonName, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_FROM", OracleDbType.Varchar2, objStoreReceiveModel.ReceiveFrom, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVED_BY", OracleDbType.Varchar2, objStoreReceiveModel.ReceivedBy, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_DATE", OracleDbType.Varchar2, objStoreReceiveModel.ReceivedDate, ParameterDirection.Input),
                new OracleParameter(":P_STORE_RECEIVE_YN", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceive_YN, ParameterDirection.Input),
                new OracleParameter(":P_WARE_HOUSE_ID", OracleDbType.Varchar2, objStoreReceiveModel.WareHouseId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objStoreReceiveModel.ShopId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_RECEIVE_SAVE_EX");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveStoreReceiveItem(StoreReceiveItem objStoreReceiveItem)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STORE_RECEIVE_ITEM_ID", OracleDbType.Varchar2, objStoreReceiveItem.StoreReceiveItemId, ParameterDirection.Input),
                new OracleParameter(":P_STORE_RECEIVE_ID", OracleDbType.Varchar2, objStoreReceiveItem.StoreReceiveId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objStoreReceiveItem.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objStoreReceiveItem.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objStoreReceiveItem.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objStoreReceiveItem.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_QUANTITY", OracleDbType.Varchar2, objStoreReceiveItem.ReceiveQuantity, ParameterDirection.Input),
                new OracleParameter(":P_SALE_PRICE", OracleDbType.Varchar2, objStoreReceiveItem.SalePrice, ParameterDirection.Input),
                new OracleParameter(":P_CATEGORY_NAME", OracleDbType.Varchar2, objStoreReceiveItem.CategoryName, ParameterDirection.Input),
                new OracleParameter(":P_SUB_CATEGORY_NAME", OracleDbType.Varchar2, objStoreReceiveItem.SubCategoryName, ParameterDirection.Input),
                new OracleParameter(":P_BRAND_NAME", OracleDbType.Varchar2, objStoreReceiveItem.BrandName, ParameterDirection.Input),
                new OracleParameter(":P_UMO", OracleDbType.Varchar2, objStoreReceiveItem.Umo, ParameterDirection.Input),
                new OracleParameter(":P_VAT", OracleDbType.Varchar2, objStoreReceiveItem.Vat, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_RECEIVE_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<IEnumerable<DeliveredProduct>> ViewAllDataByStoreReceiveChallanNo(string storeReceiveChallanNo)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":STORE_DELIVERY_NUMBER", OracleDbType.Varchar2, storeReceiveChallanNo, ParameterDirection.Input)
            };
            var query = "Select * from VEW_DELIVERED_PRODUCT WHERE STORE_DELIVERY_NUMBER = :STORE_DELIVERY_NUMBER ORDER BY PRODUCT_ID ASC  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(DeliveredProduct.ConvertDeliveredProduct);
        }

        public async Task<string> UpdateWareHouseStoreDelivery(string deliveryNumber, int deliveryShop, string updateBy)
        {
            var model = new
            {
                StoreDeliveryNumber = deliveryNumber,
                DeliveryShopId = deliveryShop,
                UpdateBy = updateBy
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "DeliveredProduct", model);
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

        public async Task<IEnumerable<TransferReturnProduct>> GetAllReturnChallanNumFromWarehouse(string shopId)
        {
            IEnumerable<TransferReturnProduct> model = new List<TransferReturnProduct>();
            if (!string.IsNullOrWhiteSpace(shopId))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("ShopToShopReturn?shopId=" + shopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<TransferReturnProduct>>();
                        readTask.Wait();
                        model = readTask.Result;
                    }
                }
            }
            return model;
        }

        public async Task<string> UpdateWareHouseStockTransfer(string stokTransferChallanNo, int receiveShop, string updateBy)
        {
            var model = new
            {
                StockTranferChallanNo = stokTransferChallanNo,
                TransferShopIdTo = receiveShop,
                ReceiveBy = updateBy
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "ShopToShopReturn", model);
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

        public async Task<string> SaveStoreReceiveForReturnrreceive(StoreReceiveModel objStoreReceiveModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_STORE_RECEIVE_ID", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceiveId, ParameterDirection.Input),
                new OracleParameter(":P_STORE_RECEIVE_CHALLAN_NO", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceiveChallanNo, ParameterDirection.Input),
                new OracleParameter(":P_SEASON_NAME", OracleDbType.Varchar2, objStoreReceiveModel.SeasonName, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_FROM", OracleDbType.Varchar2, objStoreReceiveModel.ReceiveFrom, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVED_BY", OracleDbType.Varchar2, objStoreReceiveModel.ReceivedBy, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_DATE", OracleDbType.Varchar2, objStoreReceiveModel.ReceivedDate, ParameterDirection.Input),
                new OracleParameter(":P_STORE_RECEIVE_YN", OracleDbType.Varchar2, objStoreReceiveModel.StoreReceive_YN, ParameterDirection.Input),
                new OracleParameter(":P_WARE_HOUSE_ID", OracleDbType.Varchar2, objStoreReceiveModel.WareHouseId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objStoreReceiveModel.ShopId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_RECEIVE_SAVE_FORRETU");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<DataTableAjaxPostModel> GetAllReceiveChallanForDataTable(DataTableAjaxPostModel model)
        {
            string sql = "SELECT COUNT(*) FROM VEW_STORE_RECEIVE_CHALLAN WHERE 1=1  ";

            var query = "Select " +
                        "ROW_NUMBER() OVER(ORDER BY StoreReceiveChallanNo) AS RN," +
                        "STORERECEIVEID, " +
                        "STORERECEIVECHALLANNO, " +
                        "RECEIVEFROM," +
                        "RECEIVEDBY," +
                        "RECEIVEDATE," +
                        "STORERECEIVEYN," +
                        "WAREHOUSEID," +
                        "SHOPID," +
                        "SEASONNAME," +
                        "TOTALROWCOUNT " +
                        "from VEW_STORE_RECEIVE_CHALLAN WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                query += "and ( (lower(STORERECEIVECHALLANNO) like lower(:SearchBy)  or upper(STORERECEIVECHALLANNO)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEFROM) like lower(:SearchBy)  or upper(RECEIVEFROM)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEDATE) like lower(:SearchBy)  or upper(RECEIVEDATE)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEDBY) like lower(:SearchBy)  or upper(RECEIVEDBY)like upper(:SearchBy) )" +
                       "or (lower(SEASONNAME) like lower(:SearchBy)  or upper(SEASONNAME)like upper(:SearchBy) ) )";

                sql += "and ( (lower(STORERECEIVECHALLANNO) like lower(:SearchBy)  or upper(STORERECEIVECHALLANNO)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEFROM) like lower(:SearchBy)  or upper(RECEIVEFROM)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEDATE) like lower(:SearchBy)  or upper(RECEIVEDATE)like upper(:SearchBy) )" +
                       "or (lower(RECEIVEDBY) like lower(:SearchBy)  or upper(RECEIVEDBY)like upper(:SearchBy) )" +
                       "or (lower(SEASONNAME) like lower(:SearchBy)  or upper(SEASONNAME)like upper(:SearchBy) ) )";
            }

            if (model.order != null)
            {
                query += "ORDER BY " + model.columns[model.order[0].column].data + " " + model.order[0].dir.ToUpper();
            }

            query = "SELECT * FROM (" + query + ") WHERE RN BETWEEN  '" + model.start + "' AND '" + (model.start + model.length) + "' ORDER BY STORERECEIVEID DESC  ";

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":SearchBy", OracleDbType.Varchar2, !string.IsNullOrWhiteSpace(model.search.value) ? $"%{model.search.value}%" : null, ParameterDirection.Input)
            };


            var count = await GetSingleIntAsync(sql, param);

            var dt = await GetDataThroughDataTableAsync(query, param);

            var returnValue = (dt.Rows).Cast<DataRow>().Select(StoreReceiveModel.ConvertStoreReceiveModelForDataTable);
            model.ListOfData = returnValue.ToList();
            model.recordsFiltered = count;
            model.recordsTotal = count;
            return model;
        }

        public async Task<IEnumerable<StoreReceiveItem>> ViewAllReceiveItemByStoreReceiveChallanNo(string storeReceiveId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":STORE_RECEIVE_ID", OracleDbType.Varchar2, storeReceiveId, ParameterDirection.Input)
            };
            var query = "Select * from VEW_STORE_RECEIVE_ITEM WHERE STORE_RECEIVE_ID = :STORE_RECEIVE_ID  ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return (dt.Rows).Cast<DataRow>().Select(StoreReceiveItem.ConvertStoreReceiveItemModel);
        }

        public async Task<IEnumerable<ShopToShopExchangeMain>> GetAllShopToShopExData()
        {
            var query = "Select * from VEW_SHOP_EX_REC_CHALLAN ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(ShopToShopExchangeMain.ConvertShopToShopExchangeMain);
        }

        public async Task<IEnumerable<ShopToShopExItem>> GetAllShopToShopExItemDataBYId(int storeReceiveId)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":STORE_RECEIVE_ID", OracleDbType.Varchar2, storeReceiveId, ParameterDirection.Input)
            };
            var query = "Select * from VEW_SHOP_EX_REC_CHALLAN_ITEM WHERE STORE_RECEIVE_ID = :STORE_RECEIVE_ID ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return ((dt.Rows).Cast<DataRow>().Select(ShopToShopExItem.ConvertShopToShopExItem));
        }

        public  async Task<string> UpdateExChangeStoreReceiveChallan(ShopToShopExchangeMain objShopToShopExchangeMain)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
              
                new OracleParameter(":P_STORE_RECEIVE_CHALLAN_NO", OracleDbType.Varchar2, objShopToShopExchangeMain.StoreReceiveChallanNo, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_STORE_RECEIVE_UPDATE_EX");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
