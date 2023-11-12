using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authentication.Commands.Models;
using SchoolProject.Core.SharedResourses;

namespace SchoolProject.Core.Feature.Authentication.Commands.Validatiors
{
    public class SignInValidatior : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion

        #region Constractors
        public SignInValidatior(IStringLocalizer<SharedResourse> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required]);

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required]);
        }
        #endregion
    }
}
