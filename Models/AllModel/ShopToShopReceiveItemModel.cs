using System;
using System.Data;

namespace Models.AllModel
{
    public class ShopToShopReceiveItemModel
    {
        public int ShopReceiveItemId { get; set; }
        public int ShopToShopReceiveId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public static ShopToShopReceiveItemModel ConvertShopToShopReceiveItemModel(DataRow row)
        {
            return new ShopToShopReceiveItemModel
            {
                ShopReceiveItemId = row.Table.Columns.Contains("SHOP_RECEIVE_ITEM_ID") ? Convert.ToInt32(row["SHOP_RECEIVE_ITEM_ID"]) : 0,
                ShopToShopReceiveId = row.Table.Columns.Contains("SHOP_RECEIVE_ID") ? Convert.ToInt32(row["SHOP_RECEIVE_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Price = row.Table.Columns.Contains("PRICE") ? Convert.ToDouble(row["PRICE"]) : 0.0,
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
                
            };
        }
    }
}