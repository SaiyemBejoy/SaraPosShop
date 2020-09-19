using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IShopRequisitionManager
    {
        Task<IEnumerable<ShopToShopRequisitionModel>> ShopProductListByShopId(string shopId);
        Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllToRequisitionData(string shopId);
        Task<IEnumerable<ShopToShopRequisitionMainModel>> GetAllFromRequisitionData(string shopId);
        Task<ShopToShopDeliveryModel> GetAllDeliveryItemForReceive(string shopId, string requisitionNumber);

        Task<string> GetMaxShopRequisitionId();
        Task<string> ShopToShopReQuisitionDataSave(ShopToShopRequisitionMainModel objShopToShopRequisitionMainModel);
        Task<string> SaveAllShopToShopDeliveryData(ShopToShopDeliveryModel objShopToShopDeliveryModel);
        Task<string> SaveAllShopToShopReceiveData(ShopToShopReceiveMainModel objShopToShopReceiveMainModel);
    }
}
