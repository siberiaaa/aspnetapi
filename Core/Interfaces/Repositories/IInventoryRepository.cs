using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IInventoryRepository : IBaseRepository<Inventory>
{
    Task AddItem(Inventory inventory, Item item);
    Task RemoveItem(Inventory inventory, Item item);
}