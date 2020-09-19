using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using PosShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PosShop.Controllers
{
    public class GiftVoucherController : Controller
    {
        private readonly IDataExchangeManager _manager = new DataExchangeManager();

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

        // GET: GiftVoucher
        [UserRoleFilter]
        public async Task<ActionResult> GiftVoucherDeposit()
        {
            var objGiftVoucherData = await _manager.ViewAllDataGiftvoucherActive();
            ViewBag.GiftVoucherDataList = objGiftVoucherData;
            return View();
        }
        public async Task<ActionResult> GiftVoucherInfoByCode( string giftVoucherCode)
        {
            var data = await _manager.giftVoucherInfoByCode(giftVoucherCode);
            if (data == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SaveDepositData(GiftVoucherModel objGiftVoucherModel)
        {
            LoadSession();
            objGiftVoucherModel.DepositShopId = UtilityClass.ShopId;
            objGiftVoucherModel.UpdateBy = _employeeId;
            var data = await _manager.SaveGiftVoucherDepositData(objGiftVoucherModel);
            var returnData = new
            {
                m = data,
                isRedirect = true,
                redirectUrl = Url.Action("GiftVoucherDeposit")
            };
            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}