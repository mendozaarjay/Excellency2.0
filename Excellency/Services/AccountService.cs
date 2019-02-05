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
    public class AccountService : IAccount
    {
        private EASDbContext _dbContext;

        public AccountService(EASDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Account> Accounts()
        {
            return _dbContext.Accounts
                .Include(a => a.Branch)
                .Include(a => a.Company)
                .Include(a => a.Department)
                .Include(a => a.Position)
                .Include(a => a.Category)
                .Where(a => a.IsDeleted == false && a.IsDeactivated == false);
        }

        public void Add(Account Account)
        {
            _dbContext.Accounts.Add(Account);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Branch> Branches()
        {
            return _dbContext.Branches.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<EmployeeCategory> Categories()
        {
            var items = _dbContext.EmployeeCategories.Where(a => a.IsDeleted == false);
            return items;
        }

        public IEnumerable<Company> Companies()
        {
            return _dbContext.Companies.Where(a => a.IsDeleted == false);
        }

        public IEnumerable<Department> Departments()
        {
            return _dbContext.Departments.Where(a => a.IsDeleted == false);
        }

        public Account GetAccountById(int id)
        {
            return _dbContext.Accounts
                   .Include(a => a.Company)
                   .Include(a => a.Branch)
                   .Include(a => a.Department)
                   .Include(a => a.Position)
                   .Include(a => a.Category)
                   .FirstOrDefault(a => a.Id == id);
        }

        public Branch GetBranchById(int id)
        {
            return _dbContext.Branches.FirstOrDefault(a => a.Id == id);
        }

        public EmployeeCategory GetCategoryById(int id)
        {
            var item = _dbContext.EmployeeCategories.FirstOrDefault(a => a.Id == id);
            return item;
        }

        public Company GetCompanyById(int id)
        {
            return _dbContext.Companies.FirstOrDefault(a => a.Id == id);
        }

        public Department GetDepartmentById(int id)
        {
            return _dbContext.Departments.FirstOrDefault(a => a.Id == id);
        }

        public Position GetPositionById(int id)
        {
            return _dbContext.Positions.FirstOrDefault(a => a.Id == id);
        }

        public string GetUserId(Account account)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.Username == account.Username && a.Password == account.Password).Id.ToString();
        }

        public bool IsAccountLocked(Account account)
        {
            return _dbContext.Accounts.Any(a => a.Username == account.Username && a.Password == account.Password && a.IsLocked == true);
        }

        public bool IsAvailableToLogin(Account account)
        {
            return _dbContext.Accounts.Any(a => a.Username == account.Username && a.Password == account.Password);
        }

        public bool IsLoginExpired(Account account)
        {
            return _dbContext.Accounts.Any(a => a.Username == account.Username && a.IsExpired == true);
        }

        public bool IsNotExist(Account account)
        {
            if (IsAvailableToLogin(account))
                return false;
            else
                return true;
        }

        public bool IsUserAlreadyExist(Account account)
        {
            return _dbContext.Accounts.Any(a => a.Username == account.Username || a.Email == account.Email);
        }

        public IEnumerable<Position> Positions()
        {
            return _dbContext.Positions.Where(a => a.IsDeleted == false);
        }

        public void RemoveById(int Id)
        {
            var item = _dbContext.Accounts.FirstOrDefault(a => a.Id == Id);
            item.IsDeactivated = true;
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Save(Account account, string UserId)
        {
            if(account.Id == 0)
            {
                account.CreatedBy = UserId;
                account.CreationDate = DateTime.Now;
                _dbContext.Add(account);
            }
            else
            {
                account.ModifiedBy = UserId;
                account.ModifiedDate = DateTime.Now;
                _dbContext.Entry(account).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public void Update(Account Account)
        {
            throw new NotImplementedException();
        }
        public string NextEmployeeNo()
        {
            var UserConnectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            var item = SCObjects.ReturnText("SELECT REPLICATE('0',4 - LEN(COUNT([a].[EmployeeNo]) + 1)) + CAST((COUNT([a].[EmployeeNo]) + 1) AS VARCHAR) FROM [dbo].[Accounts] [a]", UserConnectionString);
            return item;
        }
    }
}
