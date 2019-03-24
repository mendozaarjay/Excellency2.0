namespace Excellency.Interfaces
{
    public interface IEvaluationSettings
    {
        decimal GetBehavioralPercentage();
        decimal GetKRAPercentage();
        void Save(decimal behavioral, decimal kra);
    }
}
