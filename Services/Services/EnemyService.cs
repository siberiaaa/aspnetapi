using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Interfaces.Services;
using Services.Validators;

namespace Services.Services;

public class EnemyService : IEnemyService
{
    private readonly IUnitOfWork _unitOfWork;
    public EnemyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Enemy> GetById(int id)
    {
        return await _unitOfWork.EnemyRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Enemy>> GetAll()
    {
        return await _unitOfWork.EnemyRepository.GetAllAsync();
    }

    public async Task<Enemy> Create(Enemy newEnemy)
    {
        EnemyValidators validator = new();

        var validationResult = await validator.ValidateAsync(newEnemy);
        if (validationResult.IsValid)
        {
            await _unitOfWork.EnemyRepository.AddAsync(newEnemy);
            await _unitOfWork.CommitAsync();
        }
        else
        {
            throw new ArgumentException(validationResult.Errors.ToString());
        }

        return newEnemy;
    }

    public async Task<Enemy> Update(int enemyToBeUpdatedId, Enemy newEnemyValues)
    {
        EnemyValidators EnemyValidators = new();
        
        var validationResult = await EnemyValidators.ValidateAsync(newEnemyValues);
        if (!validationResult.IsValid)
            throw new ArgumentException(validationResult.Errors.ToString());

        Enemy EnemyToBeUpdated = await _unitOfWork.EnemyRepository.GetByIdAsync(enemyToBeUpdatedId);

        if (EnemyToBeUpdated == null)
            throw new ArgumentException("Invalid Enemy ID while updating");

        EnemyToBeUpdated.Name = newEnemyValues.Name;
        EnemyToBeUpdated.UrlImage = newEnemyValues.UrlImage;
        EnemyToBeUpdated.Level = newEnemyValues.Level;
        EnemyToBeUpdated.HP = newEnemyValues.HP;
        EnemyToBeUpdated.Reward = newEnemyValues.Reward;
        EnemyToBeUpdated.Abilities = newEnemyValues.Abilities;

        await _unitOfWork.CommitAsync();

        return await _unitOfWork.EnemyRepository.GetByIdAsync(enemyToBeUpdatedId);
    }

    public async Task Delete(int enemyId)
    {
        Enemy enemy = await _unitOfWork.EnemyRepository.GetByIdAsync(enemyId);
        _unitOfWork.EnemyRepository.Remove(enemy);
        await _unitOfWork.CommitAsync();
    }

    public async Task<int> CounterAttack(int enemyId)
    {
        Enemy enemy = await _unitOfWork.EnemyRepository.GetByIdAsync(enemyId);

        if (enemy == null)
            throw new ArgumentException("Invalid enemy ID");

        int attack = (int)Math.Floor(enemy.Level * 1.15);
        
        return attack;
    }

    public async Task<IsAlive> TakeDamage(int enemyId, int damage)
    {
        Enemy enemy = await _unitOfWork.EnemyRepository.GetByIdAsync(enemyId);

        if (enemy == null)
            throw new ArgumentException("Invalid enemy ID");

        enemy.HP = enemy.HP - damage;
        await _unitOfWork.CommitAsync();

        if (enemy.HP <= 0)
        {
            return IsAlive.no;
        }

        return IsAlive.yes;
    }


}