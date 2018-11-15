using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Excellency.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartment _Department;

        public DepartmentController(IDepartment department)
        {
            _Department = department;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _Department.Departments().Select
                (a => new DepartmentViewModel
                {
                    Id = a.Id,
                    Description = a.Description
                }).ToList();
            var model = new DepartmentIndexViewModel{
                Departments = result
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(DepartmentIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Id = model.Id,
                    Description = model.Description,
                };
                if (model.Id.ToString().Equals("0"))
                {
                    department.CreatedBy = UserId;
                    department.CreationDate = DateTime.Now;
                    _Department.Add(department);
                }
                else
                {
                    department.ModifiedBy = UserId;
                    department.ModifiedDate = DateTime.Now;
                    _Department.Update(department);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index",model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _Department.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}
