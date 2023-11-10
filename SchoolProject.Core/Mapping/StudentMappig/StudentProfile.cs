using AutoMapper;

namespace SchoolProject.Core.Mapping.StudentMappig
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentListMapping();
            GetStudentMapping();
            AddStudentCommandMapping();
            EditStudentCommandMapping();
        }
    }
}
