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
    public class RatingController : Controller
    {
        private IRating _Rating;

        public RatingController(IRating rating)
        {
            _Rating = rating;
        }
        public IActionResult Index()
        {
            var result = _Rating.GetAllRatings().Select(
                a => new RatingViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Score = a.Score
                }
                ).ToList();
            var model = new RatingIndexViewModel
            {
                Ratings = result
            };
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save(RatingIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new Rating
                {
                    Id = model.Id,
                    Description = model.Description,
                    Score = model.Score
                };
                if (item.Id == 0)
                {
                    item.CreatedBy = UserId;
                    item.CreationDate = DateTime.Now;
                    _Rating.Add(item);
                }
                else
                {
                    item.ModifiedBy = UserId;
                    item.ModifiedDate = DateTime.Now;
                    _Rating.Update(item);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _Rating.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}