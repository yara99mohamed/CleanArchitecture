using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Infrastructure.Abstracts.Procedures
{
    public interface IDepartmentStudentCountProcedureRepository
    {
        public Task<IReadOnlyList<DepartmentStudentCountProcedure>> GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters);
    }
}
