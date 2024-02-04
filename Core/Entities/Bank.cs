namespace Core.Entities;

public class Bank
{
    public int ID { get; set; }
    public int PlayerBalance { get; set; }
    public int Interest { get; set; }
    public int Loan { get; set; }
    public int SecurityLevel { get; set; }
}