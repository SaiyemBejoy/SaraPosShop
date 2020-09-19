using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class DamageMainItemModel
    {
        public int DamageMainItemId { get; set; }
        public string DamageChallanNo { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string Remarks { get; set; }
        public double Vat { get; set; }

        public static DamageMainItemModel ConvertDamageMainItemModel(DataRow row)
        {
            return new DamageMainItemModel
            {
                DamageMainItemId = row.Table.Columns.Contains("DAMAGE_MAIN_ITEM_ID") ? Convert.ToInt32(row["DAMAGE_MAIN_ITEM_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                DamageChallanNo = row.Table.Columns.Contains("DAMAGE_CHALLAN_NO") ? Convert.ToString(row["DAMAGE_CHALLAN_NO"]) : "",
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Price = row.Table.Columns.Contains("PRICE") ? Convert.ToDouble(row["PRICE"]) : 0.0,
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToDouble(row["QUANTITY"]) : 0.0,
                Remarks = row.Table.Columns.Contains("REMARKS") ? Convert.ToString(row["REMARKS"]) : "",
                Vat = row.Table.Columns.Contains("Vat") ? Convert.ToDouble(row["Vat"]) : 0.0,

            };
        }
    }
}
