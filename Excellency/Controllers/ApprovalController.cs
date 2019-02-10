using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class ApprovalController : Controller
    {
        private IApproval _Services;

        public ApprovalController(IApproval approval)
        {
            _Services = approval;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var model = new ApprovalIndexViewModel
            {
                FirstApprovalItems = _Services.GetAllFirstApprovals(UserId),
                SecondApprovalItems = _Services.GetAllSecondApprovals(UserId),
                IsWithFirstApproval = _Services.IsWithFirstApproval(UserId),
                IsWithSecondApproval = _Services.IsWithSecondApproval(UserId),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveFirstApproval(int id,string remarks)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            _Services.ApproveFirstApproval(id, remarks, UserId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveSecondApproval(int id, string remarks)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            _Services.ApproveSecondApproval(id, remarks, UserId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DisapproveFirstApproval(int id, string remarks)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            _Services.DisapproveFirstApproval(id, remarks, UserId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DisapproveSecondApproval(int id, string remarks)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            _Services.DisapproveSecondApproval(id, remarks, UserId);
            return RedirectToAction("Index");
        }
    }
}
