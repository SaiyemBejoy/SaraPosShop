using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Models.AllModel.Report;
using PosShop.Reports.Sale;

namespace PosShop.Controllers.Report
{
    public class AllReportController : Controller
    {
        private readonly IDropdownManager _dropdownManager;
        private readonly IReportManager _reportManager;

        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;


        public AllReportController()
        {
            _dropdownManager = new DropdownManager();
            _reportManager = new ReportManager();

        }

        public FileStreamResult ShowReport(string pReportType, string pFileDownloadName)
        {

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer = true;



            if (pReportType == "PDF")
            {
                _objExportFormatType = ExportFormatType.PortableDocFormat;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "application/pdf";

                pFileDownloadName += ".pdf";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "Excel")
            {
                _objExportFormatType = ExportFormatType.Excel;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "application/vnd.ms-excel";

                pFileDownloadName += ".xls";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "CSV")
            {
                _objExportFormatType = ExportFormatType.CharacterSeparatedValues;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "text/csv";

                pFileDownloadName += ".csv";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "TXT")
            {
                _objExportFormatType = ExportFormatType.RichText;

                Stream oStream = _objReportDocument.ExportToStream(_objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "text/plain";

                pFileDownloadName += ".txt";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                _objReportDocument.Close();
                _objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }

            return null;
        }

        #region Sale Report
        public async Task<ActionResult> SaleReport()
        {
            ViewBag.dateTime = DateTime.Now.ToString("dd/MM/yy");
            var data = await _dropdownManager.GetAllSubCategoryName();
            var data2 = await _dropdownManager.GetAllCategoryName();
            ViewBag.SubCategoryList = data;
            ViewBag.CategoryList = data2;
            return View();
        }
        public async Task<ActionResult> GetAllSubCategoryName(string categoryName)
        {
            var data = await _dropdownManager.GetAllSubCategoryName(categoryName);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaleReport(SaleReportModel objSaleReportModel)
        {

            objSaleReportModel.ReportType = "PDF";
            if (objSaleReportModel.RadioFor == "S")
            {
               await GenerateSaleSummary(objSaleReportModel);
            }
            else if (objSaleReportModel.RadioFor == "D")
            {
                await GenerateSaleDetails(objSaleReportModel);
            }
            else if (objSaleReportModel.RadioFor == "C")
            {
                await GenerateSaleSummaryByCategoryAndSubCtgy(objSaleReportModel);
            }
            else if (objSaleReportModel.RadioFor == "CS")
            {
               await GenerateCashierSaleSummary(objSaleReportModel);
            }
            else if (objSaleReportModel.RadioFor == "CSD")
            {
               await GenerateCashierSaleDetails(objSaleReportModel);
            }
            else if (objSaleReportModel.RadioFor == "TWSS")
            {
                await GenerateTimeWiseSaleDetails(objSaleReportModel);
            }
            else if (objSaleReportModel.RadioFor == "VIH")
            {
                await GenerateVoidInvoiceHistory(objSaleReportModel);
            }

            return RedirectToAction("SaleReport");
        }
        private async Task<int> GenerateSaleSummary(SaleReportModel objSaleReportModel)
        {

            await SaleSummary(objSaleReportModel);
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/SaleSummary.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.SaleSummary(objSaleReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSaleReportModel.ReportType, "Sale Summary Report");
            return 0;
        }
        private async Task<int> GenerateTimeWiseSaleDetails(SaleReportModel objSaleReportModel)
        {
            objSaleReportModel.FromDate = String.Format("{0:dd/MM/yyyy}", objSaleReportModel.FromDateAndTime);
            objSaleReportModel.ToDate = String.Format("{0:dd/MM/yyyy}", objSaleReportModel.ToDateAndTime);
            await SaleSummary(objSaleReportModel);

            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/SaleSummary.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.TimeSaleSummary(objSaleReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSaleReportModel.ReportType, "Sale Summary Report");
            return 0;
        }

        private async Task<int> GenerateCashierSaleSummary(SaleReportModel objSaleReportModel)
        {
            await SaleSummary(objSaleReportModel);
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/CashierSaleSummary.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet =await _reportManager.CashierSaleSummary(objSaleReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSaleReportModel.ReportType, "Cashier Sale Summary Report");
            return 0;
        }


        private async Task<int> GenerateCashierSaleDetails(SaleReportModel objSaleReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/CashierSaleDetails.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet =await _reportManager.GenerateCashierSaleDetails(objSaleReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSaleReportModel.ReportType, "Cashier Sale Details Report");
            return 0;
        }

        private async Task<int> GenerateSaleSummaryByCategoryAndSubCtgy(SaleReportModel objSaleReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/SaleSummaryByCtgySubCtgy.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.GenerateSaleSummaryByCategoryAndSubCtgy(objSaleReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSaleReportModel.ReportType, "Category SubCategory Wise Sale Summary ");
            return 0;
        }

        private async Task<int> GenerateSaleDetails(SaleReportModel objSaleReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/SaleDetails.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.SaleDetails(objSaleReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSaleReportModel.ReportType, "Sale Summary Report");
            return 0;
        }

        public async Task<string> SaleSummary(SaleReportModel objSaleReportModel)
        {
            var msg = await _reportManager.SaleSummarySave(objSaleReportModel);
            return "o";
        }

        private async Task<int> GenerateVoidInvoiceHistory(SaleReportModel objSaleReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/VoidInvoiceHistory.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.GenerateVoidInvoiceHistory(objSaleReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSaleReportModel.ReportType, "Void Invoice History Report");
            return 0;
        }

        #endregion

        #region StockDetails
        public async Task<ActionResult> StockDetails()
        {
            var data = await _dropdownManager.GetAllSubCategoryName();
            var data2 = await _dropdownManager.GetAllCategoryName();
            var productStyle = await _dropdownManager.GetAllStyleName();
            ViewBag.SubCategoryList = data;
            ViewBag.CategoryList = data2;
            ViewBag.ProductStyleList = productStyle;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> StockDetails(StockReportModel objStockReportModel)
        {
            objStockReportModel.ReportType = "PDF";
            if(objStockReportModel.RadioFor == "CS")
            {
                await GenerateCurrentStock(objStockReportModel);
            }
            else if (objStockReportModel.RadioFor == "SWS")
            {
                await GenerateStyleWiseCurrentStock(objStockReportModel);
            }
            else if (objStockReportModel.RadioFor == "LS")
            {
                await GenerateLowStockDetails(objStockReportModel);
            }
            else if (objStockReportModel.RadioFor == "SSS")
            {
                await GenerateStyleWiseStockSummary(objStockReportModel);
            }
            else if (objStockReportModel.RadioFor == "SWH")
            {
                await GenerateStyleWiseProductHistory(objStockReportModel);
            }
            return RedirectToAction("StockDetails");
        }
        private async Task<int> GenerateCurrentStock(StockReportModel objStockReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Stock/StockDetails.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.CurrentStockDetails(objStockReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objStockReportModel.ReportType, "Current Stock Report");
            return 0;
        }
        private async Task<int> GenerateStyleWiseCurrentStock(StockReportModel objStockReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Stock/StockDetails.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.StyleWiseCurrentStockDetails(objStockReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objStockReportModel.ReportType, "Style Wise Current Stock");
            return 0;
        }

        private async Task<int> GenerateLowStockDetails(StockReportModel objStockReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Stock/StockDetails.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.LowStockDetails(objStockReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objStockReportModel.ReportType, "Current Low Stock Details");
            return 0;
        }

        private async Task<int> GenerateStyleWiseStockSummary(StockReportModel objStockReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Stock/StyleWiseStockSummary.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.StyleWiseStockSummary(objStockReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objStockReportModel.ReportType, "Style Wise Stock Summary");
            return 0;
        }

        private async Task<int> GenerateStyleWiseProductHistory(StockReportModel objStockReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Stock/StyleWiseHistory.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.StyleWiseProductHistory(objStockReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objStockReportModel.ReportType, "Style Wise History");
            return 0;
        }

        #endregion

        #region SalesManWise Sale Report

        public async Task<ActionResult> SalesManWiseSale()
        {
            var data = await _dropdownManager.GetAllEmployeeInfo();
            ViewBag.EmployeeList = data;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SalesManWiseSale(SalesManReportModel objSalesManReportModel)
        {

            objSalesManReportModel.ReportType = "PDF";
            if (objSalesManReportModel.RadioFor == "S")
            {
               await GenerateSalesManSaleSummary(objSalesManReportModel);
            }

            return RedirectToAction("SalesManWiseSale");
        }

        private async Task<int> GenerateSalesManSaleSummary(SalesManReportModel objSalesManReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/Sale/SalesManWiseSaleReport.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.SalesManSaleSummary(objSalesManReportModel);

            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objSalesManReportModel.ReportType, "Sales Man Report");
            return 0;
        }
        #endregion

        #region "Transit Report"
        public async Task<ActionResult> TransitReport()
        {
            var data = await _dropdownManager.GetAllMarketPlaceList();
            ViewBag.MarketPlaceList = data;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TransitReport(TransitReportModel objTransitReportModel)
        {

            objTransitReportModel.ReportType = "PDF";
            if (objTransitReportModel.RadioFor == "TPS")
            {
                await GenerateTransitStock(objTransitReportModel);
            }

            return RedirectToAction("TransitReport");
        }

        private async Task<int> GenerateTransitStock(TransitReportModel objTransitReportModel)
        {
            
            string strPath = Path.Combine(Server.MapPath("~/Reports/Transit/TransitStock.rpt"));
            _objReportDocument.Load(strPath);
            DataSet objDataSet = await _reportManager.TransitStock(objTransitReportModel);
            _objReportDocument.Load(strPath);
            _objReportDocument.SetDataSource(objDataSet);
            _objReportDocument.SetDatabaseLogon("POSSHOP", "POSSHOP");

            ShowReport(objTransitReportModel.ReportType, "Transit Stock Report");
            return 0;
        }
        #endregion

    }
}