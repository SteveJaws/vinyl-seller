[System.Serializable]
public class Day
{
    public int quota;
    public int time;

    public int winBonus;
    public int loseAmount;

    public Day(int quota, int time, int winBonus, int loseAmount)
    {
        this.quota = quota;
        this.time = time;
        this.winBonus = winBonus;
        this.loseAmount = loseAmount;
    }
}