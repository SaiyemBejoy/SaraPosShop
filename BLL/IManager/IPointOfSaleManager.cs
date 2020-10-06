using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IPointOfSaleManager
    {
        Task<ItemInfoModel> GetAllInfoByBarcode(string barcode);
        Task<CustomerInfoModel> GetAllInfoByCustomerCode(string customerCode);
        Task<CustomerInfoModel> GetAllInfoByCustomerPhone(string customerPhone);
        Task<GiftVoucherModel> GetAllInfoByGiftVoucherCode(string giftVoucherCode);
        Task<DataTableAjaxPostModel> GetAllItemInfoForDataTable(DataTableAjaxPostModel model);
        Task<string> SaveData(CustomerSaleModel objCustomerSaleModel, SaleInfoModel objSaleInfoModel);
        Task<string> VoidInvoice(string invoiceNumber,string shopId);
        Task<string> SaveReturnData(ReturnSaleInfoModel objReturnSaleInfoModel);
        Task<string> SaveDataForExchange(CustomerSaleModel objCustomerSaleModel, SaleInfoModel objSaleInfoModel);
        Task<string> GetMaxInvoiceNumber(string shopId);
        Task<IEnumerable<SaleInfoModel>> GetAllHoldInvoice();
        Task<SaleInfoModel> GetAllDataByInvoiceNumber(string invoiceNumber);//same,karon ager ta only holdInvoice er data anar jonno.
        Task<SaleInfoModel> GetAllDataByInvoiceNumberForExchange(string invoiceNumber, int shopId,int ownShopId);
        Task<SaleInfoModel> GetAllDataByInvoiceNumberForVoid(string invoiceNumber);
        Task<SaleInfoModel> GetAllDataByInvoiceNumberForReturn(string invoiceNumber);
        Task<CustomerSaleModel> GetAllSaleCustomerByInvoiceNumber(int customerId);
        Task<IEnumerable<SaleInfoModel>> GetAllSaleInfo();
        Task<bool> UpdateSaleInfo(SaleInfoWarehouseUpdate model);
        Task<IEnumerable<CustomerSaleModel>> GetAllSaleCustomerInfo();
        Task<string> DeleteAllHoldInvoice(string invoiceNumber);
        Task<IEnumerable<SearchHintsModel>> GetProductGridsForHints(string model);
        Task<TransitItemInfoModel> GetAllTransitProductInfoByBarcode(string barcode,string marketPlaceId);

    }
}