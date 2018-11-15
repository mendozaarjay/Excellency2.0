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
        private IPosition _Position;

        public PositionController(IPosition position)
        {
            _Position = position;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _Position.Positions().Select
                (a => new PositionViewModel
                {
                    Id = a.Id,
                    Description = a.Description
                }).ToList();
            var model = new PositionIndexViewModel
            {
                Positions = result
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PositionIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var position = new Position
                {
                    Id = model.Id,
                    Description = model.Description,
                };
                if (model.Id.ToString().Equals("0"))
                {
                    position.CreatedBy = UserId;
                    position.CreationDate = DateTime.Now;
                    _Position.Add(position);
                }
                else
                {
                    position.ModifiedBy = UserId;
                    position.ModifiedDate = DateTime.Now;
                    _Position.Update(position);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index",model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _Position.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}