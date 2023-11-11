using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResourses;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Validatiors
{
    public class ChangeApplicationUserPasswordValidatior : AbstractValidator<ChangeApplicationUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion

        #region Constractors
        public ChangeApplicationUserPasswordValidatior(IStringLocalizer<SharedResourse> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required]);

            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required]);

            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required]);

            RuleFor(x => x.ConfirmPassword)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required])
               .Equal(x => x.NewPassword).WithMessage(_stringLocalizer[SharedResourseKey.PasswordNotEqualConfirmPassword]);
        }
        #endregion
    }
}
