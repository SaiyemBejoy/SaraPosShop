using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface ITransitProductRepository
    {
        Task<string> SaveAllTransitProductData(TransitProductModel objStockTransferModel);

        Task<string> SaveAllTransitProductItem(TransitProductItem objTransitProductItem);

        Task<string> DeleteAllTransitProductData(string ChallanNumber);

        Task<IEnumerable<TransitProductModel>> GetAllData();
    }
}
