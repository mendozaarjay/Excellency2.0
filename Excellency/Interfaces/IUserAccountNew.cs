using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Interfaces
{
    public interface IUserAccountNew
    {
        DataTable AccountInfo(int id);
        bool IsValidPassword(int id,string password);
        string ChangePassword(int id, string password);
    }
}
