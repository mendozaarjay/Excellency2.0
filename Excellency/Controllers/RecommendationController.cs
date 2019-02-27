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
    public class RecommendationController : Controller
    {
        private IRecommendation _Services;

        public RecommendationController(IRecommendation recommendation)
        {
            _Services = recommendation;
        }
        public IActionResult Index()
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var result = _Services.Accounts(UserId)
                .Select(a => new RecommendationIndexItem
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.MiddleName + " " + a.LastName,
                    IsWithRecommendation = _Services.IsWithRecommendation(a.Id)
                }).ToList();

            var aes = _Services.ActiveSeason();
            var season = new EvaluationSeasonItem();
            if (aes != null)
            {
                season.Id = aes.Id;
                season.Title = aes.Title;
                season.Remarks = aes.Remarks;
                season.StartDate = aes.StartDate;
                season.EndDate = aes.EndDate;
            };
            var model = new RecommendationIndexViewModel
            {
                Accounts = result,
                IsWithActiveSeason = _Services.IsWithActiveSeason(),
                ActiveSeason = season,
            };
            return View(model);
        }
        public IActionResult Assign(int id)
        {
            var result = _Services.RecommendationByEmployeeId(id);
            var item = new RecommendationItem();
            var aes = _Services.ActiveSeason();
            var season = new EvaluationSeasonItem();
            if (aes != null)
            {
                season.Id = aes.Id;
                season.Title = aes.Title;
                season.Remarks = aes.Remarks;
                season.StartDate = aes.StartDate;
                season.EndDate = aes.EndDate;
            };

            if (result == null)
            {
                item.Id = 0;
                item.EmployeeId = id;
                item.EmployeeName = _Services.GetNameById(id);

            }
            else
            {
                item.Id = result.Id;
                item.EmployeeId = id;
                item.EmployeeName = _Services.GetNameById(id);
                item.Recommendation = result.Comment;
                item.CreatedBy = result.CreatedBy;
                item.CreationDate = result.CreationDate;
            }
            item.IsWithActiveSeason = _Services.IsWithActiveSeason();
            item.ActiveSeason = season;
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(RecommendationItem model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var item = new Recommendation
                {
                    Id = model.Id,
                    Employee = _Services.GetAccountById(model.EmployeeId),
                    Comment = model.Recommendation,
                    CreatedBy = model.CreatedBy,
                    CreationDate = model.CreationDate,
                };
                _Services.Save(item, UserId);
            }
            return RedirectToAction("Index");
        }
    }
}
