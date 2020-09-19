using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace PosShop.Utility
{
    public class UtilityClass
    {
        //public static string EmployeeId;
        //public static string EmployeeName;
        //public static string Role;
        public static string WareHouseId;
        public static string WareHouseName;
        public static string ShopId;
        public static string ShopName;
        

        //public static void Authentication()
        //{
        //    if (Session["authentication"] is AuthModel auth)
        //    {
        //        _strEmployeeId = auth.EmployeeId;
        //        _strWareHouseId = auth.WareHouseId;
        //        _strShopId = auth.ShopId;
        //    }
        //    else
        //    {
        //        Response.Headers.Clear();
        //        string url = Url.Action("Index", "Auth");
        //        if (url != null) Response.Redirect(url);
        //    }
        //}

        //For dropdown list
        public static SelectList GetSelectListByDataTable(DataTable objDataTable, string pValueField, string pTextField)
        {
            List<SelectListItem> objSelectListItems = new List<SelectListItem>
            {
                new SelectListItem() {Value = "", Text = "--Select Item--"},
            };


            objSelectListItems.AddRange(from DataRow dataRow in objDataTable.Rows
                select new SelectListItem()
                {
                    Value = dataRow[pValueField].ToString(),
                    Text = dataRow[pTextField].ToString()
                });

            return new SelectList(objSelectListItems, "Value", "Text");
        }

    }
}