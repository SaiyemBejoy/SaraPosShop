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
    public class SalesManWiseSaleForDCRptController : ApiController
    {
        private readonly IDataExchangeManager _manager;

        public SalesManWiseSaleForDCRptController()
        {
            this._manager = new DataExchangeManager();
        }

        public async Task<IHttpActionResult> Get(string fromDate,string toDate)
        {
            var getData = await _manager.GetAllSalesManWiseSaleDataForDc(fromDate, toDate);

            if (getData == null)
            {
                return NotFound();
            }

            return Ok(getData);
        }
    }
}
