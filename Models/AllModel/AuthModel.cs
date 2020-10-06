using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Models.ApiModel;

namespace Models.AllModel
{
    public class AuthModel
    {
        public int EmployeeDisId { get; set; }
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Counter Name")]
        public string Counter { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "New Password")]
        //public string NewPassword { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //[Display(Name = "Re-type password")]
        //[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        //public string RetypePassword { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string ContactNo { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Role")]
        public string EmployeeRole { get; set; }
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        public string ActiveYn { get; set; }
        public bool Message { get; set; }
        public string UpdateBy { get; set; }
        public string WareHouseId { get; set; }
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public string VoidPermision { get; set; }
        public string ReturnPermision { get; set; }

        public IEnumerable<DeliveredProduct> DeliveredProductModels { get; set; }
        

        public static AuthModel ConvertAuthModel(DataRow row)
        {
            return new AuthModel
            {
                EmployeeDisId = row.Table.Columns.Contains("EMPLOYEE_DIS_ID") ? Convert.ToInt32(row["EMPLOYEE_DIS_ID"]) : 0,
                ActiveYn = row.Table.Columns.Contains("ACTIVE_YN") ? Convert.ToString(row["ACTIVE_YN"]) : "",
                EmployeeId = row.Table.Columns.Contains("EMPLOYEE_ID") ? Convert.ToString(row["EMPLOYEE_ID"]) : "",
                EmployeeName = row.Table.Columns.Contains("EMPLOYEE_NAME") ? Convert.ToString(row["EMPLOYEE_NAME"]) : "",
                EmployeeRole = row.Table.Columns.Contains("EMPLOYEE_ROLE") ? Convert.ToString(row["EMPLOYEE_ROLE"]) : "",
                Designation = row.Table.Columns.Contains("DESIGNATION") ? Convert.ToString(row["DESIGNATION"]) : "",
                //EmployeeImage = row.Table.Columns.Contains("SHOP_NAME") ? Convert.ToString(row["SHOP_NAME"]) : "",
                //EmployeeArea = row.Table.Columns.Contains("SHOP_NAME") ? Convert.ToString(row["SHOP_NAME"]) : "",
                Password = row.Table.Columns.Contains("EMPLOYEE_PASSWORD") ? Convert.ToString(row["EMPLOYEE_PASSWORD"]) : "",
                //NewPassword = row.Table.Columns.Contains("ADDRESS") ? Convert.ToString(row["ADDRESS"]) : "",
                //RetypePassword = row.Table.Columns.Contains("EMAIL") ? Convert.ToString(row["EMAIL"]) : "",
                Email = row.Table.Columns.Contains("EMAIL") ? Convert.ToString(row["EMAIL"]) : "",
                ContactNo = row.Table.Columns.Contains("CONTACT_NO") ? Convert.ToString(row["CONTACT_NO"]) : "",
                //Message = row.Table.Columns.Contains("POSTAL_CODE") ? Convert.ToBoolean(row["POSTAL_CODE"]) : false,
                //UpdateBy = row.Table.Columns.Contains("VAT_NO") ? Convert.ToString(row["VAT_NO"]) : "",
                WareHouseId = "1",
                ShopId = row.Table.Columns.Contains("SHOP_ID") ? Convert.ToString(row["SHOP_ID"]) : "",
                ShopName = row.Table.Columns.Contains("SHOP_NAME") ? Convert.ToString(row["SHOP_NAME"]) : "",
                VoidPermision = row.Table.Columns.Contains("VOID_PERMISION") ? Convert.ToString(row["VOID_PERMISION"]) : "",
                ReturnPermision = row.Table.Columns.Contains("RETURN_PERMISION") ? Convert.ToString(row["RETURN_PERMISION"]) : ""
            };
        }
    }
}
