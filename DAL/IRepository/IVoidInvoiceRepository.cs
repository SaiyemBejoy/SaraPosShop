using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IVoidInvoiceRepository
    {
        Task<DataTableAjaxPostModel> GetAllVoidInvoiceDataTable(DataTableAjaxPostModel model);
        Task<IEnumerable<VoidInvoiceItem>> ViewAllVoidInvoiceItemBySaleInfoId(string saleInfoId);
    }
}
