using BEService.Core.DTOs;
using FluentValidation;

namespace BEService.Infrastructure.Validators.Role
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
