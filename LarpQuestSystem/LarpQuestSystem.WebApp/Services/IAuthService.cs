using System.Threading.Tasks;
using LarpQuestSystem.Data.Model;
using LarpQuestSystem.Data.Model.Security;

namespace LarpQuestSystem.WebApp.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}