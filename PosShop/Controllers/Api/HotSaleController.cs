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
    public class HotSaleController : ApiController
    {
        private readonly IWareHouseDashboardManager _manager;

        public HotSaleController()
        {
            this._manager = new WareHouseDashboardManager();
        }
        public async Task<IHttpActionResult> Get()
        {
            var getData = await _manager.GetAllInfoForHotSale();

            if (getData == null)
            {
                return NotFound();
            }

            return Ok(getData);
        }
    }
}

