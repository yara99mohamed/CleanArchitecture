using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Response;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Feature.Students.Queries.Request
{
    public class GetStudentByIdQuery:IRequest<Response<GetStudentResponse>>
    {
        public GetStudentByIdQuery(int id)
        {
                Id = id;
        }
        public int Id { get; set; }
    }
}
