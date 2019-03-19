using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IPosition
    {
        void Add(Position Position);
        void Update(Position Position);
        void Save(Position item,int userid);
        IEnumerable<Position> Positions();
        Position GetPositionById(int id);
        void RemoveById(int Id);
        IEnumerable<PositionLevel> PositionLevels();
        PositionLevel PositionLevelById(int id);
    }
}
