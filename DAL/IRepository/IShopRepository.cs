using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IShopRepository
    {
        Task<ShopModel> GetShopInfo();
        Task<string> SaveShopInfo(ShopModel objShopModel);
        Task<string> UpdateWareHouse(ShopModel objShopModel);
    }
}
