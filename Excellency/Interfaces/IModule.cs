using System.Collections.Generic;
using Excellency.Models;

namespace Excellency.Interfaces
{
    public interface IModule
    {
        void Add(Module Module);
        void Update(Module Module);
        IEnumerable<Module> Modules();
        Module GetModuleById(int id);
        void RemoveById(int Id);
    }
}
