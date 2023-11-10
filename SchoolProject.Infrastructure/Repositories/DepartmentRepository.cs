using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        #region Fields
        private DbSet<Department> _department;
        #endregion

        #region  Constractors
        public DepartmentRepository(ApplicationDBContext context) : base(context)
        {
            _department = context.Set<Department>();
        }

        #endregion

        #region Handle Functions

        #endregion
    }
}
