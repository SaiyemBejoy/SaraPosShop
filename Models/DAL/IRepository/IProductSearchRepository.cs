using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;

namespace DAL.IRepository
{
    public interface IProductSearchRepository
    {
        Task<DataTableAjaxPostModel> GetAllItemInfoForDataTable(DataTableAjaxPostModel model);
        Task<IEnumerable<ProductSearchModel>> GetAllProductInfoByProductId(int productId);
        Task<IEnumerable<ProductSearchModel>> GetAllInfoBySearchTextBoxValue(string searchValue);
        Task<StockCalculateSummary> GetAllCalculateSummaryInfo();
        Task<string> GetTotalItemCount();
        Task<string> GetSaleItemCount();
        Task<string> GetReceiveItemCount();
    }
}