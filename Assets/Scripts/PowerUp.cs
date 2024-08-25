using UnityEngine;

public class PowerUp : MonoBehaviour
{
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
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                // Tdo
                break;
            case Type.ExtraLife:
                // Tdo
                break;
            case Type.MagicMushroom:
                // Tdo
                break;
            case Type.Starpower:
                // Tdo
                break;
        }

        Destroy(gameObject);
    }
}
