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
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class DamageProductController : Controller
    {
        private readonly IDropdownManager _dropdownManager;
        private readonly IPointOfSaleManager _manager;
        private readonly IDamageProductManager _damageProductManager;
        private readonly IStockTransferManager _TransManager = new StockTransferManager();
        private readonly IReportManager _rptManager = new ReportManager();


        public DamageProductController()
        {
            _manager = new PointOfSaleManager();
            _damageProductManager = new DamageProductManager();
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
        public async Task<ActionResult> Index()
        {
            var objDamageMainModel = await _damageProductManager.ViewAllData();
            ViewBag.DamageMainList = objDamageMainModel;
            return View();
        }
        [UserRoleFilter]
        public async Task<ActionResult> DamageProduct()
        {
            ViewBag.dateTime = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.GetMaxDamageChallanNo = await _damageProductManager.GetMaxDamageChallanNo();
            return View();
        }
        public async Task<ActionResult> GetAllDamageProductInfoByBarcode(string barcode)
        {
            var item = await _manager.GetAllInfoByBarcode(barcode);
            if (item == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> SaveAllDamageData(DamageMainModel objDamageMainModel)
        {
            LoadSession();
            objDamageMainModel.ShopId = UtilityClass.ShopId;
            objDamageMainModel.CreatedBy = _employeeId;
            var item = await _damageProductManager.SaveAllDamageData(objDamageMainModel);
            var returnData = new
            {
                m = item,
                isRedirect = true,
                redirectUrl = Url.Action("Index")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllDamageItemByChallanNo(string challanNo)
        {
            var item = await _damageProductManager.GetAllDamageItemByChallanNo(challanNo);
           
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllDamageInfoForTransfer(string challanNo)
        {
            var item = await _damageProductManager.GetAllDamageInfoForTransfer(challanNo);
            var maxChallan = await _TransManager.GetMaxChallanNo();
            var returnData = new
            {
                DmgData = item,
                MaxData = maxChallan,
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }


        #region Report
        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;

        public async Task<ActionResult> ShowReport(string challanNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/Transfer/DamageTransferDetails.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallDamageProductForRpt(challanNo));

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

            string pFileDownloadName = "Damage Product Report.pdf";

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