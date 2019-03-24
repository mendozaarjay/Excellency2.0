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
    public class EvaluationSettingsController : Controller
    {
        private IEvaluationSettings _Services;

        public EvaluationSettingsController(IEvaluationSettings settings)
        {
            _Services = settings;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = new EvaluationSettingViewModel
            {
                Behavioral = _Services.GetBehavioralPercentage(),
                KRA = _Services.GetKRAPercentage()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(EvaluationSettingViewModel model)
        {
            _Services.Save(model.Behavioral, model.KRA);
            return RedirectToAction("Index");
        }
    }
}
