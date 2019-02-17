using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Excellency.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace Excellency.Services
{
    public class PeerAssignmentService : IPeerAssignment
    {
        private EASDbContext _dbContext;

        public string UserConnectionString { get; }

        public PeerAssignmentService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
            UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }
        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Account> GetAllAccountsByRaterId(int id)
        {
            List<Account> items = new List<Account>();
            var sql = string.Format("SELECT * FROM  [dbo].[fnPeerAssignmentLookUp]({0}) [x]", id.ToString());
            DataTable dt = SCObjects.LoadDataTable(sql, UserConnectionString);
            if(dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    var item = new Account
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        FirstName = dr["Firstname"].ToString(),
                        MiddleName = dr["Middlename"].ToString(),
                        LastName = dr["Lastname"].ToString()
                    };
                    items.Add(item);
                }
            }
            return items;
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts
                .Include(a => a.Branch)
                .Include(a => a.Department)
                .Include(a => a.Company)
                .Include(a => a.Position)
                .Where(a => a.IsDeleted == false && a.IsDeactivated == false && a.IsExpired == false);
        }
        public IEnumerable<PeerAssignment> GetAssignmentsPerRater(int id)
        {
            return _dbContext.PeerAssignment
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .Where(a => a.Rater.Id == id && a.IsExpired == false && a.IsDeleted == false);
        }

        public PeerAssignment PeerAssignmentById(int id)
        {
            return _dbContext.PeerAssignment
                .Include(a => a.Ratee)
                .Include(a => a.Rater)
                .FirstOrDefault(a => a.Id == id);
        }

        public void RemoveById(int id)
        {
            var item = PeerAssignmentById(id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(PeerAssignment peerAssignment,int UserId)
        {
            peerAssignment.CreatedBy = UserId.ToString();
            peerAssignment.CreationDate = DateTime.Now;
            _dbContext.Add(peerAssignment);
            _dbContext.SaveChanges();
        }
    }
}
