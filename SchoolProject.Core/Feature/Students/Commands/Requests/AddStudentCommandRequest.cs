using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Students.Commands.Requests
{
    public class AddStudentCommandRequest : IRequest<Response<string>>
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public int? DepartmentId { get; set; }
    }
}
