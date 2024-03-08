using Core.Entities;
using Core.Interfaces;
using Core.Enum;
using Core.Interfaces.Services;
using Services.Validators;

namespace Services.Services;

public class CharacterTypeService : ICharacterTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    public CharacterTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CharacterType> Create(CharacterType newCharacterType)
    {
        CharacterTypeValidators validator = new();

        var validationResult = await validator.ValidateAsync(newCharacterType);
        if (validationResult.IsValid)
        {
            await _unitOfWork.CharacterTypeRepository.AddAsync(newCharacterType);
            await _unitOfWork.CommitAsync();
        }
        else
        {
            throw new ArgumentException(validationResult.Errors.ToString());
        }

        return newCharacterType;
    }

    public async Task Delete(int characterTypeId)
    {
        CharacterType characterType = await _unitOfWork.CharacterTypeRepository.GetByIdAsync(characterTypeId);
        _unitOfWork.CharacterTypeRepository.Remove(characterType);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<CharacterType>> GetAll()
    {
        return await _unitOfWork.CharacterTypeRepository.GetAllAsync();
    }

    public async Task<CharacterType> GetById(int id)
    {
        return await _unitOfWork.CharacterTypeRepository.GetByIdAsync(id);
    }

    public async Task<CharacterType> Update(int characterTypeToBeUpdatedId, CharacterType newCharacterTypeValues)
    {
        CharacterTypeValidators validators = new();
        
        var validationResult = await validators.ValidateAsync(newCharacterTypeValues);
        if (!validationResult.IsValid)
            throw new ArgumentException(validationResult.Errors.ToString());

        CharacterType CharacterTypeToBeUpdated = await _unitOfWork.CharacterTypeRepository.GetByIdAsync(characterTypeToBeUpdatedId);

        if (CharacterTypeToBeUpdated == null)
            throw new ArgumentException("Invalid Character Type ID while updating");

        CharacterTypeToBeUpdated.Name = newCharacterTypeValues.Name;

        await _unitOfWork.CommitAsync();

        return await _unitOfWork.CharacterTypeRepository.GetByIdAsync(characterTypeToBeUpdatedId);
    }
}
