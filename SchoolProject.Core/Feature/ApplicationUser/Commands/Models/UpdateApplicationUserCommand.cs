﻿using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Models
{
    public class UpdateApplicationUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
