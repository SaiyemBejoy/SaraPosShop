using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IStockTransferManager
    {
        Task<ItemInfoModel> GetProductInfoByBarcode(string barcode);

        Task<string> GetMaxChallanNo();

        Task<string> GetRequisitionNo();

        Task<string> UpdateStockTransferModel(string challanNo);

        Task<string> UpdateDmgStockTransferModel(string damageChallanNo);

        Task<string> UpdateStockTransferTable(TransferReturnProduct objTransferReturnProduct);

        Task<string> SaveTransferData(StockTransferModel objStockTransferModel);

        Task<string> SaveDamageTransferData(DamageTransferMain objDamageTransferMainModel);

        Task<DataTableAjaxPostModel> GetAllTransferChallanForDataTable(DataTableAjaxPostModel model);

        Task<IEnumerable<StockTransferItemModel>> ViewAllTransferInfoByStoreReceiveChallanNo(string transferId);

    }
}