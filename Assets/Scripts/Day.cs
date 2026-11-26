[System.Serializable]
public class Day
{
    public int quota;
    public int time;

    public Day(int quota, int time)
    {
        this.quota = quota;
        this.time = time;
    }
}