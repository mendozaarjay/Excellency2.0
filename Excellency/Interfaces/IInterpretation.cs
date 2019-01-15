using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IInterpretation
    {
        IEnumerable<Interpretation> GetAll();
        Interpretation GetById(int id);
        void Save(Interpretation item);
        void RemoveById(int id);
    }
}
