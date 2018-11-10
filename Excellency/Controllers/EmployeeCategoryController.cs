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
    public class EmployeeCategoryController : Controller
    {
        private IEmployeeCategory _EmployeeCategory;

        public EmployeeCategoryController(IEmployeeCategory employeeCategory)
        {
            _EmployeeCategory = employeeCategory;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var result = _EmployeeCategory.EmployeeCategories()
                .Select(a => new EmployeeCategoryViewModel
                {
                    Id = a.Id,
                    Description = a.Description
                }).ToList();
            var model = new EmployeeCategoryIndexViewModel
            {
                EmployeeCategories = result
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(EmployeeCategoryIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new EmployeeCategory
                {
                    Id = model.Id,
                    Description = model.Description
                };
                _EmployeeCategory.Save(item,UserId);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _EmployeeCategory.RemoveCategoryPerId(id);
            return RedirectToAction("Index");
        }
    }
}
