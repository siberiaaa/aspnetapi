using Core.Interfaces.Repositories;

namespace Core.Interfaces;
public interface IUnitOfWork : IDisposable
    {
        ICharacterRepository CharacterRepository { get; }
        Task<int> CommitAsync();
    }