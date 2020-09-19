using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class StockTransferManager : IStockTransferManager
    {

        private readonly IStockTransferRepository _repository;
        public StockTransferManager()
        {
            _repository = new StockTransferRepository();
        }

        public async Task<DataTableAjaxPostModel> GetAllTransferChallanForDataTable(DataTableAjaxPostModel model)
        {
            try
            {
                model = await _repository.GetAllTransferChallanForDataTable(model);

                return model;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<string> GetMaxChallanNo()
        {
            try
            {
                var data = await _repository.GetMaxChallanNo();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ItemInfoModel> GetProductInfoByBarcode(string barcode)
        {
            try
            {
                var data = await _repository.GetProductInfoByBarcode(barcode);
                return data;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public  async Task<string> GetRequisitionNo()
        {
            try
            {
                var data = await _repository.GetRequisitionNo();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveDamageTransferData(DamageTransferMain objDamageTransferMainModel)
        {
            try
            {
                var data = await _repository.SaveDmgTransferDataInWarehouse(objDamageTransferMainModel);
                if (data == "OK")
                {
                    var challanNo = objDamageTransferMainModel.RequisitionNo;
                    var updateShop = await _repository.UpdateDamageTable(challanNo);
                }

                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveTransferData(StockTransferModel objStockTransferModel)
        {
            try
            {
                var data = await _repository.SaveAllTransferDataInWarehouse(objStockTransferModel);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    objStockTransferModel.StockTransferChallanNumber = data;
                    var shopDbSave = await _repository.SaveAllTransferDataInShop(objStockTransferModel);
                    if (Convert.ToInt32(shopDbSave) > 0)
                    {
                        foreach (var tableData in objStockTransferModel.StockTransferItemList)
                        {
                            tableData.StockTransferId = Convert.ToInt32(shopDbSave);
                            await _repository.SaveAllTransferDataItemInShop(tableData);
                        }
                        return data;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> UpdateDmgStockTransferModel(string damageChallanNo)
        {
            try
            {
                var data = await _repository.UpdateDmgStockTransferModel(damageChallanNo);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> UpdateStockTransferModel(string challanNo)
        {
            try
            {
                var data = await _repository.UpdateStockTransferModel(challanNo);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> UpdateStockTransferTable(TransferReturnProduct objTransferReturnProduct)
        {
            try
            {
                var data = await _repository.UpdateStockTransferTable(objTransferReturnProduct);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<StockTransferItemModel>> ViewAllTransferInfoByStoreReceiveChallanNo(string transferId)
        {
            try
            {
                var data = await _repository.ViewAllTransferInfoByStoreReceiveChallanNo(transferId);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
