using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Excellency.Controllers
{
    public class EmployeeAssignmentController : Controller
    {
        private IEmployeeAssignment _Services;

        public EmployeeAssignmentController(IEmployeeAssignment employeeAssignment)
        {
            _Services = employeeAssignment;
        }
        [SessionAuthorized]
        public IActionResult Index(int? page)
        {
            int currentpage;
            if (page == null)
                currentpage = 1;
            else
                currentpage = (int)page;

            var maxcount = currentpage < 5 ? 5 : currentpage + 2;
            var mincount = currentpage < 5 ? 1 : currentpage - 2;

            var maxpage = (_Services.Employees().Count() / 10) + 1;

            maxcount = currentpage <= maxpage ? maxcount : maxpage;

            var result = _Services.EmployeeItems(currentpage);
            var aes = _Services.ActiveSeason();
            var season = new EvaluationSeasonItem();
            if (aes != null)
            {
                season.Id = aes.Id;
                season.Title = aes.Title;
                season.Remarks = aes.Remarks;
                season.StartDate = aes.StartDate;
                season.EndDate = aes.EndDate;
            };
            var model = new EmployeeAssignmentViewModel
            {
                Employees = result,
                IsWithActiveSeason = _Services.IsWithActiveSeason(),
                ActiveSeason = season,
            };
            ViewBag.MaxCount = maxcount;
            ViewBag.MinCount = mincount;
            ViewBag.CurrentPage = currentpage;
            ViewBag.MaxPage = maxpage;
            return View(model);
        }
        public IActionResult Search(string term)
        {
            var employees = _Services.Employees()
                .Select(a => new EmployeeViewModel
                {
                    Id = a.Id,
                    EmployeeNo = a.EmployeeNo,
                    Name = a.LastName + ", " + a.FirstName + " " + a.MiddleName,
                    Company = a.Company.Description,
                    Branch = a.Branch.Description,
                    Category = a.Category.Description,
                    Department = a.Department.Description,
                    Position = a.Position.Description,
                }).ToList().Where(x => x.Name.ToLower().Contains(term) || term.Length == 0 );
            return Json(new { result = employees });
        }
        [SessionAuthorized]
        public IActionResult Assignment(int id)
        {
            var _employee = _Services.EmployeeById(id);
            var employee = new EmployeeViewModel
            {
                Id = _employee.Id,
                EmployeeNo = _employee.EmployeeNo,
                Name = _employee.LastName + ", " + _employee.FirstName + " " + _employee.MiddleName,
                Company = _employee.Company.Description,
                Branch = _employee.Branch.Description,
                Category = _employee.Category.Description,
                Department = _employee.Department.Description,
                Position = _employee.Position.Description,
            };

            var kralist = new List<AssignedKeyResultViewModel>();
            var behaviorallist = new List<AssignedBehavioralViewModel>();
            var header = _Services.EmployeeAssignmentByEmployeeId(id);
            if (header != null)
            {
                var kra = _Services.EmployeeKRAAssignmentByHeaderId(header.Id)
                .Select(a => new AssignedKeyResultViewModel
                {
                    Id = a.Id,
                    Title = a.KeyResultArea.Title,
                    Description = a.KeyResultArea.Description,
                    Weight = a.KeyResultArea.Weight
                }).ToList();
                kralist = kra;
                var behavioral = _Services.EmployeeBehavioralAssignmentByHeaderId(header.Id)
                    .Select(a => new AssignedBehavioralViewModel
                    {
                        Id = a.Id,
                        Title = a.BehavioralFactor.Title,
                        Description = a.BehavioralFactor.Description,
                        Weight = a.BehavioralFactor.Weight
                    }).ToList();
                behaviorallist = behavioral;
            }
            var kraitems = _Services.GetAvailableKRA(id)
                .Select(a => new KeyResultAreaViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                }).ToList();
            var bfitems = _Services.GetAvailableBehavioral(id)
                .Select(a => new BehavioralFactorViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                }).ToList();

            var aes = _Services.ActiveSeason();
            var season = new EvaluationSeasonItem();
            if (aes != null)
            {
                season.Id = aes.Id;
                season.Title = aes.Title;
                season.Remarks = aes.Remarks;
                season.StartDate = aes.StartDate;
                season.EndDate = aes.EndDate;
            };


            var model = new EmployeeAssignViewModel
            {
                Employee = employee,
                AssignedBehavioralItems = behaviorallist,
                AssignedKeyResultsItems = kralist,
                BehavioralFactors = bfitems,
                KeyResultAreas = kraitems,
                IsWithActiveSeason = _Services.IsWithActiveSeason(),
                ActiveSeason = season,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveBehavioralFactor(EmployeeAssignViewModel model)
        {
            if(model.SelectedBehavioralFactors.Length > 0)
            {
                var items = new List<int>();
                for(int i = 0;i<= model.SelectedBehavioralFactors.Length - 1; i++)
                {
                    items.Add(int.Parse(model.SelectedBehavioralFactors[i].ToString()));
                }
                _Services.SaveBehavioralItems(model.Employee.Id, items);
            }
            return RedirectToAction("Assignment", new { id = model.Employee.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveKRAItems(EmployeeAssignViewModel model)
        {
            if(model.SelectedKeyResultAreas.Length > 0)
            {
                var items = new List<int>();
                for (int i = 0; i <= model.SelectedKeyResultAreas.Length - 1; i++)
                {
                    items.Add(int.Parse(model.SelectedKeyResultAreas[i].ToString()));
                }
                _Services.SaveKRAItems(model.Employee.Id, items);
            }
            return RedirectToAction("Assignment", new { id = model.Employee.Id });
        }
        [HttpPost]
        public IActionResult RemoveKRAById(int id,int employeeid)
        {
            _Services.RemoveKRAById(id);
            return RedirectToAction("Assignment", new { id = employeeid });
        }
        [HttpPost]
        public IActionResult RemoveBehavioralById(int id, int employeeid)
        {
            _Services.RemoveBehavioralPerId(id);
            return RedirectToAction("Assignment", new { id = employeeid });
        }
    }
}
