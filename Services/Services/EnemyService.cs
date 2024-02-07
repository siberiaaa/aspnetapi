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

    public async Task<Enemy> GetEnemyById(int id)
    {
        return await _unitOfWork.EnemyRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Enemy>> GetAll()
    {
        return await _unitOfWork.EnemyRepository.GetAllAsync();
    }

    public async Task<Enemy> CreateEnemy(Enemy newEnemy)
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

    public async Task<Enemy> UpdateEnemy(int enemyToBeUpdatedId, Enemy newEnemyValues)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteEnemy(int enemyId)
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

        int attack = (int)Math.Floor(enemy.Level * 0.5);
        
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