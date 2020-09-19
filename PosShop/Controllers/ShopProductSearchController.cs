using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class ShopProductSearchController : Controller
    {
        private readonly IDropdownManager _dropdownManager = new DropdownManager();
        private readonly IShopRequisitionManager _manager = new ShopRequisitionManager();

        #region "Common"
        private string _employeeId;
        private string _employeeName;
        private string _role;

        public void LoadSession()
        {
            var auth = (AuthModel)Session["authentication"];

            if (auth != null)
            {
                _employeeId = auth.EmployeeId;
                _employeeName = auth.EmployeeName;
                _role = auth.EmployeeRole;
            }
            else
            {
                string url = Url.Action("Index", "Auth");
                if (url != null) Response.Redirect(url);
            }
        }

        #endregion

        // GET: ShopProductSearch
        [UserRoleFilter]
        public async Task<ActionResult> ShopProductSearch()
        {
            var data = await _dropdownManager.GetAllShopListForShopRequisition(UtilityClass.ShopId);
            ViewBag.ShopList = data;
           
            return View();
        }
    }
}