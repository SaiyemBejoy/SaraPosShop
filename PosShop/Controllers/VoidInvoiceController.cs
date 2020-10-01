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
    public class VoidInvoiceController : Controller
    {
        private readonly IVoidInvoiceManager _manager = new VoidInvoiceManager();

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

        public ActionResult VoidInvoiceList()
        {
            return View();
        }

        public async Task<ActionResult> GetAllVoidInvoiceForDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.GetAllVoidInvoiceDataTable(model);
            List<VoidInvoiceModel> data = (List<VoidInvoiceModel>)model.ListOfData;

            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewAllVoidInvoiceItemBySaleInfoId(string saleInfoId)
        {
            var data = await _manager.ViewAllVoidInvoiceItemBySaleInfoId(saleInfoId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}