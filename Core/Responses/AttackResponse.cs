using Core.Entities;

namespace Core.Responses;

public class AttackResponse
{
    public Character Character { get; set; }
    public Enemy Enemy { get; set; }
    public string Message { get; set; }
}