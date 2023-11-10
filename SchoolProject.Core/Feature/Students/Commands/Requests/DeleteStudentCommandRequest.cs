using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Students.Commands.Requests
{
    public class DeleteStudentCommandRequest : IRequest<Response<string>>
    {
        public DeleteStudentCommandRequest(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
