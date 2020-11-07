using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace DAL.IRepository
{
    public interface IDataExchangeRepository
    {
        //Task<string> SaveData(IEnumerable<DeliveredProduct> objDeliveredProduct);
        Task<string> SaveData(DeliveredProduct objDeliveredProduct);
        Task<string> SaveCustomerInfoData(CustomerModel objCustomerModel);
        Task<string> UpdateCustomerSale(CustomerSaleModel objCustomerSaleModelModel);
        Task<string> SavePrivilegeCardDataInWarehouse(PrivilegeCustomerModel objPrivilegeCustomerModel);
        Task<string> SavePromotionData(CircularDiscountPromotionModel objCircularDiscountPromotionModel);
        Task<string> SaveGiftVoucherDepositData(GiftVoucherModel objGiftVoucherModel);
        Task<string> UpdateWarehousePromotionTable(int circularId,string shopId);
        Task<IEnumerable<PrivilegeCustomerModelForShop>> ViewAllPrivilegeCustomerData();
        Task<IEnumerable<SalesManWiseSaleReportForDc>> GetAllSalesManWiseSaleDataForDc(string fromDate, string toDate);
        Task<IEnumerable<GiftVoucherDeliveryModel>> GetAllGiftVoucherDelivery();
        Task<string> SaveGiftVoucherData(GiftVoucherDeliveryModel objGiftVoucherDeliveryModel);
        Task<GiftVoucherModel> giftVoucherInfoByCode(string giftVoucherCode);
        Task<IEnumerable<GiftVoucherModel>> ViewAllDataGiftvoucherActive();
        Task<IEnumerable<GiftVoucherModel>> GetAllGiftVoucherSaleDataFromShop();
        Task<string> UpdateGiftVoucherSaleData(GiftVoucherModel objGiftVoucherModel);
    }
}