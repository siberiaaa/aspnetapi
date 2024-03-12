using Core.Entities;
using Core.Responses;
using Core.Interfaces;
using Core.Enum;
using Core.Interfaces.Services;
using Services.Validators;

namespace Services.Services;

public class CharacterService : ICharacterService
{
    private readonly IUnitOfWork _unitOfWork;
    public CharacterService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Character> Create(Character newCharacter)
    {
        CharacterValidators validator = new();

        var validationResult = await validator.ValidateAsync(newCharacter);
        if (validationResult.IsValid)
        {
            await _unitOfWork.CharacterRepository.AddAsync(newCharacter);
            await _unitOfWork.CommitAsync();
        }
        else
        {
           
            throw new ArgumentException(validationResult.Errors[0].ErrorMessage.ToString());
        }

        return newCharacter;
    }

    public async Task Delete(int characterId)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);
        _unitOfWork.CharacterRepository.Remove(character);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<Character>> GetAll()
    {
        return await _unitOfWork.CharacterRepository.GetAllAsync();
    }

    public async Task<Character> GetById(int id)
    {
        return await _unitOfWork.CharacterRepository.GetByIdAsync(id);
    }

    public async Task<Character> Update(int characterToBeUpdatedId, Character newCharacterValues)
    {
        CharacterValidators CharacterValidator = new();
        
        var validationResult = await CharacterValidator.ValidateAsync(newCharacterValues);
        if (!validationResult.IsValid)
            throw new ArgumentException(validationResult.Errors.ToString());

        Character CharacterToBeUpdated = await _unitOfWork.CharacterRepository.GetByIdAsync(characterToBeUpdatedId);

        if (CharacterToBeUpdated == null)
            throw new ArgumentException("Invalid Character ID while updating");

        CharacterToBeUpdated.Name = newCharacterValues.Name;
        CharacterToBeUpdated.UrlImage = newCharacterValues.UrlImage;
        CharacterToBeUpdated.Level = newCharacterValues.Level;
        CharacterToBeUpdated.HP = newCharacterValues.HP;
        CharacterToBeUpdated.MP = newCharacterValues.MP;
        CharacterToBeUpdated.IQ = newCharacterValues.IQ;
        CharacterToBeUpdated.Strenght = newCharacterValues.Strenght;
        CharacterToBeUpdated.Agility = newCharacterValues.Agility;
        CharacterToBeUpdated.Exp = newCharacterValues.Exp;

        await _unitOfWork.CommitAsync();

        return await _unitOfWork.CharacterRepository.GetByIdAsync(characterToBeUpdatedId);
    }

    public async Task<Character> LevelUp(int characterId)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null)
            throw new ArgumentException("Invalid Character ID");

        character.Level += 1;
        character.MP += 5;
        character.HP += 5;
        character.IQ += 3;
        character.Strenght += 10;
        character.Agility += 10;
        character.Exp = 0;

        await _unitOfWork.CommitAsync();

        return await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);
    }

    public async Task<string> AttackEnemy(int characterId, int enemyId)
    {
        //Es para que retornara el attack response
        //AttackResponse = new AttackResponse();

        EnemyService _enemyService = new EnemyService(_unitOfWork);

        Enemy enemy = await _enemyService.GetById(enemyId);

        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null) throw new ArgumentException("Invalid character ID");
        if (enemy == null) throw new ArgumentException("Invalid enemy ID");

        if (enemy.HP <= 0) return "You can't fight this enemy, it's weakened.";
        
        int attackPoints = (int)Math.Floor(character.Strenght * 1.25);

        IsAlive attackresult = await _enemyService.TakeDamage(enemyId, attackPoints);

        if (attackresult == IsAlive.no)
        {
            ItLeveledUp expResult = await UpdateExp(characterId, enemy.Reward);
            
            if (expResult == ItLeveledUp.yes)
            {
                return $"Damage given: {attackPoints} You defeated your enemy. Your reward is {enemy.Reward} exp. You leveled up!";
            }
            return $"Damage given: {attackPoints} You defeated your enemy. Your reward is {enemy.Reward} exp.";
        }

        int counterAttackPoints = await _enemyService.CounterAttack(enemyId);

        IsAlive counterAttackResult = await TakeDamage(characterId, counterAttackPoints);

        if (counterAttackResult == IsAlive.no)
        {
            return $"Damage given: {attackPoints}\nDamage taken: {counterAttackPoints}\nYou died.";
        }
        
        return $"Damage given: {attackPoints}\nDamage taken: {counterAttackPoints}.";
    }

    public async Task<Character> Heal(int characterId)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null)
            throw new ArgumentException("Invalid Character ID");

        character.HP = character.HP + 5;
        character.MP = character.MP - 2;

        if (character.MP < 0)
        {
            return await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);
        }
        else
        {
            await _unitOfWork.CommitAsync();
        }

        return await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);
    }

    public async Task<IsAlive> TakeDamage(int characterId, int damage)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null)
            throw new ArgumentException("Invalid Character ID");

        character.HP = character.HP - damage;

        if (character.HP <= 0)
        {
            _unitOfWork.CharacterRepository.Remove(character);
            await _unitOfWork.CommitAsync();
            return IsAlive.no;
        }

        await _unitOfWork.CommitAsync();
        return IsAlive.yes;
    }

    public async Task<ItLeveledUp> UpdateExp(int characterId, int exp)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null)
            throw new ArgumentException("Invalid Character ID");

        character.Exp += exp;

        if (character.Exp >= 100)
        {
           await LevelUp(characterId);
           return ItLeveledUp.yes;
        }

        await _unitOfWork.CommitAsync();

        return ItLeveledUp.no;
    }

}