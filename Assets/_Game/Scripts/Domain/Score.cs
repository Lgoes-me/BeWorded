public class Score
{
    public int BaseScore { get; set; }
    public int BaseMultiplier { get; set; }
    
    public int TotalScore { get; set; }

    public Score(int baseScore, int baseMultiplier)
    {
        BaseScore = baseScore;
        BaseMultiplier = baseMultiplier;
    }
}