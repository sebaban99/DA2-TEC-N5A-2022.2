using Vidly.IBusinessLogic;

namespace Vidly.BusinessLogic;

public class SessionManager : ISessionManager
{
    public bool ValidateToken()
    {
        return true;
    }
}