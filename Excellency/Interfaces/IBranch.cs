using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IBranch
    {
        void Add(Branch branch);
        void Update(Branch branch);
        IEnumerable<Branch> Branches();
        Branch GetBranchById(int id);
        IEnumerable<Company> Companies();
        Company GetCompanyPerBranch(int id);
        Company GetCompanyById(int id);
        void RemoveById(int Id);
    }
}
