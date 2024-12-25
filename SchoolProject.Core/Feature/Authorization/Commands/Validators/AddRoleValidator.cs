using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constractors
        public AddRoleValidator(IStringLocalizer<SharedResourse> stringLocalizer, IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValoidationsRules();
            ApplyCustomValoidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValoidationsRules()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                                .NotNull().WithMessage("Name Must be not null");
        }

        public void ApplyCustomValoidationsRules()
        {
            RuleFor(x => x.RoleName).MustAsync(async (key, CancellationToken) => !await _authorizationService.IsRoleExsitAsync(key))
                .WithMessage(_stringLocalizer[SharedResourseKey.IsExist]); ;
        }
        #endregion
    }
}
