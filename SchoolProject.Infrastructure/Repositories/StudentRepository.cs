using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        #region Fields
        private readonly DbSet<Student> _student;

        #endregion

        #region Constractors
        public StudentRepository(ApplicationDBContext context):base(context)
        {
            _student = context.Set<Student>();
        }

        #endregion

        #region Handles Functions
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _student.Include(s=>s.Department).ToListAsync();
        }       
        #endregion
    }
}
