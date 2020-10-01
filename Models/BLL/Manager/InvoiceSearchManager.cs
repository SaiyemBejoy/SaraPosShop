using System;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;

namespace BLL.Manager
{
    public class InvoiceSearchManager : IInvoiceSearchManager
    {
        private readonly IInvoiceSearchRepository _repository;
        public InvoiceSearchManager()
        {
            _repository = new InvoiceSearchRepository();
        }

        public async Task<DataTableAjaxPostModel> GetAllSaleInfoForDataTable(DataTableAjaxPostModel model)
        {
            try
            {
                model = await _repository.GetAllSaleInfoForDataTable(model);

                return model;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<string> SaveAllSaleInfoForDataTable()
        {
            try
            {
                var test = await _repository.SaveAllSaleInfoForDataTable();
                return test;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<DataTableAjaxPostModel> GetAllExchangeItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            try
            {
                model = await _repository.GetAllExchangeItemInfoForDataTable(model);

                return model;
            }
            catch (Exception e)
            {

                return null;
            }
        }
    }
}