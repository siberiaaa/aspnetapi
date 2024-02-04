namespace Core.Entities;

public class Quest
{
    public int ID { get; set; }
    public string Name { get; set; }
    public List<QuestTask> Tasks { get; set; }
    public Reward Reward { get; set; }
    public string State { get; set; }
}



