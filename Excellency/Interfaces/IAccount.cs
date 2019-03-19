using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IUserAccount
    {
        void Add(Account Account);
        void Update(Account Account);
        IEnumerable<Account> Accounts();
        Account GetAccountById(int id);
        void RemoveById(int Id);

        void Save(Account account,List<int> userTypes, string UserId);
        string NextEmployeeNo();

        IEnumerable<Company> Companies();
        IEnumerable<Branch> Branches();
        IEnumerable<Department> Departments();
        IEnumerable<Position> Positions();
        IEnumerable<EmployeeCategory> Categories();
        IEnumerable<UserType> UserTypes();


        Company GetCompanyById(int id);
        Branch GetBranchById(int id);
        Department GetDepartmentById(int id);
        Position GetPositionById(int id);
        EmployeeCategory GetCategoryById(int id);
        UserType UserTypeById(int id);

        string GetUserId(Account account);

        bool IsUserAlreadyExist(Account account);
        bool IsAccountLocked(Account account);
        bool IsAvailableToLogin(Account account);
        bool IsLoginExpired(Account account);
        bool IsNotExist(Account account);

    }
}
