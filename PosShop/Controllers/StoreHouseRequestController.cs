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
    public class StoreHouseRequestController : Controller
    {
        private readonly IStoreHouseRequestManager _manager = new StoreHouseRequestManager();
        private readonly IDropdownManager _dropdownManager = new DropdownManager();

        #region "Common"

        private string _employeeId;
        private string _employeeName;
        private string _role;

        public void LoadSession()
        {
            var auth = (AuthModel) Session["authentication"];

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
        [UserRoleFilter]
        // GET: StoreHouseRequest
        public ActionResult StoreHouseRequest()
        {
            return View();
        }

        public async Task<ActionResult> SaveAllData(StoreHouseRequestMain objStoreHouseRequestMain)
        {
            LoadSession();
            objStoreHouseRequestMain.ShopId = UtilityClass.ShopId;
            objStoreHouseRequestMain.CreatedBy = _employeeId;
            var data = await _manager.SaveAllData(objStoreHouseRequestMain);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("StoreHouseRequest")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        [UserRoleFilter]
        public async Task<ActionResult> StoreHouseRequestList()
        {
            var data = await _manager.StoreHouseRequestList();
            ViewBag.StoreHouseRequestList = data;
            return View();
        }

        public async Task<ActionResult> SubmitData(StoreHouseRequestMain objStoreHouseRequestMain)
        {
            LoadSession();
            objStoreHouseRequestMain.UpdatedBy = _employeeId;
            var data = await _manager.SubmitData(objStoreHouseRequestMain);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("StoreHouseRequestList")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        [UserRoleFilter]
        public async Task<ActionResult> StoreHouseSave()
        {
            LoadSession();
            StoreHouseModel model = new StoreHouseModel();
            var data = await _dropdownManager.GetAllStyleName();
            ViewBag.StyleNameList = data;
            return View(model);
        }

        public async Task<ActionResult> GetAllInfoByProductId(int productId)
        {
            var data = await _manager.GetAllProductInfoByProductId(productId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllInfoByBarcode(string barcode)
        {
            var data = await _manager.GetAllInfoByBarcode(barcode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SubmitCheck(string requisitionNo)
        {
            var data = await _manager.GetAllInfoByRequisitionNo(requisitionNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> SaveAllStoreHouseData(StoreHouseModel objStoreHouseModel)
        {
            LoadSession();
            objStoreHouseModel.CreatedBy = _employeeId;
            var data = await _manager.SaveAllStoreHouseData(objStoreHouseModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("StoreHouseSave")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        [UserRoleFilter]
        public async Task<ActionResult> StoreHouseProductSearch()
        {
            return View();
        }
        public async Task<ActionResult> GetAllLineNoForDropdown()
        {
            var data = await _dropdownManager.GetAllLineNo();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllRopeNoForDropdown(string lineNo)
        {
            var data = await _dropdownManager.GetAllRopeNo(lineNo);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> StoreHouseProductSearchDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.StoreHouseProductSearch(model);
            List<StoreHouseProductSearchModel> data = (List<StoreHouseProductSearchModel>)model.ListOfData;
            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> RemoveItem(string productBarcode, int storehouseId)
        {
            string message = await _manager.RemoveItem(productBarcode, storehouseId);
            TempData["message"] = message;

            return RedirectToAction("StoreHouseProductSearch");
        }
    }
}