using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class HistoryModel
    {
        public int HistoryId { get; set; }
        public string PreviousInvoiceNumber { get; set; }
        public int PreviousSaleInfoId { get; set; }
        public string PreviousInvoiceDate { get; set; }

        public string NewInvoiceNumber { get; set; }
        public int NewSaleInfoId { get; set; }
        public string NewInvoiceDate { get; set; }

        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string WarehouseId { get; set; }
        public string ShopId { get; set; }

        public static HistoryModel ConvertHistoryModel(DataRow row)
        {
            return new HistoryModel
            {
                HistoryId = row.Table.Columns.Contains("EXCHANGEID") ? Convert.ToInt32(row["EXCHANGEID"]) : 0,
                PreviousInvoiceNumber = row.Table.Columns.Contains("PREVIOUSINVOICENUMBER") ? Convert.ToString(row["PREVIOUSINVOICENUMBER"]) : "",
                PreviousSaleInfoId = row.Table.Columns.Contains("PREVIOUSSALEINFOID") ? Convert.ToInt32(row["PREVIOUSSALEINFOID"]) : 0,
                PreviousInvoiceDate = row.Table.Columns.Contains("PREVIOUSINVOICEDATE") ? Convert.ToString(row["PREVIOUSINVOICEDATE"]) : "",
                NewInvoiceNumber = row.Table.Columns.Contains("NEWINVOICENUMBER") ? Convert.ToString(row["NEWINVOICENUMBER"]) : "",
                NewSaleInfoId = row.Table.Columns.Contains("NEWSALEINFOID") ? Convert.ToInt32(row["NEWSALEINFOID"]) : 0,
                NewInvoiceDate = row.Table.Columns.Contains("NEWINVOICEDATE") ? Convert.ToString(row["NEWINVOICEDATE"]) : "",
                CreateBy = row.Table.Columns.Contains("CREATEBY") ? Convert.ToString(row["CREATEBY"]) : "",
                CreateDate = row.Table.Columns.Contains("CREATEDATE") ? Convert.ToString(row["CREATEDATE"]) : "",
            };
        }
    }

}
