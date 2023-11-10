using SchoolProject.Core.Feature.Students.Commands.Requests;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMappig
{
    public partial class StudentProfile
    {
        public void AddStudentCommandMapping()
        {
            CreateMap<AddStudentCommandRequest, Student>().ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));
        }

        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommandRequest, Student>().ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id))
                                                           .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));
        }
    }
}
