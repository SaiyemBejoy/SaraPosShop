using System;
using System.Data;

namespace Models.AllModel
{
    public class StockTransferItemModel
    {
        public int StockTransferItemId { get; set; }
        public int StockTransferId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public int TransferQuantity { get; set; }
        public double SalePrice { get; set; }
        public double Vat { get; set; }

        public static StockTransferItemModel ConvertStockTransferItemModel(DataRow row)
        {
            return new StockTransferItemModel
            {
                StockTransferItemId = row.Table.Columns.Contains("STOCK_TRANSFER_ITEM_ID") ? Convert.ToInt32(row["STOCK_TRANSFER_ITEM_ID"]) : 0,
                StockTransferId = row.Table.Columns.Contains("STOCK_TRANSFER_ID") ? Convert.ToInt32(row["STOCK_TRANSFER_ID"]) : 0,
                ItemId = row.Table.Columns.Contains("ITEM_ID") ? Convert.ToInt32(row["ITEM_ID"]) : 0,
                ProductId = row.Table.Columns.Contains("PRODUCT_ID") ? Convert.ToInt32(row["PRODUCT_ID"]) : 0,
                ItemName = row.Table.Columns.Contains("ITEM_NAME") ? Convert.ToString(row["ITEM_NAME"]) : "",
                Barcode = row.Table.Columns.Contains("BARCODE") ? Convert.ToString(row["BARCODE"]) : "",
                TransferQuantity = row.Table.Columns.Contains("TRANSFER_QUANTITY") ? Convert.ToInt32(row["TRANSFER_QUANTITY"]) : 0,
                SalePrice = row.Table.Columns.Contains("SALE_PRICE") ? Convert.ToDouble(row["SALE_PRICE"]) : 0.0,
                Vat = row.Table.Columns.Contains("VAT") ? Convert.ToDouble(row["VAT"]) : 0.0

            };
        }
    }
}