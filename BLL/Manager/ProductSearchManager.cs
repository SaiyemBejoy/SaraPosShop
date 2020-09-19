using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;

namespace BLL.Manager
{
    public class ProductSearchManager: IProductSearchManager
    {
        private readonly IProductSearchRepository _repository;
        public ProductSearchManager()
        {
            _repository = new ProductSearchRepository();
        }

        public async Task<StockCalculateSummary> GetAllCalculateSummaryInfo()
        {
            try
            {
                var data = await _repository.GetAllCalculateSummaryInfo();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductSearchModel>> GetAllInfoBySearchTextBoxValue(string searchValue)
        {
            try
            {
                var data = await _repository.GetAllInfoBySearchTextBoxValue(searchValue);
                var list = data.ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DataTableAjaxPostModel> GetAllItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            try
            {
                model = await _repository.GetAllItemInfoForDataTable(model);

                return model;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<ProductSearchModel>> GetAllProductInfoByProductId(int productId)
        {
            try
            {
                var data = await _repository.GetAllProductInfoByProductId(productId);
                var list = data.ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetReceiveItemCount()
        {
            try
            {
                var data = await _repository.GetReceiveItemCount();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetSaleItemCount()
        {
            try
            {
                var data = await _repository.GetSaleItemCount();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetTotalItemCount()
        {
            try
            {
                var data = await _repository.GetTotalItemCount();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
