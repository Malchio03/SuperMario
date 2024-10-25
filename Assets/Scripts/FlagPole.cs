using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public int nextWorld = 1;
    public int nextStage = 1;
    public int points = 1000;
    private SimpleTimer simpleTimer;

    private void Start()
    {
        simpleTimer = FindObjectOfType<SimpleTimer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (simpleTimer != null)
            {
                simpleTimer.isFlagReached = true;
            }

            ScoreManagerTMP scoreManager = FindObjectOfType<ScoreManagerTMP>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(points);
            }

            // Disabilita immediatamente movimento e input
            player.movement.enabled = false;

            // Disabilita l'animator e imposta uno stato statico
            if (player.TryGetComponent<Animator>(out Animator animator))
            {
                animator.enabled = false;
            }

            // Disabilita anche eventuali Rigidbody2D per prevenire movimenti fisici
            if (player.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            StartCoroutine(FlagpoleSequence(player));
        }
    }

    private IEnumerator FlagpoleSequence(Player player)
    {
        Vector3 contactPosition = player.transform.position;
        bool isHighContact = contactPosition.y > poleBottom.position.y;

        if (isHighContact)
        {
            // If Mario hits the pole from above, slide down first
            // Keep X position at pole
            Vector3 slideDownPosition = new Vector3(transform.position.x, poleBottom.position.y, contactPosition.z);

            // Face left before sliding
            Vector3 newScale = player.transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x);
            player.transform.localScale = newScale;

            yield return MoveTo(player.transform, slideDownPosition);
        }
        else
        {
            // If Mario hits from bottom, move one block right first
            Vector3 oneBlockPastPole = new Vector3(transform.position.x + 2f, contactPosition.y, contactPosition.z);
            yield return MoveTo(player.transform, oneBlockPastPole);
            Vector3 newScale = player.transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x);
            player.transform.localScale = newScale;
        }

        // Wait for flag to come down
        yield return MoveTo(flag, poleBottom.position);

        if (player.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.isKinematic = false;
        }

        if (player.TryGetComponent<Animator>(out Animator animator))
        {
            animator.enabled = true;
        }

        // Face right again
        Vector3 finalScale = player.transform.localScale;
        finalScale.x = Mathf.Abs(finalScale.x);
        player.transform.localScale = finalScale;

        // If we hit from above, we need to move right now
        if (isHighContact)
        {
            yield return MoveTo(player.transform, player.transform.position + Vector3.right);
        }

        // Move to castle sequence
        yield return MoveTo(player.transform, player.transform.position + Vector3.right);
        yield return MoveTo(player.transform, player.transform.position + Vector3.right + Vector3.down);
        yield return MoveTo(player.transform, castle.position);

        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 position)
    {
        while (Vector3.Distance(subject.position, position) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, position, speed * Time.deltaTime);
            yield return null;
        }
        subject.position = position;
    }
}