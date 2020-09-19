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
    public class SaleCustomerInfoController : ApiController
    {
        private readonly IPointOfSaleManager _manager;
        private readonly IDataExchangeManager _dataExchangemanager;

        public SaleCustomerInfoController()
        {
            this._manager = new PointOfSaleManager();
            this._dataExchangemanager = new DataExchangeManager();
        }
        public async Task<IHttpActionResult> Get()
        {
            var getData = await _manager.GetAllSaleCustomerInfo();

            if (getData == null)
            {
                return NotFound();
            }

            return Ok(getData);
        }
        [HttpPost]
        public async Task<IHttpActionResult> UpdateCustomerSale(CustomerSaleModel model)
        {
            if (model.CustomerId > 0)
            {
                var returnMessage = await _dataExchangemanager.UpdateCustomerSale(model);
                return Ok(returnMessage);
            }
            return BadRequest();
        }
    }
}
