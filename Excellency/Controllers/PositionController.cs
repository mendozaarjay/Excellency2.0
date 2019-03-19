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
    public class PositionController : Controller
    {
        private IPosition _Services;

        public PositionController(IPosition position)
        {
            _Services = position;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _Services.Positions().Select
                (a => new PositionViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Level = a.PositionLevel.Id,
                    LevelDescription = a.PositionLevel.Description,
                }).ToList();
            var levels = _Services.PositionLevels()
                .Select(a => new PositionLevelItem
                {
                    Id = a.Id,
                    Description = a.Description,
                    Level = a.Level
                }).ToList();
            var model = new PositionIndexViewModel
            {
                Positions = result,
                Levels = levels,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PositionIndexViewModel model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if(ModelState.IsValid)
            {
                var item = new Position
                {
                    Id = model.Id,
                    Description = model.Description,
                    PositionLevel = _Services.PositionLevelById(model.Level),
                };
                _Services.Save(item, UserId);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _Services.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}