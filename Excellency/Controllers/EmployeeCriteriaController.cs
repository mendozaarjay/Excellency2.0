using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class EmployeeCriteriaController : Controller
    {
        private IEmployeeCriteria _Service;

        public EmployeeCriteriaController(IEmployeeCriteria employee)
        {
            _Service = employee;
        }

        public IActionResult Index()
        {
            var result = _Service.Criterias()
                .Select(a => new CriteriaAssignmentItemViewModel {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();
            var model = new CriteriaAssignmentIndexViewModel
            {
                Criterias = result,
            };
            return View(model);
        }
        public IActionResult Assign(int id)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var item = _Service.CriteriaHeaderById(id);
            var result = _Service.Assignments(id)
                .Select(a => new CriteriaAssignmentViewModel
                {
                    Id = a.Id,
                    Name = a.Employee.FirstName + " " + a.Employee.LastName,
                    EmployeeId = a.Employee.Id
                }).ToList();
            var emps = _Service.Employees(id,userId)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FirstName + " " + a.LastName,
                }).ToList();
            var model = new CriteriaAssignItemViewModel
            {
                Id = item.Id,
                Description = item.Description,
                Title = item.Title,
                Weight = item.Weight,
                Items = result,
                Employees  = emps,
            };
            return View(model);
        }
        public IActionResult Save(int employee,int criteriaid)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            _Service.Add(criteriaid, employee, userId);
            return RedirectToAction("Assign", new { id = criteriaid });
        }
        public IActionResult Remove(int id,int criteriaid)
        {
            _Service.RemoveById(id);
            return RedirectToAction("Assign", new { id = criteriaid });
        }
    }
}
