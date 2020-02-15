using System.ComponentModel.DataAnnotations;

namespace LarpQuestSystem.Data.Model.Security
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
