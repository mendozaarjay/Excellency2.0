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
    public class RaterAssignmentController : Controller
    {
        private IRaterAssignment _RaterAssignment;

        public RaterAssignmentController(IRaterAssignment raterAssignment)
        {
            _RaterAssignment = raterAssignment;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {

            return View();
        }
        [SessionAuthorized]
        public IActionResult RaterList()
        {
            var result = _RaterAssignment.Raters().Select
                (
                 a => new RaterViewModel
                 {
                     Id = a.Id,
                     Name = a.FirstName + " " + a.LastName,
                     Branch = a.Branch.Description,
                     Company = a.Company.Description,
                     Department = a.Department.Description,
                     Position = a.Position.Description
                 }
                ).ToList();
            var model = new RaterListingViewModel();
            model.Raters = result;
            return View(model);
        }
        [SessionAuthorized]
        [HttpGet]
        public IActionResult EditRater(int id)
        {
            var result = _RaterAssignment.GetAccountById(id);
            var rater = _RaterAssignment.GetRaterById(id);

            var raterAssignment = new List<RaterAssignedEmployee>();

            if (rater != null)
            {
                var _AssignedEmployees = _RaterAssignment.RaterAssignedEmployees(rater.Id).Select
                (
                 a => new RaterAssignedEmployee
                 {
                     Id = a.Employee.Id,
                     LineId = a.Id,
                     EmployeeName = a.Employee.LastName + ", " + a.Employee.FirstName + " " + a.Employee.MiddleName,
                     Branch = a.Employee.Branch.Description,
                     Company = a.Employee.Company.Description,
                     Department = a.Employee.Department.Description,
                     Position = a.Employee.Position.Description,
                 }
                ).ToList();
                raterAssignment = _AssignedEmployees;
            }
            var model = new RaterEditViewModel
            {
                Id = result.Id,
                Name = result.FirstName + " " + result.LastName,
                AssignedEmployees = raterAssignment,
                Employees = this.Employees(id)
            };
            return View(model);
        }
        private IEnumerable<Employee> Employees(int id)
        {
            var result = _RaterAssignment.Employees(id)
                .Select(
                a => new Employee
                {
                    Id = a.Id.ToString(),
                    Name = a.FirstName + " " + a.LastName
                }
                ).ToList();
            return result;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSelectedEmployee(RaterEditViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (model.SelectedEmployee.Length > 0)
            {
                var Employees = new List<int>();
                for (int i = 0; i <= model.SelectedEmployee.Length - 1; i++)
                {
                    Employees.Add(int.Parse(model.SelectedEmployee[i].ToString()));
                }
                _RaterAssignment.AddEmployee(Employees, model.Id,UserId);
            }
            return RedirectToAction("EditRater",new {id = model.Id});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLineItem(RaterEditViewModel model)
        {
            _RaterAssignment.RemoveLineItem(model.SelectedLineItem);
            return RedirectToAction("EditRater",  new { id = model.Id});
        }
    }
}