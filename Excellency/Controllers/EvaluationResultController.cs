using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class EvaluationResultController : Controller
    {
        private IEvaluationReport _Services;

        public EvaluationResultController(IEvaluationReport report)
        {
            _Services = report;
        }
        public IActionResult Index()
        {
            var UserId = HttpContext.Session.GetString("UserId");
            var result = _Services.GetAllRatingPerUser(int.Parse(UserId));
            var model = new EvaluationResultIndexViewModel
            {
                EmployeeEvaluations = result
            };
            return View(model);
        }
        public IActionResult Result(int id)
        {
            var _Interpretation = _Services.InterpretationPerEmployee(id);
            var _EvaluationResult = _Services.GetResultPerEmployee(id);
            var model = new EvaluationResultViewModel
            {
                EvaluationResultItems = _EvaluationResult,
                InterpretationPerEmployee = _Interpretation
            };
            return View(model);
        }
    }
}
