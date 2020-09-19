using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.IManager;
using BLL.Manager;
using Models.AllModel;
using PosShop.Utility;

namespace PosShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthManager _manager;

        public AuthController()
        {
            _manager = new AuthManager();
        }
        public async Task<ActionResult> Index()
        {
            if (Session["authentication"] != null)
                return RedirectToAction("Index", "Home");


            var employee = await _manager.GetEmployeeFromWareHouse(UtilityClass.ShopId);

            if (employee != null)
            {
                foreach (var data in employee)
                {
                    await _manager.SaveEmployee(data);
                }
            }
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"] as string;
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AuthModel authModel)
        {
            if (Session["authentication"] != null)
                return RedirectToAction("Index", "Home");

            if (authModel.EmployeeId != null && authModel.Password != null)
            {
                
                authModel = await _manager.Login(authModel.EmployeeId, authModel.Password);
                if (authModel != null)
                {
                   
                    var history = await _manager.LoginHistory(authModel);
                    //UtilityClass.EmployeeId = authModel.EmployeeId;
                    //UtilityClass.EmployeeName = authModel.EmployeeName;
                    //UtilityClass.Role = authModel.EmployeeRole;
                    Session["authentication"] = authModel;
                    if (Session["authentication"] != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                   
                }

                TempData["message"] = "Unauthorized access denied for this system !! ";
            }
            return RedirectToAction("Index", "Auth");
        }

        public ActionResult LogOut()
        {
            //UtilityClass.EmployeeId = null;
            //UtilityClass.EmployeeName = null;
            //UtilityClass.Role = null;
            var employee = Session["authentication"] as AuthModel;
            if (employee != null)
            {
                Session.Abandon();
            }
            return RedirectToAction("Index");

            //return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel objChangePasswordModel)
        {
            var employee = Session["authentication"] as AuthModel;
            objChangePasswordModel.EmployeeId = employee.EmployeeId;
            objChangePasswordModel.ShopId = UtilityClass.ShopId;
            var changePassword = await _manager.ChangePassword(objChangePasswordModel);
            if (changePassword == "OK")
            {
                Session.Abandon();
               return RedirectToAction("Index");
            }
            return View();
        }
    }
}