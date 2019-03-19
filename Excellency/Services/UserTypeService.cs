using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class UserTypeService : IUserType
    {
        private EASDbContext _dbContext;

        public UserTypeService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RemoveById(int id)
        {
            var item = _dbContext.UserTypes.FirstOrDefault(a => a.Id == id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(UserType item, int userId)
        {
            if(item.Id == 0)
            {
                item.IsDeleted = false;
                item.CreatedBy = userId.ToString();
                item.CreationDate = DateTime.Now;
                _dbContext.Add(item);
            }
            else
            {
                var entry = _dbContext.UserTypes.FirstOrDefault(a => a.Id == item.Id);
                entry.Description = item.Description;
                entry.ModifiedBy = userId.ToString();
                entry.ModifiedDate = DateTime.Now;
                _dbContext.Entry(entry).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public UserType UserTypeById(int id)
        {
            return _dbContext.UserTypes.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<UserType> UserTypes()
        {
            return _dbContext.UserTypes.Where(a => a.IsDeleted == false);
        }
    }
}
