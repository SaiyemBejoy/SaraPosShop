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
    public class WareHouseDashboardManager : IWareHouseDashboardManager
    {
        private readonly IWareHouseDashboardRepository _repository;
        public WareHouseDashboardManager()
        {
            _repository = new WareHouseDashboardRepository();
        }

        public async Task<IEnumerable<HotSaleItemModel>> GetAllInfoForHotSale()
        {
            try
            {
                var data = await _repository.GetAllInfoForHotSale();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ItemInfoModel>> GetAllInfoForLowStock()
        {
            try
            {
                var data = await _repository.GetAllInfoForLowStock();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
