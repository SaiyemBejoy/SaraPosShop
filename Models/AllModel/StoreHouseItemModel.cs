using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreHouseItemModel
    {
        public int StoreHouseItemId { get; set; }
        public int StoreHouseId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double SalePrice { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }

        public static StoreHouseItemModel ConvertStoreHouseItemModel(DataRow row)
        {
            return new StoreHouseItemModel
            {
                StoreHouseItemId = row.Table.Columns.Contains("STORE_HOUSE_ITEM_ID") ? Convert.ToInt32(row["STORE_HOUSE_ITEM_ID"]) : 0,
                StoreHouseId = row.Table.Columns.Contains("STORE_HOUSE_ID") ? Convert.ToInt32(row["STORE_HOUSE_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
                SalePrice = row.Table.Columns.Contains("SALE_PRICE") ? Convert.ToDouble(row["SALE_PRICE"]) : 0.0,
                Category = row.Table.Columns.Contains("CATEGORY_NAME") ? Convert.ToString(row["CATEGORY_NAME"]) : "",
                SubCategory = row.Table.Columns.Contains("SUB_CATEGORY_NAME") ? Convert.ToString(row["SUB_CATEGORY_NAME"]) : "",
            };
        }
    }
}
