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
        private IEmployeeAssignment _EmployeeAssignment;

        public EmployeeAssignmentController(IEmployeeAssignment employeeAssignment)
        {
            _EmployeeAssignment = employeeAssignment;
        }
        [SessionAuthorized]
        public IActionResult Index()
        {
            var result = _EmployeeAssignment.Employees().Select(a => new EmployeeViewModel
            {
                Id = a.Id,
                EmployeeNo = a.EmployeeNo,
                Name = a.LastName + ", " + a.FirstName + " " + a.MiddleName,
                Company = a.Company.Description,
                Branch = a.Branch.Description,
                Category = a.Category.Description,
                Department = a.Department.Description,
                Position = a.Position.Description,
            }).ToList();
            var model = new EmployeeAssignmentViewModel
            {
                Employees = result,
            };
            return View(model);
        }
        [SessionAuthorized]
        public IActionResult Assignment(int id)
        {
            var _employee = _EmployeeAssignment.EmployeeById(id);
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
            var header = _EmployeeAssignment.EmployeeAssignmentByEmployeeId(id);
            if (header != null)
            {
                var kra = _EmployeeAssignment.EmployeeKRAAssignmentByHeaderId(header.Id)
                .Select(a => new AssignedKeyResultViewModel
                {
                    Id = a.Id,
                    Title = a.KeyResultArea.Title,
                    Description = a.KeyResultArea.Description,
                    Weight = a.KeyResultArea.Weight
                }).ToList();
                kralist = kra;
                var behavioral = _EmployeeAssignment.EmployeeBehavioralAssignmentByHeaderId(header.Id)
                    .Select(a => new AssignedBehavioralViewModel
                    {
                        Id = a.Id,
                        Title = a.BehavioralFactor.Title,
                        Description = a.BehavioralFactor.Description,
                        Weight = a.BehavioralFactor.Weight
                    }).ToList();
                behaviorallist = behavioral;
            }
            var kraitems = _EmployeeAssignment.KeyResultAreas()
                .Select(a => new KeyResultAreaViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                }).ToList();
            var bfitems = _EmployeeAssignment.BehavioralFactors()
                .Select(a => new BehavioralFactorViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                }).ToList();
            var model = new EmployeeAssignViewModel
            {
                Employee = employee,
                AssignedBehavioralItems = behaviorallist,
                AssignedKeyResultsItems = kralist,
                BehavioralFactors = bfitems,
                KeyResultAreas = kraitems,
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
                _EmployeeAssignment.SaveBehavioralItems(model.Employee.Id, items);
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
                _EmployeeAssignment.SaveKRAItems(model.Employee.Id, items);
            }
            return RedirectToAction("Assignment", new { id = model.Employee.Id });
        }
        [HttpPost]
        public IActionResult RemoveKRAById(int id,int employeeid)
        {
            _EmployeeAssignment.RemoveKRAById(id);
            return RedirectToAction("Assignment", new { id = employeeid });
        }
        [HttpPost]
        public IActionResult RemoveBehavioralById(int id, int employeeid)
        {
            _EmployeeAssignment.RemoveBehavioralPerId(id);
            return RedirectToAction("Assignment", new { id = employeeid });
        }
    }
}
