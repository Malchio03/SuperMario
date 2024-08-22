using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public Sprite emptyBlock;
    public int maxHits = -1;

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        --maxHits;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        // to do
    }
}
