namespace SchoolProject.Data.ViewData
{
    public class GetRolesByUserViewData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<UserRolesViewData>? Roles { get; set; }
    }
}
