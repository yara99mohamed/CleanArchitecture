namespace SchoolProject.Data.ViewData
{
    public class RolesByUserViewData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<RoleViewData>? Roles { get; set; }
    }
}
