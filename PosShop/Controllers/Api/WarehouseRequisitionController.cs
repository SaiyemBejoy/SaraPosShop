using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;

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

        [HttpPost]
        public async Task<IHttpActionResult> Post(RequisitionMainModel model)
        {
            var updateDate = await _manager.UpdateWarehouseRequisitionInfo(model);
            if (updateDate == "UPDATE")
                return Ok(true);

            return BadRequest();
        }
    }
}
