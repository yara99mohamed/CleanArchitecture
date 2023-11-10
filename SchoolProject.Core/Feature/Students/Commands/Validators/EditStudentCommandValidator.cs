using FluentValidation;
using SchoolProject.Core.Feature.Students.Commands.Requests;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Validators
{
    public class EditStudentCommandValidator : AbstractValidator<EditStudentCommandRequest>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constractors
        public EditStudentCommandValidator(IStudentService studentService)
        {
            _studentService = studentService;
            ApplyValidationsRule();
            ApplyCustomValidationsRule();
        }
        #endregion 

        #region Actions
        public void ApplyValidationsRule()
        {
            RuleFor(s => s.NameAr).NotEmpty().WithMessage("Name Must be not empyt")
                                .NotNull().WithMessage("Name Must be not null")
                                .MaximumLength(100).WithMessage("Name Must be not max length is 100 char");

            RuleFor(s => s.Address).NotEmpty().WithMessage("{PropertyName} Must be not empyt")
                               .NotNull().WithMessage("{PropertyValue} Must be not null")
                               .MaximumLength(100).WithMessage("{PropertyName} Must be not max length is 100 char");
        }

        public async void ApplyCustomValidationsRule()
        {
            RuleFor(s => s.NameAr).MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsNameExistExcludeSelf(Key, model.Id))
                                .WithMessage("Name Is Exist");
        }
        #endregion
    }
}
