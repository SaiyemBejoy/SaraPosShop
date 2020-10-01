using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class VoidInvoiceItem
    {
        public int SaleItemId { get; set; }
        public int SaleInfoId { get; set; }
        public string Barcode { get; set; }
        public string StyleName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Vat { get; set; }
        public int Total { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubcategory { get; set; }
        public string SalesManId { get; set; }
        public string SalesManName { get; set; }

        /*
  PRODUCT_CATEGORY      VARCHAR2(100 BYTE),
  PRODUCT_SUB_CATEGORY  VARCHAR2(100 BYTE),
  SALESMAN_ID           VARCHAR2(200 BYTE),
             */

        public static VoidInvoiceItem ConvertVoidInvoiceItem(DataRow row)
        {
            return new VoidInvoiceItem
            {
                SaleItemId = row.Table.Columns.Contains("SALE_ITEM_ID") ? Convert.ToInt32(row["SALE_ITEM_ID"]) : 0,
                SaleInfoId = row.Table.Columns.Contains("SALE_INFO_ID") ? Convert.ToInt32(row["SALE_INFO_ID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                StyleName = row.Table.Columns.Contains("STYLE_NAME") ? Convert.ToString(row["STYLE_NAME"]) : "",
                Price = row.Table.Columns.Contains("PRICE") ? Convert.ToDouble(row["PRICE"]) : 0.0,
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0,
                Total = row.Table.Columns.Contains("TOTAL") ? Convert.ToInt32(row["TOTAL"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                ProductCategory = row.Table.Columns.Contains("PRODUCT_CATEGORY") ? Convert.ToString(row["PRODUCT_CATEGORY"]) : "",
                ProductSubcategory = row.Table.Columns.Contains("PRODUCT_SUB_CATEGORY") ? Convert.ToString(row["PRODUCT_SUB_CATEGORY"]) : "",
                SalesManId = row.Table.Columns.Contains("SALESMAN_ID") ? Convert.ToString(row["SALESMAN_ID"]) : "",
                SalesManName = row.Table.Columns.Contains("SALESMAN_NAME") ? Convert.ToString(row["SALESMAN_NAME"]) : ""
            };
        }

    }
}