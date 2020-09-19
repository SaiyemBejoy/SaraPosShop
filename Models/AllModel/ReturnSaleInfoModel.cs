using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class ReturnSaleInfoModel : SaleInfoModel
    {
        public int ReturnSaleInfoId { get; set; }
        public int ReturnAmount { get; set; }
        public int ReturnItem { get; set; }
        public double ReturnTotalAmount { get; set; }
        public double ReturnVat { get; set; }


        public static ReturnSaleInfoModel ConvertReturnSaleInfoModel(DataRow row)
        {
            return new ReturnSaleInfoModel
            {
                ReturnAmount = row.Table.Columns.Contains("RETURN_AMOUNT") ? Convert.ToInt32(row["RETURN_AMOUNT"]) : 0
              
            };
        }
    }
}
