using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class DashboardRepository : ApplicationDbContext, IDashboardRepository
    {
        public async Task<DashboardModel> GetAllDashboardInfo()
        {
            var query = "Select * from VEW_DASHBOARD_SALE_INFO ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return DashboardModel.ConvertDashboardModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<HotSaleAndLowStockModel>> GetAllHotSaleInfo()
        {
            var query = "Select * from VEW_HOT_SALE_ITEM  WHERE ROWNUM <= 20 ORDER BY QUANTITY DESC ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(HotSaleAndLowStockModel.ConvertHotSaleModel);
        }

        public async Task<IEnumerable<HotSaleAndLowStockModel>> GetAllLowStockInfo()
        {
            var query = "Select * from VEW_ITEM_INFO where ROWNUM <= 20 AND QUANTITY <= 2 ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(HotSaleAndLowStockModel.ConvertHotSaleAndLowStockModel);
        }

        public async Task<IEnumerable<CircularDiscountPromotionModel>> GetAllPromotionDateFromWarehouse(string shopId)
        {
            IEnumerable<CircularDiscountPromotionModel> model = new List<CircularDiscountPromotionModel>();
            if (!string.IsNullOrWhiteSpace(shopId))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(DatabaseConfiguration.WarehouseApi);

                    var responseTask = client.GetAsync("CircularDiscountPromotion?shopId=" + shopId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<CircularDiscountPromotionModel>>();
                        readTask.Wait();
                        model = readTask.Result;
                    }
                }
            }
            return model;
        }
    }
}