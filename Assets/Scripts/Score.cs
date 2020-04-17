using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Score
{
    Text scoreText;
    public int scoreResult;

    public Score (Text scoreResult)
    {
        this.scoreText = scoreResult;
        this.scoreResult = 0;
    }

    public void AddScore(int amount)
    {
        scoreResult += amount;
        scoreText.text = scoreResult.ToString();
    }
}
