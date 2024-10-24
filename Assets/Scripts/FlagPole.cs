using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(player));
        }

    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {

    }

    private IEnumerator MoveTo(Transform subject, Vector3 to)
    {

    }
}
