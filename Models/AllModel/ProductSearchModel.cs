using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class ProductSearchModel : SaleItemModel
    {
        public int ReceiveQuantity { get; set; }
        public int SaleQuantity { get; set; }
        public int DamageQuantity { get; set; }
        public int ShopToShopRecQty { get; set; }
        public int ShopToShopDeliQty { get; set; }
        public int ShopTrQty { get; set; }
        public int PendingTrQty { get; set; }
        public int RoundQuantity { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }


        public static ProductSearchModel ConvertProductSearchModel(DataRow row)
        {
            return new ProductSearchModel
            {
                ItemId = row.Table.Columns.Contains("ITEMID") ? Convert.ToInt32(row["ITEMID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCTID") ? Convert.ToInt32(row["PRODUCTID"]) : 0,
                ItemName = row.Table.Columns.Contains("ITEMNAME") ? Convert.ToString(row["ITEMNAME"]) : "",
                CategoryName = row.Table.Columns.Contains("CATEGORY_NAME") ? Convert.ToString(row["CATEGORY_NAME"]) : "",
                SubCategoryName = row.Table.Columns.Contains("SUB_CATEGORY_NAME") ? Convert.ToString(row["SUB_CATEGORY_NAME"]) : "",
                StyleName = row.Table.Columns.Contains("ITEMNAME") ? Convert.ToString(row["ITEMNAME"]) : "",
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ReceiveQuantity = row.Table.Columns.Contains("RECEIVEQUANTITY") ? Convert.ToInt32(row["RECEIVEQUANTITY"]) : 0,
                SaleQuantity = row.Table.Columns.Contains("SALEQUANTITY") ? Convert.ToInt32(row["SALEQUANTITY"]) : 0,
                DamageQuantity = row.Table.Columns.Contains("DAMAGEQUANTITY") ? Convert.ToInt32(row["DAMAGEQUANTITY"]) : 0,
                ShopToShopRecQty = row.Table.Columns.Contains("SHOPTOSHOPRECEIVEQTY") ? Convert.ToInt32(row["SHOPTOSHOPRECEIVEQTY"]) : 0,
                ShopToShopDeliQty = row.Table.Columns.Contains("SHOPTOSHOPDELIVERYQTY") ? Convert.ToInt32(row["SHOPTOSHOPDELIVERYQTY"]) : 0,
                ShopTrQty = row.Table.Columns.Contains("WAREHOUSETRANSFERQTY") ? Convert.ToInt32(row["WAREHOUSETRANSFERQTY"]) : 0,
                PendingTrQty = row.Table.Columns.Contains("PENDINGTRANSFERQTY") ? Convert.ToInt32(row["PENDINGTRANSFERQTY"]) : 0,
                Quantity = row.Table.Columns.Contains("QUANTITY") ? Convert.ToInt32(row["QUANTITY"]) : 0,
                RoundQuantity = row.Table.Columns.Contains("ROUNDQTY") ? Convert.ToInt32(row["ROUNDQTY"]) : 0,
                Price = row.Table.Columns.Contains("SALEPRICE") ? Convert.ToInt32(row["SALEPRICE"]) : 0,
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0

            };
        }
    }
}
