using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL.DBManager;
using Models.AllModel;
using Models.ApiModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class RequisitionController : Controller
    {
        private readonly IRequisitionManager _manager = new RequisitionManager();
        private readonly IReportManager _rptManager = new ReportManager();

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
        public async Task<ActionResult> Requisition()
        {
            ViewBag.dateTime = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.GetMaxRequisition = await _manager.GetMaxRequisition();
            return View();
        }
        public async Task<ActionResult> SaveAllRequisitionData(RequisitionMainModel objRequisitionMainModel)
        {
            LoadSession();
            objRequisitionMainModel.ShopId = UtilityClass.ShopId;
            objRequisitionMainModel.CreatedBy = _employeeId;
            
            var data = await _manager.SaveAllRequisitionData(objRequisitionMainModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("Requisition")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        [UserRoleFilter]
        public async Task<ActionResult> WarehouseProductSearch()
        {
            return View();
        }

        public async Task<ActionResult> GetDcProductByStyleName(string styleName)
        {
            var data = await _manager.GetDcProductByStyleName(styleName);
           
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #region Report

        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;

        public async Task<ActionResult> ShowReport(string requisitionNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/ProductRequisitionDc.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallRequisitionItem(requisitionNo));

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer = true;

            _objExportFormatType = ExportFormatType.PortableDocFormat;

            Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
            byte[] byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

            Response.ContentType = "application/pdf";

            string pFileDownloadName = "Product Requisition.pdf";

            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();
            _objReportDocument.Close();
            _objReportDocument.Dispose();

            return File(oStream, Response.ContentType, pFileDownloadName);
        }
        #endregion
    }
}