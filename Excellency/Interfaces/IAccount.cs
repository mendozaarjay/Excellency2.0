using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IAccount
    {
        void Add(Account Account);
        void Update(Account Account);
        IEnumerable<Account> Accounts();
        Account GetAccountById(int id);
        void RemoveById(int Id);

        IEnumerable<Company> Companies();
        IEnumerable<Branch> Branches();
        IEnumerable<Department> Departments();
        IEnumerable<Position> Positions();

        Company GetCompanyById(int id);
        Branch GetBranchById(int id);
        Department GetDepartmentById(int id);
        Position GetPositionById(int id);

        string GetUserId(Account account);

        bool IsUserAlreadyExist(Account account);
        bool IsAccountLocked(Account account);
        bool IsAvailableToLogin(Account account);
        bool IsLoginExpired(Account account);
        bool IsNotExist(Account account);

    }
}
