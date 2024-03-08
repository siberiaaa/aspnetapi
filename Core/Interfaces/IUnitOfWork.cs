using Core.Interfaces.Repositories;

namespace Core.Interfaces;
public interface IUnitOfWork : IDisposable
    {
        ICharacterRepository CharacterRepository { get; }
        IEnemyRepository EnemyRepository { get; }
        ICharacterTypeRepository CharacterTypeRepository { get; }
        Task<int> CommitAsync();
    }