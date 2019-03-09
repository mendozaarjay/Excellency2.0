using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Excellency.Controllers
{
    public class AccountRoleController : Controller
    {
        private IAccountRole _AccountRole;

        public AccountRoleController(IAccountRole role)
        {
            _AccountRole = role;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var items = _AccountRole.GetAllRoles()
                .Select(a => new AccountRoleItemViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Type = a.Type
                }).ToList();
            var model = new AccountRoleIndexViewModel
            {
                Roles = items
            };
            return View(model);
        }
        [SessionAuthorized]
        public IActionResult ViewRoleAssignment(int id)
        {
            var header = _AccountRole.GetRoleById(id);
            var accounts = _AccountRole.GetAllAccounts(id)
                .Select(a => new AccountItem
                {
                    Id = a.Id,
                    Name = a.LastName + ", " + a.FirstName
                }).ToList();
            var assigned = _AccountRole.GetAssignmentsByHeaderId(id)
                .Select(a => new AccountItem
                {
                    RecordId = a.Id,
                    Id = a.Account.Id,
                    Name = a.Account.LastName + ", " + a.Account.FirstName,
                }).ToList();
            var model = new AccountRoleAssignmentViewModel
            {
                RoleId = id,
                Description = header.Description,
                Accounts = accounts,
                AssignedAccounts = assigned,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoleAssignment(AccountRoleAssignmentViewModel model)
        {
            if(model.SelectedItems != null)
            {
                var items = new List<int>();
                for(int i = 0; i<= model.SelectedItems.Length - 1; i++)
                {
                    items.Add(int.Parse(model.SelectedItems[i].ToString()));
                }
                _AccountRole.AddAccount(model.RoleId, items);
                return RedirectToAction("ViewRoleAssignment", new { id = model.RoleId });
            }
            else
            {
                return RedirectToAction("ViewRoleAssignment", new { id = model.RoleId });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveAccountItem(int id,int roleid)
        {
            _AccountRole.RemoveAccountAssignment(id);
            return RedirectToAction("ViewRoleAssignment", new { id = roleid });
        }
    }
}
