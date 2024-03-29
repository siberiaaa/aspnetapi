namespace Core.Entities;

public class Character
{
    public int ID { get; set; }
    public string? UrlImage { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public virtual CharacterType? CharacterType { get; set; }
    public int CharacterTypeId { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int IQ { get; set; }
    public int Strenght { get; set; }
    public int Agility { get; set; }
    public double Exp { get; set; }

}