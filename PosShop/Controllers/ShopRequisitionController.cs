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
    public class ShopRequisitionController : Controller
    {
        private readonly IDropdownManager _dropdownManager = new DropdownManager();
        private readonly IShopRequisitionManager _manager = new ShopRequisitionManager();
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
        // GET: ShopRequisition
        public async Task<ActionResult> ShopRequisition()
        {
            //var data = await _dropdownManager.GetAllShopListForShopRequisition(UtilityClass.ShopId);
            var data = await _dropdownManager.GetAllShopListForShopRequisition(UtilityClass.ShopId);
            var getMax = await _manager.GetMaxShopRequisitionId();
            ViewBag.ShopList = data;
            ViewBag.GetMaxrequisitionNumber = getMax;
            return View();
        }
        [UserRoleFilter]
        public async Task<ActionResult> ShopRequisitionList()
        {
            
            return View();
        }

        public async Task<ActionResult> ShopRequisitionAndItemList()
        {
            var data = await _manager.GetAllToRequisitionData(UtilityClass.ShopId);
            var data2 = await _manager.GetAllFromRequisitionData(UtilityClass.ShopId);
            var returnData = new
            {
                toReQuisition= data,
                fromRequisition = data2,
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ShopProductListByShopId(string shopId)
        {
            var data = await _manager.ShopProductListByShopId(shopId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ShopToShopReQuisitionDataSave(ShopToShopRequisitionMainModel objRequisitionMainModel)
        {
            LoadSession();
            objRequisitionMainModel.RequisitionShopIdTo = UtilityClass.ShopId;
            objRequisitionMainModel.CreatedBy = _employeeId;
            var data = await _manager.ShopToShopReQuisitionDataSave(objRequisitionMainModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("ShopRequisitionList")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveAllShopToShopDeliveryData(ShopToShopDeliveryModel objShopToShopDeliveryModel)
        {
            LoadSession();
            objShopToShopDeliveryModel.DeliveryShopIdFrom = UtilityClass.ShopId;
            objShopToShopDeliveryModel.DeliveredBy = _employeeId;
            var data = await _manager.SaveAllShopToShopDeliveryData(objShopToShopDeliveryModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("ShopRequisitionList")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllDeliveryItemForReceive(string requisitionNumber)
        {
            var data = await _manager.GetAllDeliveryItemForReceive(UtilityClass.ShopId,requisitionNumber);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> SaveAllShopToShopReceiveData(ShopToShopReceiveMainModel objShopToShopReceiveMainModel)
        {
            LoadSession();
            objShopToShopReceiveMainModel.ReceiveShopId = UtilityClass.ShopId;
            objShopToShopReceiveMainModel.ReceivedBy = _employeeId;
            var data = await _manager.SaveAllShopToShopReceiveData(objShopToShopReceiveMainModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("ShopRequisitionList")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }

        #region Report
        private readonly ReportDocument _objReportDocument = new ReportDocument();
        private ExportFormatType _objExportFormatType = ExportFormatType.NoFormat;

        public async Task<ActionResult> ShowReport(string requisitionChallanNo)
        {
            _objExportFormatType = ExportFormatType.PortableDocFormat;
            ExportOptions option = new ExportOptions();
            option.ExportFormatType = ExportFormatType.PortableDocFormat;
            string strPath = Path.Combine(Server.MapPath("~/Reports/ShopToShop/ShopToShopRequisition.rpt"));
            _objReportDocument.Load(strPath);

            DataSet objDataSet = (await _rptManager.GetallShopRequisitionItem(requisitionChallanNo));

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