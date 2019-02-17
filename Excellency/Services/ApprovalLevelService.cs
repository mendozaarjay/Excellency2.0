﻿using Excellency.Interfaces;
using Excellency.Models;
using Excellency.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Services
{
    public class ApprovalLevelService : IApprovalLevel
    {
        private EASDbContext _dbContext;

        public ApprovalLevelService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ApprovalLevelAssignment ApprovalLevelById(int id)
        {
            return _dbContext.ApprovalLevelAssignment.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<ApprovalLevelAssignment> ApprovalLevels()
        {
            return _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .Include(a => a.FirstApproval)
                .Include(a => a.SecondApproval)
                .Where(a => a.IsDeleted == false);
        }

        public IEnumerable<Account> Approvers(int id)
        {
            return _dbContext.Accounts
                .FromSql(@"SELECT *
                    FROM [dbo].[Accounts] [a]
                    WHERE [a].[Id] IN (
                                          SELECT [ara].[AccountId]
                                          FROM [dbo].[AccountRoleAssignments] [ara]
                                          WHERE [ara].[RoleId] = 2
                                      )")
                .Where(a => a.Id != id);
        }

        public IEnumerable<Account> Employees()
        {
            return _dbContext.Accounts
                    .Include(a => a.Company)
                    .Include(a => a.Branch)
                    .Include(a => a.Department)
                    .Include(a => a.Position)
                ;
        }
        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        }
        public string GetNameById(int id)
        {
            var item = GetAccountById(id);
            var name = item.FirstName + " " + item.MiddleName + " " + item.LastName;
            return name;
        }
        public void Save(ApprovalLevelAssignment approval,int userid)
        {
            if(approval.Id == 0)
            {
                approval.CreatedBy = userid.ToString();
                approval.CreationDate = DateTime.Now;
                _dbContext.Add(approval);
            }
            else
            {
                approval.ModifiedBy = userid.ToString();
                approval.ModifiedDate = DateTime.Now;
                _dbContext.Entry(approval).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }
        public void RemoveById(int id)
        {
            var item = _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .FirstOrDefault(a => a.Employee.Id == id && a.IsDeleted == false);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public ApprovalLevelAssignment ApprovalAssignmentByEmployee(int id)
        {
            return _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .Include(a => a.SecondApproval)
                .Include(a => a.FirstApproval)
                .FirstOrDefault(a => a.Employee.Id == id && a.IsDeleted == false);
        }
        public bool IsSet(int id)
        {
            return _dbContext.ApprovalLevelAssignment
                .Include(a => a.Employee)
                .Where(a => a.IsDeleted == false)
                .Any(a => a.Employee.Id == id);
        }
    }
}