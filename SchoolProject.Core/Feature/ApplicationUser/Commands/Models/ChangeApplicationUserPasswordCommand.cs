﻿using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Models
{
    public class ChangeApplicationUserPasswordCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
