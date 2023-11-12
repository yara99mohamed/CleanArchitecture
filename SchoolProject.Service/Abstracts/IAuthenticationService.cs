﻿using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<string> GetJWTToken(User user);
    }
}
