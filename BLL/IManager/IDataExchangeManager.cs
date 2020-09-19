using System.Collections.Generic;
using System.Threading.Tasks;
using Models.AllModel;
using Models.ApiModel;

namespace BLL.IManager
{
    public interface IDataExchangeManager
    {
        //Task<string> SaveData(IEnumerable<DeliveredProduct> objDeliveredProduct);
        Task<string> SaveData(DeliveredProduct objDeliveredProduct);
        Task<string> SaveCustomerInfoData(CustomerModel objCustomerModel);
        Task<string> UpdateCustomerSale(CustomerSaleModel objCustomerSaleModelModel);
        Task<string> SavePrivilegeCardDataInWarehouse(PrivilegeCustomerModel objPrivilegeCustomerModel);
        Task<string> SaveGiftVoucherDepositData(GiftVoucherModel objGiftVoucherModel);
        Task<string> SavePromotionData(List<CircularDiscountPromotionModel> objCircularDiscountPromotionModel,string shopId);
        Task<IEnumerable<PrivilegeCustomerModelForShop>> ViewAllPrivilegeCustomerData();
        Task<IEnumerable<GiftVoucherModel>> ViewAllDataGiftvoucherActive();
        Task<IEnumerable<SalesManWiseSaleReportForDc>> GetAllSalesManWiseSaleDataForDc(string fromDate,string toDate);
        Task<string> GetAllGiftVoucherDeliveryAndSave();
        Task<GiftVoucherModel> giftVoucherInfoByCode(string giftVoucherCode);
    }
}