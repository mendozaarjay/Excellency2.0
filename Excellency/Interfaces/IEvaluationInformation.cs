using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Interfaces
{
    public interface IEvaluationInformation
    {
        DataTable GetAllKRAPerEmployee(int id);
        DataTable GetAllBehavioralPerEmployee(int id);
        DataTable GetAllKRARecordPerId(int recordid);
        DataTable GetAllBehavioralRecordPerId(int recordid);
        DataTable GetApprovalLevel(int id);
        string Name(int id);
        void Confirm(int id);
    }
}
