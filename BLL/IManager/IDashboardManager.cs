using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IDashboardManager
    {
        Task<DashboardModel> GetAllDashboardInfo();
        Task<IEnumerable<HotSaleAndLowStockModel>> GetAllLowStockInfo();
        Task<IEnumerable<HotSaleAndLowStockModel>> GetAllHotSaleInfo();
        Task<IEnumerable<CircularDiscountPromotionModel>> GetAllPromotionDateFromWarehouse(string shopId);
    }
}