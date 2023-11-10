using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constractions
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        #endregion

        #region Handles Function
        public async Task<List<Student>> GetAllStudentServicesAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            // return await _studentRepository.GetByIdAsync(id);
            var student = _studentRepository.GetTableNoTracking().Include(s => s.Department).Where(s => s.StudID == id).FirstOrDefault();
            return student;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student;
        }

        public async Task<string> AddAsync(Student entity)
        {
            // Check if name is Exist or nor
            if (await IsNameExist(entity.NameAr))
            {
                return "Student Exist";
            }
            // Add student
            await _studentRepository.AddAsync(entity);
            return "Success";
        }

        public async Task<bool> IsNameExist(string name)
        {
            var student = _studentRepository.GetTableNoTracking().Where(s => s.NameAr == name).FirstOrDefault();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var student = _studentRepository.GetTableNoTracking().Where(s => s.NameAr == name && s.DID != id).FirstOrDefault();
            if (student == null) return false;
            return true;
        }

        public async Task<string> EditAsync(Student entity)
        {
            await _studentRepository.UpdateAsync(entity);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public IQueryable<Student> GetAllStudentQueryablServices()
        {
            return _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentByDepartmentPaginatedQueryable(int departmentId)
        {
            return _studentRepository.GetTableNoTracking().Where(x => x.DID.Equals(departmentId)).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQueryable(string search)//StudentQrderingEnum orderingEnum,
        {
            var queryable = _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
            if (search != null)
            {
                queryable.Where(s => s.NameAr.Contains(search) || s.Address.Contains(search));
            }
            return queryable;
        }
        #endregion
    }
}