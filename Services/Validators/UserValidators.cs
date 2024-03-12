using FluentValidation;
using Core.Entities;

namespace Services.Validators;

public class UserValidators : AbstractValidator<User>
    {
        public UserValidators()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(255);
        }
    }