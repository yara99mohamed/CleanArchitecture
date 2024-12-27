using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authorization.Commands.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constractors
        public DeleteRoleValidator(IStringLocalizer<SharedResourse> stringLocalizer, IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValoidationsRules();
            //ApplyCustomValoidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValoidationsRules()
        {
            RuleFor(x => x.RoleId).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                                  .NotNull().WithMessage("Id Must be not null");
        }

        public void ApplyCustomValoidationsRules()
        {
        }
        #endregion
    }
}