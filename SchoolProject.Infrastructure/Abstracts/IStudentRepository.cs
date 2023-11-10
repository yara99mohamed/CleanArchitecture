using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Bases;

namespace SchoolProject.Infrastructure.Abstracts
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
       public Task<List<Student>> GetAllStudentsAsync();
    }
}
