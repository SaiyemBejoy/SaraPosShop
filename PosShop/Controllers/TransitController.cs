﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Models.AllModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class TransitController : Controller
    {
        // GET: Transit
        private readonly IDropdownManager _dropdownManager = new DropdownManager();
        private readonly IStockTransferManager _manager = new StockTransferManager();
        private readonly ITransitProductManager _transitManager = new TransitProductManager();
        private readonly IReportManager _rptManager = new ReportManager();

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
        public async Task<ActionResult> TransitProduct()
        {
            TransitProductModel model = new TransitProductModel();
            ViewBag.MarketPlaceNameList = await _dropdownManager.GetAllMarketPlaceList();
            return View(model);
        }
        [UserRoleFilter]
        public async Task<ActionResult> TransitProductList()
        {
            var objTarnsitModel = await _transitManager.ViewAllData();
            ViewBag.TransitModelList = objTarnsitModel;
            return View();
        }
        //public async Task<ActionResult> GetAllTransitChallanForDataTable(DataTableAjaxPostModel model)
        //{
        //    model = await _manager.GetAllTransitChallanForDataTable(model);
        //    List<TransitProductModel> data = (List<TransitProductModel>)model.ListOfData;

        //    return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        //}

        public async Task<ActionResult> GetProductInfoByBarcode(string barcode)
        {
            var dataInfo = await _manager.GetProductInfoByBarcode(barcode);
            if (dataInfo == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(dataInfo, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetProductInfoByReceiveChallanNo(string receiveChallanNo)
        {
            var dataInfo = await _transitManager.GetProductInfoByReceiveChallanNo(receiveChallanNo);
            if (dataInfo == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(dataInfo, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveTransitData(TransitProductModel objTransitProductModel)
        {
            LoadSession();
            objTransitProductModel.ShopId = UtilityClass.ShopId;
            objTransitProductModel.CreateddBy = _employeeId;
            var data = await _transitManager.SaveTransitData(objTransitProductModel);
            if (!string.IsNullOrWhiteSpace(data))
            {
                var returnData = new

                {
                    m = data,
                    isRedirect = true,
                    redirectUrl = Url.Action("TransitProduct")
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var returnData = new
                {
                    m = "",
                    isRedirect = false,
                    redirectUrl = Url.Action("TransitProduct")
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }

        }

        #region "Transit Product Return"
        [UserRoleFilter]
        public async Task<ActionResult> TransitProductReturn()
        {
            TransitProductReturnModel model = new TransitProductReturnModel();
            ViewBag.MarketPlaceNameList = await _dropdownManager.GetAllMarketPlaceList();
            return View(model);
        }

        public async Task<ActionResult> SaveTransitReturnData(TransitProductReturnModel objTransitReturnProductModel)
        {
            LoadSession();
            objTransitReturnProductModel.ShopId = UtilityClass.ShopId;
            objTransitReturnProductModel.CreateddBy = _employeeId;
            var data = await _transitManager.SaveTransitReturnData(objTransitReturnProductModel);
            if (!string.IsNullOrWhiteSpace(data))
            {
                var returnData = new

                {
                    m = data,
                    isRedirect = true,
                    redirectUrl = Url.Action("TransitProductReturn")
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var returnData = new
                {
                    m = "",
                    isRedirect = false,
                    redirectUrl = Url.Action("TransitProductReturn")
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> GetProductReturnInfoByBarcode(string barcode, int marketPlaceId)
        {
            var dataInfo = await _manager.GetProductReturnInfoByBarcode(barcode, marketPlaceId);
            if (dataInfo == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(dataInfo, JsonRequestBehavior.AllowGet);
        }

        [UserRoleFilter]
        public async Task<ActionResult> TransitReturnProductList()
        {
            var objTarnsitReturnModel = await _transitManager.ViewAllReturnData();
            ViewBag.TransitReturnModelList = objTarnsitReturnModel;
            return View();
        }
        #endregion

        #region Report
        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;

        public async Task<ActionResult> ShowReport(string transitChallanNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/Transit/TransitProductDetails.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallShopTransitItem(transitChallanNo));

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

            string pFileDownloadName = "Shop Receive Report.pdf";

            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();
            _objReportDocument.Close();
            _objReportDocument.Dispose();

            return File(oStream, Response.ContentType, pFileDownloadName);
        }

        public async Task<ActionResult> ShowTransitReturnReport(string transitReturnChallanNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/Transit/TransitReturnProductDetails.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallShopTransitReturnItem(transitReturnChallanNo));

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

            string pFileDownloadName = "Transit Return Report.pdf";

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