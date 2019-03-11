using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface ICriteriaSetting
    {
        void SaveHeader(CriteriaHeader item, int userid);
        void RemoveHeader(int id);
        CriteriaHeader CriteriaHeaderById(int id);
        IEnumerable<CriteriaHeader> CriteriaHeaders();

        void SaveLine(int headerId, CriteriaLine item);
        void RemoveLine(int id);
        CriteriaLine CriteriaLineById(int id);
        IEnumerable<CriteriaLine> LineItemsByHeaderId(int id);
    }
}
