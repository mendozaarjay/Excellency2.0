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
using Excellency.Dashboard;

namespace Excellency.Controllers
{
    public class HomeController : Controller
    {
        private IHome _Services;
        JsonSerializerSettings JsonSerializer = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        public HomeController(IHome home)
        {
            _Services = home;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var userid = int.Parse(HttpContext.Session.GetString("UserId"));

            var model = new UserAccountViewModel();
            model.DashboardAccess = _Services.DashboardAccessPerUser(userid);
            model.UserCount = _Services.UserCount();
            model.RaterCount = _Services.RaterCount();
            model.ApproverCount = _Services.ApproverCount();
            model.EmployeeCount = _Services.EmployeeCount();
            model.AccountPeriod = _Services.AccountPeriod();
            var recentaccounts = _Services.MostRecentAccounts()
                .Select(a => new RecentAccount
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.LastName,
                    CreationDate = a.CreationDate,
                }).ToList();
            model.RecentAccounts = recentaccounts;
            var recentemp = _Services.MostRecentEmployees()
                .Select(a => new RecentEmployee
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.LastName,
                    CreationDate = a.CreationDate,
                }).ToList();
            model.RecentEmployees = recentemp;

            //rater
            model.AssignedRateeCount = _Services.AssignedRateeCount(userid);
            model.EvaluatedRateeCount = _Services.EvaluatedRateeCount(userid);
            model.ApprovedRatingCount = _Services.ApprovedRatingCount(userid);
            model.PendingRatingCount = _Services.PendingRatingCount(userid);
            model.Employees = _Services.EmployeesPerRater(userid);

            //Approver
            model.AssignedPerApprover = _Services.AssignedPerApprover(userid);
            model.ApprovedEvaluation = _Services.ApprovedEvaluation(userid);
            model.PendingEvaluation = _Services.PendingForApproval(userid);
            model.UserPerApprovers = _Services.ApproverAssignedUser(userid);

            var behavioralheaderitems = _Services.GetBehavioralEvaluations(userid);
            var kraheaderitems = _Services.GetKRAEvaluations(userid);

            List<EvaluationLineItem> bitems = new List<EvaluationLineItem>();
            List<EvaluationLineItem> kitems = new List<EvaluationLineItem>();

            foreach (var item in behavioralheaderitems)
            {
                var x = _Services.GetBehavioralLineItems(item.Id);

                foreach (var aa in x)
                {
                    bitems.Add(aa);
                }
            }

            foreach (var item in kraheaderitems)
            {
                var x = _Services.GetKRAEvaluationLineItems(item.Id);
                foreach (var aa in x)
                {
                    kitems.Add(aa);
                }
            }

            model.BehavioralHeaderItems = behavioralheaderitems;
            model.KRAHeaderItems = kraheaderitems;

            model.BehavioralLineItems = bitems;
            model.KRALineItems = kitems;

            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            model.IsAdmin = _Services._IsAdmin(id);
            model.IsRater = _Services._IsRater(id);
            model.IsApprover = _Services._IsApprover(id);
            model.IsEmployee = _Services._IsEmployee(id);
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
            var name = _Services.GetAccountById(int.Parse(user)).FirstName;
            var model = new CurrentUserViewModel
            {
                Name = name,
                UserAccess = _Services.UserAccess(int.Parse(user)),
            };
            var id = int.Parse(HttpContext.Session.GetString("UserId"));
            model.IsAdmin = _Services._IsAdmin(id);
            model.IsRater = _Services._IsRater(id);
            model.IsApprover = _Services._IsApprover(id);
            model.IsEmployee = _Services._IsEmployee(id);
            return PartialView("_SideBar", model);
        }
        public IActionResult _Header()
        {
            var user = HttpContext.Session.GetString("UserId");
            var name = _Services.GetUserNameById(int.Parse(user));
            var model = new CurrentUserViewModel
            {
                Name = name,
                UserAccess = _Services.UserAccess(int.Parse(user)),
                Notifications = _Services.Notifications(int.Parse(user)),
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

                if (_Services.IsAccountLocked(account))
                {
                    ViewBag.Message = "Your account is locked.";
                    return View();
                }
                if (_Services.IsLoginExpired(account))
                {
                    ViewBag.Message = "Your account is expired.";
                    return View();
                }
                if (_Services.IsAvailableToLogin(account))
                {
                    HttpContext.Session.SetString("UserId", _Services.GetUserId(account));
                    var _id = HttpContext.Session.GetString("UserId");
                    var name = _Services.GetAccountById(int.Parse(_id)).FirstName;
                    HttpContext.Session.SetString("CurrentUser", name);
                    return RedirectToAction("Index");
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
        [SessionAuthorized]
        public IActionResult EmployeeDashboard(int id)
        {
            var total = _Services.EmployeeTotalScore(id);
            var name = _Services.EmployeeNameById(id);
            var bstrength = _Services.BehavioralStrength(id);
            var bweakness = _Services.BehavioralWeakness(id);
            var krastregth = _Services.KeyResultAreaStrength(id);
            var kraweakness = _Services.KeyResultAreaWeakness(id);
            var model = new EmployeeDashboardViewModel
            {
                EmployeeId = id,
                EmployeeName = name,
                TotalScore = total,
                BehavioralStrength = bstrength,
                BehavioralWeakness = bweakness,
                KeyResultStrength = krastregth,
                KeyResultWeakness = kraweakness,
            };
            return View(model);
        }
    }
}
