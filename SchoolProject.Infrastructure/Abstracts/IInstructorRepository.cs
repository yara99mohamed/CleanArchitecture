﻿using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Abstracts
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
    }
}
