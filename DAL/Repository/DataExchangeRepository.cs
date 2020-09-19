using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.DBManager;
using DAL.IRepository;
using Models.AllModel;
using Models.ApiModel;
using Oracle.ManagedDataAccess.Client;

namespace DAL.Repository
{
    public class DataExchangeRepository : ApplicationDbContext, IDataExchangeRepository
    {
        

        public async Task<string> SaveCustomerInfoData(CustomerModel objCustomerModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
                {
                    new OracleParameter(":P_CUSTOMER_ID", OracleDbType.Varchar2, objCustomerModel.CustomerId, ParameterDirection.Input),
                    new OracleParameter(":P_CUSTOMER_CODE", OracleDbType.Varchar2, objCustomerModel.CustomerCode, ParameterDirection.Input),
                    new OracleParameter(":P_CUSTOMER_NAME", OracleDbType.Varchar2, objCustomerModel.CustomerFirstName, ParameterDirection.Input),
                     new OracleParameter(":P_CONTACT_NO", OracleDbType.Varchar2, objCustomerModel.ContactNo, ParameterDirection.Input),
                    new OracleParameter(":P_EMAIL", OracleDbType.Varchar2, objCustomerModel.Email, ParameterDirection.Input),
                    new OracleParameter(":P_ADDRESS", OracleDbType.Varchar2, objCustomerModel.Address, ParameterDirection.Input),
                    new OracleParameter(":P_DISCOUNT", OracleDbType.Varchar2, objCustomerModel.DiscountPercent, ParameterDirection.Input),
                    new OracleParameter(":P_CUSTOMER_TYPE", OracleDbType.Varchar2, objCustomerModel.CustomerTypeName, ParameterDirection.Input),
                    new OracleParameter(":P_ACTIVE_YN", OracleDbType.Varchar2, objCustomerModel.Active_YN, ParameterDirection.Input),
                    new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)

            }; 

