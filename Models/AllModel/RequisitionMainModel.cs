using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class RequisitionMainModel
    {
        public int RequisitionId { get; set; }
        public string RequisitionNo { get; set; }
        public string RequisitionDate { get; set; }
        public string CreatedBy { get; set; }
        public string ShopId { get; set; }

        public IEnumerable<RequisitionMainItemModel> RequisitionMainItemList { get; set; }

        public static RequisitionMainModel ConvertRequisitionMainModel(DataRow row)
        {
            return new RequisitionMainModel
            {
                RequisitionId = row.Table.Columns.Contains("REQUISITION_ID") ? Convert.ToInt32(row["REQUISITION_ID"]) : 0,
                RequisitionNo = row.Table.Columns.Contains("REQUISITION_NO") ? Convert.ToString(row["REQUISITION_NO"]) : "",
                RequisitionDate = row.Table.Columns.Contains("REQUISITION_DATE") ? Convert.ToString(row["REQUISITION_DATE"]) : "",
                CreatedBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : ""
                
            };
        }
    }
}
