using SchoolProject.Core.Feature.Departments.Queries.Models;
using SchoolProject.Core.Feature.Departments.Queries.Responses;
using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentCountByIdMapping()
        {
            CreateMap<GetDepartmentStudentCountByIdQuery, DepartmentStudentCountProcedureParameters>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.Id));

            CreateMap<DepartmentStudentCountProcedure, GetDepartmentStudentCountByIdResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)));
        }
    }
}
