using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IStoreHouseRequestRepository
    {
        Task<string> SaveAllData(StoreHouseRequestMain objStoreHouseRequestMain);
        Task<string> SubmitData(StoreHouseRequestMain objStoreHouseRequestMain);
        Task<string> SubmitRequestItemData(StoreHouseRequestMainItemConfirm objStoreHouseRequestMainItemConfirm);
        Task<string> SaveAllRequestItem(StoreHouseRequestMainItem obStoreHouseRequestMainItem);
        Task<IEnumerable<StoreHouseRequestMain>> StoreHouseRequestList();
        Task<IEnumerable<StoreHouseRequestMainItem>> GetAllItemInfoDataByRequisitionNo(string requisitionNo);
        Task<IEnumerable<StoreHouseItemModel>> GetAllProductInfoByProductId(int productId);
        Task<IEnumerable<StoreHouseRequestItemModelSubmit>> GetAllInfoByRequisitionNo(string requisitionNo);
        Task<string> SaveAllStoreHouseMainData(StoreHouseModel objStoreHouseModel);
        Task<string> SaveStoreHouseItemData(StoreHouseItemModel objStoreHouseItemModel);
        Task<DataTableAjaxPostModel> StoreHouseProductSearch(DataTableAjaxPostModel model);
        Task<StoreHouseItemModel> GetAllInfoByBarcode(string barcode);
        Task<IEnumerable<StoreHouseModel>> GetAllLineRopeRackList(string barcode);
        Task<string> RemoveItem(string productBarcode, int storehouseId);
    }
}
