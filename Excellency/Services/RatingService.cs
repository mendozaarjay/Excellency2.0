using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excellency.Services
{
    public class RatingService : IRating
    {
        private EASDbContext _dbContext;

        public RatingService(EASDbContext context)
        {
            _dbContext = context;
        }

        public void Add(Rating rating)
        {
            _dbContext.Add(rating);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return _dbContext.Ratings.Where(a => a.IsDeleted == false);
        }

        public Rating GetRatingById(int id)
        {
            return _dbContext.Ratings.FirstOrDefault(a => a.Id == id);
        }

        public void RemoveById(int id)
        {
            var rating = GetRatingById(id);
            rating.IsDeleted = true;
            _dbContext.Entry(rating).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(Rating rating)
        {
            _dbContext.Entry(rating).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
