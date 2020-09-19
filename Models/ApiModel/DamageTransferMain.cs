using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class DamageTransferMain
    {
        public int DamageTransferId { get; set; }

        public string DamageTrschallanNo { get; set; }

        public string RequisitionNo { get; set; }

        public string TransferShopIdfrom { get; set; }

        public string TransferedBy { get; set; }

        public string TransferDate { get; set; }

        public List<DamageTransferItem> DamageTransferItemList { get; set; }

    }
}
