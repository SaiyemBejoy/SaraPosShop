using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StockCalculateSummary
    {
        public int TotalReceive { get; set; }
        public int TotalSale { get; set; }
        public int TotalPendingTransfer { get; set; }
        public int TotalTransfer { get; set; }
        public int TotalDamage { get; set; }
        public int TotalCurrentStock { get; set; }

        public static StockCalculateSummary ConvertStockCalculateSummary(DataRow row)
        {
            return new StockCalculateSummary
            {
                TotalReceive = row.Table.Columns.Contains("RECEIVEQUANTITY") ? Convert.ToInt32(row["RECEIVEQUANTITY"]) : 0,
                TotalSale = row.Table.Columns.Contains("SALEQUANTITY") ? Convert.ToInt32(row["SALEQUANTITY"]) : 0,
                TotalPendingTransfer = row.Table.Columns.Contains("PENDINGTRANSFERQTY") ? Convert.ToInt32(row["PENDINGTRANSFERQTY"]) : 0,
                TotalTransfer = row.Table.Columns.Contains("WAREHOUSETRANSFERQTY") ? Convert.ToInt32(row["WAREHOUSETRANSFERQTY"]) : 0,
                TotalDamage = row.Table.Columns.Contains("DAMAGEQUANTITY") ? Convert.ToInt32(row["DAMAGEQUANTITY"]): 0,
                TotalCurrentStock = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
            };
        }
    }
}
