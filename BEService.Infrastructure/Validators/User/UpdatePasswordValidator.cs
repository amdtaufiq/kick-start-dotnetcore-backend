using BEService.Core.DTOs;
using FluentValidation;

namespace BEService.Infrastructure.Validators.User
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequest>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(8).WithMessage("{PropertyName} musth 8 character");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(8).WithMessage("{PropertyName} musth 8 character");

            RuleFor(x => x.RePassword)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MinimumLength(8).WithMessage("{PropertyName} musth 8 character")
                .Equal(x => x.Password).WithMessage("Password doesn't match");
        }
    }
}
