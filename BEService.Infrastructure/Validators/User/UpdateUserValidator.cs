using BEService.Core.DTOs;
using FluentValidation;

namespace BEService.Infrastructure.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("Format {PropertyName} not valid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(8).WithMessage("{PropertyName} musth 8 character");

            RuleFor(x => x.RoleId)
                .NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
