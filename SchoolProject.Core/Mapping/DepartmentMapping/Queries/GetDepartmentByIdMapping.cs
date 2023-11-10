using SchoolProject.Core.Feature.Departments.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentResponse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.InstructorManagerName, opt => opt.MapFrom(src => src.InstructorManager.Localize(src.InstructorManager.NameAr, src.InstructorManager.NameEn)))
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors))
                .ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students));


            CreateMap<DepartmetSubject, SubjectResponse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));


            CreateMap<Instructor, InstructorResponse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InstructorId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));


            CreateMap<Student, StudentResponse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
