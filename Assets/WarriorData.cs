/// <summary>
/// Class that contains warrior characteristics
/// </summary>
[System.Serializable]
public class WarriorData
{
    
    public int cell;
    public int initiative;
    public int speed;
    public Allegiance allegiance;

    public WarriorData(int cell, int initiative, int speed)
    {
        this.cell = cell;
        this.initiative = initiative;
        this.speed = speed;
    }
}

public enum Allegiance
{
    Red,
    Blue
}

