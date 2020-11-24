using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class TransitItemInfoModel
    {
        public int MarketPlaceId { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public double SalePrice { get; set; }
        public int TransitQuantity { get; set; }
        public int TransitReturnQuantity { get; set; }
        public int Stock { get; set; }
        //Sale korer time a marletplaceId stock check korer jonno
        public int TransitStockBlance { get; set; }
        public int   TransitSaleQty { get; set; }
        //End
        //Challan No dia search kore data nia anar jonno
        public string StoreReceiveChallanNo { get; set; }
        public int ReceiveQty { get; set; }
        public string CategoryName { get; set; }
        //End
        public static TransitItemInfoModel ConvertTransitItemInfoModel(DataRow row)
        {
            return new TransitItemInfoModel
            {
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                SalePrice = row.Table.Columns.Contains("SALE_PRICE") ? Convert.ToDouble(row["SALE_PRICE"]) : 0.0,
                TransitSaleQty = row.Table.Columns.Contains("TRANSIT_SALE_QTY") ? Convert.ToInt32(row["TRANSIT_SALE_QTY"]) : 0,
                TransitStockBlance = row.Table.Columns.Contains("TRANSIT_BLANCE") ? Convert.ToInt32(row["TRANSIT_BLANCE"]) : 0,
                Stock = row.Table.Columns.Contains("STOCK") ? Convert.ToInt32(row["STOCK"]) : 0,
            };
        }

       
        public static TransitItemInfoModel ConvertTransitItemInfoByChallanNoModel(DataRow row)
        {
            return new TransitItemInfoModel
            {
                StoreReceiveChallanNo = row.Table.Columns.Contains("STORE_RECEIVE_CHALLAN_NO") ? Convert.ToString(row["STORE_RECEIVE_CHALLAN_NO"]) : "",
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                SalePrice = row.Table.Columns.Contains("SALE_PRICE") ? Convert.ToDouble(row["SALE_PRICE"]) : 0.0,
                ReceiveQty = row.Table.Columns.Contains("RECEIVE_QUANTITY") ? Convert.ToInt32(row["RECEIVE_QUANTITY"]) : 0,
            };
        }
    }
}
