using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellency.Services
{
    public class DepartmentService : IDepartment
    {
        private EASDbContext _dbContext;

        public DepartmentService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Department Department)
        {
            _dbContext.Departments.Add(Department);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Department> Departments()
        {
            return _dbContext.Departments.Where(a => a.IsDeleted == false);
        }

        public Department GetDepartmentById(int id)
        {
            return _dbContext.Departments.FirstOrDefault(a => a.Id == id);
        }

        public void RemoveById(int Id)
        {
            var department = _dbContext.Departments.FirstOrDefault(a => a.Id == Id);
            department.IsDeleted = true;
            _dbContext.Entry(department).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(Department Department)
        {
            _dbContext.Entry(Department).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
