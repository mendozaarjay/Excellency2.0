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
    public class RatingTableController : Controller
    {
        private IRatingTable _RatingTable;

        public RatingTableController(IRatingTable ratingTable)
        {
            _RatingTable = ratingTable;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _RatingTable.RatingTables().Select(a => new RatingTableViewModel
            {
                Id = a.Id,
                Description = a.Description
            }).ToList();
            var model = new RatingTableIndexViewModel
            {
                RatingTables = result
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRatingTable(RatingTableIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new RatingTable
                {
                    Id = model.RatingTable.Id,
                    Description = model.RatingTable.Description,
                };
                _RatingTable.Save(item,UserId);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveRatingTable(RatingTableIndexViewModel model)
        {
            _RatingTable.RemoveById(model.RatingTable.Id);
            return RedirectToAction("Index");
        }
        [SessionAuthorized]
        public IActionResult RatingTableItems(int id)
        {
            var result = _RatingTable.TableItemsPerId(id).Select(a => new RatingTableItemViewModel
            {
                Id = a.Id,
                Description = a.Description,
                Weight = a.Weight,
                RatingTableId = id
            }).ToList();
            var model = new RatingTableItemIndexViewModel
            {
                RatingTableItems = result,
                RatingTableId = id,
                RatingTableDescription = _RatingTable.GetRatingTableById(id).Description,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveTableItem(RatingTableItemIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new RatingTableItem
                {
                    Id = model.RatingTableItem.Id,
                    Description = model.RatingTableItem.Description,
                    RatingTable = _RatingTable.GetRatingTableById(model.RatingTableItem.RatingTableId),
                    Weight = model.RatingTableItem.Weight,
                    IsDeleted = false
                };
                _RatingTable.AddItem(item);
                return RedirectToAction("RatingTableItems", new { id = model.RatingTableItem.RatingTableId });
            }
            else
            {
                return RedirectToAction("RatingTableItems", new { id = model.RatingTableItem.RatingTableId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItemPerId(RatingTableItemIndexViewModel model)
        {
            _RatingTable.RemoveItemPerId(model.RatingTableItem.Id);
            return RedirectToAction("RatingTableItems", new { id = model.RatingTableItem.RatingTableId });
        }
    }
}
