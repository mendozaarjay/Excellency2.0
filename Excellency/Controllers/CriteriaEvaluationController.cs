using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class CriteriaEvaluationController : Controller
    {
        private ICriteriaEvaluation _Services;

        public CriteriaEvaluationController(ICriteriaEvaluation criteriaEvaluation)
        {
            _Services = criteriaEvaluation;
        }
        public IActionResult Index()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var employees = _Services.Accounts(userId)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FirstName + " " + a.LastName
                }).ToList();
            var model = new CriteriaEvaluationIndexViewModel
            {
                Employees = employees
            };

            return View(model);
        }
        public IActionResult Overview(int employee)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));

            var item = _Services.EmployeeCriteriaAssignmentByEmployeeId(employee);
            var evaluation = _Services.CriteriaEvaluationPerEmployeeId(employee, userId);
            int recordid = 0;
            if(evaluation == null)
            {
                recordid = 0;
            }
            else
            {
                recordid = evaluation.Id;
            }
            var model = new CriteriaEvaluationOverViewModel
            {
                EmployeeId = employee,
                Name = _Services.GetName(employee),
                Title = item.Criteria.Title,
                Weight = item.Criteria.Weight,
                Description = item.Criteria.Description,
                IsEvaluated = _Services.IsEvaluated(employee,item.Criteria.Id,userId),
                IsPosted = _Services.IsPosted(employee,item.Criteria.Id,userId),
                RecordId = recordid
            };
            return View(model);
        }
        public IActionResult Evaluate(int employeeid)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var item = _Services.EmployeeCriteriaAssignmentByEmployeeId(employeeid);

            var header = new EvaluatePeerHeaderViewModel
            {
                Id = item.Criteria.Id,
                Title = item.Criteria.Title,
                Description = item.Criteria.Description,
                Weight = item.Criteria.Weight,
            };
            var line = _Services.CriteriaLinesByHeaderId(item.Criteria.Id)
                .Select(a => new EvaluatePeerLineViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = Convert.ToInt32(a.Weight),
                }).ToList();
            var model = new CreatePeerEvaluationViewModel
            {
                EmployeeId = employeeid,
                Name = _Services.GetName(employeeid),
                Header = header,
                LineItems = line,
            };
            return View(model);
        }
        public IActionResult Save(CreatePeerEvaluationViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var header = new CriteriaEvaluationHeader
            {
                Criteria = _Services.CriteriaHeaderById(model.Header.Id),
                Rater = _Services.GetAccountById(userId),
                Ratee = _Services.GetAccountById(model.EmployeeId),
            };
            List<CriteriaEvaluationLine> items = new List<CriteriaEvaluationLine>();
            foreach(var item in model.LineItems)
            {
                var i = new CriteriaEvaluationLine
                {
                    Id = 0,
                    CriteriaLine = _Services.CriteriaLineById(item.Id),
                    Comment = item.Comment,
                    Score = item.Score
                };
                items.Add(i);
            }
            _Services.Save(header, items, userId);
            return RedirectToAction("Overview", new { employee = model.EmployeeId });
        }
        public IActionResult Edit(int employeeid)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var h = _Services.CriteriaEvaluationPerEmployeeId(employeeid, userId);
            var header = new EvaluatePeerHeaderViewModel
            {
                RecordId = h.Id,
                Id = h.Criteria.Id,
                Title = h.Criteria.Title,
                Description = h.Criteria.Description,
                Weight = h.Criteria.Weight
            };
            var lineitems = _Services.EvaluationLinesByHeaderId(h.Id)
                .Select(a => new EvaluatePeerLineViewModel
                {
                    RecordId = a.Id,
                    Id = a.CriteriaLine.Id,
                    Title = a.CriteriaLine.Title,
                    Description = a.CriteriaLine.Description,
                    Weight = Convert.ToInt32(a.CriteriaLine.Weight),
                    Comment = a.Comment,
                    Score = Convert.ToInt32(a.Score),
                }).ToList();
            var model = new CreatePeerEvaluationViewModel
            {
                EmployeeId = employeeid,
                Name = _Services.GetName(employeeid),
                Header = header,
                LineItems = lineitems,
            };
            return View(model);
        }
        public IActionResult Update(CreatePeerEvaluationViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var header = new CriteriaEvaluationHeader
            {
                Id = model.Header.RecordId,
                Criteria = _Services.CriteriaHeaderById(model.Header.Id),
                Rater = _Services.GetAccountById(userId),
                Ratee = _Services.GetAccountById(model.EmployeeId),
            };
            List<CriteriaEvaluationLine> items = new List<CriteriaEvaluationLine>();
            foreach (var item in model.LineItems)
            {
                var i = new CriteriaEvaluationLine
                {
                    Id = item.RecordId,
                    CriteriaLine = _Services.CriteriaLineById(item.Id),
                    Comment = item.Comment,
                    Score = item.Score
                };
                items.Add(i);
            }
            _Services.Update(header, items, userId);
            return RedirectToAction("Overview", new { employee = model.EmployeeId });
        }
        public IActionResult Post(int recordid,int employeeid)
        {
            _Services.Post(recordid);
            return RedirectToAction("Overview", new { employee = employeeid });
        }
    }
}
