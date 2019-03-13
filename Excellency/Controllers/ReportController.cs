using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class ReportController : Controller
    {
        private IReport _Services;

        public ReportController(IReport report)
        {
            _Services = report;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var kra = new SelectListItem
            {
                Value = "kra",
                Text = "KRA"
            };
            var beh = new SelectListItem
            {
                Value = "behavioral",
                Text = "Behavioral"
            };
            List<SelectListItem> types = new List<SelectListItem>();
            types.Add(kra);
            types.Add(beh);

            var periods = _Services.EvaluationSeasons()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Title,
                }).ToList();
            var model = new ReportIndexViewModel
            {
                Periods = periods,
                Types = types
            };

            return View(model);
        }
        public IActionResult GenerateReport(int period,string type)
        {
            var items = _Services.Evaluations(period, type);
            return Json(new { result = items });
        }
        public IActionResult Employees()
        {
            return View();
        }
        public IActionResult SearchEmployees(string keyword)
        {
            var items = _Services.Employees(keyword);
            return Json(new { result = items });
        }
        public IActionResult EmployeePerformance()
        {
            var periods = _Services.EvaluationSeasons()
               .Select(a => new SelectListItem
               {
                   Value = a.Id.ToString(),
                   Text = a.Title,
               }).ToList();
            var accounts = _Services.Accounts()
               .Select(a => new SelectListItem
               {
                   Value = a.Id.ToString(),
                   Text = a.Name
               }).ToList();
            var model = new EmployeePerformanceIndexViewModel
            {
                Periods = periods,
                Employees = accounts

            };
            return View(model);
        }
        public IActionResult GetEmployeePerformance(int period,int id)
        {
            var items = _Services.EmployeePerformances(period, id);
            return Json(new { result = items });
        }
        public IActionResult AppraisalHistory()
        {
            var periods = _Services.EvaluationSeasons()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Title,
                }).ToList();
            var accounts = _Services.Accounts()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList();
            var model = new AppraisalHistoryIndexViewModel
            {
                Employees = accounts,
                Periods = periods
            };
            return View(model);
        }
        public IActionResult GenerateAppraisal(int period,int id)
        {
            var items = _Services.AppraisalHistories(period, id);
            return Json(new { result = items });
        }

        public IActionResult PeerEvaluation()
        {
            var periods = _Services.EvaluationSeasons()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Title,
                }).ToList();
            var accounts = _Services.Accounts()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList();
            var model = new PeerEvaluationResultViewModel
            {
                Employees = accounts,
                Periods = periods
            };
            return View(model);
        }
        public IActionResult PeerEvaluationSummary(int period, int id)
        {
            var items = _Services.PeerRatings(period, id);
            return Json(new { result = items });
        }
        public IActionResult PeerDetailedRating(int period, int id)
        {
            var items = _Services.PeerDetailedRating(period, id);
            return Json(new { result = items });
        }
        [HttpGet]
        public IActionResult ViewDetailed(int period, int id)
        {
            var items = _Services.PeerDetailedRating(period, id);
            var header = _Services.EmployeeCriteria(period, id);
            var model = new DetailedViewModel
            {
                Id = id,
                Name = _Services.NameById(id),
                Title = header.Criteria.Title,
                Description = header.Criteria.Description,
                Weight = header.Criteria.Weight,
                Items = items,
            };
            return View(model);
        }
        public IActionResult GraphicalDistribution()
        {
            var periods = _Services.EvaluationSeasons()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Title,
                }).ToList();
            var model = new GraphicalDistributionViewModel
            {
                Periods = periods
            };
            return View(model);
        }
        public IActionResult Graph()
        {
            var periods = _Services.EvaluationSeasons()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Title,
                }).ToList();
            var model = new GraphicalDistributionViewModel
            {
                Periods = periods
            };
            return View(model);
        }

    }
}
