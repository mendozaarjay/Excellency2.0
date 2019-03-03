using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class ApprovalLevelController : Controller
    {
        private IApprovalLevel _Services;

        public ApprovalLevelController(IApprovalLevel approvalLevel)
        {
            _Services = approvalLevel;
        }


        public IActionResult Index()
        {
            var result = _Services.Employees()
                .Select(a => new ApprovalLevelAccountItem
                {
                    Id = a.Id,
                    Name = a.FirstName + " " + a.MiddleName + " " + a.LastName,
                    Company = a.Company.Description,
                    Branch = a.Branch.Description,
                    Department = a.Department.Description,
                    Position = a.Position.Description,
                    IsSet = _Services.IsSet(a.Id)
                }).ToList();
            var model = new ApprovalLevelIndexViewModel
            {
                Employees = result
            };
            return View(model);
        }

        public IActionResult Search(string term)
        {
            var employees = _Services.Employees()
               .Select(a => new ApprovalLevelAccountItem
               {
                   Id = a.Id,
                   Name = a.FirstName + " " + a.MiddleName + " " + a.LastName,
                   Company = a.Company.Description,
                   Branch = a.Branch.Description,
                   Department = a.Department.Description,
                   Position = a.Position.Description,
                   IsSet = _Services.IsSet(a.Id)
               }).ToList().Where(x => x.Name.ToLower().Contains(term));

            return Json(new { result = employees });
        }
        [HttpGet]
        public IActionResult Assign(int id)
        {
            var model = new ApprovalLevelAssignViewModel();
            model.EmployeeId = id;
            model.FirstApprovers = this.Approvers(id);
            model.SecondApprovers = this.Approvers(id);
            model.Name = _Services.GetNameById(id);
            model.EmployeeId = id;
            var _item = _Services.ApprovalAssignmentByEmployee(id);
            if (_item != null)
            {
                var item = new ApprovalLevelItemViewModel
                {
                    Id = _item.Id,
                    EmployeeId = _item.Id,
                    FirstApprovalId = _item.FirstApproval.Id,
                    SecondApprovalId = _item.SecondApproval.Id,
                    IsWithSecondApproval = _item.IsWithSecondApproval ? "on" : "off"
                };
                model.ApprovalLevel = item;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ApprovalLevelAssignViewModel model)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            if(model.ApprovalLevel != null)
            {
                var approvalLevel = new ApprovalLevelAssignment
                {
                    Id = model.ApprovalLevel.Id,
                    Employee = _Services.GetAccountById(model.ApprovalLevel.EmployeeId),
                    FirstApproval = _Services.GetAccountById(model.ApprovalLevel.FirstApprovalId),
                    SecondApproval = model.ApprovalLevel.IsWithSecondApproval == "on" ? _Services.GetAccountById(model.ApprovalLevel.SecondApprovalId) : null,
                    IsWithSecondApproval = model.ApprovalLevel.IsWithSecondApproval == "on" ? true : false
                };
                _Services.Save(approvalLevel,userId);
                return RedirectToAction("Index");
            }
            else
            {
        
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveById(int id)
        {
            _Services.RemoveById(id);
            return RedirectToAction("Index");
        }
        public IEnumerable<SelectListItem> Approvers(int id)
        {
            var result = _Services.Approvers(id)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FirstName + " " + a.MiddleName + " " + a.LastName
                }).ToList();
            return result;
        }
    }
}
