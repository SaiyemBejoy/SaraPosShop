using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IPointOfSaleRepository
    {
        Task<ItemInfoModel> GetAllInfoByBarcode(string barcode);

        Task<GiftVoucherModel> GetAllInfoByGiftVoucherCode(string giftVoucherCode);

        Task<DataTableAjaxPostModel> GetAllItemInfoForDataTable(DataTableAjaxPostModel model);

        Task<CustomerInfoModel> GetAllInfoByCustomerCode(string customerCode);

        Task<CustomerInfoModel> GetAllInfoByCustomerPhone(string customerPhone);

        Task<CustomerInfoModel> GetAllInfoByCustomerPhoneFromSale(string customerPhone);

        Task<string> SaveDataCustomer(CustomerSaleModel objCustomerSaleModel);

        Task<string> SaveGiftVoucherData(string invoiceNumber, string giftVoucherCode, double giftVoucherOldBalance, double giftVoucherNewBalance,string createdBy);

        Task<string> SaveSaleInfo(SaleInfoModel objSaleInfoModel);

        Task<string> SaveSaleItem(SaleItemModel objSaleItemModel);

        Task<string> SaveAllPaymentInfo(SalePaymentInfoModel objSalePaymentInfoModel);

        Task<string> SaveSaleInfoForExchange(SaleInfoModel objSaleInfoModel);

        Task<string> SaveReturnSaleInfoData(ReturnSaleInfoModel objReturnSaleInfoModel);

        Task<string> SaveSaleItemForExchange(int saleInfoId);

        Task<string> VoidInvoice(string invoiceNumber);

        Task<string> VoidInvoiceWarehouse(string invoiceNumber, string shopId);

        Task<string> SaveReturnSaleItemData(SaleItemModel objSaleItemModel);

        Task<string> SaveReturnSaleInfoDataAndDelete(ReturnSaleInfoModel objReturnSaleInfoModel);


        Task<string> SaveAllPaymentInfoForExchange(int saleInfoId);
        Task<string> SaveAllPaymentInfoForExchangePayZero(int saleInfoId);

        Task<string> GetMaxInvoiceNumber();

        Task<string> GetLastInvoiceNumber();

        Task<int> GetStockByItemId(int itemId);

        Task<IEnumerable<SaleInfoModel>> GetAllHoldInvoice();

        Task<IEnumerable<SaleInfoModel>> GetAllSaleInfo();
        Task<string> UpdateSaleInfo(SaleInfoWarehouseUpdate model);

        Task<IEnumerable<CustomerSaleModel>> GetAllSaleCustomerInfo();

        Task<SaleInfoModel> GetAllDataByInvoiceNumber(string invoiceNumber);//same,karon ager ta only holdInvoice er data anar jonno.

        Task<SaleInfoModel> GetAllDataByInvoiceNumberForExchange(string invoiceNumber);

        Task<SaleInfoModel> GetAllDataFromDcByInvoiceNumberForExchange(string invoiceNumber, int shopId);

        Task<ItemInfoShopReceiveModel> GetAllDataFromDcByInvoiceNumberForDirectReceive(int productId , int itemId);

        Task<IEnumerable<SaleItemModel>> GetAllItemInfoDataBySaleInfoId(int saleInfoId);

        Task<IEnumerable<SaleItemModel>> GetAllItemInfoDataByInvoiceNum(string invoiceNum);


        Task<IEnumerable<SalePaymentInfoModel>> GetAllPaymentInfoDataBySaleInfoId(int saleInfoId);

        Task<string> DeleteSaleInfoItemForHoldInvoiceSave(int saleInfoId);

        Task<string> DeleteAllHoleInfo(string invoiceNumber);

        Task<string> DeleteSaleInfo(int saleInfoId);

        Task<CustomerSaleModel> GetAllSaleCustomerByInvoiceNumber(int customerId);

        Task<string> SaveExchangeHistory(HistoryModel model);
        Task<string> SaveReturnHistory(HistoryModel model);


        Task<IEnumerable<SearchHintsModel>> GetProductGridsForHints(string model);
    }
}