using FluentValidation;
using Core.Entities;

namespace Services.Validators;

public class EnemyValidators : AbstractValidator<Enemy>
    {
        public void CharacterValidator()
        {
            RuleFor(x => x.Level)
                .LessThanOrEqualTo(99)
                .GreaterThanOrEqualTo(Enemy => Enemy.Level);

            RuleFor(x => x.HP)
                .LessThanOrEqualTo(Enemy => Enemy.HP * 10);

            RuleFor(x => x.Reward)
                .LessThanOrEqualTo(Enemy => Enemy.Level * 10);
            
            RuleFor(x => x.Abilities)
                .MaximumLength(255);
                
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);

        }
    }