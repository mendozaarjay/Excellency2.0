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
        private IRaterAssignment _Services;

        public RaterAssignmentController(IRaterAssignment raterAssignment)
        {
            _Services = raterAssignment;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {

            return View();
        }
        [SessionAuthorized]
        public IActionResult RaterList(int? page)
        {
            int currentpage;
            if (page == null)
                currentpage = 1;
            else
                currentpage = (int)page;

            var maxcount = currentpage < 5 ? 5 : currentpage + 2;
            var mincount = currentpage < 5 ? 1 : currentpage - 2;

            var maxpage = (_Services.Raters().Count() / 10) + 1;

            maxcount = currentpage <= maxpage ? maxcount : maxpage;
            var result = _Services.RaterList(currentpage);
            var model = new RaterListingViewModel();
            model.Raters = result;
            ViewBag.MaxCount = maxcount;
            ViewBag.MinCount = mincount;
            ViewBag.CurrentPage = currentpage;
            ViewBag.MaxPage = maxpage;
            return View(model);
        }
        public IActionResult Search(string keyword)
        {
            var items = _Services.SearchRater(keyword);
            return Json(new { result = items });
        }
        [SessionAuthorized]
        [HttpGet]
        public IActionResult EditRater(int id)
        {
            var result = _Services.GetAccountById(id);
            var rater = _Services.GetRaterById(id);

            var raterAssignment = new List<RaterAssignedEmployee>();

            if (rater != null)
            {
                var _AssignedEmployees = _Services.RaterAssignedEmployees(rater.Id).Select
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
            var result = _Services.Employees(id)
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
                _Services.AddEmployee(Employees, model.Id,UserId);
            }
            return RedirectToAction("EditRater",new {id = model.Id});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLineItem(RaterEditViewModel model)
        {
            _Services.RemoveLineItem(model.SelectedLineItem);
            return RedirectToAction("EditRater",  new { id = model.Id});
        }
    }
}