using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IStockTransferRepository
    {
        Task<ItemInfoModel> GetProductInfoByBarcode(string barcode);

        Task<string> GetMaxChallanNo();

        Task<string> GetRequisitionNo();

        Task<string> UpdateStockTransferModel(string challanNo);

        Task<string> UpdateDmgStockTransferModel(string damageChallanNo);

        Task<string> UpdateDamageTable(string challanNo);

        Task<string> UpdateStockTransferTable(TransferReturnProduct objTransferReturnProduct);

        Task<string> SaveAllTransferDataInWarehouse(StockTransferModel objStockTransferModel);

        Task<string> SaveAllTransferDataInShop(StockTransferModel objStockTransferModel);

        Task<string> SaveAllTransferDataItemInShop(StockTransferItemModel objStockTransferItemModel);

        Task<string> SaveDmgTransferDataInWarehouse(DamageTransferMain objDamageTransferMainModel);

        Task<DataTableAjaxPostModel> GetAllTransferChallanForDataTable(DataTableAjaxPostModel model);
        Task<IEnumerable<StockTransferItemModel>> ViewAllTransferInfoByStoreReceiveChallanNo(string transferId);

    }
}