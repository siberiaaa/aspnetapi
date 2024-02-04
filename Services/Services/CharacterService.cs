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
            Character Character = await _unitOfWork.CharacterRepository.GetByIdAsync(characterId);
            _unitOfWork.CharacterRepository.Remove(Character);
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

            CharacterToBeUpdated.IQ = newCharacterValues.IQ;
            CharacterToBeUpdated.Name = newCharacterValues.Name;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.CharacterRepository.GetByIdAsync(characterToBeUpdatedId);
        }

        public async Task<Character> LevelUp(int characterToBeUpdatedId, Character newCharacterValues)
        {
            CharacterValidators PersonajeValidator = new();
            
            var validationResult = await PersonajeValidator.ValidateAsync(newCharacterValues);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.ToString());

            Character CharacterToBeUpdated = await _unitOfWork.CharacterRepository.GetByIdAsync(characterToBeUpdatedId);

            if (CharacterToBeUpdated == null)
                throw new ArgumentException("Invalid Character ID while updating");

            CharacterToBeUpdated.Strenght = newCharacterValues.Strenght;
            CharacterToBeUpdated.HP = newCharacterValues.HP;
            CharacterToBeUpdated.Exp = newCharacterValues.Exp;
            CharacterToBeUpdated.MP = newCharacterValues.MP;
            CharacterToBeUpdated.IQ = newCharacterValues.IQ;
            CharacterToBeUpdated.Level = newCharacterValues.Level;

            await _unitOfWork.CommitAsync();

            return await _unitOfWork.CharacterRepository.GetByIdAsync(characterToBeUpdatedId);
        }
}