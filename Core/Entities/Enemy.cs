namespace Core.Entities;

public class Enemy
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public Reward Reward { get; set; }
    public string Abilities { get; set; }
}