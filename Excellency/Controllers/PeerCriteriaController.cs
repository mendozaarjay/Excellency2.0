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
    public class PeerCriteriaController : Controller
    {
        private IPeerCriteria _PeerCriteria;

        public PeerCriteriaController(IPeerCriteria peer)
        {
            _PeerCriteria = peer;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _PeerCriteria.PeerCriteriaHeaders()
                .Select(a => new PeerCriteriaHeaderViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();
            var model = new PeerCriteriaIndexViewModel
            {
                Criterias = result
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PeerCriteriaIndexViewModel model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var item = new PeerCriteriaHeader
                {
                    Id = model.PeerCriteria.Id,
                    Title = model.PeerCriteria.Title,
                    Description = model.PeerCriteria.Description,
                    Weight = model.PeerCriteria.Weight
                };
                _PeerCriteria.SavePeerCriteria(item, UserId);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveHeaderItem(int id)
        {
            _PeerCriteria.RemoveHeaderById(id);
            return RedirectToAction("Index");
        }
        [SessionAuthorized]
        public IActionResult PeerLineItem(int id)
        {
            var header = _PeerCriteria.GetPeerCriteriaHeaderById(id);
            var items = _PeerCriteria.PeerCriteriaLinesByHeaderId(id)
                .Select(a => new PeerCriteriaLineViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();
            var model = new PeerCriteriaLineIndexViewModel
            {
                HeaderId = id,
                HeaderTitle = header.Title,
                HeaderDescription = header.Description,
                LineItems = items
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveLineItem(PeerCriteriaLineIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new PeerCriteriaLine
                {
                    Id = model.Criteria.Id,
                    Description = model.Criteria.Description,
                    Weight = model.Criteria.Weight
                };
                _PeerCriteria.SaveCriteriaLine(model.HeaderId, item);
            }
            return RedirectToAction("PeerLineItem", new { id = model.HeaderId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLineItem(int id)
        {
            _PeerCriteria.RemoveLineById(id);
            return RedirectToAction("Index");
        }
    }
}
