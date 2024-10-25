using System.Collections;
using UnityEngine;

public class BlocItem : MonoBehaviour
{
    // punti oggetto
    //public int points = 100;

    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;



        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition;

        rigidbody.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;

        // AGGIUNTO ORA 
       // AddPoints();
    }

    // aggiunto ora
    /*private void AddPoints()
    {
        // Trova lo ScoreManager nella scena e aggiungi i punti
        ScoreManagerTMP scoreManager = FindObjectOfType<ScoreManagerTMP>();
        if (scoreManager != null)
        {
            scoreManager.AddScore(points); // Aggiungi i punti specificati
        }
    }
    */
}
