﻿using Excellency.Models;
using System.Collections.Generic;

namespace Excellency.Interfaces
{
    public interface IEmployeeAssignment
    {
        void Save(EmployeeAssignment employeeAssignment, EmployeeKRAAssignment kraAssignment, EmployeeBehavioralAssignment behavioralAssignment);
        void Save(int EmployeeId, List<int> KRAItems, List<int> BehavioralItems);
        void SaveKRAItems(int EmployeeId, List<int> Items);
        void SaveBehavioralItems(int EmployeeId, List<int> Items);
        void RemoveAssignmentById(int id);
        void RemoveKRAById(int id);
        void RemoveBehavioralPerId(int id);

        EmployeeAssignment EmployeeAssignmentByEmployeeId(int id);
        Account EmployeeById(int id);
        BehavioralFactor BehavioralFactorById(int id);
        KeyResultArea KeyResultAreaById(int id);

        IEnumerable<Account> Employees();
        IEnumerable<EmployeeAssignment> EmployeeAssignments();
        IEnumerable<EmployeeKRAAssignment> EmployeeKRAAssignmentByHeaderId(int id);
        IEnumerable<EmployeeBehavioralAssignment> EmployeeBehavioralAssignmentByHeaderId(int id);
        IEnumerable<KeyResultArea> KeyResultAreas();
        IEnumerable<BehavioralFactor> BehavioralFactors();


        bool IsWithActiveSeason();
        EvaluationSeason ActiveSeason();

    }
}
