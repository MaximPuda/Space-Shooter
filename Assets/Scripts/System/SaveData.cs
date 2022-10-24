[System.Serializable]
public class SaveData
{
    public int TotalPoints { get; private set; }
    public int[] LevelsStatus { get; private set; }

    public SaveData(int points, Level[] levels)
    {
        TotalPoints = points;

        LevelsStatus = new int[levels.Length];
        for (int i = 0; i < levels.Length; i++)
            LevelsStatus[i] = (int)levels[i].Status;
    }
}
