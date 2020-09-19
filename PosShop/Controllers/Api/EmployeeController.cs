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
using Models.ApiModel;

namespace PosShop.Controllers.Api
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeManager _manager;

        public EmployeeController()
        {
            this._manager = new EmployeeManager();
        }

        [HttpPost]
        public async Task<IHttpActionResult> UpdateEmployeeDistribution(AuthModel objAuthModel)
        {

            if (objAuthModel.EmployeeId != null)
            {
                var returnMessage = await _manager.UpdateEmployee(objAuthModel);

                if (returnMessage == "SUCCESS")
                {
                    return Ok(returnMessage);
                }

            }

            return BadRequest();
        }
    }
}
