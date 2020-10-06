using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class UserMenuManageController : Controller
    {
        private readonly IDropdownManager _dropdownManager;
        private readonly IAuthManager _authManager;
        private readonly IUserPermissionManager _userPermissionManager;

        public UserMenuManageController()
        {
            _dropdownManager = new DropdownManager();
            _authManager = new AuthManager();
            _userPermissionManager = new UserPermissionManager();
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
        // GET: UserMenuManage
        [UserRoleFilter]
        public async Task<ActionResult> Index()
        {
            LoadSession();
            var menuList = await _dropdownManager.GetAllMenuList();
            //var menuListHaveSubMenu = await _dropdownManager.GetAllMenuListHaveSubMenu();
            var userRoleList = await _dropdownManager.GetAllUserRoleList();
            var employee = await _dropdownManager.GetAllEmployeeInfo();
            ViewBag.MenuList = menuList;
            ViewBag.UserRoleList = userRoleList;
            ViewBag.EmployeeList = employee;
            //ViewBag.MenuListHaveSubMenu = menuListHaveSubMenu;
            return View();
        }

        public ActionResult ControllerList()
        {
            //string[] separator = {"Controller"};
            //.Split(separator,StringSplitOptions.RemoveEmptyEntries)[0]
            var controllers = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t)).Select(t => t.Name).ToList();
            return Json(controllers,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetActionName(string controllerName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var controllerActionList = asm.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            var result = controllerActionList.FindAll(x => x.Controller == controllerName);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<string> ActionNames(string controllerName)
        {
            var types =
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                where typeof(Controller).IsAssignableFrom(t) &&
                      string.Equals(controllerName + "Controller", t.Name, StringComparison.OrdinalIgnoreCase)
                select t;

            var controllerType = types.FirstOrDefault();

            if (controllerType == null)
            {
                return Enumerable.Empty<string>().ToList();
            }
            var actionList = new ReflectedControllerDescriptor(controllerType).GetCanonicalActions().Select(x => x.ActionName).ToList();
            return actionList;

        }

        public async Task<ActionResult> GetSubMenuListByMenuId(string menuId)
        {
            var data = await _dropdownManager.GetAllSubMenuList(menuId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveAllData(List<MainMenuPermisionModel> obMainMenuPermisionModels,List<SubMenuPermisionModel> obSubMenuPermisionModels)
        {
            LoadSession();
            var result = await _authManager.SaveMenuPermisionData(obMainMenuPermisionModels, obSubMenuPermisionModels);
            var returnData = new
            {
                m = result,
                isRedirect = true,
                redirectUrl = Url.Action("Index")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        #region Controller And Action And ReturnType List

        //Assembly asm = Assembly.GetExecutingAssembly();
        //var controlleractionlist = asm.GetTypes()
        //    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
        //    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
        //    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
        //    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
        //    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();



        /////Another Way
        //string s = “”;

        //var controllers = Assembly.GetExecutingAssembly().GetExportedTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t)).Select(t => t);

        //    foreach (Type controller in controllers)
        //{

        //    var actions = controller.GetMethods().Where(t => t.Name != “Dispose” && !t.IsSpecialName && t.DeclaringType.IsSubclassOf(typeof(ControllerBase)) && t.IsPublic && !t.IsStatic).ToList();

        //    foreach (var action in actions)
        //    {
        //        var myAttributes = action.GetCustomAttributes(false);
        //        for (int j = 0; j<myAttributes.Length; j++)
        //            s += string.Format(“ActionName: { 0}, Attribute: {1}<br>”, action.Name, myAttributes[j]);

        //    }
        //}
        /// End
        #endregion

        #region UserMenuPermision

        public async Task<ActionResult> GetSubMenuListForTableByMenuId(string menuId)
        {
            var data = await _userPermissionManager.GetSubMenuListByMenu(menuId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModels)
        {
            LoadSession();
            objMainMenuPermisionModels.UpdatedBy = _employeeId;
            var result = await _userPermissionManager.SaveAllMenuPermisionData(objMainMenuPermisionModels);
            var returnData = new
            {
                m = result,
                isRedirect = true,
                redirectUrl = Url.Action("Index")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DeleteAllMenuPermisionData(MainMenuPermisionModel objMainMenuPermisionModels)
        {
            LoadSession();
            objMainMenuPermisionModels.UpdatedBy = _employeeId;
            var result = await _userPermissionManager.DeleteAllMenuPermisionData(objMainMenuPermisionModels);
            var returnData = new
            {
                m = result,
                isRedirect = true,
                redirectUrl = Url.Action("Index")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> VoidAndReturnPermisionData(UserMenuManageModel objUserMenuManageModel)
        {
            LoadSession();
            var result = await _userPermissionManager.VoidAndReturnPermisionData(objUserMenuManageModel);
            var returnData = new
            {
                m = result,
                isRedirect = true,
                redirectUrl = Url.Action("Index")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}