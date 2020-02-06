using Microsoft.AspNetCore.Authorization;

namespace LarpQuestSystem.Data.Model
{
    public static class Policies
    {
        public const string IsAdmin = "IsAdmin";
        public const string IsUser = "IsUser";
        public const string IsSuperUser = "IsSuperUser";

        public static AuthorizationPolicy IsAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole("Admin", "SuperUser")
                .Build();
        }

        public static AuthorizationPolicy IsUserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole("User", "Admin", "SuperUser")
                .Build();
        }

        public static AuthorizationPolicy IsSuperUserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole("SuperUser")
                .Build();
        }
    }
}
