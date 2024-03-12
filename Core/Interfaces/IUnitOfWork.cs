using Core.Interfaces.Repositories;

namespace Core.Interfaces;
public interface IUnitOfWork : IDisposable
    {
        ICharacterRepository CharacterRepository { get; }
        IEnemyRepository EnemyRepository { get; }
        IUserRepository UserRepository { get; }
        ICharacterTypeRepository CharacterTypeRepository { get; }
        Task<int> CommitAsync();
    }