using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
{
    public class UserInfo
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
