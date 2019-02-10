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
    public class PeerEvaluationController : Controller
    {
        private IPeerEvaluation _Services;

        public PeerEvaluationController(IPeerEvaluation peerEvaluation)
        {
            _Services = peerEvaluation;
        }
        public IActionResult Index()
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));

            var model = new PeerEvaluationIndexViewModel
            {
                Evaluations = _Services.Evaluations(UserId),
                Employees = _Services.GetAccounts(UserId),
            };
            return View(model);
        }
        public IActionResult Evaluate(int id)
        {
            var lineitems = _Services.GetCriterias()
                .Select(a => new PeerEvaluationLineItemViewModel
                {
                    CriteriaId = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();

            var model = new PeerEvaluationViewModel
            {
                Id = id,
                Name = _Services.GetNameById(id),
                LineItems = lineitems,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PeerEvaluationViewModel model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {

                var header = new PeerEvaluationHeader
                {
                    Employee = _Services.GetAccountById(model.Header.EmployeeId)
                };
                List<PeerEvaluationLine> items = new List<PeerEvaluationLine>();
                foreach(var item in model.LineItems)
                {
                    var lineitem = new PeerEvaluationLine
                    {
                        PeerCriteria = _Services.GetPeerCriteriaById(item.CriteriaId),
                        Score = item.Score,
                        Comment = item.Comment
                    };
                    items.Add(lineitem);
                }
                _Services.SavePeerEvaluation(header, items, UserId);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            var _header = _Services.GetHeader(id);
            var header = new PeerEvaluationHeaderViewModel
            {
                Id = _header.Id,
                EmployeeId = _header.Employee.Id,
                Name = _Services.GetNameById(_header.Employee.Id),
            };
            var line = _Services.GetLineItems(_header.Id)
                .Select(a => new PeerEvaluationLineItemViewModel
                {
                    Id = a.Id,
                    CriteriaId = a.PeerCriteria.Id,
                    HeaderId = _header.Id,
                    Comment = a.Comment,
                    Description = a.PeerCriteria.Description,
                    Score = a.Score,
                    Title = a.PeerCriteria.Title,
                    Weight = a.PeerCriteria.Weight
                }).ToList();
            var model = new PeerEvaluationViewModel
            {
                Header = header,
                LineItems = line,
                Id = _header.Employee.Id,
                Name = _Services.GetNameById(_header.Employee.Id)
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PeerEvaluationViewModel model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {

                var header = new PeerEvaluationHeader
                {
                    Id = model.Header.Id,
                    Employee = _Services.GetAccountById(model.Header.EmployeeId)
                };
                List<PeerEvaluationLine> items = new List<PeerEvaluationLine>();
                foreach (var item in model.LineItems)
                {
                    var lineitem = new PeerEvaluationLine
                    {
                        Id = item.Id,
                        PeerCriteria = _Services.GetPeerCriteriaById(item.CriteriaId),
                        Score = item.Score,
                        Comment = item.Comment
                    };
                    items.Add(lineitem);
                }
                _Services.SavePeerEvaluation(header, items, UserId);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(int id)
        {
            _Services.PostEvaluation(id);
            return RedirectToAction("Index");
        }
    }
}
