using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Models.AllModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class ProductReceiveController : Controller
    {
        private readonly IDropdownManager _dropdownManager = new DropdownManager();
        private readonly IShopReceiveManager _manager = new ShopReceiveManager();
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
        public async Task<ActionResult> ProductReceive()
        {
            ViewBag.StoreDeliveryNumber = await _dropdownManager.GetAllStoreDeliveryNumber();
            ViewBag.dateTime = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }
        public async Task<ActionResult> ViewAllDataByStoreReceiveChallanNo(string storeReceiveChallanNo)
        {
            var data = await _manager.ViewAllDataByStoreReceiveChallanNo(storeReceiveChallanNo);
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllReturnChallanNumFromWarehouse()
        {
            var data = await _manager.GetAllReturnChallanNumFromWarehouse(UtilityClass.ShopId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Save(StoreReceiveModel objStoreReceiveModel)
        {
            LoadSession();
            objStoreReceiveModel.ShopId = UtilityClass.ShopId;
            objStoreReceiveModel.WareHouseId = UtilityClass.WareHouseId;
            objStoreReceiveModel.ReceivedBy = _employeeId;

            var data = await _manager.Save(objStoreReceiveModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("ProductReceive")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductReceiveList()
        {
            return View();
        }
        public async Task<ActionResult> GetAllReceiveChallanForDataTable(DataTableAjaxPostModel model)
        {
            model = await _manager.GetAllReceiveChallanForDataTable(model);
            List<StoreReceiveModel> data = (List<StoreReceiveModel>)model.ListOfData;

            return Json(new { model.draw, model.recordsTotal, model.recordsFiltered, data }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ViewAllReceiveInfoByStoreReceiveChallanNo(string storeReceiveId)
        {
            var data = await _manager.ViewAllReceiveItemByStoreReceiveChallanNo(storeReceiveId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region Report
        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;
       
        public async Task<ActionResult> ShowReport(string storeReceiveChallanNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/Receive/StoreReceiveProductDetails.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallShopReceiveItem(storeReceiveChallanNo));

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