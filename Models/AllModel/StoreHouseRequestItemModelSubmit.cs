using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreHouseRequestItemModelSubmit
    {
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public IEnumerable<StoreHouseModel> LineRopeRackList { get; set; }

        public static StoreHouseRequestItemModelSubmit ConvertStoreHouseRequestItemModelSubmit(DataRow row)
        {
            return new StoreHouseRequestItemModelSubmit
            {
               
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0

            };
        }
    }
}
