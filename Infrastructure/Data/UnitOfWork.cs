using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private CharacterRepository  _characterRepository;
        private EnemyRepository  _enemyRepository;
        
        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        // ! Si es null devuelvo lo que esta a la izquierda, si no es null devuelvo lo que está a la derecha (al revés creo que era)
        public ICharacterRepository CharacterRepository => _characterRepository ??= new CharacterRepository(_context);

        public IEnemyRepository EnemyRepository => _enemyRepository ??= new EnemyRepository(_context);

        public async Task<int> CommitAsync()
            {
                return await _context.SaveChangesAsync();
            }

            public void Dispose()
            {
                _context.Dispose();
            }
    }