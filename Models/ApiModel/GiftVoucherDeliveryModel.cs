using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class GiftVoucherDeliveryModel
    {
        public int GiftVoucherDeliveryId { get; set; }
        public int GiftVoucherId { get; set; }
        public int DeliveryItemNum { get; set; }
        public string GiftVoucherCode { get; set; }
        public string GiftVoucherValue { get; set; }
        public double RemainingValue { get; set; }
        public string DeliveryDate { get; set; }
        public string UpdateBy { get; set; }
        public string DepositYN { get; set; }
        public string DepositShopId { get; set; }
    }
}
