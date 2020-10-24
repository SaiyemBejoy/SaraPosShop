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
    public class ShopRequisitionRepository : ApplicationDbContext, IShopRequisitionRepository
    {
        public async Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllFromRequisitionData(string shopId)
        {
            IEnumerable<ShopToShopRequisitionMainModel> model = new List<ShopToShopRequisitionMainModel>();
            if (!string.IsNullOrWhiteSpace(shopId))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("ShopToShopRequisition?toShopId=" + shopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<ShopToShopRequisitionMainModel>>();
                        readTask.Wait();

                        model = readTask.Result;
                    }
                }
            }
            return model;
        }

        public async Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllToRequisitionData(string shopId)
        {
            IEnumerable<ShopToShopRequisitionMainModel> model = new List<ShopToShopRequisitionMainModel>();
            if (!string.IsNullOrWhiteSpace(shopId))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("ShopToShopRequisition?fromShopId=" + shopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<ShopToShopRequisitionMainModel>>();
                        readTask.Wait();

                        model = readTask.Result;
                    }
                }
            }
            return model;
        }
        public async Task<ShopToShopDeliveryModel> GetAllDeliveryItemForReceive(string shopId, string requisitionNumber)
        {
            ShopToShopDeliveryModel model = new ShopToShopDeliveryModel();
            if (!string.IsNullOrWhiteSpace(shopId))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("ShopToShopDelivery?toShopId=" + shopId + "&requisitionNumber=" + requisitionNumber);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ShopToShopDeliveryModel>();
                        readTask.Wait();

                        model = readTask.Result;
                    }
                }
            }
            return model;
        }

        public async Task<string> GetMaxShopRequisitionId()
        {
            string query = "SELECT    TO_CHAR (SYSDATE, 'yy') " +
                           "|| LPAD(TO_CHAR(SYSDATE, 'MM'), 2, 0)" +
                           "|| LPAD(TO_CHAR(SYSDATE, 'dd'), 2, 0)" +
                           "|| LPAD((SELECT SHOP_ID FROM SHOP), 2, 0)" +
                           "|| (SELECT LPAD(NVL(MAX(SUBSTR((REQUISITION_NUMNER), 9, 8)), 0) + 1,8,0) " +
                           " FROM SHOP_SHOP_REQUISITION_MAIN)REQUISITION_NUMNER FROM DUAL";
            var maxRequisitionNumber = await GetSingleStringAsync(query, null);
            return maxRequisitionNumber;
            //ShopToShopRequisitionMainModel model = new ShopToShopRequisitionMainModel();
            //string maxRequisitionNumber = "";
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

            //    var responseTask = client.GetAsync("MaxRequisitionNumber");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<string>();
            //        readTask.Wait();
            //        model.RequisitionNumber = Convert.ToString(readTask.Result);
            //        maxRequisitionNumber = model.RequisitionNumber;

            //    }
            //}
        }

        public async Task<string> SaveAllShopToShopDeliveryData(ShopToShopDeliveryModel objShopToShopDeliveryModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "ShopToShopDelivery", objShopToShopDeliveryModel);
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

        public async Task<IEnumerable<ShopToShopRequisitionModel>> ShopProductListByShopId(string shopId)
        {
            IEnumerable<ShopToShopRequisitionModel> model = new List<ShopToShopRequisitionModel>();
            if (!string.IsNullOrWhiteSpace(shopId))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("ShopToShopRequisition?shopId=" + shopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<ShopToShopRequisitionModel>>();
                        readTask.Wait();

                        model = readTask.Result;
                    }
                }
            }
            return model;

        }

        public async Task<string> ShopToShopReQuisitionDataSave(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "ShopToShopRequisition", objShopToShopRequisitionMainModel);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    //return response.StatusCode.ToString();
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

        public async Task<string> SaveAllShopToShopReceiveData(ShopToShopReceiveMainModel objShopToShopReceiveMainModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SHOP_RECEIVE_ID", OracleDbType.Varchar2, objShopToShopReceiveMainModel.ShopToShopReceiveId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_RECEIVE_NUMBER", OracleDbType.Varchar2, objShopToShopReceiveMainModel.ShopToShopReceiveNumber, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_NUMBER", OracleDbType.Varchar2, objShopToShopReceiveMainModel.RequisitionNumber, ParameterDirection.Input),
                new OracleParameter(":P_DELIVERY_NUMBER", OracleDbType.Varchar2, objShopToShopReceiveMainModel.DeliveryNumber, ParameterDirection.Input),
                  new OracleParameter(":P_RECEIVED_BY", OracleDbType.Varchar2, objShopToShopReceiveMainModel.ReceivedBy, ParameterDirection.Input),
                new OracleParameter(":P_RECEIVE_SHOP_ID", OracleDbType.Varchar2, objShopToShopReceiveMainModel.ReceiveShopId, ParameterDirection.Input),
                 new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOPSHOP_RECEIVE_MAIN_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveShopToShopReceiveItem(ShopToShopReceiveItemModel objShopToShopReceiveItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_SHOP_RECEIVE_ITEM_ID", OracleDbType.Varchar2, objShopToShopReceiveItemModel.ShopToShopReceiveId, ParameterDirection.Input),
                new OracleParameter(":P_SHOP_RECEIVE_ID", OracleDbType.Varchar2, objShopToShopReceiveItemModel.ShopToShopReceiveId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objShopToShopReceiveItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objShopToShopReceiveItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objShopToShopReceiveItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objShopToShopReceiveItemModel.ItemName, ParameterDirection.Input),
                 new OracleParameter(":P_PRICE", OracleDbType.Varchar2, objShopToShopReceiveItemModel.Price, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objShopToShopReceiveItemModel.Quantity, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOP_TOSHOP_MAIN_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> UpdateShopToShopRequAndDelivery(ShopToShopReceiveMainModel objShopToShopReceiveMainModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync("ShopToShopRequiAndDeliveryUpdate", objShopToShopReceiveMainModel);
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

        public async Task<string> ShopToShopDeliveryDataSaveInShop(ShopToShopDeliveryModel objShopToShopDeliveryModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_DELIVERY_ID", OracleDbType.Varchar2, objShopToShopDeliveryModel.DeliveryId, ParameterDirection.Input),
                new OracleParameter(":P_DELIVERY_NUMNER", OracleDbType.Varchar2, objShopToShopDeliveryModel.DeliveryNumber, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_NUMNER", OracleDbType.Varchar2, objShopToShopDeliveryModel.RequisitionNumber, ParameterDirection.Input),
                new OracleParameter(":P_DELIVERY_SHOPID_TO", OracleDbType.Varchar2, objShopToShopDeliveryModel.DeliveryShopIdTo, ParameterDirection.Input),
                new OracleParameter(":P_DELIVERY_SHOPID_FROM", OracleDbType.Varchar2, objShopToShopDeliveryModel.DeliveryShopIdFrom, ParameterDirection.Input),
                new OracleParameter(":P_DELIVERED_BY", OracleDbType.Varchar2, objShopToShopDeliveryModel.DeliveredBy, ParameterDirection.Input),
                  new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOP_TO_SHOP_DELIVERY_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllDeliveryItemInShop(ShopToShopRequDeliveryItemModel objShopToShopRequDeliveryItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_DELIVERY_ITEM_ID", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.DeliveryId, ParameterDirection.Input),
                new OracleParameter(":P_DELIVERY_ID", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.DeliveryId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_PRICE", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.Price, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objShopToShopRequDeliveryItemModel.Quantity, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOP_SHOP_DELIVY_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllRequisitionDataInShop(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_ID", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.RequisitionId, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_NUMNER", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.RequisitionNumber, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_SHOPID_TO", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.RequisitionShopIdTo, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_SHOPID_FROM", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.RequisitionShopIdFrom, ParameterDirection.Input),
                new OracleParameter(":P_CREATED_BY", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.CreatedBy, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
              
            };
            query.Append("PRO_SHOP_SHOP_REQUI_MAIN_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveAllRequisitionItemDataInShop(ShopToShopRequisitionMainItemModel objShopToShopRequisitionMainItemModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_ITEM_ID", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.ShopRequisitionMainItemId, ParameterDirection.Input),
                new OracleParameter(":P_REQUISITION_ID", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.RequisitionId, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.ItemId, ParameterDirection.Input),
                new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.ProductId, ParameterDirection.Input),
                new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.Barcode, ParameterDirection.Input),
                new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.ItemName, ParameterDirection.Input),
                new OracleParameter(":P_PRICE", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.Price, ParameterDirection.Input),
                new OracleParameter(":P_QUANTITY", OracleDbType.Varchar2, objShopToShopRequisitionMainItemModel.Quantity, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOP_SHOP_REQUI_ITEM_SAVE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> UpdateShopToShopRequForDeliveryStatus(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_NUMBER", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.RequisitionNumber, ParameterDirection.Input),
                new OracleParameter(":P_TRANSFER_CHALLAN_NUMBER", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.TransferChallanNumber, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOP_DELIV_STATUS_UPDATE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> DeleteShopRequisitionChallanDC(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);

                HttpResponseMessage response = await client.PostAsJsonAsync("ShopRequisitionUpdate", objShopToShopRequisitionMainModel);
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

        public async Task<string> DeleteShopRequisitionChallan(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel)
        {

            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_REQUISITION_NUMBER", OracleDbType.Varchar2, objShopToShopRequisitionMainModel.RequisitionNumber, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };
            query.Append("PRO_SHOP_REQUI_MAIN_DELETE");

            return await ExecuteNonQueryAsync(query.ToString(), param);
        }
    }
}
