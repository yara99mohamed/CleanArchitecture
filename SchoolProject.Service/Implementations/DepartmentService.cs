﻿using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
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
        #endregion 
    }
}
