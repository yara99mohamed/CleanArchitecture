using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Bases;
using SchoolProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        #region Fields
        private readonly DbSet<Subject> _subject;
        #endregion

        #region  Constractors
        public SubjectRepository(ApplicationDBContext context) : base(context)
        {
            _subject = context.Set<Subject>();
        }
        #endregion

        #region Handle Functions
        #endregion
    }
}

