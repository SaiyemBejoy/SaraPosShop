using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreHouseRequestMainItem
    {
        public int RequestMainItemId { get; set; }
        public string RequisitionNo { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public static StoreHouseRequestMainItem ConvertStoreHouseRequestMainItem(DataRow row)
        {
            return new StoreHouseRequestMainItem
            {
                RequestMainItemId = row.Table.Columns.Contains("REQUEST_MAIN_ITEM_ID") ? Convert.ToInt32(row["REQUEST_MAIN_ITEM_ID"]) : 0,
                RequisitionNo = row.Table.Columns.Contains("REQUISITION_NO") ? Convert.ToString(row["REQUISITION_NO"]) :"",
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
                Price = row.Table.Columns.Contains("PRICE") ? Convert.ToDouble(row["PRICE"]) : 0

            };
        }
    }
}
