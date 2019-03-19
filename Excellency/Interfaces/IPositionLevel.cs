using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IPositionLevel
    {
        void Save(PositionLevel item, int userId);
        void RemoveById(int id);
        IEnumerable<PositionLevel> PositionLevels();
        PositionLevel PositionLevelById(int id);
    }
}
