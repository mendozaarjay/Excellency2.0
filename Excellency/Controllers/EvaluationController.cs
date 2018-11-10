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
    public class EvaluationController : Controller
    {
        private IEvaluation _Evaluation;

        public EvaluationController(IEvaluation evaluation)
        {
            _Evaluation = evaluation;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var result = _Evaluation.AssignedEmployees(UserId)
                .Select(a => new EmployeeViewModel
                {
                    Id = a.Id,
                    Name = a.LastName + ", " + a.FirstName + " " + a.MiddleName,
                    EmployeeNo = a.EmployeeNo,
                    Company = a.Company.Description,
                    Branch = a.Branch.Description,
                    Department = a.Department.Description,
                    Position = a.Position.Description,
                    Category = a.Category.Description,
                }).ToList();
            var model = new EmployeeEvaluationIndexViewModel
            {
                Employees = result,
            };
            return View(model);
        }
        public IActionResult Create()
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var result = _Evaluation.AssignedEmployees(UserId)
                .Select(a => new EvaluationEmployeeViewModel
                {
                    Id = a.Id,
                    Name = a.LastName + ", " + a.FirstName + " " + a.MiddleName
                }).ToList();
            var kra = _Evaluation.KeyResultAreas()
                .Select(a => new KeyResultAreaViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Title = a.Title,
                    Weight = a.Weight,
                }).ToList();
            var model = new EvaluationIndexViewModel
            {
                Employees = result,
                KeyResultAreas = kra
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Evaluate(int id,int kra)
        {
            var item = _Evaluation.GetEmployeePerId(id);


            var ksi = _Evaluation.GetKeySuccessIndicators(kra)
                .Select(a => new KeySuccessIndicatorViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                }).ToList();

            var kradetail = _Evaluation.KeyResultAreaPerId(kra);
            var ksitable = _Evaluation.GetKeySuccessIndicators(kra);
            var ratingtableitem = new List<EvaluationRatingTableViewModel>();
            foreach (var ksiitem in ksitable)
            {
                var ratingitems = _Evaluation.GetRatingTableItemsPerId(ksiitem.RatingTable.Id);
                foreach (var i in ratingitems)
                {
                    var _ratingitem = new EvaluationRatingTableViewModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        Weight = i.Weight,
                        KSIId = ksiitem.Id
                    };
                    ratingtableitem.Add(_ratingitem);
                }
            }

            var model = new EvaluationViewModel
            {
                EmployeeId = item.Id,
                Name = item.LastName + ", " + item.FirstName + " " + item.MiddleName,
                Position = item.Position.Description,
                Company = item.Company.Description,
                Branch = item.Branch.Description,
                Department = item.Department.Description,
                SuccessIndicators = ksi,
                RatingTableItems = ratingtableitem,
                KeyResultAreaId = kradetail.Id,
                Title = kradetail.Title,
                Description = kradetail.Description,
                Weight = kradetail.Weight
            };


            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitEvaluation(IFormCollection form)
        {
            var counter = _Evaluation.KeySuccessCounterByKRA(int.Parse(form["kra"].ToString()));
            var Header = new EvaluationHeader();
            var LineItems = new List<EvaluationLine>();
            var UserId = 2; //This is just a test data and would be removed once the application was injected with session.

            Header.CreatedBy = _Evaluation.GetAccountById(UserId).Username;
            Header.CreationDate = DateTime.Now;
            Header.KeyResultArea = _Evaluation.KeyResultAreaPerId(int.Parse(form["kra"].ToString()));
            Header.Rater = _Evaluation.GetAccountById(UserId);
            Header.Ratee = _Evaluation.GetEmployeePerId(int.Parse(form["employeeid"].ToString()));
            Header.Remarks = form["remarks"].ToString();

            var account = _Evaluation.GetAccountById(UserId);
            for (int i = 1; i <= counter; i++)
            {
                var ratingtablename = "ratingtable-" + i.ToString();
                var ratingtableid = int.Parse(form[ratingtablename].ToString());
                var ksiname = "ksi-" + i.ToString();
                var ksiid = int.Parse(form[ksiname].ToString());
                var commentname = "comment-" + i.ToString();
                var comment = form[commentname].ToString();
                var scorename = "score-" + i.ToString();
                var score = decimal.Parse(form[scorename].ToString());
                var item = new EvaluationLine
                {
                    RatingTableItem = _Evaluation.RatingTableItem(ratingtableid),
                    SuccessIndicator = _Evaluation.KeySuccessIndicatorPerId(ksiid),
                    Comment = comment,
                    Score = score,
                };
                LineItems.Add(item);
            }
            _Evaluation.Save(Header, LineItems, account);
            return RedirectToAction("Create");
        }
        public IActionResult EmployeeEvaluation(int id)
        {
            var model = new EmployeeEvaluationViewModel();
            model.EmployeeId = id;
            model.Name = _Evaluation.EmployeeNameById(id);

            var behavioral = _Evaluation.BehavioralFactorPerEmployee(id)
                .Select(a => new EvaluationCategoryViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight.ToString(),
                    IsExist = _Evaluation.IsBehavioralExists(a.Id, id),
                    IsSaved = _Evaluation.IsBehavioralSaved(a.Id, id)
                }).ToList();

            var kra = _Evaluation.KeyResultAreasPerEmployee(id)
                .Select(a => new EvaluationCategoryViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Weight = a.Weight.ToString(),
                    IsExist = _Evaluation.IsKeyResultExists(a.Id,id),
                    IsSaved = _Evaluation.IsKeyResultSaved(a.Id,id)
                }).ToList();
            model.BehavioralCategories = behavioral;
            model.KRACategories = kra;
            return View(model);
        }
        public IActionResult EvaluateBehavioral(int id,int factor)
        {
            var name = _Evaluation.EmployeeNameById(id);
            var behavioral = _Evaluation.GetBehavioralFactorById(factor);
            var items = _Evaluation.GetBehavioralItemsByHeaderId(factor)
                .Select(a => new BehavioralItemViewModel
                {
                    Id = a.Id,
                    Description = a.Description,
                    Weight = a.Weight.ToString(),
                    Score = 0,
                    Comment = string.Empty,
                }).ToList();
            var model = new EmployeeBehavioralEvaluation
            {
                EmployeeId = id,
                BehavioralId = factor,
                Name = name,
                Title = behavioral.Title,
                Description = behavioral.Description,
                Weight = behavioral.Weight.ToString(),
                BehavioralItems = items,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitBehavioral(EmployeeBehavioralEvaluation model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var header = new RatingHeader
                {
                    Type = "behavioral",
                    CreatedBy = _Evaluation.GetAccountById(UserId).Id.ToString(),
                    CreationDate = DateTime.Now,
                    Rater = _Evaluation.GetAccountById(UserId),
                    Ratee = _Evaluation.GetEmployeePerId(model.EmployeeId),
                    Status = _Evaluation.GetStatusPerId(TransactionStatus.Save.ToInt()),
                };
                var items = new List<RatingBehavioralFactor>();
                foreach(var item in model.BehavioralItems)
                {
                    var factoritem = new RatingBehavioralFactor
                    {
                        BehavioralFactor = _Evaluation.GetBehavioralFactorById(model.BehavioralId),
                        BehavioralFactorItem = _Evaluation.GetBehavioralFactorItemById(item.Id),
                        Score = item.Score,
                        Comment = item.Comment,
                    };
                    items.Add(factoritem);
                }
                _Evaluation.SaveBehavioralEvaluation(header, items);
                return RedirectToAction("EmployeeEvaluation", new { id = model.EmployeeId });
            }
            else
            {
                return RedirectToAction("EvaluateBehavioral",new { id = model.EmployeeId, factor = model.BehavioralId });
            }
        }
        public IActionResult EvaluateKeyResultArea(int id,int kraid)
        {
            var name = _Evaluation.EmployeeNameById(id);
            var kra = _Evaluation.KeyResultAreaPerId(kraid);
            var ksi = _Evaluation.GetKeySuccessIndicators(kraid)
                .Select(a => new EvaluationSuccessIndicatorItem
                {
                    Id = a.Id,
                    Description = a.Description,
                    Title = a.Title,
                    Weight = a.Weight,
                    Comment = string.Empty,
                    Score = 0,
                    RatingTableItems = _Evaluation.GetRatingTableItemsPerId(a.RatingTable.Id)
                    .Select(b => new EvaluationRatingTableItem
                    {
                        Id = b.Id,
                        Description = b.Description,
                        Weight = b.Weight.ToString(),
                    }).ToList(),

                }).ToList();
            var model = new EmployeeSuccessAreaEvaluation
            {
                EmployeeId = id,
                Name = name,
                KeyResultAreaId = kraid,
                Title = kra.Title,
                Description = kra.Description,
                Weight = kra.Weight.ToString(),
                EvaluationSuccessIndicators = ksi
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitKeyResultArea(EmployeeSuccessAreaEvaluation model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));

            if (ModelState.IsValid)
            {
                var header = new RatingHeader
                {
                    Type = "kra",
                    CreatedBy = _Evaluation.GetAccountById(UserId).Id.ToString(),
                    CreationDate = DateTime.Now,
                    Rater = _Evaluation.GetAccountById(UserId),
                    Ratee = _Evaluation.GetEmployeePerId(model.EmployeeId),
                    Status = _Evaluation.GetStatusPerId(TransactionStatus.Save.ToInt()),
                };
                var items = new List<RatingKeySuccessArea>();
                foreach(var item in model.EvaluationSuccessIndicators)
                {
                    var rating = new RatingKeySuccessArea
                    {
                        KeyResultArea = _Evaluation.GetKeyResultAreaById(model.KeyResultAreaId),
                        KeySuccessIndicator = _Evaluation.GetSuccessIndicatorById(item.Id),
                        Score = item.Score,
                        Comment = item.Comment,
                    };
                    items.Add(rating);
                }
                _Evaluation.SaveKeyResultAreaEvaluation(header, items);
                return RedirectToAction("EmployeeEvaluation", new { id = model.EmployeeId });
            }
            else
            {
                return RedirectToAction("EvaluateBehavioral", new { id = model.EmployeeId, kraid = model.KeyResultAreaId });
            }
        }

        public IActionResult EditBehavioral(int empid,int id)
        {
            var name = _Evaluation.EmployeeNameById(empid);
            var behavioral = _Evaluation.GetBehavioralFactorById(id);
            var items = _Evaluation.GetRatingBehavioralByHeaderId(id,empid)
                .Select(a => new EditBehavioralItemViewModel
                {
                    RecordId = a.Id,
                    Id = a.BehavioralFactorItem.Id,
                    Description = a.BehavioralFactorItem.Description,
                    Weight = a.BehavioralFactorItem.Weight.ToString(),
                    Score = a.Score,
                    Comment = a.Comment,
                }).ToList();
            var model = new EditBehavioralEvaluation
            {
                HeaderId = _Evaluation.GetRatingHeaderBehavioral(empid).Id,
                FactorId = id,
                EmployeeId = empid,
                BehavioralId = id,
                Name = name,
                Title = behavioral.Title,
                Description = behavioral.Description,
                Weight = behavioral.Weight.ToString(),
                BehavioralItems = items,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitEditBehavioral(EditBehavioralEvaluation model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var items = new List<RatingBehavioralFactor>();
            foreach (var item in model.BehavioralItems)
            {
                var factoritem = new RatingBehavioralFactor
                {
                    Id = item.RecordId,
                    BehavioralFactor = _Evaluation.GetBehavioralFactorById(model.BehavioralId),
                    BehavioralFactorItem = _Evaluation.GetBehavioralFactorItemById(item.Id),
                    Score = item.Score,
                    Comment = item.Comment,
                };
                items.Add(factoritem);
            }
            _Evaluation.UpdateRatingBehavioral(model.HeaderId, UserId, items);
            return RedirectToAction("EmployeeEvaluation", new { id = model.EmployeeId });
        }
        public IActionResult EditKeyResult(int empid,int id)
        {
            var name = _Evaluation.EmployeeNameById(empid);
            var kra = _Evaluation.KeyResultAreaPerId(id);
            var ksi = _Evaluation.GetRatingKeyResultByHeaderId(id, empid)
                .Select(a => new EditSuccessIndicatorItem
                {
                    RecordId = a.Id,
                    Id = a.KeySuccessIndicator.Id,
                    Description = a.KeySuccessIndicator.Description,
                    Title = a.KeySuccessIndicator.Title,
                    Weight = a.KeySuccessIndicator.Weight,
                    Comment = a.Comment,
                    Score = a.Score,
                    RatingTableItems = _Evaluation.GetRatingTableItemsPerId(a.KeySuccessIndicator.RatingTable.Id)
                    .Select(b => new EvaluationRatingTableItem
                    {
                        Id = b.Id,
                        Description = b.Description,
                        Weight = b.Weight.ToString(),
                    }).ToList(),

                }).ToList();
            var model = new EditKeyResultAreaEvaluation
            {
                HeaderId = _Evaluation.GetRatingHeaderKeyResult(empid).Id,
                EmployeeId = empid,
                Name = name,
                KeyResultAreaId = id,
                Title = kra.Title,
                Description = kra.Description,
                Weight = kra.Weight.ToString(),
                EvaluationSuccessIndicators = ksi
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitEditKeyResultArea(EditKeyResultAreaEvaluation model)
        {
            var UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            var items = new List<RatingKeySuccessArea>();
            foreach (var item in model.EvaluationSuccessIndicators)
            {
                var rating = new RatingKeySuccessArea
                {
                    Id = item.RecordId,
                    KeyResultArea = _Evaluation.GetKeyResultAreaById(model.KeyResultAreaId),
                    KeySuccessIndicator = _Evaluation.GetSuccessIndicatorById(item.Id),
                    Score = item.Score,
                    Comment = item.Comment,
                };
                items.Add(rating);
            }
            _Evaluation.UpdateRatingKeyResultArea(model.HeaderId, UserId, items);
            return RedirectToAction("EmployeeEvaluation", new { id = model.EmployeeId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostBehavioralItem(int empid,int id)
        {
            _Evaluation.PostBehavioral(id, empid);
            return RedirectToAction("EmployeeEvaluation", new { id = empid });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostKeyResultItem(int empid,int id)
        {
            _Evaluation.PostKeyResultArea(id, empid);
            return RedirectToAction("EmployeeEvaluation", new { id = empid });
        }
    }
}
