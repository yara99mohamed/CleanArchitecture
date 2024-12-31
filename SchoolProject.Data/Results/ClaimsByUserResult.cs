namespace SchoolProject.Data.Results
{
    public class ClaimsByUserResult
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Claims>? Claims { get; set; }
    }

    public class Claims
    {
        public string Name { get; set; }

        public bool Value { get; set; }
    }
}
