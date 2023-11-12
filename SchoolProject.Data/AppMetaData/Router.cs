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
        }
    }
}
