using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Infrastructure.Abstracts.Procedures;
using SchoolProject.Infrastructure.Context;
using StoredProcedureEFCore;

namespace SchoolProject.Infrastructure.Repositories.Procedures
{
    public class DepartmentStudentCountProcedureRepository : IDepartmentStudentCountProcedureRepository
    {
        #region Fields
        private readonly ApplicationDBContext _context;
        #endregion

        #region Constructor(S)
        public DepartmentStudentCountProcedureRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Handle Functions

        public async Task<IReadOnlyList<DepartmentStudentCountProcedure>> GetDepartmentStudentCountProcedure(DepartmentStudentCountProcedureParameters parameters)
        {
            var rows = new List<DepartmentStudentCountProcedure>();
            await _context.LoadStoredProc(nameof(DepartmentStudentCountProcedure))
                 .AddParam(nameof(DepartmentStudentCountProcedureParameters.DID), parameters.DID)
                 .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProcedure>());
            return rows;
        }
        #endregion
    }
}
