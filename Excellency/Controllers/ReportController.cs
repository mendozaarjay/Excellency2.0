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
        public IActionResult KRAResult()
        {
            return View();
        }
    }
}
