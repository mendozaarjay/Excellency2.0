using Excellency.Interfaces;
using Excellency.Models;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Excellency.Controllers
{
    public class KeyResultAreaController : Controller
    {
        private IKeyResultArea _Services;

        public KeyResultAreaController(IKeyResultArea keyResultArea)
        {
            _Services = keyResultArea;
        }
        #region Key Result Area
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _Services.GetAllKeyResultArea().Select(
                a => new KeyResultAreaViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }
                ).ToList();
            var aes = _Services.ActiveSeason();
            var season = new EvaluationSeasonItem();
            if(aes != null)
            {
                season.Id = aes.Id;
                season.Title = aes.Title;
                season.Remarks = aes.Remarks;
                season.StartDate = aes.StartDate;
                season.EndDate = aes.EndDate;
            };
            var model = new KeyResultAreaIndexViewModel
            {
                KeyResultAreas = result,
                IsWithActiveSeason = _Services.IsWithActiveSeason(),
                ActiveSeason = season,
                
            };
            return View(model);
        }
        public IActionResult Search(string keyword)
        {
            var term = keyword.ToLower();
            var items = _Services.GetAllKeyResultArea().Where(a => a.Title.ToLower().Contains(term) || a.Description.ToLower().Contains(term))
                .Select(
                a => new KeyResultAreaViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }
                ).ToList();
            return Json(new { result = items });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(KeyResultAreaIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new KeyResultArea
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Weight = 100
                };
                if (model.Id == 0)
                {
                    item.CreatedBy = UserId;
                    item.CreationDate = DateTime.Now;
                    _Services.Add(item);
                }
                else
                {
                    item.ModifiedBy = UserId;
                    item.ModifiedDate = DateTime.Now;
                    _Services.Update(item);
                }
                return RedirectToAction("Index");
            }
            else { return RedirectToAction("Index", model); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _Services.RemoveById(id);
            return RedirectToAction("Index");
        }
        #endregion
        #region Key Success Indicator
        [SessionAuthorized]
        public IActionResult Content(int id)
        {
            var kra = _Services.GetKeyResultAreaById(id);
            var result = _Services.SuccessIndicatorPerKRA(id).Select
                (
                a => new KeySuccessIndicatorViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }
                ).ToList();

            var RatingTables = _Services.GetRatingTables().Select(a => new RatingTableViewModel
            {
                Id = a.Id,
                Description = a.Description
            }).ToList();
            var item = new KeyResultAreaContentViewModel
            {
                KeyResultAreaId = kra.Id,
                Title = kra.Title,
                Description = kra.Description,
                SuccessIndicators = result,
                RatingTables = RatingTables,
            };
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveKeySuccessIndicator(KeyResultAreaContentViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new KeySuccessIndicator
                {
                    Id = model.KSIId,
                    Title = model.KSITitle,
                    Description = model.KSIDescription,
                    KeyResultArea = _Services.GetKeyResultAreaById(model.KRAId),
                    Weight = model.KSIWeight
                };
                _Services.SaveKeySuccessIndicator(item,UserId);
                return RedirectToAction("Content", new { id = model.KRAId });
            }
            else
            {
                return RedirectToAction("Content", new { id = model.KeyResultAreaId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveSuccessIndicator(KeyResultAreaContentViewModel model)
        {
            _Services.RemoveSuccessIndicatorById(model.KSIId);
            return RedirectToAction("Content", new { id = model.KRAId });
        }
        #endregion

        #region Category
        [SessionAuthorized]
        public IActionResult Category(int id)
        {
            var result = _Services.CategoriesPerKSIId(id).
                Select(a => new CategoryViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();
            var ksi = _Services.GetKeySuccessIndicatorById(id);
            var model = new CategoryIndexViewModel
            {
                Categories = result,
                KSIId = ksi.Id,
                KSIDescription = ksi.Description
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(CategoryIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = model.Id,
                    Description = model.Description,
                    Weight = model.Weight,
                    SuccessIndicator = _Services.GetKeySuccessIndicatorById(model.KSIId)
                };
                _Services.SaveCategory(category,UserId);
                return RedirectToAction("Category", new { id = model.KSIId });
            }
            else
            {
                return RedirectToAction("Category", new { id = model.KSIId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveCategoryById(CategoryIndexViewModel model)
        {
            _Services.RemoveCategoryPerId(model.Id);
            return RedirectToAction("Category", new { id = model.KSIId });
        }
        #endregion
    }
}