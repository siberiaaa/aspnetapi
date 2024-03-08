using FluentValidation;
using Core.Entities;

namespace Services.Validators;

public class CharacterTypeValidators : AbstractValidator<CharacterType>
    {
        public CharacterTypeValidators()
        { 
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
        }
    }