using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class MenuMainModel
    {
        public int MenuMainId { get; set; }

        public string MenuMainName { get; set; }

        public string MenuUrl { get; set; }

        public string MenuIcon { get; set; }

        public string UpdateBy { get; set; }

        public int MenuOrder { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public IEnumerable<MenuSubModel> MenuSubModels { get; set; }

        public static MenuMainModel ConvertMenuMainModel(DataRow row)
        {
            return new MenuMainModel
            {
                MenuMainId = row.Table.Columns.Contains("MENU_ID") ? Convert.ToInt32(row["MENU_ID"]) : 0,
                MenuMainName = row.Table.Columns.Contains("MENU_NAME") ? Convert.ToString(row["MENU_NAME"]) : "",
                Controller = row.Table.Columns.Contains("CONTROLLER") ? Convert.ToString(row["CONTROLLER"]) : "",
                Action = row.Table.Columns.Contains("ACTION") ? Convert.ToString(row["ACTION"]) : "",
                MenuUrl = row.Table.Columns.Contains("MENU_URL") ? Convert.ToString(row["MENU_URL"]) : "",
                MenuIcon = row.Table.Columns.Contains("MENU_ICON") ? Convert.ToString(row["MENU_ICON"]) : "",
                UpdateBy = row.Table.Columns.Contains("CREATED_BY") ? Convert.ToString(row["CREATED_BY"]) : "",
                MenuOrder = row.Table.Columns.Contains("MENU_ORDER") ? Convert.ToInt32(row["MENU_ORDER"]) : 0,

            };
        }

        public static MenuMainModel ConvertMenuOrder(DataRow row)
        {
            return new MenuMainModel
            {
                MenuOrder = row.Table.Columns.Contains("MENU_ORDER") ? Convert.ToInt32(row["MENU_ORDER"]) : 0

            };
        }

    }

    public class MenuMainModelList
    {
        public IEnumerable<MenuMainModel> MenuMainModelsList { get; set; }
    }

}
