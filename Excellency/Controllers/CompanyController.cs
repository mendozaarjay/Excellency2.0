using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Excellency.Models;
using Microsoft.AspNetCore.Http;

namespace Excellency.Controllers
{
    public class CompanyController : Controller
    {
        private ICompany _Company;

        public CompanyController(ICompany company)
        {
            _Company = company;
        }
        public IActionResult Index()
        {
            var result = _Company.Companies().Select
                (
                   a => new CompanyViewModel
                   {
                       Id = a.Id,
                       Description = a.Description
                   }
                ).ToList();
            var model = new CompanyIndexViewModel();
            model.Companies = result;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CompanyIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var company = new Company
                {
                    Id = model.Id,
                    Description = model.Description,
                };
                if (model.Id.ToString().Length <= 0 || model.Id == 0)
                {
                    company.CreatedBy = UserId;
                    company.CreationDate = DateTime.Now;
                    _Company.Add(company);
                }
                else
                {
                    company.ModifiedBy = UserId;
                    company.ModifiedDate = DateTime.Now;
                    _Company.Update(company);
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
            _Company.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}