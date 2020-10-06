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
    public class MenuSetupController : Controller
    {
        private readonly IUserPermissionManager _manager = new UserPermissionManager();
        private readonly IDropdownManager _dropdownManager;
        private readonly IAuthManager _authManager;

        public MenuSetupController()
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

        // GET: MenuSetup
        [UserRoleFilter]
        public async Task<ActionResult> Index(int? menuId)
        {
            var objMenuListData = await _manager.GetMainMenuList();
            ViewBag.MenuList = objMenuListData;

            ViewBag.MaxMenuOrder = await _manager.GetMaxOrderNumberForMenu();

            MenuMainModel model = new MenuMainModel();

            if (menuId != null && menuId != 0)
            {
                model = await _manager.GetAMainMenu(menuId);
            }

            return View(model);
        }

        public async Task<ActionResult> SaveMenuData(MenuMainModel objMenuMainModel)
        {
            LoadSession();
            objMenuMainModel.UpdateBy = _employeeId;
            var data = await _manager.SaveMenuData(objMenuMainModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("Index")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAMenu(int menuId)
        {
          LoadSession();
           var  model = await _manager.GetAMainMenu(menuId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}