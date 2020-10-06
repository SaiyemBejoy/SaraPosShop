using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BLL.IManager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;
using Models.ApiModel;


namespace BLL.Manager
{
    public class PointOfSaleManager : IPointOfSaleManager
    {
        private readonly IPointOfSaleRepository _repository;
        private readonly IShopReceiveRepository _shopReceiveRepository;
        private readonly IShopReceiveManager _shopReceiveManager;
        public PointOfSaleManager()
        {
            _repository = new PointOfSaleRepository();
            _shopReceiveManager = new ShopReceiveManager();
            _shopReceiveRepository = new ShopReceiveRepository();
        }

        public async Task<ItemInfoModel> GetAllInfoByBarcode(string barcode)
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

        public async Task<CustomerInfoModel> GetAllInfoByCustomerCode(string customerCode)
        {
            try
            {
                var data = await _repository.GetAllInfoByCustomerCode(customerCode);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveData(CustomerSaleModel objCustomerSaleModel, SaleInfoModel objSaleInfoModel)
        {
            List<StoreReceiveItem> negativeList = new List<StoreReceiveItem>();
            string returnMessage = "";
            string data = null;
            bool ShopToShopEx = false;
            try
            {
                var directReceive = "0";
                if (objSaleInfoModel.ShopToShopExchangeYN == "Y")//Shop To Shop Ex A Direct receive
                {
                    ShopToShopEx = true;
                    if (objSaleInfoModel.SaleItemList != null)
                        foreach (var negForReceive in objSaleInfoModel.SaleItemList)
                        {
                            if (negForReceive.Quantity < 0)
                            {
                                var receiveDataForShopToShopExchange =
                                    await _repository.GetAllDataFromDcByInvoiceNumberForDirectReceive(
                                        negForReceive.ProductId, negForReceive.ItemId);// direct receive korar jonno kisu value silo na ...oi golo anar jonno
                                if (receiveDataForShopToShopExchange != null)
                                {
                                    StoreReceiveItem item = new StoreReceiveItem
                                    {
                                        ProductId = negForReceive.ProductId,
                                        ItemId = negForReceive.ItemId,
                                        SalePrice = receiveDataForShopToShopExchange.SalePrice,
                                        Barcode = negForReceive.Barcode,
                                        ItemName = negForReceive.StyleName,
                                        ReceiveQuantity = System.Math.Abs(negForReceive.Quantity),
                                        Vat = negForReceive.Vat,
                                        Umo = receiveDataForShopToShopExchange.Umo,
                                        CategoryName = receiveDataForShopToShopExchange.Category,
                                        SubCategoryName = receiveDataForShopToShopExchange.SubCategory,
                                        BrandName = receiveDataForShopToShopExchange.Brand
                                    };
                                    negativeList.Add(item);
                                }
                            }
                        }
                    StoreReceiveModel receiveModel = new StoreReceiveModel();
                    receiveModel.StoreReceiveChallanNo = objSaleInfoModel.InvoiceNumber;
                    receiveModel.ReceivedBy = objSaleInfoModel.CreatedBy;
                    receiveModel.ReceiveFrom = Convert.ToString(objSaleInfoModel.ExchangeShopId);
                    receiveModel.ShopId = objSaleInfoModel.ShopId;
                    receiveModel.WareHouseId = objSaleInfoModel.WareHouseId;
                    directReceive = await _shopReceiveRepository.SaveStoreReceiveEX(receiveModel);
                    if (directReceive != null)
                    {
                        foreach (var negativeListForList in negativeList)
                        {
                            negativeListForList.StoreReceiveId = Convert.ToInt32(directReceive);
                            var receiveData = await _shopReceiveRepository.SaveStoreReceiveItem(negativeListForList);
                        }
                    }
                }

                if (Convert.ToInt32(directReceive) <= 0 && 
                    (!string.IsNullOrWhiteSpace(objSaleInfoModel.ShopToShopExchangeYN) 
                     && objSaleInfoModel.ShopToShopExchangeYN == "Y"))
                {
                    return null;
                }

                //Gift Voucher
                string giftVoucherCode = objSaleInfoModel.GiftVoucherCode;
                double giftVoucherOldBalance = objSaleInfoModel.GiftVoucherOldBalance;
                double giftVoucherNewBalance = objSaleInfoModel.GiftVoucherNewBalance;
                //End
                string customerName = objCustomerSaleModel.CustomerName;
                string phoneNo = objCustomerSaleModel.ContactNo;

                var invoiceNumber = "";
                if (customerName != null || phoneNo != null)
                {
                    data = await _repository.SaveDataCustomer(objCustomerSaleModel);
                }
                objSaleInfoModel.CustomerId = Convert.ToInt32(data);
                var saleData = await _repository.SaveSaleInfo(objSaleInfoModel);

                if (saleData != null)
                {
                    var saleInfoId = Convert.ToInt32(saleData.Split(',')[0]);
                    invoiceNumber = saleData.Split(',')[1].Trim();

                    if (saleInfoId > 0 && !string.IsNullOrWhiteSpace(invoiceNumber))
                    {
                        var deleteMessage = await _repository.DeleteSaleInfoItemForHoldInvoiceSave(saleInfoId);

                        if (objSaleInfoModel.SaleItemList != null && deleteMessage != null)
                        {
                            foreach (var tableData in objSaleInfoModel.SaleItemList)
                            {
                                if (ShopToShopEx && tableData.Quantity < 0)
                                {
                                    tableData.ShopToShopExYn = "Y";
                                    tableData.Quantity = 0;
                                }
                                tableData.SaleInfoId = saleInfoId;
                                returnMessage = await _repository.SaveSaleItem(tableData);
                            }
                        }
                        if (objSaleInfoModel.SalePaymentInfoList != null)
                        {
                            if (returnMessage != "0")
                            {
                                foreach (var paymentInfoData in objSaleInfoModel.SalePaymentInfoList)
                                {
                                    paymentInfoData.SaleInfoId = saleInfoId;
                                    paymentInfoData.DiscountAmount =
                                        (paymentInfoData.Amount * paymentInfoData.DiscountPercent) / 100;
                                    string returnMessage2 = await _repository.SaveAllPaymentInfo(paymentInfoData);
                                }
                            }
                        }
                    }
                    else
                    {
                        var deleteMessage = await _repository.DeleteSaleInfo(saleInfoId);
                    }

                    if (objSaleInfoModel.ExchangeYN == "Y")
                    {
                        var exchangeHistoryModel = new HistoryModel();
                        exchangeHistoryModel.HistoryId = 0;
                        exchangeHistoryModel.PreviousInvoiceNumber = objSaleInfoModel.InvoiceNumber;
                        exchangeHistoryModel.PreviousSaleInfoId = objSaleInfoModel.SaleInfoId;
                        exchangeHistoryModel.NewInvoiceNumber = invoiceNumber;
                        exchangeHistoryModel.NewSaleInfoId = Convert.ToInt32(saleData.Split(',')[0]);
                        exchangeHistoryModel.CreateBy = objSaleInfoModel.CreatedBy;
                        exchangeHistoryModel.WarehouseId = objSaleInfoModel.WareHouseId;
                        exchangeHistoryModel.ShopId = objSaleInfoModel.ShopId;

                        var exchangeHistory = await _repository.SaveExchangeHistory(exchangeHistoryModel);
                    }
                    if (objSaleInfoModel.ReturnYN == "Y")
                    {
                        var returnHistoryModel = new HistoryModel();
                        returnHistoryModel.HistoryId = 0;
                        returnHistoryModel.PreviousInvoiceNumber = objSaleInfoModel.InvoiceNumber;
                        returnHistoryModel.PreviousSaleInfoId = objSaleInfoModel.SaleInfoId;
                        returnHistoryModel.NewInvoiceNumber = invoiceNumber;
                        returnHistoryModel.NewSaleInfoId = Convert.ToInt32(saleData.Split(',')[0]);
                        returnHistoryModel.CreateBy = objSaleInfoModel.CreatedBy;
                        returnHistoryModel.WarehouseId = objSaleInfoModel.WareHouseId;
                        returnHistoryModel.ShopId = objSaleInfoModel.ShopId;

                        var returnHistory = await _repository.SaveReturnHistory(returnHistoryModel);
                    }

                    if (giftVoucherCode != null)
                    {
                        string createdBy = objSaleInfoModel.CreatedBy;
                       var  saveGiftVoucherData = await _repository.SaveGiftVoucherData(invoiceNumber,giftVoucherCode, giftVoucherOldBalance, giftVoucherNewBalance, createdBy);
                    }
                }
                return invoiceNumber;
            }
            catch (Exception e)
            {
                return "something went wrong";
            }
        }

        public async Task<string> GetMaxInvoiceNumber(string shopId)
        {
            try
            {
                var data = await _repository.GetMaxInvoiceNumber();
                return data;
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
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<SaleInfoModel>> GetAllHoldInvoice()
        {
            try
            {
                var data = await _repository.GetAllHoldInvoice();
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SaleInfoModel> GetAllDataByInvoiceNumber(string invoiceNumber)
        {
            try
            {
                var data = await _repository.GetAllDataByInvoiceNumber(invoiceNumber);
                if (data != null)
                {
                    int saleInfoId = data.SaleInfoId;
                    var itemList = await _repository.GetAllItemInfoDataBySaleInfoId(saleInfoId);

                    var dataSaleItemList = itemList.ToList();
                    foreach (var item in dataSaleItemList)
                    {
                        item.Stock = await _repository.GetStockByItemId(item.ItemId);
                    }

                    data.SaleItemList = dataSaleItemList;
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<SaleInfoModel> GetAllDataByInvoiceNumberForExchange(string invoiceNumber, int shopId,int ownShopId)
        {
            var data = new SaleInfoModel();
            try
            {
                if (shopId == ownShopId)
                {
                     data = await _repository.GetAllDataByInvoiceNumberForExchange(invoiceNumber);
                    if (data.InvoiceNumber != null && data.SaleItemList == null)
                    {
                        int saleInfoId = data.SaleInfoId;
                        var itemList = await _repository.GetAllItemInfoDataBySaleInfoId(saleInfoId);
                        var dataSaleItemList = itemList.ToList();
                        foreach (var item in dataSaleItemList)
                        {
                            item.Stock = await _repository.GetStockByItemId(item.ItemId);
                        }
                        data.SaleItemList = dataSaleItemList;
                    }
                }
               
                else
                {
                    var dcData = await _repository.GetAllDataFromDcByInvoiceNumberForExchange(invoiceNumber, shopId);
                    if (dcData != null)
                    {
                        foreach (var item in dcData.SaleItemList)
                        {
                            item.Stock = await _repository.GetStockByItemId(item.ItemId);
                        }
                    }
                   
                    //if (dcData != null)
                    //{
                    //    int saleInfoId = dcData.SaleInfoId;
                    //    var itemList = await _repository.GetAllItemInfoDataBySaleInfoId(saleInfoId);
                    //    var dataSaleItemList = itemList.ToList();
                    //    //foreach (var item in dataSaleItemList)
                    //    //{
                    //    //    item.Stock = await _repository.GetStockByItemId(item.ItemId);
                    //    //}
                    //    dcData.SaleItemList = dataSaleItemList;
                    //}
                    return dcData;
                }
                return data;
            }
            catch (Exception e)
            {
                try
                {
                    var dcData = await _repository.GetAllDataFromDcByInvoiceNumberForExchange(invoiceNumber, shopId);
                    return dcData;
                }
                catch (Exception exception)
                {
                    return null;
                }
            }
        }
        public async Task<SaleInfoModel> GetAllDataByInvoiceNumberForReturn(string invoiceNumber)
        {
            try
            {
                var data = await _repository.GetAllDataByInvoiceNumberForExchange(invoiceNumber);
                var getLastInvoiceNumForChecck = await _repository.GetLastInvoiceNumber();
                if (data.InvoiceNumber != getLastInvoiceNumForChecck)
                {
                        int saleInfoId = data.SaleInfoId;
                        data.SaleItemList = await _repository.GetAllItemInfoDataBySaleInfoId(saleInfoId);
                        data.SalePaymentInfoList = await _repository.GetAllPaymentInfoDataBySaleInfoId(saleInfoId);
                   

                    return data;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> SaveDataForExchange(CustomerSaleModel objCustomerSaleModel, SaleInfoModel objSaleInfoModel)
        {
            string returnMessage = "";
            string saleItemSave = "";
            string data = null;
            try
            {
                string customerName = objCustomerSaleModel.CustomerName;
                string phoneNo = objCustomerSaleModel.ContactNo;
                if (customerName != null || phoneNo != null)
                {
                    data = await _repository.SaveDataCustomer(objCustomerSaleModel);
                }
                objSaleInfoModel.CustomerId = Convert.ToInt32(data);
                var saleData = await _repository.SaveSaleInfoForExchange(objSaleInfoModel);
                if (saleData != null)
                {
                    if (objSaleInfoModel.SaleItemList != null)
                    {
                        var saleInfoId = Convert.ToInt32(saleData);
                        returnMessage = await _repository.SaveSaleItemForExchange(saleInfoId);
                        if (returnMessage != null)
                        {
                            foreach (var tableData in objSaleInfoModel.SaleItemList)
                            {
                                tableData.SaleInfoId = Convert.ToInt32(saleData);
                                saleItemSave = await _repository.SaveSaleItem(tableData);
                            }
                        }
                    }

                    if (objSaleInfoModel.SalePaymentInfoList != null)
                    {
                        if (saleItemSave != null)
                        {
                            var saleInfoId = Convert.ToInt32(saleData);
                            string returnMessage2 = await _repository.SaveAllPaymentInfoForExchange(saleInfoId);
                            if (returnMessage2 != null)
                            {
                                foreach (var paymentInfoData in objSaleInfoModel.SalePaymentInfoList)
                                {
                                    paymentInfoData.SaleInfoId = Convert.ToInt32(saleData);
                                    paymentInfoData.DiscountAmount = (paymentInfoData.Amount * paymentInfoData.DiscountPercent) / 100;
                                    returnMessage = await _repository.SaveAllPaymentInfo(paymentInfoData);
                                }
                            }
                        }
                    }
                    else
                    {
                        var saleInfoId = Convert.ToInt32(saleData);
                        string returnMessage3 = await _repository.SaveAllPaymentInfoForExchangePayZero(saleInfoId);
                    }
                }
                return returnMessage;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<CustomerSaleModel> GetAllSaleCustomerByInvoiceNumber(int customerId)
        {
            try
            {
                var data = await _repository.GetAllSaleCustomerByInvoiceNumber(customerId);
                return data;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<SaleInfoModel>> GetAllSaleInfo()
        {
            try
            {

                var data = await _repository.GetAllSaleInfo();
                data = data.Take(100);
                var saleInfoModels = data.OrderBy(c => c.SaleInfoId).ToList();
                if (saleInfoModels.Count > 0)
                {
                    foreach (var saleInfoModel in saleInfoModels)
                    {
                        saleInfoModel.SaleItemList = await _repository.GetAllItemInfoDataBySaleInfoId(saleInfoModel.SaleInfoId);
                        saleInfoModel.SalePaymentInfoList = await _repository.GetAllPaymentInfoDataBySaleInfoId(saleInfoModel.SaleInfoId);
                    }
                }
                return saleInfoModels;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> UpdateSaleInfo(SaleInfoWarehouseUpdate model)
        {
            try
            {
                var data = await _repository.UpdateSaleInfo(model);

                if(data == "1")
                    return true;

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CustomerSaleModel>> GetAllSaleCustomerInfo()
        {
            try
            {
                var data = await _repository.GetAllSaleCustomerInfo();
                data = data.Take(100);
                var customerSaleModels = data.OrderBy(c => c.CustomerId).ToList();
                return customerSaleModels;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> SaveReturnData(ReturnSaleInfoModel objReturnSaleInfoModel)
        {
            ReturnSaleInfoModel returnModel = new ReturnSaleInfoModel();
            List<SaleItemModel> returnItemList = new List<SaleItemModel>();
            string data = null;
            string data2 = null;
            try
            {
                if (objReturnSaleInfoModel.SaleItemList != null)
                {
                    
                    var invoiceNumber = objReturnSaleInfoModel.InvoiceNumber;
                    var saleItemDataForCompare = await _repository.GetAllItemInfoDataByInvoiceNum(invoiceNumber);
                    //compare For Two List
                    var saleItemData = saleItemDataForCompare.OrderBy(c => c.ItemId).ToList();
                    var returnItemData = objReturnSaleInfoModel.SaleItemList.OrderBy(c => c.ItemId).ToList();

                    for (int i = 0; i < saleItemData.Count; i++)
                    {
                        bool isExist = false;
                        for (var j = 0; j < returnItemData.Count; j++)
                        {
                            if (saleItemData[i].ItemId == returnItemData[j].ItemId)
                            {
                                if (saleItemData[i].Quantity > returnItemData[j].Quantity)
                                {
                                    var quantity = saleItemData[i].Quantity - returnItemData[j].Quantity;
                                    saleItemData[i].Quantity = quantity;
                                    saleItemData[i].Total = saleItemData[i].Price * quantity;
                                    returnItemList.Add(saleItemData[i]);
                                }
                                isExist = true;
                            }
                        }
                        if (!isExist)
                        {
                            returnItemList.Add(saleItemData[i]);
                        }                      
                    }

                    objReturnSaleInfoModel.ReturnItem = returnItemList.Count;
                    objReturnSaleInfoModel.ReturnTotalAmount = returnItemList.Select(x => x.Total).Sum();
                    var vat = 100 + returnItemList[0].Vat;
                    var vat1 = (100 / vat) * returnItemList.Select(x => x.Total).Sum();
                    objReturnSaleInfoModel.ReturnVat = (returnItemList[0].Vat / 100) * vat1;
                   
                    data = await _repository.SaveReturnSaleInfoData(objReturnSaleInfoModel);


                    //HashSet<int> sentIDs = new HashSet<int>(objReturnSaleInfoModel.SaleItemList.Select(s => s.ItemId));
                    //IEnumerable<SaleItemModel> compareData = itemDataForCompare.Where(m => !sentIDs.Contains(m.ItemId));

                    //End
                    foreach (var returnItem in returnItemList)
                    {
                        data2 = await _repository.SaveReturnSaleItemData(returnItem);
                    }

                    foreach (var returnSaleItemModel in objReturnSaleInfoModel.SaleItemList)
                    {
                        returnSaleItemModel.SaleInfoId = Convert.ToInt32(data);
                        var saleItemSave = await _repository.SaveSaleItem(returnSaleItemModel);
                    }
                    return data2;
                }
                else
                {
                    data = await _repository.SaveReturnSaleInfoDataAndDelete(objReturnSaleInfoModel);
                    var saleInfoId = Convert.ToInt32(data);
                    var saleItemDataSaveForReturnTable= await _repository.GetAllItemInfoDataBySaleInfoId(saleInfoId);
                    foreach (var returnSaleItemModel in saleItemDataSaveForReturnTable)
                    {
                        returnSaleItemModel.SaleInfoId = Convert.ToInt32(data);
                         data2 = await _repository.SaveReturnSaleItemData(returnSaleItemModel);
                    }
                    return data2;
                }
                
            }
            catch (Exception e)
            {
                return "something went wrong";
            }
        }
        
        public async Task<string> DeleteAllHoldInvoice(string invoiceNumber)
        {
            try
            {
                var delete = await _repository.DeleteAllHoleInfo(invoiceNumber);
                return delete;
            }
            catch (Exception)
            {
                return "something went wrong";
            }
        }

        public async Task<SaleInfoModel> GetAllDataByInvoiceNumberForVoid(string invoiceNumber)
        {

            try
            {
                var data = await _repository.GetAllDataByInvoiceNumberForExchange(invoiceNumber);
                if (data != null)
                {
                    int saleInfoId = data.SaleInfoId;
                     data.SaleItemList = await _repository.GetAllItemInfoDataBySaleInfoId(saleInfoId);
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> VoidInvoice(string invoiceNumber,string shopId)
        {
            try
            {
                var voidInvoice = "";
                var voidInvoiceWarehouse = await _repository.VoidInvoiceWarehouse(invoiceNumber,shopId);
                return await _repository.VoidInvoice(invoiceNumber);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public  async Task<GiftVoucherModel> GetAllInfoByGiftVoucherCode(string giftVoucherCode)
        {
            try
            {
                var data = await _repository.GetAllInfoByGiftVoucherCode(giftVoucherCode);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<CustomerInfoModel> GetAllInfoByCustomerPhone(string customerPhone)
        {
            try
            {
                var data = await _repository.GetAllInfoByCustomerPhone(customerPhone);
                return data;
            }
            catch (Exception e)
            {
                var data = await _repository.GetAllInfoByCustomerPhoneFromSale(customerPhone);
                return data;
            }
        }

        public async Task<IEnumerable<SearchHintsModel>> GetProductGridsForHints(string model)
        {
            try
            {
                var data = await _repository.GetProductGridsForHints(model);
                
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public  async Task<TransitItemInfoModel> GetAllTransitProductInfoByBarcode(string barcode, string marketPlaceId)
        {
            try
            {
                var data = await _repository.GetAllTransitProductInfoByBarcode(barcode, marketPlaceId);
                return data;
            }
            catch (Exception e)
            {

                return null;
            }
        }
    }
}
