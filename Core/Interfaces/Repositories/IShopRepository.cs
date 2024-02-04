using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IShopRepository : IBaseRepository<Shop>
{
    Task SellItem(Shop shop, ItemShop item);
}