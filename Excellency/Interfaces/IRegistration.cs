using Excellency.Models;
using System.Collections.Generic;
namespace Excellency.Interfaces
{
    public interface IRegistration
    {
        IEnumerable<Registration> Registrations();
        void Save(Registration registration);
        void RemoveById(int id);
    }
}
