using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreHouseRequestMainItemConfirm
    {
        public string RequisitionNo { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public string LineNo { get; set; }
        public string RopeNo { get; set; }
        public string RackNo { get; set; }

        public static StoreHouseRequestMainItemConfirm ConvertStoreHouseRequestMainItem(DataRow row)
        {
            return new StoreHouseRequestMainItemConfirm
            {
                RequisitionNo = row.Table.Columns.Contains("REQUISITION_NO") ? Convert.ToString(row["REQUISITION_NO"]) : "",
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                LineNo = row.Table.Columns.Contains("LINE_NO") ? Convert.ToString(row["LINE_NO"]) : "",
                RopeNo = row.Table.Columns.Contains("ROPE_NO") ? Convert.ToString(row["ROPE_NO"]) : "",
                RackNo = row.Table.Columns.Contains("RACK_NO") ? Convert.ToString(row["RACK_NO"]) : "",
            };
        }
    }
}
