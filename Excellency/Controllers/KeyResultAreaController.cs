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
    public class KeyResultAreaController : Controller
    {
        private IKeyResultArea _KeyResultArea;

        public KeyResultAreaController(IKeyResultArea keyResultArea)
        {
            _KeyResultArea = keyResultArea;
        }
        #region Key Result Area
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _KeyResultArea.GetAllKeyResultArea().Select(
                a => new KeyResultAreaViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }
                ).ToList();
            var model = new KeyResultAreaIndexViewModel
            {
                KeyResultAreas = result
            };
            return View(model);
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
                    Weight = model.Weight
                };
                if (model.Id == 0)
                {
                    item.CreatedBy = UserId;
                    item.CreationDate = DateTime.Now;
                    _KeyResultArea.Add(item);
                }
                else
                {
                    item.ModifiedBy = UserId;
                    item.ModifiedDate = DateTime.Now;
                    _KeyResultArea.Update(item);
                }
                return RedirectToAction("Index");
            }
            else { return RedirectToAction("Index", model); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _KeyResultArea.RemoveById(id);
            return RedirectToAction("Index");
        }
        #endregion
        #region Key Success Indicator
        [SessionAuthorized]
        public IActionResult Content(int id)
        {
            var kra = _KeyResultArea.GetKeyResultAreaById(id);
            var result = _KeyResultArea.SuccessIndicatorPerKRA(id).Select
                (
                a => new KeySuccessIndicatorViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }
                ).ToList();

            var RatingTables = _KeyResultArea.GetRatingTables().Select(a => new RatingTableViewModel
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
                    KeyResultArea = _KeyResultArea.GetKeyResultAreaById(model.KRAId),
                    Weight = model.KSIWeight
                };
                _KeyResultArea.SaveKeySuccessIndicator(item,UserId);
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
            _KeyResultArea.RemoveSuccessIndicatorById(model.KSIId);
            return RedirectToAction("Content", new { id = model.KRAId });
        }
        #endregion

        #region Category
        [SessionAuthorized]
        public IActionResult Category(int id)
        {
            var result = _KeyResultArea.CategoriesPerKSIId(id).
                Select(a => new CategoryViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();
            var ksi = _KeyResultArea.GetKeySuccessIndicatorById(id);
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
                    SuccessIndicator = _KeyResultArea.GetKeySuccessIndicatorById(model.KSIId)
                };
                _KeyResultArea.SaveCategory(category,UserId);
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
            _KeyResultArea.RemoveCategoryPerId(model.Id);
            return RedirectToAction("Category", new { id = model.KSIId });
        }
        #endregion
    }
}