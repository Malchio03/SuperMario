using UnityEngine;
using TMPro;  // Assicurati di includere il namespace di TextMesh Pro

public class ScoreManagerTMP : MonoBehaviour
{
    // Il punteggio del giocatore
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void AddScore(int points)
    {
        // Aggiungi i punti al punteggio attuale
        score += points;

        // scoreText.text = $"MARIO\n{score}";
        //scoreText.text = "Score: " + score.ToString();
        scoreText.text = $"    Score: {score}";
    }
}
