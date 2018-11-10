using Excellency.Models;
using System.Collections.Generic;
namespace Excellency.Interfaces
{
    public interface ICompany
    {
        void Add(Company company);
        void Update(Company company);
        IEnumerable<Company> Companies();
        Company GetCompanyById(int id);
        string GetCompanyLogoUrlById(int id);
        void RemoveById(int Id);
    }
}
