using System.Threading.Tasks;
using LarpQuestSystem.Data.Model;

namespace LarpQuestSystem.WebApp.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}