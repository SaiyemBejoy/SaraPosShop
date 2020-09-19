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
    public class ProductSearchController : Controller
    {
        private readonly IProductSearchManager _manager;
        private readonly IDropdownManager _dropdownManager;

        public ProductSearchController()
        {
            _manager = new ProductSearchManager();
            _dropdownManager = new DropdownManager();
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
        [UserRoleFilter]
        public async Task<ActionResult> ProductSearch()
        {
            LoadSession();
            //var data = await _manager.GetTotalItemCount();
            //var data2 = await _manager.GetSaleItemCount();
            //var data3 = await _manager.GetReceiveItemCount();
            //ViewBag.TotalItemCount = data;
            //ViewBag.TotalSaleItemCount = data2;
            //ViewBag.TotalReceiveItemCount = data3;
            var allStyle = await _dropdownManager.GetAllStyleName();
            ViewBag.StyleNameList = allStyle;
            return View();
        }
        public async Task<ActionResult> GetAllItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.GetAllItemInfoForDataTable(model);
            List<ProductSearchModel> data = (List<ProductSearchModel>)model.ListOfData;

            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllInfoByProductId(int productId)
        {
            LoadSession();
            if (_employeeId != null)
            {
                var data = await _manager.GetAllProductInfoByProductId(productId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllCalculateSummaryInfo()
        {
            LoadSession();
            var data = await _manager.GetAllCalculateSummaryInfo();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllInfoBySearchTextBoxValue(string searchValue)
        {
            LoadSession();
            if (_employeeId != null)
            {
                var data = await _manager.GetAllInfoBySearchTextBoxValue(searchValue);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

    }
}