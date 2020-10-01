using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class SaleInfoModel
    {
        public int SaleInfoId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string SalesManId { get; set; }
        public string SalesManName { get; set; }
        public int MarketPlaceId { get; set; }
        public string MarketPlaceName { get; set; }
        public int TotalItem { get; set; }
        public double TotalAmount { get; set; }
        public double Vat { get; set; }
        public double BagPrice { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountAmount{ get; set; }
        public string PaymentType{ get; set; }
        public double CashAmount{ get; set; }
        public double CardAmount{ get; set; }
        public double SubTotal{ get; set; }
        public double NetAmount{ get; set; }
        public int CustomerId{ get; set; }
        public string CustomerName{ get; set; }
        public string CustomerContactNO{ get; set; }
        public string HoldInvoiceYN{ get; set; }
        public string ExchangeYN{ get; set; }
        public string ShopToShopExchangeYN { get; set; }
        public string ReturnYN{ get; set; }
        public int ExchangeShopId{ get; set; }
        //GiftVoucher
        public string GiftVoucherCode { get; set; }
        public double GiftVoucherOldBalance { get; set; }
        public double GiftVoucherNewBalance { get; set; }
        //End
        public string ExchangeShopName{ get; set; }
        public string CreatedBy{ get; set; }
        public string CreatedByName { get; set; }
        public string PaymentInfo{ get; set; }
        public string InvoiceStyleName { get; set; }
        public string ShopId{ get; set; }
        public string ShopName{ get; set; }
        public string WareHouseId{ get; set; }

        public  IEnumerable<SaleItemModel> SaleItemList { get; set; }
        public  IEnumerable<SalePaymentInfoModel> SalePaymentInfoList { get; set; }


        public static SaleInfoModel ConvertSaleInfoModel(DataRow row)
        {
            return new SaleInfoModel
            {
                SaleInfoId = row.Table.Columns.Contains("SALE_INFO_ID") ? Convert.ToInt32(row["SALE_INFO_ID"]) : 0,
                InvoiceNumber = row.Table.Columns.Contains("INVOICE_NUMBER") ? Convert.ToString(row["INVOICE_NUMBER"]) : "",
                InvoiceDate = row.Table.Columns.Contains("INVOICE_DATE") ? Convert.ToString(row["INVOICE_DATE"]) : "",
                SalesManId = row.Table.Columns.Contains("SALESMAN_ID") ? Convert.ToString(row["SALESMAN_ID"]) : "",
                SalesManName = row.Table.Columns.Contains("SALESMAN_NAME") ? Convert.ToString(row["SALESMAN_NAME"]) : "",
                TotalItem = row.Table.Columns.Contains("TOTAL_ITEM") ? Convert.ToInt32(row["TOTAL_ITEM"]) : 0,
                TotalAmount = row.Table.Columns.Contains("TOTAL_AMOUNT") ? Convert.ToDouble(row["TOTAL_AMOUNT"]) : 0,
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0,
                DiscountPercent = row.Table.Columns.Contains("DISCOUNT_P") ? Convert.ToDouble(row["DISCOUNT_P"]) : 0.0,
                DiscountAmount = row.Table.Columns.Contains("DISCOUNT_A") ? Convert.ToDouble(row["DISCOUNT_A"]) : 0.0,
                BagPrice = row.Table.Columns.Contains("BAG_PRICE") ? Convert.ToDouble(row["BAG_PRICE"]) : 0.0,
                PaymentType = row.Table.Columns.Contains("PAYMENT_TYPE") ? Convert.ToString(row["PAYMENT_TYPE"]) : "",
                CashAmount = row.Table.Columns.Contains("CASH_AMOUNT") ? Convert.ToDouble(row["CASH_AMOUNT"]) : 0.0,
                CardAmount = row.Table.Columns.Contains("CARD_AMOUNT") ? Convert.ToDouble(row["CARD_AMOUNT"]) : 0.0,
                SubTotal = row.Table.Columns.Contains("SUB_TOTAL") ? Convert.ToDouble(row["SUB_TOTAL"]) : 0.0,
                NetAmount = row.Table.Columns.Contains("NET_AMOUNT") ? Convert.ToDouble(row["NET_AMOUNT"]) : 0.0,
                CustomerId = row.Table.Columns.Contains("CUSTOMER_ID") ? Convert.ToInt32(row["CUSTOMER_ID"]) : 0,
                CustomerName = row.Table.Columns.Contains("CUSTOMER_NAME") ? Convert.ToString(row["CUSTOMER_NAME"]) : "",
                CustomerContactNO = row.Table.Columns.Contains("CONTACT_NO") ? Convert.ToString(row["CONTACT_NO"]) : "",
                HoldInvoiceYN = row.Table.Columns.Contains("HOLD_INVOICE_YN") ? Convert.ToString(row["HOLD_INVOICE_YN"]) : "",
                ExchangeYN = row.Table.Columns.Contains("EXCHANGE_YN") ? Convert.ToString(row["EXCHANGE_YN"]) : "",
                ExchangeShopId = row.Table.Columns.Contains("EXCHANGE_SHOP_ID") ? Convert.ToInt32(row["EXCHANGE_SHOP_ID"]) : 0,
                CreatedBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                CreatedByName = row.Table.Columns.Contains("CREATED_BY_NAME") ? Convert.ToString(row["CREATED_BY_NAME"]) : "",
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : "",
                WareHouseId = row.Table.Columns.Contains("WAREHOUSE_ID") ? Convert.ToString(row["WAREHOUSE_ID"]) : ""
            };
        }
        public static SaleInfoModel ConvertSaleInfoModelForDataTable(DataRow row)
        {
            return new SaleInfoModel
            {
                SaleInfoId = row.Table.Columns.Contains("SaleInfoId") ? Convert.ToInt32(row["SaleInfoId"]) : 0,
                InvoiceNumber = row.Table.Columns.Contains("InvoiceNumber") ? Convert.ToString(row["InvoiceNumber"]) : "",
                InvoiceDate = row.Table.Columns.Contains("InvoiceDate") ? Convert.ToString(row["InvoiceDate"]) : "",
                SalesManId = row.Table.Columns.Contains("SalesManId") ? Convert.ToString(row["SalesManId"]) : "",
                SalesManName = row.Table.Columns.Contains("SALESMANNAME") ? Convert.ToString(row["SALESMANNAME"]) : "",
                TotalItem = row.Table.Columns.Contains("TotalItem") ? Convert.ToInt32(row["TotalItem"]) : 0,
                TotalAmount = row.Table.Columns.Contains("TotalAmount") ? Convert.ToDouble(row["TotalAmount"]) : 0,
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0,
                DiscountPercent = row.Table.Columns.Contains("DiscountPercent") ? Convert.ToDouble(row["DiscountPercent"]) : 0.0,
                DiscountAmount = row.Table.Columns.Contains("DiscountAmount") ? Convert.ToDouble(row["DiscountAmount"]) : 0.0,      
                SubTotal = row.Table.Columns.Contains("SubTotal") ? Convert.ToDouble(row["SubTotal"]) : 0.0,
                NetAmount = row.Table.Columns.Contains("NetAmount") ? Convert.ToDouble(row["NetAmount"]) : 0.0,
                CustomerId = row.Table.Columns.Contains("CustomerId") ? Convert.ToInt32(row["CustomerId"]) : 0,
                CustomerName = row.Table.Columns.Contains("CustomerName") ? Convert.ToString(row["CustomerName"]) : "",
                CustomerContactNO = row.Table.Columns.Contains("CustomerContactNO") ? Convert.ToString(row["CustomerContactNO"]) : "",
                HoldInvoiceYN = row.Table.Columns.Contains("HoldInvoiceYN") ? Convert.ToString(row["HoldInvoiceYN"]) : "",
                ExchangeYN = row.Table.Columns.Contains("ExchangeYN") ? Convert.ToString(row["ExchangeYN"]) : "",
                ExchangeShopId = row.Table.Columns.Contains("ExchangeShopId") ? Convert.ToInt32(row["ExchangeShopId"]) : 0,
                ExchangeShopName = row.Table.Columns.Contains("ExchangeShopName") ? Convert.ToString(row["ExchangeShopName"]) : "",
                CreatedBy = row.Table.Columns.Contains("CreatedBy") ? Convert.ToString(row["CreatedBy"]) : "",
                PaymentInfo = row.Table.Columns.Contains("PAYMENTTYPE") ? Convert.ToString(row["PAYMENTTYPE"]) : "",
                InvoiceStyleName = row.Table.Columns.Contains("INVOICESTYLENAME") ? Convert.ToString(row["INVOICESTYLENAME"]) : "",
                ShopId = row.Table.Columns.Contains("ShopId") ? Convert.ToString(row["ShopId"]) : "",
                ShopName = row.Table.Columns.Contains("ShopName") ? Convert.ToString(row["ShopName"]) : "",
                WareHouseId = row.Table.Columns.Contains("WareHouseId") ? Convert.ToString(row["WareHouseId"]) : ""
            };
        }

    }
}
