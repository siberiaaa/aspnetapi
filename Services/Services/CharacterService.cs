using Core.Entities;
using Core.Interfaces;
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

    public async Task<Character> CreateCharacter(Character newCharacter)
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
            throw new ArgumentException(validationResult.Errors.ToString());
        }

        return newCharacter;
    }

    public async Task DeleteCharacter(int characterId)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);
        _unitOfWork.CharacterRepository.Remove(character);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<Character>> GetAll()
    {
        return await _unitOfWork.CharacterRepository.GetAllAsync();
    }

    public async Task<Character> GetCharacterById(int id)
    {
        return await _unitOfWork.CharacterRepository.GetByIdAsync(id);
    }

    public async Task<Character> UpdateCharacter(int characterToBeUpdatedId, Character newCharacterValues)
    {
        CharacterValidators CharacterValidator = new();
        
        var validationResult = await CharacterValidator.ValidateAsync(newCharacterValues);
        if (!validationResult.IsValid)
            throw new ArgumentException(validationResult.Errors.ToString());

        Character CharacterToBeUpdated = await _unitOfWork.CharacterRepository.GetByIdAsync(characterToBeUpdatedId);

        if (CharacterToBeUpdated == null)
            throw new ArgumentException("Invalid Character ID while updating");

        CharacterToBeUpdated.Name = newCharacterValues.Name;
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

    public async Task<int> AttackEnemy(int characterId)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null)
            throw new ArgumentException("Invalid Character ID");

        int attack = (int)Math.Floor(character.Strenght * 0.25);
        
        return attack;
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

    public async Task<int> TakeDamage(int characterId, int damage)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null)
            throw new ArgumentException("Invalid Character ID");

        character.HP = character.HP - damage;

        if (character.HP <= 0)
        {
            _unitOfWork.CharacterRepository.Remove(character);
            await _unitOfWork.CommitAsync();
            return 1;
        }

        await _unitOfWork.CommitAsync();

        return -1;
    }

    public async Task<int> UpdateExp(int characterId, int exp)
    {
        Character character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);

        if (character == null)
            throw new ArgumentException("Invalid Character ID");

        character.Exp += exp;

        if (character.Exp >= 100)
        {
           await LevelUp(characterId);
           return 1;
        }

        await _unitOfWork.CommitAsync();

        return -1;
    }

}