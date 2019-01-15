using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Excellency.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployee _Employee;

        public EmployeeController(IEmployee employee)
        {
            _Employee = employee;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _Employee.Employees().Select
                (
                 a => new EmployeeViewModel
                 {
                     Id = a.Id,
                     EmployeeNo = a.EmployeeNo,
                     Name = a.LastName + ", " + a.FirstName + " " + a.MiddleName,
                     Company = a.Company.Description,
                     Branch  = a.Branch.Description,
                     Department = a.Department.Description,
                     Position = a.Position.Description,
                 }
                ).ToList();
            var model = new EmployeeIndexViewModel
            {
                Employees = result
            };
            return View(model);
        }
        [SessionAuthorized]
        public IActionResult AddEmployee()
        {
            var model = new EmployeeRegisterViewModel
            {
                Companies = this.Companies(),
                Branches = this.Branches(),
                Departments = this.Departments(),
                Positions = this.Positions(),
                EmployeeCategories = this.Categories(),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(EmployeeRegisterViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new Models.Account
                {

                    Branch = _Employee.GetBranchById(int.Parse(model.Branch)),
                    Department = _Employee.GetDepartmentById(int.Parse(model.Department)),
                    Company = _Employee.GetCompanyById(int.Parse(model.Company)),
                    Position = _Employee.GetPositionById(int.Parse(model.Position)),
                    Category = _Employee.GetCategoryById(int.Parse(model.Category)),
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    CreatedBy = UserId,
                    CreationDate = DateTime.Now,
                };
                _Employee.Add(item);
               return RedirectToAction("Index");
            }
            else
            {
                model.Companies = this.Companies();
                model.Branches = this.Branches();
                model.Departments = this.Departments();
                model.Positions = this.Positions();
                return RedirectToAction("AddEmployee", model);
            }
        }
        [SessionAuthorized]
        public IActionResult Edit(int id)
        {
            var item = _Employee.GetEmployeeById(id);
            var model = new EmployeeRegisterViewModel
            {
                Id = item.Id.ToString(),
                FirstName = item.FirstName,
                MiddleName = item.MiddleName,
                LastName = item.LastName,
                EmployeeNo = item.EmployeeNo,
                Branch = item.Branch.Id.ToString(),
                Company = item.Company.Id.ToString(),
                Category = item.Category.Id.ToString(),
                Department = item.Department.Id.ToString(),
                Position = item.Position.Id.ToString(),
                Companies = this.Companies(),
                Branches = this.Branches(),
                Departments = this.Departments(),
                Positions = this.Positions(),
                EmployeeCategories = this.Categories(),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeRegisterViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new Models.Account
                {
                    Id = int.Parse(model.Id),
                    Branch = _Employee.GetBranchById(int.Parse(model.Branch)),
                    Department = _Employee.GetDepartmentById(int.Parse(model.Department)),
                    Company = _Employee.GetCompanyById(int.Parse(model.Company)),
                    Position = _Employee.GetPositionById(int.Parse(model.Position)),
                    Category = _Employee.GetCategoryById(int.Parse(model.Category)),
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    ModifiedBy = UserId,
                    ModifiedDate = DateTime.Now
                };
                _Employee.Update(item);
                return RedirectToAction("Index");
            }
            else
            {
                model.Companies = this.Companies();
                model.Branches = this.Branches();
                model.Departments = this.Departments();
                model.Positions = this.Positions();
                return RedirectToAction("Edit", model);
            }
        }

        #region Select List Items
        private IEnumerable<SelectListItem> Companies()
        {
            var result = _Employee.Companies()
                .Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                );
            return result;
        }
        private IEnumerable<SelectListItem> Branches()
        {
            var result = _Employee.Branches()
                .Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                );
            return result;
        }
        private IEnumerable<SelectListItem> Departments()
        {
            var result = _Employee.Departments()
                .Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                );
            return result;
        }
        private IEnumerable<SelectListItem> Positions()
        {
            var result = _Employee.Positions()
                .Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                );
            return result;
        }
        private IEnumerable<SelectListItem> Categories()
        {
            var result = _Employee.EmployeeCategories()
                .Select(
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                );
            return result;
        }
        #endregion
    }
}