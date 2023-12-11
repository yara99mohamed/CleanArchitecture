using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentWithIncludeByIdAsync(int id);
        Task<IReadOnlyList<DepartmentStudentCountProcedure>> GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters);

    }
}
