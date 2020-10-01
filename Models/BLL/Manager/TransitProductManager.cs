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
    public class TransitProductManager : ITransitProductManager
    {
        private readonly ITransitProductRepository _repository;
        public TransitProductManager()
        {
            _repository = new TransitProductRepository();
        }

        public async Task<string> SaveTransitData(TransitProductModel objTransitProductModel)
        {
            try
            {
                var shopDbSave = "";
                //var data = await _repository.SaveAllTransferDataInWarehouse(objStockTransferModel);
                //if (!string.IsNullOrWhiteSpace(data))
                //{
                //objTransitProductModel.StockTransferChallanNumber = data;
                try
                {
                    shopDbSave = await _repository.SaveAllTransitProductData(objTransitProductModel);
                    if (!string.IsNullOrWhiteSpace(shopDbSave))
                    {
                        foreach (var tableData in objTransitProductModel.TransitProductItemList)
                        {
                            tableData.TransitChallnNo = shopDbSave;
                            var itemSave = await _repository.SaveAllTransitProductItem(tableData);
                        }
                        return shopDbSave;
                    }
                }
                catch (Exception e)
                {
                    shopDbSave = await _repository.DeleteAllTransitProductData(shopDbSave);
                }
                    
                //}

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TransitProductModel>> ViewAllData()
        {
            try
            {
                var data = await _repository.GetAllData();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region "Transit Return"
        public async Task<string> SaveTransitReturnData(TransitProductReturnModel objTransitProductReturnModel)
        {
            try
            {
                var shopDbSave = "";
                try
                {
                    shopDbSave = await _repository.SaveAllTransitProductReturnData(objTransitProductReturnModel);
                    if (!string.IsNullOrWhiteSpace(shopDbSave))
                    {
                        foreach (var tableData in objTransitProductReturnModel.TransitProductReturnItemList)
                        {
                            tableData.TransitReturnChallnNo = shopDbSave;
                            var itemSave = await _repository.SaveAllTransitProductReturnItem(tableData);
                        }
                        return shopDbSave;
                    }
                }
                catch (Exception e)
                {
                    shopDbSave = await _repository.DeleteAllTransitReturnProductData(shopDbSave);
                }

                //}

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TransitProductReturnModel>> ViewAllReturnData()
        {
            try
            {
                var data = await _repository.GetAllReturnData();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
