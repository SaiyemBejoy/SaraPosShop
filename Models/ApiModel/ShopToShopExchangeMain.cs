using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.AllModel;

namespace Models.ApiModel
{
    public class ShopToShopExchangeMain
    {
        public int StoreReceiveId { get; set; }
        public string StoreReceiveChallanNo { get; set; }
        public string SeasonName { get; set; }
        public string ReceiveFrom { get; set; }
        public string ReceivedBy { get; set; }
        public string ReceivedDate { get; set; }
        public string StoreReceive_YN { get; set; }
        public string WareHouseId { get; set; }
        public string ShopId { get; set; }
        public string ShopExChallan_YN { get; set; }

        public IEnumerable<ShopToShopExItem> ShopToShopExItemList { get; set; }

        public static ShopToShopExchangeMain ConvertShopToShopExchangeMain(DataRow row)
        {
            return new ShopToShopExchangeMain
            {
                StoreReceiveId = row.Table.Columns.Contains("STORE_RECEIVE_ID") ? Convert.ToInt32(row["STORE_RECEIVE_ID"]) : 0,
                StoreReceiveChallanNo = row.Table.Columns.Contains("STORE_RECEIVE_CHALLAN_NO") ? Convert.ToString(row["STORE_RECEIVE_CHALLAN_NO"]) : "",
                SeasonName = row.Table.Columns.Contains("SEASON_NAME") ? Convert.ToString(row["SEASON_NAME"]) : "",
                ReceiveFrom = row.Table.Columns.Contains("RECEIVE_FROM") ? Convert.ToString(row["RECEIVE_FROM"]) : "",
                ReceivedBy = row.Table.Columns.Contains("RECEIVED_BY") ? Convert.ToString(row["RECEIVED_BY"]) : "",
                ReceivedDate = row.Table.Columns.Contains("RECEIVE_DATE") ? Convert.ToString(row["RECEIVE_DATE"]) : "",
                StoreReceive_YN = row.Table.Columns.Contains("STORE_RECEIVE_YN") ? Convert.ToString(row["STORE_RECEIVE_YN"]) : "",
                WareHouseId = row.Table.Columns.Contains("WARE_HOUSE_ID") ? Convert.ToString(row["WARE_HOUSE_ID"]) : "",
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : "",
                ShopExChallan_YN = row.Table.Columns.Contains("SHOP_EX_CHALLAN_YN") ? Convert.ToString(row["SHOP_EX_CHALLAN_YN"]) : "",

            };
        }
    }
}
