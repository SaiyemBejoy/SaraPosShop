using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AllModel
{
    public class SubMenuPermisionModel
    {
        public string MainMenuId { get; set; }
        public string SubMenuId { get; set; }
        public string UserRole { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}
