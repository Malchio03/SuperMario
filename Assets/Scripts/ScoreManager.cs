using UnityEngine;
using TMPro; 

public class ScoreManagerTMP : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void AddScore(int points)
    {
        score += points;

        // scoreText.text = $"MARIO\n{score}";
        //scoreText.text = "Score: " + score.ToString();
        scoreText.text = $"    Score: {score}";
    }
}
