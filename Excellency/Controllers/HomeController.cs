using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Excellency.Models;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Excellency.Interfaces;
using Excellency.Secured;
using Newtonsoft.Json;

namespace Excellency.Controllers
{
    public class HomeController : Controller
    {
        private IHome _Home;
        JsonSerializerSettings JsonSerializer = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        public HomeController(IHome home)
        {
            _Home = home;
        }
        [SessionAuthorized]
        public IActionResult Index(UserAccountViewModel model)
        {

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult _SideBar()
        {
            var user = HttpContext.Session.GetString("UserId");
            var name = _Home.GetAccountById(int.Parse(user)).FirstName;
            var model = new CurrentUserViewModel
            {
                Name = name,
                UserAccess = _Home.UserAccess(int.Parse(user)),
            };
            return PartialView("_SideBar",model);
        }
        public IActionResult _Header()
        {
            var user = HttpContext.Session.GetString("UserId");
            var name = _Home.GetUserNameById(int.Parse(user));
            var model = new CurrentUserViewModel
            {
                Name = name,
                UserAccess = _Home.UserAccess(int.Parse(user)),
            };
            return PartialView("_Header", model);
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = new Account
                {
                    Username = model.UserName,
                    Password = Security.Encrypt(model.Password)
                };

                if (_Home.IsAccountLocked(account))
                {
                    ViewBag.Message = "Your account is locked.";
                    return View();
                }
                if (_Home.IsLoginExpired(account))
                {
                    ViewBag.Message = "Your account is expired.";
                    return View();
                }
                if (_Home.IsAvailableToLogin(account))
                {
                    HttpContext.Session.SetString("UserId", _Home.GetUserId(account));
                    var _id = HttpContext.Session.GetString("UserId");
                    var name = _Home.GetAccountById(int.Parse(_id)).FirstName;
                    HttpContext.Session.SetString("CurrentUser", name);
                    return View("Index");
                }
                else
                {
                    ViewBag.Message = "Username and password did not match.";
                    return View();
                }
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserId", string.Empty);
            HttpContext.Session.SetString("CurrentUser", string.Empty);

            return RedirectToAction("Login");
        }
    }
}
