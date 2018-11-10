using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.ViewModels
{
    public class RaterEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<RaterAssignedEmployee> AssignedEmployees { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public string[] SelectedEmployee { get; set; }
        public int SelectedLineItem { get; set; }
    }
}
