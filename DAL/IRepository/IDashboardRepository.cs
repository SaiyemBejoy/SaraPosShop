using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IDashboardRepository
    {
        Task<DashboardModel> GetAllDashboardInfo();
        Task<IEnumerable<HotSaleAndLowStockModel>> GetAllLowStockInfo();
        Task<IEnumerable<HotSaleAndLowStockModel>> GetAllHotSaleInfo();
        Task<IEnumerable<CircularDiscountPromotionModel>> GetAllPromotionDateFromWarehouse(string shopId);
    }
}