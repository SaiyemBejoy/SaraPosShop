using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using PosShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PosShop.Controllers
{
    public class SubMenuSetupController : Controller
    {
        private readonly IUserPermissionManager _manager = new UserPermissionManager();
        private readonly IDropdownManager _dropdownManager;
        private readonly IAuthManager _authManager;

        public SubMenuSetupController()
        {
            _dropdownManager = new DropdownManager();
            _authManager = new AuthManager();
        }
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

        // GET: SubMenuSetup
        [UserRoleFilter]
        public async Task<ActionResult> Index()
        {
            var objSubMenuListData = await _manager.GetSubMenuList();
            ViewBag.SubMenuList = objSubMenuListData;

            var menuList = await _dropdownManager.GetAllMenuList();
            ViewBag.MenuList = menuList;

            return View();
        }

        public async Task<ActionResult> GetMenuIdFromView(int menuId)
        {
            var data = await _manager.GetMaxOrderNumberForSubMenu(menuId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetASubMenu(int subMenuId)
        {
            LoadSession();
            var model = await _manager.GetASubMenu(subMenuId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveSubMenuData(MenuSubModel objMenuSubModel)
        {
            LoadSession();
            objMenuSubModel.UpdateBy = _employeeId;
            var data = await _manager.SaveSubMenuData(objMenuSubModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("Index")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}