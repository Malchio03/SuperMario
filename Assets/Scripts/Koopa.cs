using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled;
    private bool pushed;
    public int points = 150; // Punti che rilascia Koopa quando muore
    public int pointsOnShell = 100; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower)
            {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            } else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starpower)
                {
                    Hit();
                } else {
                    player.Hit();
                }
            }
        }
        else if(!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        AddPoints(pointsOnShell);

        shelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimationsSprites>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        // Aggiungi i punti quando il Koopa viene sconfitto
        AddPoints(points);


        GetComponent<AnimationsSprites>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    // Funzione per aggiungere i punti
    private void AddPoints(int pointsToAdd)
    {
        // Trova lo ScoreManager nella scena
        ScoreManagerTMP scoreManager = FindObjectOfType<ScoreManagerTMP>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(points); // Aggiungi 150 punti
        }
    }


    /* private void OnBecameInvisible()
     {
         if (pushed)
         {
             Destroy(gameObject);
         }
     }
    */
}
