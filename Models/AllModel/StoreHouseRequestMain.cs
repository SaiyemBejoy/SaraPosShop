using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class StoreHouseRequestMain
    {
        public int RequestId { get; set; }
        public string RequisitionNo { get; set; }
        public string FloorNo { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ShopId { get; set; }
        public string SubmitYN { get; set; }
        public string UpdateddDate { get; set; }
        public string UpdatedBy { get; set; }
        public string EmployeeName { get; set; }

        public IEnumerable<StoreHouseRequestMainItem> StoreHouseRequestMainItemList { get; set; }
        public IEnumerable<StoreHouseRequestMainItemConfirm> StoreHouseRequestMainItemConfirmList { get; set; }

        public static StoreHouseRequestMain ConvertStoreHouseRequestMain(DataRow row)
        {
            return new StoreHouseRequestMain
            {
                RequestId = row.Table.Columns.Contains("REQUEST_ID") ? Convert.ToInt32(row["REQUEST_ID"]) : 0,
                RequisitionNo = row.Table.Columns.Contains("REQUISITION_NO") ? Convert.ToString(row["REQUISITION_NO"]) : "",
                FloorNo = row.Table.Columns.Contains("FLOOR_NO") ? Convert.ToString(row["FLOOR_NO"]) : "",
                CreatedDate = row.Table.Columns.Contains("CREATED_DATE") ? Convert.ToString(row["CREATED_DATE"]) : "",
                CreatedBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : "",
                UpdateddDate = row.Table.Columns.Contains("UPDATED_DATE") ? Convert.ToString(row["UPDATED_DATE"]) : "",
                UpdatedBy = row.Table.Columns.Contains("UPDATED_BY") ? Convert.ToString(row["UPDATED_BY"]) : "",
                SubmitYN = row.Table.Columns.Contains("SUBMIT_YN") ? Convert.ToString(row["SUBMIT_YN"]) : "",
                EmployeeName = row.Table.Columns.Contains("EMPLOYEE_NAME") ? Convert.ToString(row["EMPLOYEE_NAME"]) : "",
            };
        }
    }
}
