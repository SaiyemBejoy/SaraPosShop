using System.Threading.Tasks;
using Models.AllModel;

namespace DAL.IRepository
{
    public interface IInvoiceSearchRepository
    {
        Task<DataTableAjaxPostModel> GetAllSaleInfoForDataTable(DataTableAjaxPostModel model);
        Task<string> SaveAllSaleInfoForDataTable();
        Task<DataTableAjaxPostModel> GetAllExchangeItemInfoForDataTable(DataTableAjaxPostModel model);
    }
}