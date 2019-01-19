using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.ViewModels;
using Excellency.Secured;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Excellency.Controllers
{
    public class AccountController : Controller
    {
        private IAccount _UserAccount;

        public AccountController(IAccount account)
        {
            _UserAccount = account;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {

            var result = _UserAccount.Accounts().Select
                (a => new AccountListingViewModel
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.LastName + " " + a.MiddleName,
                    Branch = a.Branch.Description,
                    Company = a.Company.Description,
                    Department = a.Department.Description,
                    Position = a.Position.Description
                }).ToList();
            var model = new AccountIndexViewModel();
            model.Accounts = result;
            return View(model);
        }

        #region User Registration
        [SessionAuthorized]
        public IActionResult Register()
        {
            var model = new AccountRegisterViewModel
            {
                Companies = this.Companies(),
                Branches = this.Branches(),
                Departments = this.Departments(),
                Positions = this.Positions(),
                Categories = this.Categories(),
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountRegisterViewModel account)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var UserAccount = new Account
                {
                    Id = account.Id,
                    EmployeeNo = account.EmployeeNo,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    MiddleName = account.MiddleName,
                    Username = account.Username,
                    Mobile = account.Mobile,
                    Password = Security.Encrypt(account.Password),
                    Email = account.Email,
                    Company = _UserAccount.GetCompanyById(int.Parse(account.Company.ToString())),
                    Branch = _UserAccount.GetBranchById(int.Parse(account.Branch.ToString())),
                    Position = _UserAccount.GetPositionById(int.Parse(account.Position.ToString())),
                    Department = _UserAccount.GetDepartmentById(int.Parse(account.Department.ToString())),
                    Category = _UserAccount.GetCategoryById(int.Parse(account.Category))
                };
                _UserAccount.Save(UserAccount,UserId);
                return RedirectToAction("Index");
            }
            else
            {
                account.Companies = this.Companies();
                account.Branches = this.Branches();
                account.Positions = this.Positions();
                account.Departments = this.Departments();
                account.Categories = this.Categories();
                return View(account);
            }
        }
        public IEnumerable<SelectListItem> Companies()
        {
            var result = _UserAccount.Companies().
                Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                ).ToList();

            return result;
        }
        public IEnumerable<SelectListItem> Branches()
        {
            var result = _UserAccount.Branches().
                Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                ).ToList();

            return result;
        }
        public IEnumerable<SelectListItem> Departments()
        {
            var result = _UserAccount.Departments().
                Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                ).ToList();

            return result;
        }
        public IEnumerable<SelectListItem> Positions()
        {
            var result = _UserAccount.Positions().
                Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                ).ToList();

            return result;
        } 
        public IEnumerable<SelectListItem> Categories()
        {
            var items = _UserAccount.Categories()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description,
                }).ToList();
            return items;
        }
        #endregion
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

                if (_UserAccount.IsAccountLocked(account))
                {
                    ViewBag.Message = "Your account is locked.";
                    return View();
                }
                if (_UserAccount.IsLoginExpired(account))
                {
                    ViewBag.Message = "Your account is expired.";
                    return View();
                }
                if (_UserAccount.IsAvailableToLogin(account))
                {
                    HttpContext.Session.SetString("UserId", _UserAccount.GetUserId(account));
                    var _id = HttpContext.Session.GetString("UserId");
                    var name = _UserAccount.GetAccountById(int.Parse(_id)).FirstName;
                    HttpContext.Session.SetString("CurrentUser",name);
                    return RedirectToAction("Index", "Home");
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