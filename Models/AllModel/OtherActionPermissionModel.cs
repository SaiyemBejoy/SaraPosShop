using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class OtherActionPermissionModel
    {
        public int AutoId { get; set; }

        public string RoleName { get; set; }
        
        public string CreateBy { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string ActiveYN { get; set; }

        public static OtherActionPermissionModel ConvertOtherPermissionModel(DataRow row)
        {
            return new OtherActionPermissionModel
            {
                AutoId = row.Table.Columns.Contains("AUTO_ID") ? Convert.ToInt32(row["AUTO_ID"]) : 0,
                RoleName = row.Table.Columns.Contains("ROLE_NAME") ? Convert.ToString(row["ROLE_NAME"]) : "",
                Controller = row.Table.Columns.Contains("CONTROLLER") ? Convert.ToString(row["CONTROLLER"]) : "",
                Action = row.Table.Columns.Contains("ACTION_NAME") ? Convert.ToString(row["ACTION_NAME"]) : "",
                ActiveYN = row.Table.Columns.Contains("ACTIVE_YN") ? Convert.ToString(row["ACTIVE_YN"]) : "",
                CreateBy = row.Table.Columns.Contains("CREATE_BY") ? Convert.ToString(row["CREATE_BY"]) : ""

            };
        }
    }
}
