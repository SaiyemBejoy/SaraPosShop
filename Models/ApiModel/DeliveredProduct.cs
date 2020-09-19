using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class DeliveredProduct
    {
        public string StoreDeliveryNumber { get; set; }

        public string RegisterId { get; set; }

        public int DeliveryShopId { get; set; }

        public string DeliveryShopName { get; set; }

        public int SeasonId { get; set; }

        public string SeasonName { get; set; }

        public string PurchaseReceiveNumber { get; set; }

        public string DeliveryDate { get; set; }

        public string RequisitionNo { get; set; }

        public string ReceiveChallanDelivery { get; set; }

        public int DeliveryItemId { get; set; }

        public int ItemId { get; set; }

        public int ProductId { get; set; }

        public string ItemName { get; set; }

        public string BarCode { get; set; }

        public int DeliveryQuantity { get; set; }

        public double PurchasePrice { get; set; }

        public double SalePrice { get; set; }

        public double Vat { get; set; }
        public int ReceivedShopId { get; set; }
        public string UMO { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }

        public static DeliveredProduct ConvertDeliveredProduct(DataRow row)
        {
            DeliveredProduct product = new DeliveredProduct();
            //return new DeliveredProduct();
            //{
            product.StoreDeliveryNumber = row.Table.Columns.Contains("STORE_DELIVERY_NUMBER")
                ? Convert.ToString(row["STORE_DELIVERY_NUMBER"])
                : "";
            product.RegisterId = row.Table.Columns.Contains("REGISTER_ID") ? Convert.ToString(row["REGISTER_ID"]) : "";
            product.DeliveryShopId = row.Table.Columns.Contains("DELIVERY_SHOP_ID")
                ? Convert.ToInt32(row["DELIVERY_SHOP_ID"])
                : 0;
            product.PurchaseReceiveNumber = row.Table.Columns.Contains("PURCHASE_RECEIVE_NUMBER")
                ? Convert.ToString(row["PURCHASE_RECEIVE_NUMBER"])
                : "";
            product.DeliveryDate = row.Table.Columns.Contains("DELIVERY_DATE")
                ? Convert.ToDateTime(row["DELIVERY_DATE"]).ToString("dd/MMM/yyyy")
                : "";
            product.RequisitionNo = row.Table.Columns.Contains("REQUISTION_NO")
                ? Convert.ToString(row["REQUISTION_NO"])
                : "";
            product.ReceiveChallanDelivery = row.Table.Columns.Contains("RECEIVE_CHALLAN_DELIVERY")
                ? Convert.ToString(row["RECEIVE_CHALLAN_DELIVERY"])
                : "";
            product.DeliveryItemId = row.Table.Columns.Contains("DELIVERY_ITEM_ID")
                ? Convert.ToInt32(row["DELIVERY_ITEM_ID"])
                : 0;
            product.ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0;
            product.ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0;
            product.ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "";
            product.BarCode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "";
            product.DeliveryQuantity = row.Table.Columns.Contains("DELIVERY_QUANTITY")
                ? Convert.ToInt32(row["DELIVERY_QUANTITY"])
                : 0;
            product.PurchasePrice = row.Table.Columns.Contains("PURCHASE_PRICE")
                ? Convert.ToDouble(row["PURCHASE_PRICE"])
                : 0;
            product.SalePrice = row.Table.Columns.Contains("SALE_PRICE") ? Convert.ToDouble(row["SALE_PRICE"]) : 0;
            product.Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0;
            product.ReceivedShopId =
                row.Table.Columns.Contains("RECEIVE_FROM") ? Convert.ToInt32(row["RECEIVE_FROM"]) : 0;
            product.UMO =
                row.Table.Columns.Contains("UMO") ? Convert.ToString(row["UMO"]) : "";
            product.Category =
                row.Table.Columns.Contains("PRODUCT_CATEGORY") ? Convert.ToString(row["PRODUCT_CATEGORY"]) : "";
            product.SubCategory =
                row.Table.Columns.Contains("PRODUCT_SUB_CATEGORY") ? Convert.ToString(row["PRODUCT_SUB_CATEGORY"]) : "";
            product.Brand =
                row.Table.Columns.Contains("BRAND") ? Convert.ToString(row["BRAND"]) : "";
            product.SeasonId =
                row.Table.Columns.Contains("SEASON_ID") ? Convert.ToInt32(row["SEASON_ID"]) : 0;
            product.SeasonName =
                row.Table.Columns.Contains("SEASON_NAME") ? Convert.ToString(row["SEASON_NAME"]) : "";

            return product;
            //};
        }
    }
}
