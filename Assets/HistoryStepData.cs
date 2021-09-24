/// <summary>
/// Class that contains data of one row of history table
/// </summary>
[System.Serializable]
public class HistoryStepData
{
    public WarriorData warriorData;
    public int round;
    public int step;

    public HistoryStepData(WarriorData warriorData, int round, int step)
    {
        this.warriorData = warriorData;
        this.round = round;
        this.step = step;
    }
}