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
    public class StoreHouseRequestManager : IStoreHouseRequestManager
    {
        private readonly IStoreHouseRequestRepository _repository;

        public StoreHouseRequestManager()
        {
            _repository = new StoreHouseRequestRepository();
        }

        public async Task<StoreHouseItemModel> GetAllInfoByBarcode(string barcode)
        {
            try
            {
                var data = await _repository.GetAllInfoByBarcode(barcode);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<StoreHouseRequestItemModelSubmit>> GetAllInfoByRequisitionNo(string requisitionNo)
        {
            try
            {
                List<StoreHouseRequestItemModelSubmit> CheckItemList = new List<StoreHouseRequestItemModelSubmit>();

                var data = await _repository.GetAllInfoByRequisitionNo(requisitionNo);
                var list = data.ToList();
                foreach (var value in list)
                {
                    var result = await _repository.GetAllLineRopeRackList(value.Barcode);
                    var LineRopeRackList = result.ToList();
                    StoreHouseRequestItemModelSubmit item = new StoreHouseRequestItemModelSubmit
                    {
                        Barcode = value.Barcode,
                        ItemName = value.ItemName,
                        Quantity = value.Quantity,
                        LineRopeRackList = LineRopeRackList
                    };
                    CheckItemList.Add(item);
                }
                return CheckItemList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<StoreHouseItemModel>> GetAllProductInfoByProductId(int productId)
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
        
        public async Task<string> SaveAllData(StoreHouseRequestMain objStoreHouseRequestMain)
        {
            string returnMessage = "";
            try
            {
                var data = await _repository.SaveAllData(objStoreHouseRequestMain);
                if (data != null)
                {
                    foreach (var tableData in objStoreHouseRequestMain.StoreHouseRequestMainItemList)
                    {
                        tableData.RequisitionNo = data;
                        returnMessage = await _repository.SaveAllRequestItem(tableData);
                    }
                }
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<string> SaveAllStoreHouseData(StoreHouseModel objStoreHouseModel)
        {
            string returnMessage = "";
            try
            {
                var data = await _repository.SaveAllStoreHouseMainData(objStoreHouseModel);
                if (data != null)
                {
                    foreach (var tableData in objStoreHouseModel.StoreHouseItemModelList)
                    {
                        tableData.StoreHouseId = Convert.ToInt32(data);
                        returnMessage = await _repository.SaveStoreHouseItemData(tableData);
                    }
                }
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<DataTableAjaxPostModel> StoreHouseProductSearch(DataTableAjaxPostModel model)
        {
            try
            {
                model = await _repository.StoreHouseProductSearch(model);
                return model;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<StoreHouseRequestMain>> StoreHouseRequestList()
        {
            try
            {
                var data = await _repository.StoreHouseRequestList();
                var storeHouseRequestMainModel = data.OrderByDescending(c => c.RequisitionNo).ToList();
                if (storeHouseRequestMainModel.Count > 0)
                {
                    foreach (var storeHouseRequestMain in storeHouseRequestMainModel)
                    {
                        storeHouseRequestMain.StoreHouseRequestMainItemList =
                            await _repository.GetAllItemInfoDataByRequisitionNo(storeHouseRequestMain.RequisitionNo);

                    }
                }

                return storeHouseRequestMainModel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SubmitData(StoreHouseRequestMain objStoreHouseRequestMain)
        {
            string returnMessage = "";
            try
            {
                returnMessage = await _repository.SubmitData(objStoreHouseRequestMain);
                if(returnMessage == "UPDATE SUCCESS")
                {
                    foreach (var item in objStoreHouseRequestMain.StoreHouseRequestMainItemConfirmList)
                    {
                        item.RequisitionNo = objStoreHouseRequestMain.RequisitionNo;
                        var result = await _repository.SubmitRequestItemData(item);
                    }
                }
                
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<string> RemoveItem(string productBarcode, int storehouseId)
        {
            string returnMessage = "";
            try
            {
                returnMessage = await _repository.RemoveItem(productBarcode, storehouseId);
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

    }
}
