using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class TransitItemInfoModel
    {
        public int MarketPlaceId { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public double SalePrice { get; set; }
        public int TransitQuantity { get; set; }
        public int TransitReturnQuantity { get; set; }
        public int Stock { get; set; }

        public static TransitItemInfoModel ConvertTransitItemInfoModel(DataRow row)
        {
            return new TransitItemInfoModel
            {
                MarketPlaceId = row.Table.Columns.Contains("MARKET_PLACE_ID") ? Convert.ToInt32(row["MARKET_PLACE_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                SalePrice = row.Table.Columns.Contains("SALE_PRICE") ? Convert.ToDouble(row["SALE_PRICE"]) : 0.0,
                TransitQuantity = row.Table.Columns.Contains("TRANSIT_QTY") ? Convert.ToInt32(row["TRANSIT_QTY"]) : 0,
                TransitReturnQuantity = row.Table.Columns.Contains("TRANSIT_RETURN_QTY") ? Convert.ToInt32(row["TRANSIT_RETURN_QTY"]) : 0,
                Stock = row.Table.Columns.Contains("STOCK") ? Convert.ToInt32(row["STOCK"]) : 0,

            };
        }
    }
}