            query.Append("PRO_CUSTOMER_INFO_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        //public async Task<string> SaveData(IEnumerable<DeliveredProduct> objDeliveredProduct)
        //{

        //   List<List<OracleParameter>> parameter = new List<List<OracleParameter>>();
        //    StringBuilder query = new StringBuilder();
        //    query.Clear();
        //    foreach (var data in objDeliveredProduct)
        //    {

        //        List<OracleParameter> param = new List<OracleParameter>
        //        {
        //            new OracleParameter(":P_STORE_DELIVERY_NUMBER", OracleDbType.Varchar2, data.StoreDeliveryNumber, ParameterDirection.Input),
        //            new OracleParameter(":P_REGISTER_ID", OracleDbType.Varchar2, data.RegisterId, ParameterDirection.Input),
        //            new OracleParameter(":P_DELIVERY_SHOP_ID", OracleDbType.Varchar2, data.DeliveryShopId, ParameterDirection.Input),
        //            new OracleParameter(":P_PURCHASE_RECEIVE_NUMBER", OracleDbType.Varchar2, data.PurchaseReceiveNumber, ParameterDirection.Input),
        //            new OracleParameter(":P_DELIVERY_DATE", OracleDbType.Varchar2, data.DeliveryDate, ParameterDirection.Input),
        //            new OracleParameter(":P_REQUISITION_NO", OracleDbType.Varchar2, data.RequisitionNo, ParameterDirection.Input),
        //            new OracleParameter(":P_RECEIVE_CHALLAN_DELIVERY", OracleDbType.Varchar2, data.ReceiveChallanDelivery, ParameterDirection.Input),
        //            new OracleParameter(":P_DELIVERY_ITEM_ID", OracleDbType.Varchar2, data.DeliveryItemId, ParameterDirection.Input),
        //            new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, data.ProductId, ParameterDirection.Input),
        //            new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, data.BarCode, ParameterDirection.Input),
        //            new OracleParameter(":P_DELIVERY_QUANTITY", OracleDbType.Varchar2, data.DeliveryQuantity, ParameterDirection.Input),
        //            new OracleParameter(":P_PURCHASE_PRICE", OracleDbType.Varchar2, data.PurchasePrice, ParameterDirection.Input),
        //            new OracleParameter(":P_SALE_PRICE", OracleDbType.Varchar2, data.SalePrice, ParameterDirection.Input),
        //            new OracleParameter(":P_VAT", OracleDbType.Varchar2, data.Vat, ParameterDirection.Input),
        //            new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
        //        };

        //        parameter.Add(param);
        //    }

        //    query.Append("PRO_DELIVERED_PRODUCT_SAVE");
        //    return await ExecuteListNonQueryAsync(query.ToString(), parameter);
        //}

        public async Task<string> SaveData(DeliveredProduct objDeliveredProduct)
        {

            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
                {
                    new OracleParameter(":P_STORE_DELIVERY_NUMBER", OracleDbType.Varchar2, objDeliveredProduct.StoreDeliveryNumber, ParameterDirection.Input),
                    new OracleParameter(":P_REGISTER_ID", OracleDbType.Varchar2, objDeliveredProduct.RegisterId, ParameterDirection.Input),
                    new OracleParameter(":P_DELIVERY_SHOP_ID", OracleDbType.Varchar2, objDeliveredProduct.DeliveryShopId, ParameterDirection.Input),
                    new OracleParameter(":P_SEASON_ID", OracleDbType.Varchar2, objDeliveredProduct.SeasonId, ParameterDirection.Input),
                    new OracleParameter(":P_SEASON_NAME", OracleDbType.Varchar2, objDeliveredProduct.SeasonName, ParameterDirection.Input),
                    new OracleParameter(":P_PURCHASE_RECEIVE_NUMBER", OracleDbType.Varchar2, objDeliveredProduct.PurchaseReceiveNumber, ParameterDirection.Input),
                    new OracleParameter(":P_DELIVERY_DATE", OracleDbType.Varchar2, objDeliveredProduct.DeliveryDate, ParameterDirection.Input),
                    new OracleParameter(":P_REQUISITION_NO", OracleDbType.Varchar2, objDeliveredProduct.RequisitionNo, ParameterDirection.Input),
                    new OracleParameter(":P_RECEIVE_CHALLAN_DELIVERY", OracleDbType.Varchar2, objDeliveredProduct.ReceiveChallanDelivery, ParameterDirection.Input),
                    new OracleParameter(":P_DELIVERY_ITEM_ID", OracleDbType.Varchar2, objDeliveredProduct.DeliveryItemId, ParameterDirection.Input),
                    new OracleParameter(":P_ITEM_ID", OracleDbType.Varchar2, objDeliveredProduct.ItemId, ParameterDirection.Input),
                    new OracleParameter(":P_PRODUCT_ID", OracleDbType.Varchar2, objDeliveredProduct.ProductId, ParameterDirection.Input),
                    new OracleParameter(":P_ITEM_NAME", OracleDbType.Varchar2, objDeliveredProduct.ItemName, ParameterDirection.Input),
                    new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objDeliveredProduct.BarCode, ParameterDirection.Input),
                    new OracleParameter(":P_DELIVERY_QUANTITY", OracleDbType.Varchar2, objDeliveredProduct.DeliveryQuantity, ParameterDirection.Input),
                    new OracleParameter(":P_PURCHASE_PRICE", OracleDbType.Varchar2, objDeliveredProduct.PurchasePrice, ParameterDirection.Input),
                    new OracleParameter(":P_SALE_PRICE", OracleDbType.Varchar2, objDeliveredProduct.SalePrice, ParameterDirection.Input),
                    new OracleParameter(":P_VAT", OracleDbType.Varchar2, objDeliveredProduct.Vat, ParameterDirection.Input),
                    new OracleParameter(":P_RECEIVE_FROM", OracleDbType.Varchar2, objDeliveredProduct.ReceivedShopId, ParameterDirection.Input),
                    new OracleParameter(":P_UMO", OracleDbType.Varchar2, objDeliveredProduct.UMO, ParameterDirection.Input),
                    new OracleParameter(":P_PRODUCT_CATEGORY", OracleDbType.Varchar2, objDeliveredProduct.Category, ParameterDirection.Input),
                    new OracleParameter(":P_PRODUCT_SUB_CATEGORY", OracleDbType.Varchar2, objDeliveredProduct.SubCategory, ParameterDirection.Input),
                    new OracleParameter(":P_BRAND", OracleDbType.Varchar2, objDeliveredProduct.Brand, ParameterDirection.Input),
                    new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
                };

            query.Append("PRO_DELIVERED_PRODUCT_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SavePrivilegeCardDataInWarehouse(PrivilegeCustomerModel objPrivilegeCustomerModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "PrivilegeCardCustomer", objPrivilegeCustomerModel);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<string> SavePromotionData(CircularDiscountPromotionModel objCircularDiscountPromotionModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

                List<OracleParameter> param = new List<OracleParameter>
                {
                    new OracleParameter(":P_DISCOUNT_CIRCULAR_ID", OracleDbType.Varchar2, objCircularDiscountPromotionModel.DiscountCircularId, ParameterDirection.Input),
                    new OracleParameter(":P_DISCOUNT_CIRCULAR_SUB_ID", OracleDbType.Varchar2, objCircularDiscountPromotionModel.DiscountCircularItemId, ParameterDirection.Input),
                    new OracleParameter(":P_DISCOUNT_CIRCULAR_NAME", OracleDbType.Varchar2, objCircularDiscountPromotionModel.DiscountCircularName, ParameterDirection.Input),
                    new OracleParameter(":P_VALID_FROM", OracleDbType.Varchar2, objCircularDiscountPromotionModel.ValidFrom, ParameterDirection.Input),
                    new OracleParameter(":P_VALID_TO", OracleDbType.Varchar2, objCircularDiscountPromotionModel.ValidTo, ParameterDirection.Input),
                    new OracleParameter(":P_BARCODE", OracleDbType.Varchar2, objCircularDiscountPromotionModel.Barcode, ParameterDirection.Input),
                    new OracleParameter(":P_PURCHASE_PRICE", OracleDbType.Varchar2, objCircularDiscountPromotionModel.PurchasePrice, ParameterDirection.Input),
                    new OracleParameter(":P_SALE_PRICE", OracleDbType.Varchar2, objCircularDiscountPromotionModel.SalePrice, ParameterDirection.Input),
                    new OracleParameter(":P_DISCOUNT_PERCENT", OracleDbType.Varchar2, objCircularDiscountPromotionModel.DiscountPercent, ParameterDirection.Input),
                    new OracleParameter(":P_DISCOUNT_AMOUNT", OracleDbType.Varchar2, objCircularDiscountPromotionModel.DiscountAmount, ParameterDirection.Input),
                    new OracleParameter(":P_UPDATE_BY", OracleDbType.Varchar2, objCircularDiscountPromotionModel.UpdateBy, ParameterDirection.Input),
                    new OracleParameter(":P_WARE_HOUSE_ID", OracleDbType.Varchar2, objCircularDiscountPromotionModel.WareHouseId, ParameterDirection.Input),
                    new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
                };
         
            query.Append("PRO_CIR_DISP_ROMOTION_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public  async Task<string> UpdateCustomerSale(CustomerSaleModel objCustomerSaleModelModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();

            List<OracleParameter> param = new List<OracleParameter>
                {
                    new OracleParameter(":P_CUSTOMER_ID", OracleDbType.Varchar2, objCustomerSaleModelModel.CustomerId, ParameterDirection.Input),
                    new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };

            query.Append("PRO_SALE_CUSTOMER_UPDATE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> UpdateWarehousePromotionTable(int circularId,string shopId)
        {
            try
            {
                var model = new
                {
                    DiscountCircularId = circularId,
                    ShopId = shopId
                };
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);

                    HttpResponseMessage response = await client.PostAsJsonAsync(
                        "CircularDiscountPromotion", model);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        return response.StatusCode.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PrivilegeCustomerModelForShop>> ViewAllPrivilegeCustomerData()
        {
            var query = "Select * from CUSTOMER_INFO ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(PrivilegeCustomerModelForShop.ConvertPrivilegeCustomerModelForShop);
        }

        //public async Task<string> SaveEmployeeInfoData(EmployeeDistributionModel objEmployeeDistributionModel)
        //{
        //    StringBuilder query = new StringBuilder();
        //    query.Clear();
        //    List<OracleParameter> param = new List<OracleParameter>
        //        {
        //            new OracleParameter(":P_EMPLOYEE_DIS_ID", OracleDbType.Varchar2, objEmployeeDistributionModel.EmployeeDisId, ParameterDirection.Input),
        //            new OracleParameter(":P_EMPLOYEE_ID", OracleDbType.Varchar2, objEmployeeDistributionModel.EmployeeId, ParameterDirection.Input),
        //            new OracleParameter(":P_EMPLOYEE_NAME", OracleDbType.Varchar2, objEmployeeDistributionModel.EmployeeName, ParameterDirection.Input),
        //            new OracleParameter(":P_DESIGNATION", OracleDbType.Varchar2, objEmployeeDistributionModel.Designation, ParameterDirection.Input),
        //            new OracleParameter(":P_CONTACT_NO", OracleDbType.Varchar2, objEmployeeDistributionModel.ContactNo, ParameterDirection.Input),
        //            new OracleParameter(":P_EMAIL", OracleDbType.Varchar2, objEmployeeDistributionModel.Email, ParameterDirection.Input),
        //            new OracleParameter(":P_SHOP_NAME", OracleDbType.Varchar2, objEmployeeDistributionModel.ShopName, ParameterDirection.Input),
        //            new OracleParameter(":P_SHOP_ID", OracleDbType.Varchar2, objEmployeeDistributionModel.ShopId, ParameterDirection.Input),
        //            new OracleParameter(":P_EMPLOYEE_ROLE", OracleDbType.Varchar2, objEmployeeDistributionModel.EmployeeRole, ParameterDirection.Input),
        //            new OracleParameter(":P_EMPLOYEE_PASSWORD", OracleDbType.Varchar2, objEmployeeDistributionModel.Password, ParameterDirection.Input),
        //            new OracleParameter(":P_ACTIVE_YN", OracleDbType.Varchar2, objEmployeeDistributionModel.Active_YN, ParameterDirection.Input),
        //            new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
        //        };

        //    query.Append("PRO_EMPLOYEE_DISTRIBUTION_SAVE");
        //    return await ExecuteNonQueryAsync(query.ToString(), param);
        //}

        public async Task<IEnumerable<SalesManWiseSaleReportForDc>> GetAllSalesManWiseSaleDataForDc(string fromDate, string toDate)
        {
            var query = "Select * from VEW_RPT_SALES_MAN_WISE_SALE where INVOICE_DATE BETWEEN  to_date('" + fromDate.Trim() + "', 'dd/mm/yyyy') AND  to_date('" + toDate.Trim() + "', 'dd/mm/yyyy') ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(SalesManWiseSaleReportForDc.ConvertSalesManWiseSaleReportForDc);
        }

        public async Task<IEnumerable<GiftVoucherDeliveryModel>> GetAllGiftVoucherDelivery()
        {
            IEnumerable<GiftVoucherDeliveryModel> giftVoucher = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WarehouseApi);
                    //HTTP GET
                    var responseTask = client.GetAsync("GiftVoucher");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<GiftVoucherDeliveryModel>>();
                        readTask.Wait();
                        await Task.Run(() => giftVoucher = readTask.Result);

                        return giftVoucher;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<string> SaveGiftVoucherData(GiftVoucherDeliveryModel objGiftVoucherDeliveryModel)
        {
            StringBuilder query = new StringBuilder();
            query.Clear();
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":P_GIFT_VOUCHER_ID", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.GiftVoucherId, ParameterDirection.Input),
                new OracleParameter(":P_GIFT_VOUCHER_ITEM_NUM", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.DeliveryItemNum, ParameterDirection.Input),
                new OracleParameter(":P_DELIVERY_DATE", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.DeliveryDate, ParameterDirection.Input),
                new OracleParameter(":P_GIFT_VOUCHER_CODE", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.GiftVoucherCode, ParameterDirection.Input),
                new OracleParameter(":P_GIFT_VOUCHER_VALUE", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.GiftVoucherValue, ParameterDirection.Input),
                new OracleParameter(":P_GIFT_VOUCHER_REMAINING_VALUE", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.RemainingValue, ParameterDirection.Input),
                new OracleParameter(":P_UPDATE_BY", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.UpdateBy, ParameterDirection.Input),
                new OracleParameter(":P_DEPOSIT_YN", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.DepositYN, ParameterDirection.Input),
                new OracleParameter(":P_DEPOSIT_SHOP_ID", OracleDbType.Varchar2, objGiftVoucherDeliveryModel.DepositShopId, ParameterDirection.Input),
                new OracleParameter("P_MESSAGE", OracleDbType.Varchar2, 500, "", ParameterDirection.Output)
            };

            query.Append("PRO_GIFT_VOUCHER_SAVE");
            return await ExecuteNonQueryAsync(query.ToString(), param);
        }

        public async Task<string> SaveGiftVoucherDepositData(GiftVoucherModel objGiftVoucherModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WarehouseApi);
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "GiftVoucher", objGiftVoucherModel);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<GiftVoucherModel> giftVoucherInfoByCode(string giftVoucherCode)
        {
            List<OracleParameter> param = new List<OracleParameter>
            {
                new OracleParameter(":GIFT_VOUCHER_CODE", OracleDbType.Varchar2, giftVoucherCode, ParameterDirection.Input)
            };

            var query = "Select * from GIFT_VOUCHER_DELIVERY WHERE GIFT_VOUCHER_CODE = :GIFT_VOUCHER_CODE AND DEPOSIT_YN IS NULL ";
            var dt = await GetDataThroughDataTableAsync(query, param);
            return GiftVoucherModel.ConvertGiftVoucherModel(dt.Rows[0]);
        }

        public async Task<IEnumerable<GiftVoucherModel>> ViewAllDataGiftvoucherActive()
        {
            var query = "Select * from VEW_GIFT_VOUCHER_DELIVERY WHERE DEPOSIT_YN = 'Y' ";
            var dt = await GetDataThroughDataTableAsync(query, null);
            return (dt.Rows).Cast<DataRow>().Select(GiftVoucherModel.ConvertGiftVoucherModel);
        }
    }
}
