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
    [SessionAuthorized]
    public class RecommendationAssignmentController : Controller
    {
        private IRecommendationAssignment _Services;

        public RecommendationAssignmentController(IRecommendationAssignment assignment)
        {
            _Services = assignment;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _Services.Recommender()
                .Select(a => new RecommendationAccountItemViewModel
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.LastName,
                }).ToList();
            var model = new RecommendationAssignmentIndexViewModel
            {
                Recommenders = items,
            };
            return View(model);
        }
        public IActionResult Assign(int id)
        {
            var model = new RecommendationAssignViewModel();
            var assigned = _Services.RecommendationAssignments(id)
                .Select(a => new RecommendationAssignmentViewModel
                {
                    Id = a.Id,
                    EmployeeId = a.Employee.Id,
                    Name = a.Employee.FirstName + " " + a.Employee.LastName
                }).ToList();
            var employess = _Services.Employees(id)
                .Select(a => new RecommendationAccountItemViewModel
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.LastName
                }).ToList();
            model.Assigned = assigned;
            model.Employees = employess;
            model.Id = id;
            model.Name = _Services.GetNameById(id);
            return View(model);
        }
        public IActionResult Save(int id,int employeeid)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var item = new RecommendationAssignment
            {
                Id = 0,
                Employee = _Services.GetAccountById(employeeid),
                Recommender = _Services.GetAccountById(id),
            };
            _Services.Save(item, userId);
            return RedirectToAction("Assign", new { id = id });
        }
        public IActionResult Remove(int id,int recordid)
        {
            _Services.RemoveById(recordid);
            return RedirectToAction("Assign", new { id = id });
        }

    }
}
