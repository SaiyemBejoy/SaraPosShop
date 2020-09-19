using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IShopManager
    {
        Task<ShopModel> GetShopInfo();
       Task<string> SaveShopInfo(ShopModel objShopModel);
    }
}