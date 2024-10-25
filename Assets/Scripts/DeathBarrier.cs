using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(1f); // cambiare a 3f se metti il sound
        } else
        {
            Destroy(other.gameObject);
        }
    }
}
