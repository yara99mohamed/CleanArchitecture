using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        #region Fields
        private readonly DbSet<Instructor> _instructor;
        #endregion

        #region  Constractors
        public InstructorRepository(ApplicationDBContext context) : base(context)
        {
            _instructor = context.Set<Instructor>();
        }
        #endregion

        #region Handle Functions
        #endregion
    }
}


