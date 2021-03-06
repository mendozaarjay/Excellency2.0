﻿using System;
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
        private IUserAccount _Services;

        public AccountController(IUserAccount account)
        {
            _Services = account;
        }
        [SessionAuthorized]
        public IActionResult Index(int? page)
        {
            int currentpage;
            if (page == null)
                currentpage = 1;
            else
                currentpage = (int)page;

            var maxcount = currentpage < 5 ? 5 : currentpage + 2;
            var mincount = currentpage < 5 ? 1 : currentpage - 2;

            var maxpage = (_Services.Accounts().Count() / 10) + 1;

            maxcount = currentpage <= maxpage ? maxcount : maxpage;
            var result = _Services.AccountsPage(currentpage);
            var model = new AccountIndexViewModel();
            model.Accounts = result;
            ViewBag.MaxCount = maxcount;
            ViewBag.MinCount = mincount;
            ViewBag.CurrentPage = currentpage;
            ViewBag.MaxPage = maxpage;
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var item = _Services.GetAccountById(id);
            var useraccess = _Services.UserAccessTypePerEmployee(id)
                .Select(a => new UserAccessTypeItemViewModel
                {
                    RecordId = a.Id,
                    Id = a.UserType.Id,
                    Description = a.UserType.Description,
                }).ToList();
            var result = _Services.AvailableUserTypesPerEmployee(id);
            List<SelectListItem> types = new List<SelectListItem>();
            foreach(var i in result)
            {
                var x = new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Description,
                };
                types.Add(x);
            }
            var model = new AccountRegisterViewModel
            {
                Id = item.Id,
                FirstName = item.FirstName,
                MiddleName = item.MiddleName,
                LastName = item.LastName,
                EmployeeNo = item.EmployeeNo,
                Company = item.Company.Id.ToString(),
                Branch = item.Branch.Id.ToString(),
                Department = item.Department.Id.ToString(),
                Position = item.Position.Id.ToString(),
                Password = Security.Decrypt(item.Password),
                ConfirmPassword = Security.Decrypt(item.Password),
                Category = item.Category.Id.ToString(),
                Companies = this.Companies(),
                Branches = this.Branches(),
                Departments = this.Departments(),
                Positions = this.Positions(),
                Username = item.Username,
                Categories = this.Categories(),
                UserAccessTypes = useraccess,
                UserTypes = types
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AccountRegisterViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var UserAccount = new Account
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName == null ? "" : model.MiddleName,
                    Username = model.Username,
                    Mobile = model.Mobile,
                    Password = Security.Encrypt(model.Password),
                    Email = model.Email,
                    Company = _Services.GetCompanyById(int.Parse(model.Company.ToString())),
                    Branch = _Services.GetBranchById(int.Parse(model.Branch.ToString())),
                    Position = _Services.GetPositionById(int.Parse(model.Position.ToString())),
                    Department = _Services.GetDepartmentById(int.Parse(model.Department.ToString())),
                    Category = _Services.GetCategoryById(int.Parse(model.Category))
                };
                _Services.Save(UserAccount, model.Types, UserId);
                return RedirectToAction("Index");
            }
            else
            {
                var item = _Services.GetAccountById(model.Id);
                var useraccess = _Services.UserAccessTypePerEmployee(model.Id)
                    .Select(a => new UserAccessTypeItemViewModel
                    {
                        RecordId = a.Id,
                        Id = a.UserType.Id,
                        Description = a.UserType.Description,
                    }).ToList();
                var result = _Services.AvailableUserTypesPerEmployee(model.Id);
                List<SelectListItem> types = new List<SelectListItem>();
                foreach (var i in result)
                {
                    var x = new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Description,
                    };
                    types.Add(x);
                }
                model.Companies = this.Companies();
                model.Branches = this.Branches();
                model.Positions = this.Positions();
                model.Departments = this.Departments();
                model.Categories = this.Categories();
                model.UserTypes = types;
                model.UserAccessTypes = useraccess;
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _Services.RemoveById(id);
            return RedirectToAction("Index");   
        }
        public IActionResult Search(string keyword)
        {
            var items = _Services.SearchAccount(keyword);
            return Json(new { result = items });
        }

        #region User Registration
        [SessionAuthorized]
        public IActionResult Register()
        {

            var model = new AccountRegisterViewModel
            {
                EmployeeNo = _Services.NextEmployeeNo(),
                Companies = this.Companies(),
                Branches = this.Branches(),
                Departments = this.Departments(),
                Positions = this.Positions(),
                Categories = this.Categories(),
                UserTypes = this.UserTypes(),
            };

            return View(model);
        }
        private IEnumerable<SelectListItem> UserTypes()
        {
            var items = _Services.UserTypes()
                .Select(a => new SelectListItem
                {
                    Text = a.Description,
                    Value = a.Id.ToString()
                }).ToList();
            return items;
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
                    MiddleName = account.MiddleName == null ? "" : account.MiddleName,
                    Username = account.Username,
                    Mobile = account.Mobile,
                    Password = Security.Encrypt(account.Password),
                    Email = account.Email,
                    Company = _Services.GetCompanyById(int.Parse(account.Company.ToString())),
                    Branch = _Services.GetBranchById(int.Parse(account.Branch.ToString())),
                    Position = _Services.GetPositionById(int.Parse(account.Position.ToString())),
                    Department = _Services.GetDepartmentById(int.Parse(account.Department.ToString())),
                    Category = _Services.GetCategoryById(int.Parse(account.Category))
                };
                _Services.Save(UserAccount,account.Types,UserId);
                return RedirectToAction("Index");
            }
            else
            {
                account.Companies = this.Companies();
                account.Branches = this.Branches();
                account.Positions = this.Positions();
                account.Departments = this.Departments();
                account.Categories = this.Categories();
                account.UserTypes = this.UserTypes();
                return View(account);
            }
        }
        public IActionResult GetBranches(int id)
        {
            var items = _Services.Branches()
                .Where(a => a.Company.Id == id)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Description }).ToList();
            return Json(new { result = items });
        }
        public IEnumerable<SelectListItem> Companies()
        {
            var result = _Services.Companies().
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
            var result = _Services.Branches().
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
            var result = _Services.Departments().
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
            var result = _Services.Positions().
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
            var items = _Services.Categories()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description,
                }).ToList();
            return items;
        }
        #endregion
        [HttpPost]
        public IActionResult RemoveAccess(int recordid,int employeeid)
        {
            _Services.RemoveAccessById(recordid);
            return RedirectToAction("Edit", new { id = employeeid });
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