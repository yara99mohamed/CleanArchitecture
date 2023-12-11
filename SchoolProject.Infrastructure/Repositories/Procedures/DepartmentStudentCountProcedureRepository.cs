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

//CREATE procedure DepartmentStudentCountProcedure
//@DID int
//as
//begin
//create table #temp(DID int , DNameAr nvarchar(200),DNameEn nvarchar(200))
//--, StudentCount int)
//insert into #temp 
//	select d.DID, d.InstructorManagerId, d.DNameAr, d.DNameEn
//	-- , count(StudID) as StudentCount
//	 from Departments as d left join Students as s on d.DID = s.DID 
//	 where d.DID = case when @DID = 0 then d.DID else  @DID end
//	 group by d.DID, d.InstructorManagerId, d.DNameAr, d.DNameEn
// end
//select * from #temp
