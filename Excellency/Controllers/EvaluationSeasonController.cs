using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class EvaluationSeasonController : Controller
    {
        private IEvaluationSeason _Services;

        public EvaluationSeasonController(IEvaluationSeason season)
        {
            _Services = season;
        }
        public IActionResult Index()
        {
            var items = _Services.Seasons().Select(a => new EvaluationSeasonItem
            {
                Id = a.Id,
                Title = a.Title,
                Remarks = a.Remarks,
                StartDate = a.StartDate,
                EndDate = a.StartDate,
                IsActive = a.IsActive
            }).ToList();
            var model = new EvaluationSeasonIndexViewModel
            {
                Seasons = items,
            };
            return View();
        }
    }
}
