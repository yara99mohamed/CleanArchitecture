using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Feature.Students.Commands.Requests;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Validators
{
    public class AddStudentCommandValidator : AbstractValidator<AddStudentCommandRequest>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;

        #endregion

        #region Constractors
        public AddStudentCommandValidator(IStudentService studentService, IStringLocalizer<SharedResourse> stringLocalizer)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRule();
            ApplyCustomValidationsRule();
        }
        #endregion 

        #region Actions
        public void ApplyValidationsRule()
        {
            RuleFor(s => s.NameAr).NotEmpty().WithMessage(_stringLocalizer[SharedResourseKey.NotEmpty])
                                .NotNull().WithMessage("Name Must be not null")
                                .MaximumLength(10).WithMessage("Name Must be not max length is 10 char");

            RuleFor(s => s.Address).NotEmpty().WithMessage("{PropertyName} Must be not empyt")
                               .NotNull().WithMessage("{PropertyValue} Must be not null")
                               .MaximumLength(10).WithMessage("{PropertyName} Must be not max length is 10 char");
        }

        public async void ApplyCustomValidationsRule()
        {
            // await _studentService.IsNameExist(Key) ==> must return  flase to read WithMessage
            RuleFor(s => s.NameAr).MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key))
                                .WithMessage("Name Is Exist");
        }
        #endregion

    }
}
