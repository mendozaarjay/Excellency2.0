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
    public class KeyResultAreaService : IKeyResultArea
    {
        private EASDbContext _dbContext;

        public KeyResultAreaService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Key Result Area
        public void Add(KeyResultArea keyResultArea)
        {
            _dbContext.Add(keyResultArea);
            _dbContext.SaveChanges();
        }

        public IEnumerable<KeyResultArea> GetAllKeyResultArea()
        {
            return _dbContext.KeyResultAreas.Where(a => a.IsDeleted == false);
        }

        public KeyResultArea GetKeyResultAreaById(int id)
        {
            return _dbContext.KeyResultAreas.FirstOrDefault(a => a.Id == id);
        }

        public void RemoveById(int id)
        {
            var kra = GetKeyResultAreaById(id);
            kra.IsDeleted = true;
            _dbContext.Entry(kra).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }


        public void Update(KeyResultArea keyResultArea)
        {
            _dbContext.Entry(keyResultArea).State = EntityState.Modified;
            _dbContext.SaveChanges();
        } 
        #endregion

        #region Key Success Indicator
        public KeySuccessIndicator GetKeySuccessIndicatorById(int id)
        {
            return _dbContext.KeySuccessIndicators.FirstOrDefault(a => a.Id == id);
        }
        public void RemoveSuccessIndicatorById(int id)
        {
            var ksi = GetKeySuccessIndicatorById(id);
            ksi.IsDeleted = true;
            _dbContext.Entry(ksi).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void SaveKeySuccessIndicator(KeySuccessIndicator successIndicator,string UserId)
        {
            if(successIndicator.Id == 0)
            {
                successIndicator.CreatedBy = UserId;
                successIndicator.CreationDate = DateTime.Now;
                _dbContext.Add(successIndicator);
            }
            else
            {
                successIndicator.ModifiedBy = UserId;
                successIndicator.ModifiedDate = DateTime.Now;
                _dbContext.Entry(successIndicator).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<KeySuccessIndicator> SuccessIndicatorPerKRA(int KRAId)
        {
            return _dbContext.KeySuccessIndicators.Include(a => a.KeyResultArea)
                .Include(a => a.RatingTable)
                .Where(a => a.IsDeleted == false && a.KeyResultArea.Id == KRAId);
        }

        #endregion

        #region Category
        public void SaveCategory(Category category,string UserId)
        {
            if(category.Id == 0)
            {
                category.CreatedBy = UserId;
                category.CreationDate = DateTime.Now;
                _dbContext.Add(category);
            }
            else
            {
                category.ModifiedBy = UserId;
                category.ModifiedDate = DateTime.Now;
                _dbContext.Entry(category).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public void RemoveCategoryPerId(int id)
        {
            var category = GetCategoryById(id);
            category.IsDeleted = true;
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Category> CategoriesPerKSIId(int KSIId)
        {
            return _dbContext.Categories.Include(a => a.SuccessIndicator)
                    .Where(a => a.IsDeleted == false && a.SuccessIndicator.Id == KSIId);
        }

        public IEnumerable<RatingTable> GetRatingTables()
        {
            return _dbContext.RatingTables.Where(a => a.IsDeleted == false);
        }

        public RatingTable GetRatingTablePerId(int id)
        {
            return _dbContext.RatingTables.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<RatingTableItem> RatingTableItems(int RatingTableId)
        {
            return _dbContext.RatingTableItems.Include(a => a.RatingTable)
                .Where(a => a.IsDeleted == false && a.RatingTable.Id == RatingTableId);
        }
        #endregion

    }
}
