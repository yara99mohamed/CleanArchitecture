using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Abstracts.Procedures;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentStudentCountProcedureRepository _departmentStudentCountProcedureRepository;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentStudentCountProcedureRepository departmentStudentCountProcedureRepository)
        {
            _departmentRepository = departmentRepository;
            _departmentStudentCountProcedureRepository = departmentStudentCountProcedureRepository;
        }
        #endregion

        #region Handle Functions
        public async Task<Department> GetDepartmentWithIncludeByIdAsync(int id)
        {
            var department = await _departmentRepository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                                                        .Include(d => d.DepartmentSubjects).ThenInclude(x => x.Subject)
                                                        .Include(d => d.Instructors)
                                                        .Include(d => d.InstructorManager)
                                                        .FirstOrDefaultAsync();
            return department;
        }

        public async Task<IReadOnlyList<DepartmentStudentCountProcedure>> GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters)
        {
            return await _departmentStudentCountProcedureRepository.GetDepartmentStudentCountProcedure(parameters);
        }
        #endregion 
    }
}
