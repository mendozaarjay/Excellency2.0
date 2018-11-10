using Excellency.Interfaces;
using Excellency.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Models;

namespace Excellency.Services
{
    public class ModuleService : IModule
    {
        private EASDbContext _dbContext;

        public ModuleService(EASDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        public void Add(Module Module)
        {
            _dbContext.Modules.Add(Module);
            _dbContext.SaveChanges();
        }

        public Module GetModuleById(int id)
        {
            return _dbContext.Modules.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Module> Modules()
        {
            return _dbContext.Modules.Where(a => a.IsDeleted == false);
        }

        public void RemoveById(int Id)
        {
            var Module = _dbContext.Modules.FirstOrDefault(a => a.Id == Id);
            Module.IsDeleted = true;
            _dbContext.Entry(Module).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(Module Module)
        {
            _dbContext.Entry(Module).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
