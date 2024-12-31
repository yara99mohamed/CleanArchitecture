namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string Root = "Api";
        public const string Version = "/V1";
        public const string SingleRoute = "{id}";
        public const string rule = Root + Version + "/";
        public static class StudentRouting
        {
            public const string Prefix = rule + "Student/";
            public const string List = Prefix + "List";
            public const string Paginate = Prefix + "Paginate";
            public const string GetById = Prefix + SingleRoute;
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete/" + SingleRoute;
        }

        public static class DepartmentRouting
        {
            public const string Prefix = rule + "Department/";
            public const string List = Prefix + "List";
            //public const string Paginate = Prefix + "Paginate";
            public const string GetById = Prefix + "ID";
            public const string Create = Prefix + "Create";
            public const string Edit = Prefix + "Edit";
            public const string Delete = Prefix + "Delete/" + SingleRoute;
        }

        public static class ApplicationUserRouting
        {
            public const string Prefix = rule + "User/";
            public const string Paginate = Prefix + "Paginate";
            public const string GetById = Prefix + SingleRoute;
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + "Update";
            public const string ChangePassword = Prefix + "Change-Password";
            public const string Delete = Prefix + "Delete/" + SingleRoute;
        }

        public static class AuthenticationRouting
        {
            public const string Prefix = rule + "Authentication/";
            public const string SignIn = Prefix + "SignIn";
            public const string RefreshToken = Prefix + "Refresh-Token";
            public const string ValidateToken = Prefix + "Validate-Token";
        }

        public static class AuthorizationRouting
        {
            public const string Prefix = rule + "Authorization/";
            public const string Roles = Prefix + "Role/";
            public const string Claims = Prefix + "Claim/";
            public const string GetById = Roles + SingleRoute;
            public const string List = Roles + "List";
            public const string Create = Roles + "Create";
            public const string Edit = Roles + "Edit";
            public const string Delete = Roles + "Delete/" + SingleRoute;
            public const string RolesByUser = Roles + "Roles-By-User/" + SingleRoute;
            public const string UpdateRolesByUser = Roles + "Update-Roles-By-User/";

            public const string ClaimsByUser = Claims + "Claims-By-User/" + SingleRoute;
            public const string UpdateClaimsByUser = Claims + "Update-Claims-By-User/";
        }
    }
}
