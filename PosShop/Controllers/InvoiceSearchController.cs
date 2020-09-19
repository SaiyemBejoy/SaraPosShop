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
    public class InvoiceSearchController : Controller
    {
       private readonly IInvoiceSearchManager _manager;
      
        public InvoiceSearchController()
        {
            _manager = new InvoiceSearchManager();
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
        public  ActionResult InvoiceSearch()
        {
            return View();
        }
        public async Task<ActionResult> GetAllSaleInfoForDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.GetAllSaleInfoForDataTable(model);
            List<SaleInfoModel> data = (List<SaleInfoModel>)model.ListOfData;

            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ProcessAllDataForDataTable()
        {
            var data = await _manager.SaveAllSaleInfoForDataTable();
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("InvoiceSearch")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}