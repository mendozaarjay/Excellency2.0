using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;



namespace Excellency.Controllers
{
    public class ApproverAssignmentController : Controller
    {
        private IApproverAssignment _Approver;

        public ApproverAssignmentController(IApproverAssignment approver)
        {
            _Approver = approver;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var result = _Approver.GetAllAccounts()
                .Select(a => new AccountViewModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Company = a.Company.Description,
                    Branch = a.Branch.Description,
                    Department = a.Department.Description,
                    Position = a.Position.Description
                }).ToList();
            var model = new ApproverAssignmentIndexViewModel
            {
                Approvers = result
            };
            return View(model);
        }
        public IActionResult Assignment(int id)
        {
            var name = _Approver.GetNameById(id);
            var assigned = _Approver.GetAssignedAccountsById(id)
                .Select(a => new AssignedAccountViewModel
                {
                    Id = a.Id,
                    Name = _Approver.GetNameById(a.User.Id),
                    Company = a.User.Company.Description,
                    Branch = a.User.Branch.Description,
                    Department = a.User.Department.Description,
                    Position = a.User.Position.Description,
                }).ToList();
            var accounts = _Approver.GetAccountsById(id)
                .Select(a => new AccountItemViewModel
                {
                    Id = a.Id,
                    Name = a.LastName + ", " + a.FirstName
                });
            var model = new ApproverAssignmentViewModel
            {
                ApproverId = id,
                ApproverName = name,
                AssignedAccounts = assigned,
                Accounts = accounts
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Assign(ApproverAssignmentViewModel model)
        {
            var UserId = HttpContext.Session.GetString("UserId");
            if(model.SelectedItems != null)
            {
                var items = new List<int>();
                for(int i = 0;i<= model.SelectedItems.Length - 1; i++)
                {
                    var item = int.Parse(model.SelectedItems[i].ToString());
                    items.Add(item);
                }
                _Approver.AssignAccounts(items, model.ApproverId, int.Parse(UserId));
            }

            return RedirectToAction("Assignment", new { id = model.ApproverId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int itemid,int approverid)
        {
            _Approver.RemoveById(itemid);
            return RedirectToAction("Assignment", new { id = approverid });
        }
    }
}
