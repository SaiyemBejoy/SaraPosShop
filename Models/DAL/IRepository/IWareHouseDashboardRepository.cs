using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IWareHouseDashboardRepository
    {

        Task<IEnumerable<ItemInfoModel>> GetAllInfoForLowStock();

        Task<IEnumerable<HotSaleItemModel>> GetAllInfoForHotSale();
    }
}
