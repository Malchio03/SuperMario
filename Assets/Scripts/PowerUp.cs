using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int points = 100;    //aggiunt

    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AddPoints();
            Collect(other.gameObject);
        }
    }

    private void AddPoints()
    {
        // Trova lo ScoreManager nella scena e aggiungi i punti
        ScoreManagerTMP scoreManager = FindObjectOfType<ScoreManagerTMP>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(points); // Aggiungi i punti specificati
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;
            case Type.Starpower:
                player.GetComponent<Player>().Starpower();
                break;
        }

        Destroy(gameObject);
    }
}
