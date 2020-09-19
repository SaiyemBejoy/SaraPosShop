using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using DAL.IRepository;
using DAL.Repository;
using Models.AllModel;

namespace PosShop.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestManager _manager;

        public TestController()
        {
            this._manager = new TestManager();
        }

        public async Task<ActionResult> ViewAllData()
        {
            var test = await _manager.ViewAllData();
            return View(test);
        }

        public async Task<ActionResult> Details(int id)
        {
            var test = await _manager.ViewSingleDataById(id);
            return View(test);
        }

        public async Task<ActionResult> Details(string name)
        {
            var test = await _manager.ViewSingleDataByName(name);
            return View(test);
        }

        public async Task<ActionResult> ViewMaxId()
        {
            var test = await _manager.ViewMaxId();
            ViewBag.MaxId = test;
            ViewBag.IsExist = test;
            return View();
        }

        public async Task<ActionResult> ViewIsExist(int id)
        {
            var test = await _manager.ViewIsExist(id);
            ViewBag.MaxId = test;
            ViewBag.IsExist = test;
            return View("ViewMaxId");
        }

        public ActionResult SaveOrUpdate()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<ActionResult> SaveOrUpdate(TestModel objModel)
        {
            var test = await _manager.SaveOrUpdate(objModel);
            return View("Create");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var test = await _manager.Delete(id);
            return View();
        }
    }
}