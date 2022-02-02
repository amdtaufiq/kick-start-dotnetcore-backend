using BEService.Core.DTOs;
using FluentValidation;

namespace BEService.Infrastructure.Validators.Role
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
