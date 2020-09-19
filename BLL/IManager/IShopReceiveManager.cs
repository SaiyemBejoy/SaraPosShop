using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IShopReceiveManager
    {
        Task<IEnumerable<DeliveredProduct>> ViewAllDataByStoreReceiveChallanNo(string storeReceiveChallanNo);
        Task<IEnumerable<StoreReceiveItem>> ViewAllReceiveItemByStoreReceiveChallanNo(string storeReceiveId);
        Task<IEnumerable<TransferReturnProduct>> GetAllReturnChallanNumFromWarehouse(string shopId);
        Task<IEnumerable<ShopToShopExchangeMain>> GetAllShopToShopExData();
        Task<string> Save(StoreReceiveModel objStoreReceiveModel);
        Task<string> UpdateExChangeStoreReceiveChallan(ShopToShopExchangeMain objShopToShopExchangeMain);
        Task<DataTableAjaxPostModel> GetAllReceiveChallanForDataTable(DataTableAjaxPostModel model);
    }
}