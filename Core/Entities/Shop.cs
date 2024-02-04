namespace Core.Entities; 

public class Shop
{
    public int ID { get; set; }
    public List<ItemShop> ShopStock { get; set; }
    public int Balance { get; set; }

}

