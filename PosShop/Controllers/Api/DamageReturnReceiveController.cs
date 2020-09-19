using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.IManager;
using BLL.Manager;

namespace PosShop.Controllers.Api
{
    public class DamageReturnReceiveController : ApiController
    {
        private readonly IStockTransferManager _manager;

        public DamageReturnReceiveController()
        {
            this._manager = new StockTransferManager();
        }

        public async Task<IHttpActionResult> Get(string damageChallanNo)
        {
            var getData = await _manager.UpdateDmgStockTransferModel(damageChallanNo);

            if (getData == null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
