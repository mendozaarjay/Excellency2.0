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

namespace Excellency.Controllers
{
    public class BehavioralFactorController : Controller
    {
        private IBehavioralFactor _BehavioralFactor;

        public BehavioralFactorController(IBehavioralFactor behavioralFactor)
        {
            _BehavioralFactor = behavioralFactor;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _BehavioralFactor.GetBehavioralFactors()
                .Select(a => new BehavioralFactorViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight,
                    Category = a.Category.Id
                }).ToList();
            var aes = _BehavioralFactor.ActiveSeason();
            var season = new EvaluationSeasonItem();
            if (aes != null)
            {
                season.Id = aes.Id;
                season.Title = aes.Title;
                season.Remarks = aes.Remarks;
                season.StartDate = aes.StartDate;
                season.EndDate = aes.EndDate;
            };
            var model = new BehavioralFactorIndexViewModel
            {
                BehavioralFactors = result,
                EmployeeCategories = this.Categories(),
                IsWithActiveSeason = _BehavioralFactor.IsWithActiveSeason(),
                ActiveSeason = season,
            };
            return View(model);
        }
        public IEnumerable<SelectListItem> Categories()
        {
            var categories = _BehavioralFactor.EmployeeCategories()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }).ToList();
            return categories;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveBehavioralFactor(BehavioralFactorIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new BehavioralFactor
                {
                    Id = model.BehavioralFactor.Id,
                    Title = model.BehavioralFactor.Title,
                    Description = model.BehavioralFactor.Description,
                    Weight = 100,
                    Category = _BehavioralFactor.GetEmployeeCategoryById(model.BehavioralFactor.Category),
                    CreatedBy = HttpContext.Session.GetString("UserId"),
                    CreationDate = DateTime.Now,
                };
                _BehavioralFactor.SaveBehavioralFactor(item);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index",model);
            }
        }
        [SessionAuthorized]
        public IActionResult BehavioralFactorItems(int id)
        {
            var _factor = _BehavioralFactor.GetBehavioralFactorById(id);
            var factor = new BehavioralFactorViewModel
            {
                Id = _factor.Id,
                Title = _factor.Title,
                Description = _factor.Description,
                Weight = _factor.Weight
            };
            var factorItems = _BehavioralFactor.GetBehavioralFactorItemsByHeaderId(id)
                .Select(a => new BehavioralFactorItemViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Weight = a.Weight,
                }).ToList();
            var model = new BehavioralFactorPerHeaderViewModel
            {
                HeaderId = id,
                BehavioralFactor = factor,
                BehavioralFactorItems = factorItems
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveFactorItem(BehavioralFactorPerHeaderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new BehavioralFactorItem
                {
                    Id = model.BehavioralFactorItem.Id,
                    Description = model.BehavioralFactorItem.Description,
                    Weight = model.BehavioralFactorItem.Weight,
                    BehavioralFactor = _BehavioralFactor.GetBehavioralFactorById(model.HeaderId)    
                };
                _BehavioralFactor.SaveFactorItem(item);
                return RedirectToAction("BehavioralFactorItems", new { id = model.HeaderId });
            }
            else
            {
                return RedirectToAction("BehavioralFactorItems", new { id = model.HeaderId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFactor(int id)
        {
            _BehavioralFactor.RemoveBehavioralFactorById(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFactorItem(int id, int headerid)
        {
            _BehavioralFactor.RemoveFactorItemById(id);
            return RedirectToAction("BehavioralFactorItems", new { id = headerid });
        }
    }
}
