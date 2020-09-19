using System;
using System.Collections.Generic;
using System.Data;

namespace Models.AllModel
{
    public class StockTransferModel
    {
        public int StockTransferId { get; set; }

        public string StockTransferChallanNumber { get; set; }

        public string RequisitionNumber { get; set; }

        public string ShopToShopRequisitionNumber { get; set; }

        public string TransferDate { get; set; }

        public string TransferToShopId { get; set; }

        public string TransferToShopName { get; set; }

        public string TransferFromShopId { get; set; }

        public string ReceivedYN{ get; set; }

        public string TransferedBy{ get; set; }
        public string TransferedByName { get; set; }

        public List<StockTransferItemModel> StockTransferItemList { get; set; }


        public static StockTransferModel ConvertStockTransferModel(DataRow row)
        {
            return new StockTransferModel
            {
                StockTransferId = row.Table.Columns.Contains("STOCK_TRANSFER_ID") ? Convert.ToInt32(row["STOCK_TRANSFER_ID"]) : 0,
                StockTransferChallanNumber = row.Table.Columns.Contains("STOCK_TRANSFER_CHALLAN_NUM") ? Convert.ToString(row["STOCK_TRANSFER_CHALLAN_NUM"]) : "",
                RequisitionNumber = row.Table.Columns.Contains("REQUISITION_NUM") ? Convert.ToString(row["REQUISITION_NUM"]) : "",
                TransferDate = row.Table.Columns.Contains("TRANSFER_DATE") ? Convert.ToString(row["TRANSFER_DATE"]) : "",
                TransferToShopId = row.Table.Columns.Contains("TRANSFER_SHOPID_TO") ? Convert.ToString(row["TRANSFER_SHOPID_TO"]) : "",
                TransferFromShopId = row.Table.Columns.Contains("TRANSFER_SHOPID_FROM") ? Convert.ToDateTime(row["TRANSFER_SHOPID_FROM"]).ToString("dd/mm/yyyy") : "",
                ReceivedYN = row.Table.Columns.Contains("RECEIVE_YN") ? Convert.ToString(row["RECEIVE_YN"]) : "",
                TransferedBy = row.Table.Columns.Contains("TRANSFERED_BY") ? Convert.ToString(row["TRANSFERED_BY"]) : "",
               
            };
        }

        public static StockTransferModel ConvertStockTransferModelForDataTable(DataRow row)
        {
            return new StockTransferModel
            {
                StockTransferId = row.Table.Columns.Contains("STOCKTRANSFERID") ? Convert.ToInt32(row["STOCKTRANSFERID"]) : 0,
                StockTransferChallanNumber = row.Table.Columns.Contains("STOCKTRANSFERCHALLANNUMBER") ? Convert.ToString(row["STOCKTRANSFERCHALLANNUMBER"]) : "",
                RequisitionNumber = row.Table.Columns.Contains("REQUISITIONNUM") ? Convert.ToString(row["REQUISITIONNUM"]) : "",
                TransferDate = row.Table.Columns.Contains("TRANSFERDATE") ? Convert.ToString(row["TRANSFERDATE"]) : "",
                TransferToShopId = row.Table.Columns.Contains("TRANSFERSHOPIDTO") ? Convert.ToString(row["TRANSFERSHOPIDTO"]) : "",
                TransferToShopName = row.Table.Columns.Contains("TRANSFERSHOPIDTONAME") ? Convert.ToString(row["TRANSFERSHOPIDTONAME"]): "",
                TransferFromShopId = row.Table.Columns.Contains("TRANSFERSHOPIDFROM") ? Convert.ToString(row["TRANSFERSHOPIDFROM"]) : "",
                ReceivedYN = row.Table.Columns.Contains("RECEIVEYN") ? Convert.ToString(row["RECEIVEYN"]) : "",
                TransferedBy = row.Table.Columns.Contains("TRANSFEREDBY") ? Convert.ToString(row["TRANSFEREDBY"]) : "",
                TransferedByName = row.Table.Columns.Contains("TRANSFEREDBYNAME") ? Convert.ToString(row["TRANSFEREDBYNAME"]) : "",
            };
        }

    }
}