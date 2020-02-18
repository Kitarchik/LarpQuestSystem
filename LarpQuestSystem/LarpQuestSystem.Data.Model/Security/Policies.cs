using Microsoft.AspNetCore.Authorization;

namespace LarpQuestSystem.Data.Model.Security
{
    public static class Policies
    {
        public const string IsAdmin = "IsAdmin";
        public const string IsUser = "IsUser";
        public const string IsSuperUser = "IsSuperUser";
        public const string IsScriptWriter = "IsScriptWriter";
        public const string IsScriptManager = "IsScriptManager";
        public const string IsMaterialsManager = "IsMaterialsManager";

        public static AuthorizationPolicy IsAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(Roles.Admin, Roles.SuperUser)
                .Build();
        }

        public static AuthorizationPolicy IsUserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(Roles.User, Roles.ScriptWriter, Roles.Admin, Roles.SuperUser)
                .Build();
        }

        public static AuthorizationPolicy IsScriptWriterPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(Roles.ScriptManager, Roles.ScriptWriter, Roles.Admin, Roles.SuperUser)
                .Build();
        }

        public static AuthorizationPolicy IsScriptManagerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(Roles.ScriptManager, Roles.Admin, Roles.SuperUser)
                .Build();
        }

        public static AuthorizationPolicy IsMaterialsManagerPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(Roles.MaterialsManager, Roles.Admin, Roles.SuperUser)
                .Build();
        }

        public static AuthorizationPolicy IsSuperUserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                .RequireRole(Roles.SuperUser)
                .Build();
        }
    }
}
