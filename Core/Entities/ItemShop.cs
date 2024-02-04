namespace Core.Entities; 

public class ItemShop
{
    public int ID { get; set; }
    public Item Item { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}