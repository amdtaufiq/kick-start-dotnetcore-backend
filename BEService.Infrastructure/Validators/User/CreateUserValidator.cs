using BEService.Core.DTOs;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace BEService.Infrastructure.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
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
