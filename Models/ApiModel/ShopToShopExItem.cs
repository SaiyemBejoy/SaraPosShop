using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class ShopToShopExItem
    {
        public int StoreReceiveItemId { get; set; }
        public int StoreReceiveId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public int ReceiveQuantity { get; set; }
        public double SalePrice { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }
        public string Umo { get; set; }
        public double Vat { get; set; }

        public static ShopToShopExItem ConvertShopToShopExItem(DataRow row)
        {
            return new ShopToShopExItem
            {
                StoreReceiveItemId = row.Table.Columns.Contains("STORE_RECEIVE_ITEM_ID") ? Convert.ToInt32(row["STORE_RECEIVE_ITEM_ID"]) : 0,
                StoreReceiveId = row.Table.Columns.Contains("STORE_RECEIVE_ID") ? Convert.ToInt32(row["STORE_RECEIVE_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ReceiveQuantity = row.Table.Columns.Contains("RECEIVE_QUANTITY") ? Convert.ToInt32(row["RECEIVE_QUANTITY"]) : 0,
                SalePrice = row.Table.Columns.Contains("SALE_PRICE") ? Convert.ToDouble(row["SALE_PRICE"]) : 0.0,
                CategoryName = row.Table.Columns.Contains("CATEGORY_NAME") ? Convert.ToString(row["CATEGORY_NAME"]) : "",
                SubCategoryName = row.Table.Columns.Contains("SUB_CATEGORY_NAME") ? Convert.ToString(row["SUB_CATEGORY_NAME"]) : "",
                BrandName = row.Table.Columns.Contains("BRAND_NAME") ? Convert.ToString(row["BRAND_NAME"]) : "",
                Umo = row.Table.Columns.Contains("UMO") ? Convert.ToString(row["UMO"]) : "",
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0

            };
        }
    }
}
