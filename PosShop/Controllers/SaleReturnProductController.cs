using BLL.IManager;
using BLL.Manager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Models.AllModel;
using PosShop.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PosShop.Controllers
{
    public class SaleReturnProductController : Controller
    {
        private readonly IInvoiceSearchManager _manager;
        private readonly IReportManager _rptManager;

        public SaleReturnProductController()
        {
            _manager = new InvoiceSearchManager();
            _rptManager = new ReportManager();
        }
        // GET: SaleReturnProduct
        [UserRoleFilter]
        public ActionResult SaleReturnProduct()
        {
            return View();
        }
        public async Task<ActionResult> GetAllExchangeItemInfoForDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.GetAllExchangeItemInfoForDataTable(model);
            List<HistoryModel> data = (List<HistoryModel>)model.ListOfData;

            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        #region Report
        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;

        public async Task<ActionResult> ShowReport(string newInvoiceNUmber)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/ProductExchangeDetails.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallExchangeProductDetails(newInvoiceNUmber));

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

            string pFileDownloadName = "Exchange Report.pdf";

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