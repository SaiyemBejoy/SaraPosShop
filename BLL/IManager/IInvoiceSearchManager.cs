using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;

namespace BLL.IManager
{
    public interface IInvoiceSearchManager
    {
        Task<DataTableAjaxPostModel> GetAllSaleInfoForDataTable(DataTableAjaxPostModel model);
        Task<string> SaveAllSaleInfoForDataTable();
        Task<DataTableAjaxPostModel> GetAllExchangeItemInfoForDataTable(DataTableAjaxPostModel model);
    }
}
