using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourses;

namespace SchoolProject.Core.Feature.Authorization.Commands.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion

        #region Constractors
        public EditRoleValidator
            (IStringLocalizer<SharedResourse> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValoidationsRules();
            ApplyCustomValoidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValoidationsRules()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                               .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.NotNull]);

            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.NotNull]);
        }

        public void ApplyCustomValoidationsRules()
        {

        }
        #endregion
    }
}