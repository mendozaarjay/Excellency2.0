using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class UserTypeController : Controller
    {
        private IUserType _Services;

        public UserTypeController(IUserType user)
        {
            _Services = user;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var result = _Services.UserTypes()
                .Select(a => new UserTypeItem
                {
                    Id = a.Id,
                    Description = a.Description,
                }).ToList();
            var model = new UserTypeIndexViewModel
            {
                UserTypes = result,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(UserTypeIndexViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var item = new UserType();
                item.Id = model.Item.Id;
                item.Description = model.Item.Description;
                _Services.Save(item, userId);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            _Services.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}
