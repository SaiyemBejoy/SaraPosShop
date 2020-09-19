using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreReceiveModel
    {
        public int StoreReceiveId { get; set; }
        //This For PurchaseReceiveNumber Save and Dropdown
        public string StoreReceiveChallanNo { get; set; }
        //End
        // same screen A return er data receive korar jonno manager a chaek kora hoise
        public string ReturnChallanNoReceive { get; set; }

        public string SeasonName { get; set; }
        public string ReceiveFrom { get; set; }
        public string ReceivedBy { get; set; }
        public string ReceivedDate { get; set; }
        public string StoreReceive_YN { get; set; }
        public string WareHouseId { get; set; }
        public string ShopId { get; set; }

        public  List<StoreReceiveItem> StoreReceiveItemList { get; set; }

        public static StoreReceiveModel ConvertStoreReceiveModel(DataRow row)
        {
            return new StoreReceiveModel
            {
                StoreReceiveId = row.Table.Columns.Contains("STORE_RECEIVE_ID") ? Convert.ToInt32(row["STORE_RECEIVE_ID"]) : 0,
                StoreReceiveChallanNo = row.Table.Columns.Contains("STORE_RECEIVE_CHALLAN_NO") ? Convert.ToString(row["STORE_RECEIVE_CHALLAN_NO"]) : "",
                SeasonName = row.Table.Columns.Contains("SEASON_NAME") ? Convert.ToString(row["SEASON_NAME"]) : "",
                ReceiveFrom = row.Table.Columns.Contains("RECEIVE_FROM") ? Convert.ToString(row["RECEIVE_FROM"]) : "",
                ReceivedBy = row.Table.Columns.Contains("RECEIVE_BY") ? Convert.ToString(row["RECEIVE_BY"]) : "",
                ReceivedDate = row.Table.Columns.Contains("RECEIVED_DATE") ? Convert.ToDateTime(row["RECEIVED_DATE"]).ToString("dd/mm/yyyy") : "",
                StoreReceive_YN = row.Table.Columns.Contains("STORE_RECEIVE_YN") ? Convert.ToString(row["STORE_RECEIVE_YN"]) : "",
                WareHouseId = row.Table.Columns.Contains("WAREHOUSE_ID") ? Convert.ToString(row["WAREHOUSE_ID"]) : "",
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : ""

            };
        }
        public static StoreReceiveModel ConvertStoreReceiveModelForDataTable(DataRow row)
        {
            return new StoreReceiveModel
            {
                StoreReceiveId = row.Table.Columns.Contains("STORERECEIVEID") ? Convert.ToInt32(row["STORERECEIVEID"]) : 0,
                StoreReceiveChallanNo = row.Table.Columns.Contains("STORERECEIVECHALLANNO") ? Convert.ToString(row["STORERECEIVECHALLANNO"]) : "",
                SeasonName = row.Table.Columns.Contains("SEASONNAME") ? Convert.ToString(row["SEASONNAME"]) : "",
                ReceiveFrom = row.Table.Columns.Contains("RECEIVEFROM") ? Convert.ToString(row["RECEIVEFROM"]) : "",
                ReceivedBy = row.Table.Columns.Contains("RECEIVEDBY") ? Convert.ToString(row["RECEIVEDBY"]) : "",
                ReceivedDate = row.Table.Columns.Contains("RECEIVEDATE") ? Convert.ToString(row["RECEIVEDATE"]) : "",
                StoreReceive_YN = row.Table.Columns.Contains("STORERECEIVEYN") ? Convert.ToString(row["STORERECEIVEYN"]) : "",
                WareHouseId = row.Table.Columns.Contains("WAREHOUSEID") ? Convert.ToString(row["WAREHOUSEID"]) : "",
                ShopId = row.Table.Columns.Contains("SHOPID") ? Convert.ToString(row["SHOPID"]) : ""

            };
        }

    }
}
