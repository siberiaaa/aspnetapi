namespace Core.Entities;

public class Enemy
{
    public int ID { get; set; }
    public string UrlImage { get; set; }
    public string Name { get; set; }
    public int HP { get; set; }
    public int Level { get; set; }
    public int Reward { get; set; } // ! Temporalmente int
    public string Abilities { get; set; }
}