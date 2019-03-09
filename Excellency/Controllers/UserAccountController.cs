using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class UserAccountController : Controller
    {
        private IUserAccountNew _Services;

        public UserAccountController(IUserAccountNew userAccountNew)
        {
            _Services = userAccountNew;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var model = new ProfileViewModel();
            var item = _Services.AccountInfo(UserId);
            if(item != null)
            {
                if(item.Rows.Count > 0)
                {
                    model.EmployeeNo = item.Rows[0]["EmployeeNo"].ToString();
                    model.FirstName = item.Rows[0]["Firstname"].ToString();
                    model.MiddleName = item.Rows[0]["Middlename"].ToString();
                    model.LastName = item.Rows[0]["Lastname"].ToString();
                    model.Username = item.Rows[0]["Username"].ToString();
                    model.Email = item.Rows[0]["Email"].ToString();
                    model.Mobile = item.Rows[0]["Mobile"].ToString();
                    model.Company = item.Rows[0]["Company"].ToString();
                    model.Branch = item.Rows[0]["Branch"].ToString();
                    model.Department = item.Rows[0]["Department"].ToString();
                    model.Position = item.Rows[0]["Position"].ToString();
                }
            }
            return View(model);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            if(ModelState.IsValid)
            {
                if(!_Services.IsValidPassword(userId,model.CurrentPassword))
                {
                    ViewBag.Message = "Current password did not match. Please input the correct password.";
                    return View(model);
                }
                else
                {
                    var result = _Services.ChangePassword(userId, model.ConfirmPassword);
                    return RedirectToAction("Profile");
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
