using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using Models.ApiModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class PrivilegeCardController : Controller
    {
        private readonly IDataExchangeManager _manager = new DataExchangeManager();
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
        // GET: PrivilegeCard
        public async Task<ActionResult> PrivilegeCard()
        {  
            return View();
        }
        public async Task<ActionResult> PrivilegeCardList()
        {
            var data = await _manager.ViewAllPrivilegeCustomerData();
            ViewBag.PrivilegeCustomer = data.ToList();
            return View();
        }

        public async Task<ActionResult> PrivilegeCardListAjax()
        {
            var data = await _manager.ViewAllPrivilegeCustomerData();
            var result = data.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> SavePrivilegeCardData(PrivilegeCustomerModel objPrivilegeCustomerModel)
        {
            LoadSession();
            objPrivilegeCustomerModel.ShopId = UtilityClass.ShopId;
            objPrivilegeCustomerModel.WareHouseId = UtilityClass.WareHouseId;
            objPrivilegeCustomerModel.UpdateBy = _employeeId;
            var data = await _manager.SavePrivilegeCardDataInWarehouse(objPrivilegeCustomerModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("PrivilegeCard")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}