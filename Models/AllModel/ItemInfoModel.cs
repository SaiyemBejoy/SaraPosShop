using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class ItemInfoModel : StoreReceiveItem
    {
        public int Quantity { get; set; }
        public int RoundQuantity { get; set; }
        public int PendingTransferQuantity { get; set; }
        public int TotalRowCount { get; set; }

        public static ItemInfoModel ConvertItemInfoModel(DataRow row)
        {
            return new ItemInfoModel
            {
                ItemId = row.Table.Columns.Contains("ItemId") ? Convert.ToInt32(row["ItemId"]) : 0,
                ProductId = row.Table.Columns.Contains("ProductId") ? Convert.ToInt32(row["ProductId"]) : 0,
                ItemName = row.Table.Columns.Contains("ItemName") ? Convert.ToString(row["ItemName"]) : "",
                Barcode = row.Table.Columns.Contains("Barcode") ? Convert.ToString(row["Barcode"]) : "",
                Quantity = row.Table.Columns.Contains("Quantity") ? Convert.ToInt32(row["Quantity"]) : 0,
                PendingTransferQuantity = row.Table.Columns.Contains("PENDINGTRANSFERQTY") ? Convert.ToInt32(row["PENDINGTRANSFERQTY"]) : 0,
                RoundQuantity = row.Table.Columns.Contains("ROUNDQTY") ? Convert.ToInt32(row["ROUNDQTY"]) : 0,
                SalePrice = row.Table.Columns.Contains("SalePrice") ? Convert.ToDouble(row["SalePrice"]) : 0.0,
                Umo = row.Table.Columns.Contains("Umo") ? Convert.ToString(row["Umo"]) : "",
                Vat = row.Table.Columns.Contains("Vat") ? Convert.ToDouble(row["Vat"]) : 0.0

            };
        }
    }
}
