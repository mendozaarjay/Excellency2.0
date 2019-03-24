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
    public class PeerAssignmentController : Controller
    {
        private IPeerAssignment _Services;
        
        public PeerAssignmentController(IPeerAssignment peerAssignment)
        {
            _Services = peerAssignment;
        }
        // GET: /<controller>/
        public IActionResult Index(int? page)
        {
            int currentpage;
            if (page == null)
                currentpage = 1;
            else
                currentpage = (int)page;

            var maxcount = currentpage < 5 ? 5 : currentpage + 2;
            var mincount = currentpage < 5 ? 1 : currentpage - 2;

            var maxpage = (_Services.GetAllAccounts().Count() / 10) + 1;

            maxcount = currentpage <= maxpage ? maxcount : maxpage;
            var result = _Services.Employees(currentpage);

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
            var model = new PeerAssignmentIndexViewModel
            {
                Accounts = result,
                IsWithActiveSeason = _Services.IsWithActiveSeason(),
                ActiveSeason = season,
            };
            ViewBag.MaxCount = maxcount;
            ViewBag.MinCount = mincount;
            ViewBag.CurrentPage = currentpage;
            ViewBag.MaxPage = maxpage;
            return View(model);
        }
        public IActionResult Search(string keyword)
        {
            var items = _Services.Search(keyword);
            return Json(new { result = items });
        }
        public IActionResult Assign(int id)
        {
            var accounts = _Services.GetAllAccountsByRaterId(id)
                .Select(a => new PeerAssignmentAccountViewModel
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.MiddleName + " " + a.LastName
                }).ToList();
            var assigned = _Services.GetAssignmentsPerRater(id)
                .Select(a => new AssignedPeerItemViewModel
                {
                    Id = a.Id,
                    Name = a.Ratee.FirstName + " " + a.Ratee.MiddleName + " " + a.Ratee.LastName
                }).ToList();

            var rater = _Services.GetAccountById(id);
            string name = rater.FirstName + " " + rater.MiddleName + " " + rater.LastName;
            var model = new PeerAssignViewModel
            {
                RaterId = id,
                RaterName = name,
                Accounts = accounts,
                Assigned = assigned
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int raterid,int id)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var item = new PeerAssignment
            {
                Rater = _Services.GetAccountById(raterid),
                Ratee = _Services.GetAccountById(id),
                IsDeleted = false,
                IsExpired = false,
                Id = 0
            };
            _Services.Save(item, UserId);
            return RedirectToAction("Assign", new { id = raterid });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int raterid,int id)
        {
            _Services.RemoveById(id);
            return RedirectToAction("Assign", new { id = raterid });
        }
    }
}
