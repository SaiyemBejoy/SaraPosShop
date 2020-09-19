using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;

namespace Models.ApiModel
{
    public class HotSaleItemModel
    {

        public string BarCode { get; set; }
        public string StyleName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ItemCount { get; set; }

        public static HotSaleItemModel ConvertHotSaleItemModel(DataRow row)
        {
            return new HotSaleItemModel
            {
                BarCode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                StyleName = row.Table.Columns.Contains("STYLE_NAME") ? Convert.ToString(row["STYLE_NAME"]) : "",
                Price = row.Table.Columns.Contains("PRICE") ? Convert.ToDouble(row["PRICE"]) : 0.0,
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
                ItemCount = row.Table.Columns.Contains("ITEM_COUNT") ? Convert.ToInt32(row["ITEM_COUNT"]) : 0

            };
        }
    }
}
