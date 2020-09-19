using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using Models.ApiModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShopManager _manager = new ShopManager();
        private readonly IDashboardManager _dashboardManager = new DashboardManager();
        private readonly IDataExchangeManager _dataExchangeManager = new DataExchangeManager();
        private readonly IAuthManager _authManager = new AuthManager();

        public async Task<ActionResult> Index()
        {
            var employee = (AuthModel) Session["authentication"];
            if (UtilityClass.ShopId == null)
            {
                var model = await _manager.GetShopInfo();

                if (model == null)
                {
                    return RedirectToAction("Shop", "Shop");
                }

                LoadShopData(model);
            }

            if (employee == null )
            {
                return RedirectToAction("Index", "Auth");
            }
            var giftVoucherDelivery = await _dataExchangeManager.GetAllGiftVoucherDeliveryAndSave();
            var discountPromotion = await _dashboardManager.GetAllPromotionDateFromWarehouse(UtilityClass.ShopId);
            if (discountPromotion != null && discountPromotion.Any())
            {
                string shopId = UtilityClass.ShopId;
                var savePromotion = await _dataExchangeManager.SavePromotionData(discountPromotion.ToList(), shopId);
            }
            var modelData = await _dashboardManager.GetAllDashboardInfo();

            var lowStock = await _dashboardManager.GetAllLowStockInfo();
            ViewBag.LowStock = lowStock;
            var hotSale = await _dashboardManager.GetAllHotSaleInfo();
            ViewBag.HotSale = hotSale;

            return View(modelData);

        }

        private static void LoadShopData(ShopModel model)
        {
            UtilityClass.ShopId = model.ShopId.ToString();
            UtilityClass.ShopName = model.ShopName;
            UtilityClass.WareHouseId = model.WareHouseId.ToString();
            UtilityClass.WareHouseName = model.WareHouseName;
        }

        public ActionResult LeftMenu()
        {
            MenuMainModelList menuMainModels = new MenuMainModelList();
            var employee = (AuthModel)Session["authentication"];
            menuMainModels.MenuMainModelsList = _authManager.GetMainMenuList(employee.EmployeeRole).ToList();
            return PartialView("Admin/_LeftMenuPartial", menuMainModels);
        }
        public async Task<ActionResult> GetAllOtherCompanyOffer()
        {
            var data = await _authManager.GetAllOtherCompanyOffer();
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        //public ActionResult LeftMenu()
        //{
        //    return PartialView("Admin/_LeftMenuPartial");
        //}
        public ActionResult TopMenu()
        {
            if (Session["authentication"] is AuthModel employee)
            {
                employee.DeliveredProductModels =  _authManager.GetDeliveryProductChallanNo();
                return PartialView("Admin/_TopMenuPartial", employee);
            }
            return null;
           
        }

        public ActionResult TestScreen()
        {
            return View();
        }
    }
}