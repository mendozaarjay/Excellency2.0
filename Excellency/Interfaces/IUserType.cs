using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IUserType
    {
        void Save(UserType item, int userId);
        void RemoveById(int id);
        IEnumerable<UserType> UserTypes();
        UserType UserTypeById(int id);
    }
}
