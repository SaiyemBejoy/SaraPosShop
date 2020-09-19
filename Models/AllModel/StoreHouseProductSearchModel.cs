using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreHouseProductSearchModel
    {
        public int StoreHouseId { get; set; }
        public string LineNo { get; set; }
        public string RopeNo { get; set; }
        public string RackNo { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public int Quantity { get; set; }

        public static StoreHouseProductSearchModel ConvertStoreHouseProductSearchModel(DataRow row)
        {
            return new StoreHouseProductSearchModel
            {
                StoreHouseId = row.Table.Columns.Contains("STOREHOUSEID") ? Convert.ToInt32(row["STOREHOUSEID"]) : 0,
                LineNo = row.Table.Columns.Contains("LINENO") ? Convert.ToString(row["LINENO"]) : "",
                RopeNo = row.Table.Columns.Contains("ROPENO") ? Convert.ToString(row["ROPENO"]) : "",
                RackNo = row.Table.Columns.Contains("RACKNO") ? Convert.ToString(row["RACKNO"]) : "",
                ProductId = row.Table.Columns.Contains("PRODUCTID") ? Convert.ToInt32(row["PRODUCTID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEMID") ? Convert.ToInt32(row["ITEMID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEMNAME") ? Convert.ToString(row["ITEMNAME"]) : "",
                CategoryName = row.Table.Columns.Contains("CATEGORYNAME") ? Convert.ToString(row["CATEGORYNAME"]) : "",
                SubCategoryName = row.Table.Columns.Contains("SUBCATEGORYNAME") ? Convert.ToString(row["SUBCATEGORYNAME"]) : "",
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0
            };
        }
    }
}
