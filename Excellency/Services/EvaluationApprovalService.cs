using System;
using System.Collections.Generic;
using System.Linq;
using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Excellency.Services
{
    public class EvaluationApprovalService : IEvaluationApproval
    {
        private EASDbContext _dbContext;

        public EvaluationApprovalService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Approved(int HeaderId, int UserId, string Remarks)
        {
            var header = _dbContext.RatingHeader.FirstOrDefault(a => a.Id == HeaderId);
            var approver = _dbContext.Accounts.FirstOrDefault(a => a.Id == UserId);
            var status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.Approved.ToInt());

            //header.Approver = approver;
            //header.ApprovedDate = DateTime.Now;
            header.Remarks = Remarks;
            header.Status = status;

            _dbContext.Entry(header).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Disapproved(int HeaderId, int UserId, string Remarks)
        {
            var header = _dbContext.RatingHeader.FirstOrDefault(a => a.Id == HeaderId);
            var approver = _dbContext.Accounts.FirstOrDefault(a => a.Id == UserId);
            var status = _dbContext.Statuses.FirstOrDefault(a => a.Id == TransactionStatus.Disapproved.ToInt());

            //header.Approver = approver;
            //header.ApprovedDate = DateTime.Now;
            header.Remarks = Remarks;
            header.Status = status;

            _dbContext.Entry(header).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<RatingHeader> GetAssignedEvaluation(int UserId)
        {
            var AssignedUser = _dbContext.ApproverAssignments
                .Include(a => a.User)
                .Include(a => a.Approver)
                .Where(a => a.Approver.Id == UserId && a.IsDeleted == false)
                .Select(a => a.User.Id);

            var ratings = new List<RatingHeader>();
            foreach(var id in AssignedUser)
            {
                var items = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Ratee)
                .Include(a => a.Status)
 
                .Where(a => a.Rater.Id == id && a.IsExpired == false);

                foreach(var item in items)
                {
                    ratings.Add(item);
                }
            }

            return ratings;         
        }

        public IEnumerable<RatingBehavioralFactor> GetRatingBehavioralFactorsById(int headerid)
        {
            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.BehavioralFactor)
                .Include(a => a.BehavioralFactorItem)
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Ratee)
                .Where(a => a.RatingHeader.Id == headerid);
            return result;
        }
        public IEnumerable<RatingKeySuccessArea> GetRatingKeySuccessAreasById(int headerid)
        {
            var result = _dbContext.RatingKeySuccessAreas
                .Include(a => a.KeyResultArea)
                .Include(a => a.KeySuccessIndicator)
                .Include(a => a.KeySuccessIndicator.RatingTable)
                .Include(a => a.RatingHeader)
                .Include(a => a.RatingHeader.Ratee)
                .Where(a => a.RatingHeader.Id == headerid);
            return result;
        }
        public RatingHeader GetRatingHeaderById(int id)
        {
            var header = _dbContext.RatingHeader
                .Include(a => a.Rater)
                .Include(a => a.Ratee)
                .Include(a => a.Status)
                .FirstOrDefault(a => a.Id == id);
            return header;
        }

        public KeyResultArea GetKeyResultAreaByHeaderId(int headerid)
        {
            var result = _dbContext.RatingKeySuccessAreas
                .Include(a => a.RatingHeader)
                .Include(a => a.KeyResultArea)
                .FirstOrDefault(a => a.RatingHeader.Id == headerid);
            var item = new KeyResultArea
            {
                Id = result.KeyResultArea.Id,
                Title = result.KeyResultArea.Title,
                Description = result.KeyResultArea.Description,
                Weight = result.KeyResultArea.Weight,
            };
            return item;
        }

        public BehavioralFactor GetBehavioralFactorByHeaderId(int headerid)
        {
            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.BehavioralFactor)
                .Include(a => a.RatingHeader)
                .FirstOrDefault(a => a.RatingHeader.Id == headerid);
            var item = new BehavioralFactor
            {
                Id = result.BehavioralFactor.Id,
                Title = result.BehavioralFactor.Title,
                Description = result.BehavioralFactor.Description,
                Weight = result.BehavioralFactor.Weight
            };
            return item;
        }

        public int GetBehavioralTotalScore(int headerid)
        {
            var result = _dbContext.RatingBehavioralFactors
                .Include(a => a.BehavioralFactor)
                .Include(a => a.RatingHeader)
                .Where(a => a.RatingHeader.Id == headerid)
                .Sum(a => a.Score);
            return result;
        }

        public int GetKeyResultAreaTotalScore(int headerid)
        {
            var result = _dbContext.RatingKeySuccessAreas
                .Include(a => a.KeyResultArea)
                .Include(a => a.RatingHeader)
                .Include(a => a.KeySuccessIndicator)
                .Where(a => a.RatingHeader.Id == headerid)
                .Select(a => a.Score);
            var item = _dbContext.RatingTableItems.Where(a => result.Contains(a.Id))
                .Sum(a => a.Weight);

            return item;
        }

        public string SuccessRating(int headerid, int id)
        {
            var item = GetRatingKeySuccessAreasById(headerid).FirstOrDefault(a => a.Id == id).Score;
            var result = _dbContext.RatingTableItems.FirstOrDefault(a => a.Id == item).Description;
            return result;
        }

        public int SuccessScore(int headerid, int id)
        {
            var item = GetRatingKeySuccessAreasById(headerid).FirstOrDefault(a => a.Id == id).Score;
            var result = _dbContext.RatingTableItems.FirstOrDefault(a => a.Id == item).Weight;
            return result;
        }
    }
}
