using System;
using System.Data;

namespace Models.AllModel
{
    public class HotSaleAndLowStockModel
    {
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public static HotSaleAndLowStockModel ConvertHotSaleAndLowStockModel(DataRow row)
        {
            return new HotSaleAndLowStockModel
            {
                BarCode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEMNAME") ? Convert.ToString(row["ITEMNAME"]) : "",
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
            };
        }
        public static HotSaleAndLowStockModel ConvertHotSaleModel(DataRow row)
        {
            return new HotSaleAndLowStockModel
            {
                BarCode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("STYLE_NAME") ? Convert.ToString(row["STYLE_NAME"]) : "",
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
            };
        }
    }
}