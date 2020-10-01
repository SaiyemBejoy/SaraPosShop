using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Manager
{
    public class VoidInvoiceManager : IVoidInvoiceManager
    {
        private readonly IVoidInvoiceRepository _repository;

        public VoidInvoiceManager()
        {
            _repository = new VoidInvoiceRepository();
        }

        public async Task<DataTableAjaxPostModel> GetAllVoidInvoiceDataTable(DataTableAjaxPostModel model)
        {
            try
            {
                model = await _repository.GetAllVoidInvoiceDataTable(model);

                return model;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<VoidInvoiceItem>> ViewAllVoidInvoiceItemBySaleInfoId(string saleInfoId)
        {
            try
            {
                var data = await _repository.ViewAllVoidInvoiceItemBySaleInfoId(saleInfoId);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
