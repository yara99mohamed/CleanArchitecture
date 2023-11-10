using SchoolProject.Core.Feature.Students.Queries.Response;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMappig
{
    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DNameAr))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }

        public void GetStudentMapping()
        {
            CreateMap<Student, GetStudentResponse>().ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DNameAr));
        }
    }
}
