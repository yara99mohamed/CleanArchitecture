﻿using AutoMapper;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddApplicationUserCommandMapping();
            GetApplicationUserQueryMapping();
            UpdateApplicationUserCommandMapping();
        }
    }
}
