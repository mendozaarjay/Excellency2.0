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
    public class CriteriaSettingController : Controller
    {
        private ICriteriaSetting _Service;

        public CriteriaSettingController(ICriteriaSetting criteriaSetting)
        {
            _Service = criteriaSetting;
        }
        public IActionResult Index()
        {
            var result = _Service.CriteriaHeaders()
                .Select(a => new CriteriaHeaderViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();
            var model = new CriteriaSettingIndexViewModel
            {
                Items = result,
            };
            return View(model);
        }
        public IActionResult SaveHeader (CriteriaSettingIndexViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var item = new CriteriaHeader
                {
                    Id = model.Header.Id,
                    Title = model.Header.Title,
                    Description = model.Header.Description,
                    Weight = model.Header.Weight,
                };
                _Service.SaveHeader(item, userId);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveHeader(int id)
        {
            _Service.RemoveHeader(id);
            return RedirectToAction("Index");
        }
        public IActionResult Assign(int id)
        {
            var result = _Service.CriteriaHeaderById(id);
            var header = new CriteriaHeaderViewModel
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Weight = result.Weight,
            };
            var lineitems = _Service.LineItemsByHeaderId(id)
                .Select(a => new CriteriaLineViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight
                }).ToList();
            var model = new CriteriaAssignViewModel
            {
                Header = header,
                LineItems = lineitems,
            };
            return View(model);
        }
        public IActionResult SaveLine(CriteriaAssignViewModel model)
        {

            if (ModelState.IsValid)
            {
                var item = new CriteriaLine
                {
                    Id = model.Line.Id,
                    Title = model.Line.Title,
                    Description = model.Line.Description,
                    Weight = model.Line.Weight,
                };
                _Service.SaveLine(model.Line.HeaderId, item);
            }
            return RedirectToAction("Assign", new { id = model.Line.HeaderId});
        }
        public IActionResult RemoveLine(int id,int headerid)
        {
            _Service.RemoveLine(id);
            return RedirectToAction("Assign", new { id = headerid });
        }
    }
}
