using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;

namespace BLL.IManager
{
    public interface IStoreHouseRequestManager
    {
        Task<string> SaveAllData(StoreHouseRequestMain objStoreHouseRequestMain);
        Task<string> SubmitData(StoreHouseRequestMain objStoreHouseRequestMain);
        Task<IEnumerable<StoreHouseRequestMain>> StoreHouseRequestList();
        Task<IEnumerable<StoreHouseItemModel>> GetAllProductInfoByProductId(int productId);
        Task<StoreHouseItemModel> GetAllInfoByBarcode(string barcode);
        Task<IEnumerable<StoreHouseRequestItemModelSubmit>> GetAllInfoByRequisitionNo(string requisitionNo);
        Task<string> SaveAllStoreHouseData(StoreHouseModel objStoreHouseModel);
        Task<DataTableAjaxPostModel> StoreHouseProductSearch(DataTableAjaxPostModel model);
        Task<string> RemoveItem(string productBarcode, int storehouseId);
    }
}
