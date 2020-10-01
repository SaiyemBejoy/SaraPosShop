using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class ShopRequisitionManager : IShopRequisitionManager
    {
        private readonly IShopRequisitionRepository _repository;
        private readonly IPointOfSaleRepository _pointOfSaleRepository;

        public ShopRequisitionManager()
        {
            _repository = new ShopRequisitionRepository();
            _pointOfSaleRepository = new PointOfSaleRepository();
        }

        public async Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllFromRequisitionData(string shopId)
        {
            try
            {
                var data = await _repository.GetAllFromRequisitionData(shopId);
           
                foreach (var item in data)
                {
                    foreach (var itemData in item.ShopToShopRequisitionMainItemList)
                    {
                        itemData.ItemStock = await _pointOfSaleRepository.GetStockByItemId(itemData.ItemId);
                    }
                }

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllToRequisitionData(string shopId)
        {
            try
            {
                var data = await _repository.GetAllToRequisitionData(shopId);
                if (data.Count() > 0)
                {
                    foreach (var shopToShopRequisitionMainModel in data)
                    {
                        if (shopToShopRequisitionMainModel.DeliveryStatus == "Y")
                        {
                            var updateReqForDeliveryYN = _repository.UpdateShopToShopRequForDeliveryStatus(shopToShopRequisitionMainModel);
                        }
                    }
                }
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ShopToShopDeliveryModel> GetAllDeliveryItemForReceive(string shopId, string requisitionNumber)
        {
            try
            {
                var data = await _repository.GetAllDeliveryItemForReceive(shopId,requisitionNumber);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetMaxShopRequisitionId()
        {
            try
            {

                var data = await _repository.GetMaxShopRequisitionId();
                //var concateValue =string.Concat(shopId,data);
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SaveAllShopToShopDeliveryData(ShopToShopDeliveryModel objShopToShopDeliveryModel)
        {
            string saveMessage = "";
            try
            {
                var data = await _repository.SaveAllShopToShopDeliveryData(objShopToShopDeliveryModel);
                if (data =="OK")
                {
                    var shopDbSave = await _repository.ShopToShopDeliveryDataSaveInShop(objShopToShopDeliveryModel);
                    foreach (var tableData in objShopToShopDeliveryModel.ShopToShopRequDeliveryItemModelList)
                    {
                        tableData.DeliveryId = Convert.ToInt32(shopDbSave);
                        saveMessage = await _repository.SaveAllDeliveryItemInShop(tableData);
                    }
                    return saveMessage;
                }

                    return saveMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveAllShopToShopReceiveData(ShopToShopReceiveMainModel objShopToShopReceiveMainModel)
        {
            string returnMessage = "";
            try
            {
                var data = await _repository.SaveAllShopToShopReceiveData(objShopToShopReceiveMainModel);
                var shopToShopReceiveId = Convert.ToInt32(data);
                if (shopToShopReceiveId != 0)
                {
                    foreach (var tableData in objShopToShopReceiveMainModel.ShopToShopShopToShopReceiveItemList)
                    {
                        tableData.ShopToShopReceiveId = shopToShopReceiveId;
                        returnMessage = await _repository.SaveShopToShopReceiveItem(tableData);
                    }

                    if (returnMessage != null)
                    {
                        var message = await _repository.UpdateShopToShopRequAndDelivery(objShopToShopReceiveMainModel);
                    }
                    return returnMessage;

                }


                return "Data Already Exist";
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ShopToShopRequisitionModel>> ShopProductListByShopId(string shopId)
        {
            try
            {
                var data = await _repository.ShopProductListByShopId(shopId);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> ShopToShopReQuisitionDataSave(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel)
        {
            try
            {
                var data = await _repository.ShopToShopReQuisitionDataSave(objShopToShopRequisitionMainModel);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    objShopToShopRequisitionMainModel.RequisitionNumber = data;
                    var shopDbSave = await _repository.SaveAllRequisitionDataInShop(objShopToShopRequisitionMainModel);
                    if (Convert.ToInt32(shopDbSave) > 0)
                    {
                        foreach (var tableData in objShopToShopRequisitionMainModel.ShopToShopRequisitionMainItemList)
                        {
                            tableData.RequisitionId = Convert.ToInt32(shopDbSave);
                            var shopItemSave =await _repository.SaveAllRequisitionItemDataInShop(tableData);
                        }
                        return data;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
