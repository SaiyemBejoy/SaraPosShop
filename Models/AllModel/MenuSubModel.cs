using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class MenuSubModel
    {
        public int MenuMainId { get; set; }

        public int MenuSubId { get; set; }

        public string MenuSubName { get; set; }

        public string SubMenuUrl { get; set; }

        public string SubMenuIcon { get; set; }

        public string RoleName { get; set; }

        public string UpdateBy { get; set; }

        public int SubMenuMainId { get; set; }

        public int SubMenuOrder { get; set; }


        public static MenuSubModel ConvertMenuSubModel(DataRow row)
        {
            return new MenuSubModel
            {
                MenuMainId = row.Table.Columns.Contains("MENU_MAIN_ID") ? Convert.ToInt32(row["MENU_MAIN_ID"]) : 0,
                MenuSubId = row.Table.Columns.Contains("MENU_SUB_ID") ? Convert.ToInt32(row["MENU_SUB_ID"]) : 0,
                MenuSubName = row.Table.Columns.Contains("MENU_SUB_NAME") ? Convert.ToString(row["MENU_SUB_NAME"]) : "",
                RoleName = row.Table.Columns.Contains("ROLE_NAME") ? Convert.ToString(row["ROLE_NAME"]) : "",
                SubMenuUrl = row.Table.Columns.Contains("MENU_SUB_URL") ? Convert.ToString(row["MENU_SUB_URL"]) : "",
                SubMenuIcon = row.Table.Columns.Contains("MENU_SUB_ICON") ? Convert.ToString(row["MENU_SUB_ICON"]) : "",
                UpdateBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                SubMenuOrder = row.Table.Columns.Contains("MENU_ORDER") ? Convert.ToInt32(row["MENU_ORDER"]) : 0,

            };
        }
    }

}
