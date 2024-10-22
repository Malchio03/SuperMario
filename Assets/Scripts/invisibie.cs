using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 direction = collision.transform.position - transform.position;
            if (direction.y < 0) // Mario è sotto il blocco
            {
                ActivateBlock();
            }
        }
    }

    private void ActivateBlock()
    {
        // Logica per attivare il blocco, ad esempio, suonare un'animazione o generare un oggetto
        Debug.Log("Blocco attivato!");
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = true;
    }
}
