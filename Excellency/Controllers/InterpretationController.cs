using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class InterpretationController : Controller
    {
        private IInterpretation _context;

        public InterpretationController(IInterpretation interpretation)
        {
            _context = interpretation;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var items = _context.GetAll()
                .Select(a => new InterpretationItemViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    ScoreFrom = a.ScoreFrom,
                    ScoreTo = a.ScoreTo,
                }).ToList();
            var model = new InterpretationIndexViewModel
            {
                Interpretations = items
            };    
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(InterpretationIndexViewModel model)
        {
            var User = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var item = new Interpretation
                {
                    Id = model.Interpretation.Id,
                    Title = model.Interpretation.Title,
                    Description = model.Interpretation.Description,
                    ScoreFrom = model.Interpretation.ScoreFrom,
                    ScoreTo = model.Interpretation.ScoreTo,
                    CreatedBy = User,
                    ModifiedBy = User
                };
                _context.Save(item);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _context.RemoveById(id);
            return RedirectToAction("Index");
        }

    }
}
