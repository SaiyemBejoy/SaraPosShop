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
    public class StockReturnReceiveController : ApiController
    {
        private readonly IStockTransferManager _manager;

        public StockReturnReceiveController()
        {
            this._manager = new StockTransferManager();
        }

        public async Task<IHttpActionResult> Get(string challanNo)
        {
            var getData = await _manager.UpdateStockTransferModel(challanNo);

            if (getData == null)
            {
               return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateStockTransferTable(TransferReturnProduct objTransferReturnProduct)
        {
           
            if (objTransferReturnProduct.StockTranferChallanNo != null)
            {
                var returnMessage = await _manager.UpdateStockTransferTable(objTransferReturnProduct);

                if (returnMessage == "SUCCESS")
                {
                    return Ok(returnMessage);
                }

            }

            return BadRequest();
        }


    }
}
