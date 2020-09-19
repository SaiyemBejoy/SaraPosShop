using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class GiftVoucherModel
    {
        public int GiftVoucherDeliveryId { get; set; }
        public int GiftVoucherId { get; set; }
        public int DeliveryItemNum { get; set; }
        public string GiftVoucherCode { get; set; }
        public string GiftVoucherValue { get; set; }

        public string GiftVoucherCustomerName { get; set; }//giftvoucher Deposit korar jonno save kora hoise
        public string GiftVoucherCustomerPhone{ get; set; }//giftvoucher Deposit korar jonno save kora hoise
        public string DepositShopId { get; set; }
        public string DepositShopName { get; set; }

        public double RemainingValue { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }

        public static GiftVoucherModel ConvertGiftVoucherModel(DataRow row)
        {
            return new GiftVoucherModel
            {
                GiftVoucherDeliveryId = row.Table.Columns.Contains("GIFT_VOUCHER_DELIVERY_ID") ? Convert.ToInt32(row["GIFT_VOUCHER_DELIVERY_ID"]) : 0,
                GiftVoucherId = row.Table.Columns.Contains("GIFT_VOUCHER_ID") ? Convert.ToInt32(row["GIFT_VOUCHER_ID"]) : 0,
                DeliveryItemNum = row.Table.Columns.Contains("GIFT_VOUCHER_ITEM_NUM") ? Convert.ToInt32(row["GIFT_VOUCHER_ITEM_NUM"]) : 0,
                GiftVoucherCode = row.Table.Columns.Contains("GIFT_VOUCHER_CODE") ? Convert.ToString(row["GIFT_VOUCHER_CODE"]) : "",
                GiftVoucherValue = row.Table.Columns.Contains("GIFT_VOUCHER_VALUE") ? Convert.ToString(row["GIFT_VOUCHER_VALUE"]) : "",
                RemainingValue = row.Table.Columns.Contains("GIFT_VOUCHER_REMAINING_VALUE") ? Convert.ToDouble(row["GIFT_VOUCHER_REMAINING_VALUE"]) : 0,
                UpdateBy = row.Table.Columns.Contains("UPDATE_BY") ? Convert.ToString(row["UPDATE_BY"]) : "",
                UpdateDate = row.Table.Columns.Contains("UPDATE_DATE") ? Convert.ToString(row["UPDATE_DATE"]) : "",
                DepositShopName = row.Table.Columns.Contains("DEPOSIT_SHOP_NAME") ? Convert.ToString(row["DEPOSIT_SHOP_NAME"]) : "",
            };
        }
    }
}
