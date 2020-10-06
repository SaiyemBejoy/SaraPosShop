using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PosShop.Controllers
{
    public class OtherActionPermissionController : Controller
    {
        private readonly IUserPermissionManager _manager = new UserPermissionManager();
        private readonly IDropdownManager _dropdownManager;
        private readonly IAuthManager _authManager;

        public OtherActionPermissionController()
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


        // GET: OtherActionPermission
        public async Task<ActionResult> Index()
        {
            var objOtherPermissionListData = await _manager.GetOtherPermissionList();
            ViewBag.OtherPermissionList = objOtherPermissionListData;

            var userRoleList = await _dropdownManager.GetAllUserRoleList();
            ViewBag.UserRoleList = userRoleList;

            return View();
        }

        public async Task<ActionResult> SaveOtherPermissionData(OtherActionPermissionModel objOtherPermissionModel)
        {
            LoadSession();
            objOtherPermissionModel.CreateBy = _employeeId;
            var data = await _manager.SaveOtherPermissionData(objOtherPermissionModel);
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