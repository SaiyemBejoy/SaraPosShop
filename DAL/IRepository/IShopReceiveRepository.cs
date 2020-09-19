using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IShopReceiveRepository
    {
        Task<IEnumerable<DeliveredProduct>> ViewAllDataByStoreReceiveChallanNo(string storeReceiveChallanNo);
        Task<IEnumerable<StoreReceiveItem>> ViewAllReceiveItemByStoreReceiveChallanNo(string storeReceiveId);
        Task<IEnumerable<TransferReturnProduct>> GetAllReturnChallanNumFromWarehouse(string shopId);
        Task<IEnumerable<ShopToShopExchangeMain>> GetAllShopToShopExData();
        Task<IEnumerable<ShopToShopExItem>> GetAllShopToShopExItemDataBYId(int storeReceiveId);
        Task<string> SaveStoreReceive(StoreReceiveModel objStoreReceiveModel);
        Task<string> SaveStoreReceiveEX(StoreReceiveModel objStoreReceiveModel);
        Task<string> SaveStoreReceiveForReturnrreceive(StoreReceiveModel objStoreReceiveModel);
        Task<string> SaveStoreReceiveItem(StoreReceiveItem objStoreReceiveItem);
        Task<string> UpdateWareHouseStoreDelivery(string deliveryNumber, int deliveryShop, string updateBy);
        Task<string> UpdateWareHouseStockTransfer(string stokTransferChallanNo, int receiveShop, string updateBy);
        Task<DataTableAjaxPostModel> GetAllReceiveChallanForDataTable(DataTableAjaxPostModel model);

        Task<string> UpdateExChangeStoreReceiveChallan(ShopToShopExchangeMain objShopToShopExchangeMain);

    }
}
