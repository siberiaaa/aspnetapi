using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private CharacterRepository  _characterRepository;
        
        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public ICharacterRepository CharacterRepository => _characterRepository ??= new CharacterRepository(_context);
        
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }