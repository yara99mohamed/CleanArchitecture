//using FluentValidation;
//using Microsoft.Extensions.Localization;
//using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
//using SchoolProject.Core.SharedResourses;

//namespace SchoolProject.Core.Feature.ApplicationUser.Validatiors
//{
//    public class AddUserValidatior : AbstractValidator<AddUserCommand>
//    {
//        #region Fields
//        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
//        #endregion

//        #region Constractors
//        public AddUserValidatior(IStringLocalizer<SharedResourse> stringLocalizer)
//        {
//            _stringLocalizer = stringLocalizer;
//            ApplyValidatiosRules();
//        }
//        #endregion

//        #region Handle Functions
//        public void ApplyValidatiosRules()
//        {
//            RuleFor(x => x.FullName)
//                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
//                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required])
//                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourseKey.MaxLengthIs50]);

//            RuleFor(x => x.UserName)
//                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
//                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required])
//                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourseKey.MaxLengthIs50]);

//            RuleFor(x => x.Email)
//                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
//                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required])
//                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourseKey.MaxLengthIs50])
//                .EmailAddress().WithMessage(_stringLocalizer[SharedResourseKey.MustEmail]);

//            RuleFor(x => x.PhoneNumber)
//                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
//                .Length(11, 11).WithMessage(_stringLocalizer[SharedResourseKey.PhoneLengthIs11]);

//            RuleFor(x => x.Password)
//                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
//                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required]);

//            RuleFor(x => x.ConfirmPassword)
//                .NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
//                .NotNull().WithMessage(_stringLocalizer[SharedResourseKey.Required])
//                .Equal(x => x.Password).WithMessage(_stringLocalizer[SharedResourseKey.PasswordNotEqualConfirmPassword]);
//        }
//        #endregion
//    }
//}
