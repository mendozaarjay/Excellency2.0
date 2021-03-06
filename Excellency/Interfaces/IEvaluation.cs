﻿using Excellency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Interfaces
{
    public interface IEvaluation
    {
        IEnumerable<Account> Employees();
        IEnumerable<Account> AssignedEmployees(int UserId);
        Account GetEmployeePerId(int id);
        string EmployeeNameById(int id);
        IEnumerable<KeyResultArea> KeyResultAreas();
        KeyResultArea KeyResultAreaPerId(int id);
        IEnumerable<KeySuccessIndicator> GetKeySuccessIndicators(int kraid);
        KeySuccessIndicator KeySuccessIndicatorPerId(int id);
        RatingTable RatingTablePerId(int id);
        IEnumerable<RatingTableItem> GetRatingTableItemsPerId(int RatingTableId);
        RatingTableItem RatingTableItem(int id);
        int KeySuccessCounterByKRA(int kraid);

        IEnumerable<KeyResultArea> KeyResultAreasPerEmployee(int id);
        IEnumerable<BehavioralFactor> BehavioralFactorPerEmployee(int id);

        BehavioralFactor GetBehavioralFactorById(int id);
        KeyResultArea GetKeyResultAreaById(int id);

        IEnumerable<BehavioralFactorItem> GetBehavioralItemsByHeaderId(int id);

        IEnumerable<EvaluationHeader> EvaluationsPerUser(int id);
        IEnumerable<EvaluationLine> EvaluationLinesPerId(int headerid);
        void Save(EvaluationHeader header, IEnumerable<EvaluationLine> lineitems,Account account);
        void Post(EvaluationHeader header, Account account);
        void Approve(EvaluationHeader header, Account account);
        Status GetStatusPerId(int id);
        Account GetAccountById(int id);

        BehavioralFactorItem GetBehavioralFactorItemById(int id);
        KeySuccessIndicator GetSuccessIndicatorById(int id);

        bool IsBehavioralExists(int behavioralid, int employeeid);
        bool IsKeyResultExists(int kraid, int employeeid);

        void SaveRating(RatingHeader header, IEnumerable<RatingBehavioralFactor> ratings, IEnumerable<RatingKeySuccessArea> areas);
        void SaveBehavioralEvaluation(RatingHeader header, IEnumerable<RatingBehavioralFactor> ratings, int userid, int employeeid);
        void SaveKeyResultAreaEvaluation(RatingHeader header, IEnumerable<RatingKeySuccessArea> ratings, int userid, int employeeid);

        void UpdateRatingBehavioral(int HeaderId, int UserId, IEnumerable<RatingBehavioralFactor> ratings);
        void UpdateRatingKeyResultArea(int HeaderId, int UserId, IEnumerable<RatingKeySuccessArea> ratings);

        IEnumerable<RatingBehavioralFactor> GetRatingBehavioralByHeaderId(int factorid, int id);
        IEnumerable<RatingKeySuccessArea> GetRatingKeyResultByHeaderId(int kraid, int id);

        RatingHeader GetRatingHeaderBehavioral(int id);
        RatingHeader GetRatingHeaderKeyResult(int id);

        bool IsBehavioralSaved(int behavioralid, int employeeid);
        bool IsKeyResultSaved(int kraid, int employeeid);
        void PostBehavioral(int behavioralid, int employeeid);
        void PostKeyResultArea(int kraid, int employeeid);

        bool IsWithActiveSeason();
        EvaluationSeason ActiveSeason();

    }
}
