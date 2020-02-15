using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.UserManagement
{
    public class UserInfo
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
