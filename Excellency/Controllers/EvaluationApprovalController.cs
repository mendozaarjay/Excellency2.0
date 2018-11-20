using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Excellency.Controllers
{
    public class EvaluationApprovalController : Controller
    {
        private IEvaluationApproval _EvaluationApproval;

        public EvaluationApprovalController(IEvaluationApproval evaluation)
        {
            _EvaluationApproval = evaluation;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var items = _EvaluationApproval.GetAssignedEvaluation(UserId)
                .Select(a => new EvaluationApprovaItemViewModel
                {
                    Id = a.Id,
                    EmployeeName = a.Ratee.LastName + ", " + a.Ratee.FirstName,
                    RaterName = a.Rater.LastName + ", " + a.Rater.FirstName,
                    CreationDate = a.CreationDate.ToString("MM/dd/yyy hh:mm tt"),
                    Status = a.Status.Description,
                    Type = a.Type,
                    TypeDescription = a.Type == "kra" ? "Key Result Area" : "Behavioral Factor",
                    
                }).ToList();
            var model = new EvaluationApprovalIndexViewModel
            {
                Evaluations = items,
            };
            return View(model);
        }
        [SessionAuthorized]
        public IActionResult ViewBehavioral(int headerid)
        {
            var header = _EvaluationApproval.GetRatingHeaderById(headerid);

            var result = _EvaluationApproval.GetRatingBehavioralFactorsById(headerid)
                .Select(a => new ApprovalBehavioralItemViewModel
                {
                    Id = a.Id,
                    Description = a.BehavioralFactorItem.Description,
                    Comment = a.Comment,
                    Score = a.Score.ToString(),
                    Weight = a.BehavioralFactorItem.Weight.ToString(),
                }).ToList();
            var factor = _EvaluationApproval.GetBehavioralFactorByHeaderId(headerid);
            var model = new ApprovalBehavioralViewModel
            {
                Id = header.Id,
                EmployeeName = header.Ratee.LastName + ", " + header.Ratee.FirstName,
                RaterName = header.Rater.LastName + ", " + header.Rater.FirstName,
                BehavioralFactor = factor.Title + "-" + factor.Description,
                Weight = factor.Weight.ToString(),
                TotalScore = _EvaluationApproval.GetBehavioralTotalScore(headerid).ToString(),
                Items = result,
                Remarks = string.Empty,
            };
            return View(model);
        }
        [SessionAuthorized]
        public IActionResult ViewKeyResultArea(int headerid)
        {
            var header = _EvaluationApproval.GetRatingHeaderById(headerid);
            var result = _EvaluationApproval.GetRatingKeySuccessAreasById(headerid)
                .Select(a => new ApprovalKeyResultItemViewModel
                {
                    Id = a.Id,
                    SuccessIndicator = a.KeySuccessIndicator.RatingTable.Description,
                    Rating = _EvaluationApproval.SuccessRating(headerid,a.Id),
                    Comment = a.Comment,
                    Score = _EvaluationApproval.SuccessScore(headerid,a.Id).ToString(),
                    Weight = a.KeySuccessIndicator.Weight.ToString(),

                }).ToList();

            var kra = _EvaluationApproval.GetKeyResultAreaByHeaderId(headerid);

            var model = new ApprovalKeyResultAreaViewModel
            {
                Id = header.Id,
                EmployeeName = header.Ratee.LastName + ", " + header.Ratee.FirstName,
                RaterName = header.Rater.LastName + ", " + header.Rater.FirstName,
                KeyResultArea = kra.Title + "-" + kra.Description,
                Weight = kra.Weight.ToString(),
                Remarks = string.Empty,
                TotalScore = _EvaluationApproval.GetKeyResultAreaTotalScore(headerid).ToString(),
                Items = result,
            };
            return View(model);
        }
        [SessionAuthorized]
        public IActionResult ViewEvaluation(int id,string type)
        {
            if (type == "kra")
                return RedirectToAction("ViewKeyResultArea", new { headerid = id });
            else
                return RedirectToAction("ViewBehavioral", new { headerid = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostBehavioral(ApprovalBehavioralViewModel model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (model.Action == "Approve")
            {

                _EvaluationApproval.Approved(model.Id, UserId, model.Remarks);
            }
            else
            {
                _EvaluationApproval.Disapproved(model.Id, UserId, model.Remarks);
            }
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostKeyResultArea(ApprovalKeyResultAreaViewModel model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if(model.Action == "Approve")
            {
                _EvaluationApproval.Approved(model.Id, UserId, model.Remarks);
            }
            else
            {
                _EvaluationApproval.Disapproved(model.Id, UserId, model.Remarks);
            }

            return RedirectToAction("Index");
        }
    }
}
