﻿using System;
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
    public class ShopController : ApiController
    {
       
        private readonly IShopManager _manager;

        public ShopController()
        {
            this._manager = new ShopManager();
        }
        public async Task<IHttpActionResult> Get()
        {
            var getData = await _manager.GetShopInfo();

            if (getData == null)
            {
                return NotFound();
            }

            return Ok(getData);
        }
    }
}
