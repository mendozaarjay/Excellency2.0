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
    [SessionAuthorized]
    public class PositionLevelController : Controller
    {
        private IPositionLevel _Services;

        public PositionLevelController(IPositionLevel level)
        {
            _Services = level;
        }
        public IActionResult Index()
        {
            var result = _Services.PositionLevels()
                .Select(a => new PositionLevelItem
                {
                    Id = a.Id,
                    Level = a.Level,
                    Description = a.Description,
                }).ToList();
            var model = new PositionLevelIndexViewModel
            {
                PositionLevels = result,
                Levels = Levels(),
            };
            return View(model);
        }
        private IEnumerable<SelectListItem> Levels()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for(int i = 1; i <= 10; i++)
            {
                var item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                };
                items.Add(item);
            }
            return items;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PositionLevelIndexViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = int.Parse(HttpContext.Session.GetString("UserId"));
                var item = new PositionLevel
                {
                    Id = model.Item.Id,
                    Description = model.Item.Description,
                    Level = model.Item.Level,
                };
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
    }
}
