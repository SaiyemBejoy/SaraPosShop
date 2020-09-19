using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.IManager;
using BLL.Manager;
using Models.ApiModel;

namespace PosShop.Controllers.Api
{
    public class ShopToShopEXProductController : ApiController
    {
        private readonly IShopReceiveManager _manager;

        public ShopToShopEXProductController()
        {
            this._manager = new ShopReceiveManager();
        }
        public async Task<IHttpActionResult> Get()
        {
            var getData = await _manager.GetAllShopToShopExData();

            if (getData == null)
            {
                return NotFound();
            }

            return Ok(getData);
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateExChangeStoreReceiveChallan(ShopToShopExchangeMain objShopToShopExchangeMain)
        {
            if (objShopToShopExchangeMain.StoreReceiveChallanNo != null)
            {
                var returnMessage = await _manager.UpdateExChangeStoreReceiveChallan(objShopToShopExchangeMain);

                if (returnMessage == "SUCCESS")
                {
                    return Ok(returnMessage);
                }

            }

            return BadRequest();
        }

    }
}
