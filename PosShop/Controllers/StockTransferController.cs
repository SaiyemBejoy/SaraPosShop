using System;
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
using Models.ApiModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class StockTransferController : Controller
    {
        private readonly IDropdownManager _dropdownManager = new DropdownManager();
        private readonly IStockTransferManager _manager = new StockTransferManager();
        private readonly IShopRequisitionManager _shopToshopmanager = new ShopRequisitionManager();
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
        public async Task<ActionResult> StockTransfer()
        {
            ViewBag.GetMaxChallanNo = await _manager.GetMaxChallanNo();
            ViewBag.GetRequisitionNo = await _manager.GetRequisitionNo();
            var data = await _dropdownManager.GetAllShopListForTransfer(UtilityClass.ShopId);
            ViewBag.ShopList = data;
            ViewBag.dateTime = DateTime.Now.ToString("MM/dd/yyyy");
            return View();
        }

        public async Task<ActionResult> GetProductInfoByBarcode(string barcode)
        {
            var dataInfo = await _manager.GetProductInfoByBarcode(barcode);
            if (dataInfo == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(dataInfo, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveTransferData(StockTransferModel objStockTransferModel)
        {
            LoadSession();
            objStockTransferModel.TransferFromShopId = UtilityClass.ShopId;
            objStockTransferModel.TransferedBy = _employeeId;
            var data = await _manager.SaveTransferData(objStockTransferModel);

            if (!string.IsNullOrWhiteSpace(data))
            {
                var returnData = new
                {
                    m = data,
                    isRedirect = true,
                    redirectUrl = Url.Action("StockTransfer")
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var returnData = new
                {
                    m = "",
                    isRedirect = false,
                    redirectUrl = Url.Action("StockTransfer")
                };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
           
        }

        public async Task<ActionResult> SaveDamageTransferData(DamageTransferMain objDamageTransferMainModel)
        {
            LoadSession();
            objDamageTransferMainModel.TransferShopIdfrom = UtilityClass.ShopId;
            objDamageTransferMainModel.TransferedBy = _employeeId;
            var data = await _manager.SaveDamageTransferData(objDamageTransferMainModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("Index","DamageProduct")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> StockTransferList()
        {
            return View();
        }
        public async Task<ActionResult> GetAllTransferChallanForDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.GetAllTransferChallanForDataTable(model);
            List<StockTransferModel> data = (List<StockTransferModel>)model.ListOfData;

            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewAllTransferInfoByStoreReceiveChallanNo(string transferId)
        {
            var data = await _manager.ViewAllTransferInfoByStoreReceiveChallanNo(transferId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllShopToShopRequDetailsForDeliveryByChallanNo(string shopToShopRequasitionChallanNo)
        {
            var data = await _shopToshopmanager.GetAllFromRequisitionData(UtilityClass.ShopId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #region Report
        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;

        public async Task<ActionResult> ShowReport(string transferChallanNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/Transfer/StockTransferDetails.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallShopTransferItem(transferChallanNo));

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

        public async Task<ActionResult> ShowReportGovtFormate(string transferChallanNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/Transfer/GovtFormate/StockTransferDetailsGovtFormate.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallShopTransferItemForGvtFormate(transferChallanNo));

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

        #endregion
    }
}