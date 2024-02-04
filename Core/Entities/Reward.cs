namespace Core.Entities;

public class Reward
{
    public int ID { get; set; }
    public int Exp { get; set; }
    public List<Item> Item { get; set; }
    public int Coins { get; set; }
}