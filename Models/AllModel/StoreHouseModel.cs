using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreHouseModel
    {
        public int StoreHouseId { get; set; }
        public string LineNo { get; set; }
        public string RopeNo { get; set; }
        public string RackNo { get; set; }
        public string Remarks { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string StyleName { get; set; }

        public List<StoreHouseItemModel> StoreHouseItemModelList { get; set; }

        public static StoreHouseModel ConvertStoreHouseModel(DataRow row)
        {
            return new StoreHouseModel
            {
                StoreHouseId = row.Table.Columns.Contains("STORE_HOUSE_ID") ? Convert.ToInt32(row["STORE_HOUSE_ID"]) : 0,
                LineNo = row.Table.Columns.Contains("LINE_NO") ? Convert.ToString(row["LINE_NO"]) : "",
                RopeNo = row.Table.Columns.Contains("ROPE_NO") ? Convert.ToString(row["ROPE_NO"]) : "",
                RackNo = row.Table.Columns.Contains("RACK_NO") ? Convert.ToString(row["RACK_NO"]) : "",
                Remarks = row.Table.Columns.Contains("REMARKS") ? Convert.ToString(row["REMARKS"]) : "",
                CreatedDate = row.Table.Columns.Contains("CREATED_DATE") ? Convert.ToString(row["CREATED_DATE"]) : "",
                CreatedBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
               
            };
        }
    }
}
