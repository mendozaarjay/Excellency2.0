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
                EndDate = a.EndDate,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedBy,
                CreationDate = a.CreationDate
            }).ToList();
            var model = new EvaluationSeasonIndexViewModel
            {
                Seasons = items,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(EvaluationSeasonIndexViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            if(ModelState.IsValid)
            {
                var item = new EvaluationSeason();
                if(model.Evaluation.Id == 0)
                {
                    item.Id = 0;
                    item.Title = model.Evaluation.Title;
                    item.Remarks = model.Evaluation.Remarks;
                    item.StartDate = model.Evaluation.StartDate;
                    item.EndDate = model.Evaluation.EndDate;
                }
                else
                {
                    item.Id = model.Evaluation.Id;
                    item.Title = model.Evaluation.Title;
                    item.Remarks = model.Evaluation.Remarks;
                    item.StartDate = model.Evaluation.StartDate;
                    item.EndDate = model.Evaluation.EndDate;
                    item.CreatedBy = model.Evaluation.CreatedBy;
                    item.CreationDate = model.Evaluation.CreationDate;
                }
                _Services.Save(item, userId);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            _Services.RemoveById(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetActive(int id)
        {
            _Services.SetActive(id);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var model = new EvaluationPeriodItem();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EvaluationPeriodItem model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var item = new EvaluationSeason
                {
                    Id = 0,
                    Title = model.Title,
                    Remarks = model.Remarks,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };
                _Services.Save(item, userId);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new EvaluationPeriodItem();
            var item = _Services.EvaluationSeasonById(id);
            model.Id = item.Id;
            model.Title = item.Title;
            model.Remarks = item.Remarks;
            model.StartDate = item.StartDate;
            model.EndDate = item.EndDate;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EvaluationPeriodItem model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var item = new EvaluationSeason
                {
                    Id = model.Id,
                    Title = model.Title,
                    Remarks = model.Remarks,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };
                _Services.Save(item, userId);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}
