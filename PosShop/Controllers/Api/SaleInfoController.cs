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
    public class SaleInfoController : ApiController
    {

        private readonly IPointOfSaleManager _manager;

        public SaleInfoController()
        {
            this._manager = new PointOfSaleManager();
        }
        public async Task<IHttpActionResult> Get()
        {
            var getData = await _manager.GetAllSaleInfo();

            if (getData == null)
            {
                return NotFound();
            }

            return Ok(getData);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(SaleInfoWarehouseUpdate model)
        {
            var updateDate = await _manager.UpdateSaleInfo(model);

            if (updateDate)
                return Ok(true);

            return BadRequest();
        }
    }
}
