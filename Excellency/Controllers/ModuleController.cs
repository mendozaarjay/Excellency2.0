using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Excellency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Excellency.Controllers
{
    public class ModuleController : Controller
    {
        private IModule _Module;

        public ModuleController(IModule module)
        {
            _Module = module;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _Module.Modules().Select
                (a => new ModuleViewModel
                {
                    Id = a.Id,
                    Description = a.Description
                }).ToList();
            var model = new ModuleIndexViewModel
            {
                Modules = result
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ModuleViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var module = new Module
                {
                    Id = model.Id,
                    Description = model.Description,
                };
                if (model.Id.ToString().Length <= 0)
                {
                    module.CreatedBy = UserId;
                    module.CreationDate = DateTime.Now;
                    _Module.Add(module);
                }
                else
                {
                    module.ModifiedBy = UserId;
                    module.ModifiedDate = DateTime.Now;
                    _Module.Update(module);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _Module.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}