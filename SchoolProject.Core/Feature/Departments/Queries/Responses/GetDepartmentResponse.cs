using SchoolProject.Core.wrappers;

namespace SchoolProject.Core.Feature.Departments.Queries.Responses
{
    public class GetDepartmentResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? InstructorManagerName { get; set; }

        public PaginatedResult<StudentResponse>? StudentList { get; set; }
        public List<SubjectResponse>? SubjectList { get; set; }
        public List<InstructorResponse>? InstructorList { get; set; }
    }
    public class StudentResponse
    {
        public StudentResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class SubjectResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class InstructorResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
