namespace Core.Entities;

public class Inventory
{
    public int ID { get; set; }
    public int Capacity { get; set; }
    public List<Item> Items { get; set; }
    public int Weight { get; set; }
}