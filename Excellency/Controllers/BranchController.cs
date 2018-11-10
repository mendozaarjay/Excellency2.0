using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Excellency.Models;
using Microsoft.AspNetCore.Http;

namespace Excellency.Controllers
{
    public class BranchController : Controller
    {
        private IBranch _Branch;

        public BranchController(IBranch branch)
        {
            _Branch = branch;
        }

        public IActionResult Index()
        {
            var result = _Branch.Branches().Select
                (
                a => new BranchViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Company = a.Company.Description,
                    CompanyId = a.Company.Id.ToString()
                }
                ).ToList();
            var model = new BranchIndexViewModel
            {
                Branches = result,
                Companies = this.CompanyList()
            };
            return View(model);
        }

        private IEnumerable<SelectListItem> CompanyList()
        {
            var result = _Branch.Companies().Select
                (
                a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Description
                }
                ).ToList();
            return result;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(BranchIndexViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                var branch = new Branch
                {
                    Id = model.Id,
                    Description = model.Description,
                    Company = _Branch.GetCompanyById(int.Parse(model.CompanyId))
                };
                if (model.Id.ToString().Equals("0"))
                {
                    branch.CreatedBy = UserId;
                    branch.CreationDate = DateTime.Now;
                    _Branch.Add(branch);
                }
                else
                {
                    branch.ModifiedBy = UserId;
                    branch.ModifiedDate = DateTime.Now;
                    _Branch.Update(branch);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _Branch.RemoveById(id);
            return RedirectToAction("Index");
        }
    }
}