using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class DamageMainModel
    {

        public int DamageId { get; set; }
        public string DamageChallanNo { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ShopId { get; set; }
        //For Display Data
        public string ShopName { get; set; }
        public string EmployeeName { get; set; }
        //End

        public IEnumerable<DamageMainItemModel> DamageMainItemList { get; set; }

        public static DamageMainModel ConvertDamageMainModel(DataRow row)
        {
            return new DamageMainModel
            {
                DamageId = row.Table.Columns.Contains("DAMAGE_ID") ? Convert.ToInt32(row["DAMAGE_ID"]) : 0,
                DamageChallanNo = row.Table.Columns.Contains("DAMAGE_CHALLAN_NO") ? Convert.ToString(row["DAMAGE_CHALLAN_NO"]) : "",
                CreatedDate = row.Table.Columns.Contains("CREATED_DATE") ? Convert.ToString(row["CREATED_DATE"]) : "",
                CreatedBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : "",
                //This is for data display purpose
                EmployeeName = row.Table.Columns.Contains("EMPLOYEE_NAME") ? Convert.ToString(row["EMPLOYEE_NAME"]) : "",
                ShopName = row.Table.Columns.Contains("SHOP_NAME") ? Convert.ToString(row["SHOP_NAME"]) : ""
                //End
            };
        }
    }
}
