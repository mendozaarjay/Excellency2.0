using Excellency.Models;

namespace Excellency.Interfaces
{
    public interface IHome
    {
        string GetUserId(Account account);

        bool IsUserAlreadyExist(Account account);
        bool IsAccountLocked(Account account);
        bool IsAvailableToLogin(Account account);
        bool IsLoginExpired(Account account);
        bool IsNotExist(Account account);

        UserAccess UserAccess(int UserId);
        bool IsAdministrator(int UserId);
        bool IsApprover(int UserId);
        bool IsRater(int UserId);

    }
}
