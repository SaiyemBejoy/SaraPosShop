using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IShopRequisitionRepository
    {
        Task<IEnumerable<ShopToShopRequisitionModel>> ShopProductListByShopId(string shopId);

        Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllToRequisitionData(string shopId);

        Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllFromRequisitionData(string shopId);

        Task<ShopToShopDeliveryModel> GetAllDeliveryItemForReceive(string shopId, string requisitionNumber);

        Task<string> GetMaxShopRequisitionId();

        Task<string> ShopToShopReQuisitionDataSave(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel);

        Task<string> SaveAllShopToShopDeliveryData(ShopToShopDeliveryModel objShopToShopDeliveryModel);

        Task<string> ShopToShopDeliveryDataSaveInShop(ShopToShopDeliveryModel objShopToShopDeliveryModel);

        Task<string> SaveAllDeliveryItemInShop(ShopToShopRequDeliveryItemModel objShopToShopRequDeliveryItemModel);

        Task<string> SaveAllShopToShopReceiveData(ShopToShopReceiveMainModel objShopToShopReceiveMainModel);

        Task<string> SaveShopToShopReceiveItem(ShopToShopReceiveItemModel objShopToShopReceiveItemModel);

        Task<string> UpdateShopToShopRequAndDelivery(ShopToShopReceiveMainModel objShopToShopReceiveMainModel);

        Task<string> UpdateShopToShopRequForDeliveryStatus(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel);

        Task<string> SaveAllRequisitionDataInShop(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel);

        Task<string> SaveAllRequisitionItemDataInShop(ShopToShopRequisitionMainItemModel objShopToShopRequisitionMainItemModel);
    }
}