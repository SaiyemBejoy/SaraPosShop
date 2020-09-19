using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class RequisitionMainItemModel
    {
        public int RequisitionMainItemId { get; set; }
        public int RequisitionId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public string Price { get; set; }

        public static RequisitionMainItemModel ConvertRequisitionMainItemModel(DataRow row)
        {
            return new RequisitionMainItemModel
            {
                RequisitionMainItemId = row.Table.Columns.Contains("REQUISITION_MAIN_ITEM_ID") ? Convert.ToInt32(row["REQUISITION_MAIN_ITEM_ID"]) : 0,
                RequisitionId = row.Table.Columns.Contains("REQUISITION_ID") ? Convert.ToInt32(row["REQUISITION_ID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Price = row.Table.Columns.Contains("PRICE") ? Convert.ToString(row["PRICE"]) : ""

            };
        }
    }
}
