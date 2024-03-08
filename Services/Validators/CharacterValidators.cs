using FluentValidation;
using Core.Entities;

namespace Services.Validators;

public class CharacterValidators : AbstractValidator<Character>
    {
        public CharacterValidators()
        {
            RuleFor(x => x.HP)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(Personaje => Personaje.HP);

            RuleFor(x => x.MP)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(Personaje => Personaje.MP);
            
            RuleFor(x => x.Strenght)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(Personaje => Personaje.Strenght);
            
            RuleFor(x => x.IQ)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(Personaje => Personaje.IQ);

            RuleFor(x => x.Agility)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(Personaje => Personaje.Agility);

            RuleFor(x => x.Exp)
                .LessThanOrEqualTo(Personaje => Personaje.Level * 10 )
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Level)
                .LessThanOrEqualTo(99)
                .GreaterThanOrEqualTo(Personaje => Personaje.Level);
                
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);

        }
    }