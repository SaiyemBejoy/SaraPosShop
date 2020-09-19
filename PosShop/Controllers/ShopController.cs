using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using Models.ApiModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopManager _manager = new ShopManager();
        private readonly IDropdownManager _dropdownManager = new DropdownManager();

        public async Task<ActionResult> Shop()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"] as string;
            }

            var data = await _dropdownManager.GetAllShopList();

            var model = await _manager.GetShopInfo();

            ViewBag.ShopList = data;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(ShopModel objShopModel)
        {
            if (Request.Url != null) objShopModel.ShopUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Api/";

            var data = await _manager.SaveShopInfo(objShopModel);
            TempData["message"] = data;
            return RedirectToAction("Index", "Home");
        }
    }
}