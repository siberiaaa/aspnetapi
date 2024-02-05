using Core.Interfaces.Repositories;

namespace Core.Interfaces;
public interface IUnitOfWork : IDisposable
    {
        ICharacterRepository CharacterRepository { get; }
        IEnemyRepository EnemyRepository { get; }
        Task<int> CommitAsync();
    }