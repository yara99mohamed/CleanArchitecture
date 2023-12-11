﻿using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Authentication.Queries.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}