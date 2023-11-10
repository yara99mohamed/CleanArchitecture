using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudentServicesAsync();
        public IQueryable<Student> GetAllStudentQueryablServices();
        public Task<Student> GetStudentByIdWithIncludeAsync(int id);
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddAsync(Student entity);
        public Task<string> EditAsync(Student entity);
        public Task<string> DeleteAsync(Student entity);
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
        public IQueryable<Student> FilterStudentByDepartmentPaginatedQueryable(int departmentId);
        public IQueryable<Student> FilterStudentPaginatedQueryable(string search);//StudentQrderingEnum orderingEnum,
    }
}
