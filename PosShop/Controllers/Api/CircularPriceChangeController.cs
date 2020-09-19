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
    public class CircularPriceChangeController : ApiController
    {
        private readonly IPriceChangeAndPromotionManager _manager;

        public CircularPriceChangeController()
        {
            this._manager = new PriceChangeAndPromotionManager();
        }

        [HttpPost]
        public async Task<IHttpActionResult> ChangeAllPriceByBarcode(CircularPriceChangeMain objCircularPriceChangeMain)
        {

            if (objCircularPriceChangeMain.CircularPriceChangeSubList != null)
            {
                var returnMessage = await _manager.ChangeAllPriceByBarcode(objCircularPriceChangeMain);

                if (returnMessage == "SUCCESS")
                {
                    return Ok(returnMessage);
                }
                else if (returnMessage == "NOTFOUND")
                {
                    return NotFound();
                }
            }

            return BadRequest();
        }


    }
}
