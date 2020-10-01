using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class DashboardManager : IDashboardManager
    {
        private readonly IDashboardRepository _repository;
        public DashboardManager()
        {
            _repository = new DashboardRepository();
        }

        public async Task<DashboardModel> GetAllDashboardInfo()
        {
            try
            {
                var data = await _repository.GetAllDashboardInfo();
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<HotSaleAndLowStockModel>> GetAllHotSaleInfo()
        {
            try
            {
                var data = await _repository.GetAllHotSaleInfo();
                var data2 = data.Take(20).ToList();
                return data2;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<HotSaleAndLowStockModel>> GetAllLowStockInfo()
        {
            try
            {
                var data = await _repository.GetAllLowStockInfo();
                data = data.ToList();
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<CircularDiscountPromotionModel>> GetAllPromotionDateFromWarehouse(string shopId)
        {
            try
            {
                var data = await _repository.GetAllPromotionDateFromWarehouse(shopId);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
