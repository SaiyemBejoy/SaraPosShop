using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class ShopReceiveManager : IShopReceiveManager
    {
        private readonly IShopReceiveRepository _repository;

        public ShopReceiveManager()
        {
            _repository = new ShopReceiveRepository();
        }

        public async Task<DataTableAjaxPostModel> GetAllReceiveChallanForDataTable(DataTableAjaxPostModel model)
        {
            try
            {
                model = await _repository.GetAllReceiveChallanForDataTable(model);

                return model;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<TransferReturnProduct>> GetAllReturnChallanNumFromWarehouse(string shopId)
        {
            try
            {
                var data = await _repository.GetAllReturnChallanNumFromWarehouse(shopId);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public  async Task<IEnumerable<ShopToShopExchangeMain>> GetAllShopToShopExData()
        {
            try
            {
                var data = await _repository.GetAllShopToShopExData();
                var shopToshopExData = data.ToList();
                if (shopToshopExData.Count > 0)
                {
                    foreach (var shopToshopExDataModel in shopToshopExData)
                    {
                        shopToshopExDataModel.ShopToShopExItemList = await _repository.GetAllShopToShopExItemDataBYId(shopToshopExDataModel.StoreReceiveId);
                    }
                }
                return shopToshopExData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Save(StoreReceiveModel objStoreReceiveModel)
        {
            string returnMessage = "";
            string  updateWareHouse = "";
            try
            {
                
                if (objStoreReceiveModel.ReturnChallanNoReceive == "0")
                {
                     updateWareHouse = await _repository.UpdateWareHouseStoreDelivery(
                        objStoreReceiveModel.StoreReceiveChallanNo, Convert.ToInt32(objStoreReceiveModel.ShopId),
                        objStoreReceiveModel.ReceivedBy);
                }
                else
                {
                    updateWareHouse = await _repository.UpdateWareHouseStockTransfer(
                        objStoreReceiveModel.ReturnChallanNoReceive, Convert.ToInt32(objStoreReceiveModel.ShopId),
                        objStoreReceiveModel.ReceivedBy);
                    if (updateWareHouse == "OK")
                    {
                        var data = await _repository.SaveStoreReceiveForReturnrreceive(objStoreReceiveModel);
                        var storeReceiveId = Convert.ToInt32(data);
                        if (storeReceiveId != 0)
                        {
                            foreach (var tableData in objStoreReceiveModel.StoreReceiveItemList)
                            {
                                tableData.StoreReceiveId = storeReceiveId;
                                returnMessage = await _repository.SaveStoreReceiveItem(tableData);
                            }

                            return returnMessage;
                        }
                    }
                }
                if (updateWareHouse == "OK")
                {
                    var data = await _repository.SaveStoreReceive(objStoreReceiveModel);
                    var storeReceiveId = Convert.ToInt32(data);
                    if (storeReceiveId != 0)
                    {
                        foreach (var tableData in objStoreReceiveModel.StoreReceiveItemList)
                        {
                            tableData.StoreReceiveId = storeReceiveId;
                            returnMessage = await _repository.SaveStoreReceiveItem(tableData);
                        }

                        return returnMessage;
                    }
                }
                
                return "something went wrong";
            }
            catch (Exception e)
            {
                return "0";
            }
        }

        public async Task<string> UpdateExChangeStoreReceiveChallan(ShopToShopExchangeMain objShopToShopExchangeMain)
        {
            try
            {
                var data = await _repository.UpdateExChangeStoreReceiveChallan(objShopToShopExchangeMain);
                return data;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<IEnumerable<DeliveredProduct>> ViewAllDataByStoreReceiveChallanNo(string storeReceiveChallanNo)
        {
            try
            {
                var test = await _repository.ViewAllDataByStoreReceiveChallanNo(storeReceiveChallanNo);
                return test;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<StoreReceiveItem>> ViewAllReceiveItemByStoreReceiveChallanNo(string storeReceiveId)
        {
            try
            {
                var data = await _repository.ViewAllReceiveItemByStoreReceiveChallanNo(storeReceiveId);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
