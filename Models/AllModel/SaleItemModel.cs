using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class SaleItemModel
    {
        public int SaleItemId { get; set; }
        public int SaleInfoId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string StyleName { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public double Vat { get; set; }
        public double Total { get; set; }
        public double PaidAmount { get; set; }
        public string SalesManId { get; set; }
        public string SalesManName { get; set; }
        public string ShopToShopExYn { get; set; }

        public static SaleItemModel ConvertSaleItemModel(DataRow row)
        {
            return new SaleItemModel
            {
                SaleItemId = row.Table.Columns.Contains("SALE_ITEM_ID") ? Convert.ToInt32(row["SALE_ITEM_ID"]) : 0,
                SaleInfoId = row.Table.Columns.Contains("SALE_INFO_ID") ? Convert.ToInt32(row["SALE_INFO_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                StyleName = row.Table.Columns.Contains("STYLE_NAME") ? Convert.ToString(row["STYLE_NAME"]) : "",
                Price = row.Table.Columns.Contains("PRICE") ? Convert.ToDouble(row["PRICE"]) : 0.0,
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,               
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0,
                Total = row.Table.Columns.Contains("TOTAL") ? Convert.ToDouble(row["TOTAL"]) : 0.0,
                SalesManId = row.Table.Columns.Contains("SALESMAN_ID") ? Convert.ToString(row["SALESMAN_ID"]) : "",
                SalesManName = row.Table.Columns.Contains("SALESMAN_NAME") ? Convert.ToString(row["SALESMAN_NAME"]) : "",
                PaidAmount = row.Table.Columns.Contains("PAID_AMOUNT") ? Convert.ToDouble(row["PAID_AMOUNT"]) : 0.0,
                DiscountPrice = row.Table.Columns.Contains("DIS_PRICE") ? Convert.ToDouble(row["DIS_PRICE"]) : 0.0,

            };
        }

    }
}
