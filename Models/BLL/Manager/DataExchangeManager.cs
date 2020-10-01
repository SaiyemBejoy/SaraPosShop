using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.Manager
{
    public class DataExchangeManager : IDataExchangeManager
    {
        private readonly IDataExchangeRepository _repository;

        public DataExchangeManager()
        {
            _repository = new DataExchangeRepository();
        }

        public async Task<string> GetAllGiftVoucherDeliveryAndSave()
        {
            var  rtnMessage = "";
            try
            {
                var data = await _repository.GetAllGiftVoucherDelivery();
                if (data != null)
                {
                    foreach (var giftVoucherDeliveryModel in data)
                    {
                        rtnMessage = await _repository.SaveGiftVoucherData(giftVoucherDeliveryModel);
                    } 
                }
                return rtnMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<SalesManWiseSaleReportForDc>> GetAllSalesManWiseSaleDataForDc(string fromDate, string toDate)
        {
            try
            {
                var data = await _repository.GetAllSalesManWiseSaleDataForDc(fromDate, toDate);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<GiftVoucherModel> giftVoucherInfoByCode(string giftVoucherCode)
        {
            try
            {
                var data = await _repository.giftVoucherInfoByCode(giftVoucherCode);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveCustomerInfoData(CustomerModel objCustomerModel)
        {
            try
            {
                var customer = await _repository.SaveCustomerInfoData(objCustomerModel);
                return customer;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        //public async Task<string> SaveData(IEnumerable<DeliveredProduct> objDeliveredProduct)
        //{
        //    try
        //    {
        //        var test = await _repository.SaveData(objDeliveredProduct);
        //        return test;
        //    }
        //    catch (Exception)
        //    {
        //        return "something went wrong";
        //    }
        //}

        public async Task<string> SaveData(DeliveredProduct objDeliveredProduct)
        {
            try
            {
                var product = await _repository.SaveData(objDeliveredProduct);
                return product;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<string> SaveGiftVoucherDepositData(GiftVoucherModel objGiftVoucherModel)
        {
            try
            {
                var giftVoucherDeposit = await _repository.SaveGiftVoucherDepositData(objGiftVoucherModel);
                var giftVoucherUpdateDateFromDC = await GetAllGiftVoucherDeliveryAndSave();
                return giftVoucherDeposit;
            }
            catch (Exception e)
            {
                return "something went wrong";
            }
        }

        public async Task<string> SavePrivilegeCardDataInWarehouse(PrivilegeCustomerModel objPrivilegeCustomerModel)
        {
            try
            {
               
                var customer = await _repository.SavePrivilegeCardDataInWarehouse(objPrivilegeCustomerModel);
                return customer;
            }
            catch (Exception e)
            {
                return "something went wrong";
            }
        }

        public async Task<string> SavePromotionData(List<CircularDiscountPromotionModel> objCircularDiscountPromotionModel,string shopId)
        {
            try
            {
                var product = "";
                var cId = new List<int>();
                foreach (var model in objCircularDiscountPromotionModel)
                {                   
                    product = await _repository.SavePromotionData(model);
                    if (product != "")
                    {
                        if (!cId.Contains(model.DiscountCircularId))
                        {
                            cId.Add(model.DiscountCircularId);
                            var circularId = model.DiscountCircularId;
                               await _repository.UpdateWarehousePromotionTable(circularId, shopId);
                        }
                        
                    }
                }
               
                return product;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public  async Task<string> UpdateCustomerSale(CustomerSaleModel objCustomerSaleModelModel)
        {
            try
            {
                var customer = await _repository.UpdateCustomerSale(objCustomerSaleModelModel);
                return customer;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<IEnumerable<GiftVoucherModel>> ViewAllDataGiftvoucherActive()
        {
            try
            {
                var test = await _repository.ViewAllDataGiftvoucherActive();
                return test;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PrivilegeCustomerModelForShop>> ViewAllPrivilegeCustomerData()
        {
            try
            {
                var test = await _repository.ViewAllPrivilegeCustomerData();
                return test;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
