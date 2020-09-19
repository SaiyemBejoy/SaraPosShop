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
    public class WarehouseRequisitionController : ApiController
    {
        private readonly IRequisitionManager _manager;

        public WarehouseRequisitionController()
        {
            this._manager = new RequisitionManager();
        }
        public async Task<IHttpActionResult> Get()
        {
            var getData = await _manager.GetAllWarehouseRequisitionData();

            if (getData == null)
            {
                return NotFound();
            }

            return Ok(getData);
        }
    }
}
