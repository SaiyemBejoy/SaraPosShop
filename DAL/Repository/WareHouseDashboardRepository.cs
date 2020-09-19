using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.Repository
{
    public class WareHouseDashboardRepository: ApplicationDbContext, IWareHouseDashboardRepository
    {
        public async Task<IEnumerable<HotSaleItemModel>> GetAllInfoForHotSale()
        {
            var query = "Select * from VEW_HOT_SALE_ITEM WHERE ROWNUM <=20 ORDER BY QUANTITY DESC";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(HotSaleItemModel.ConvertHotSaleItemModel);
        }

        public async Task<IEnumerable<ItemInfoModel>> GetAllInfoForLowStock()
        {
            var query = "Select * from VEW_ITEM_INFO WHERE QUANTITY <=5";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(ItemInfoModel.ConvertItemInfoModel);
        }

    }
}
