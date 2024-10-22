using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;

    private void OnTriggerStay(Collider other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(enterKeyCode))
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }
    private IEnumerator Enter(tranform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;
    }

    private IEnumerator Move(Transform player, Vector3 endposition, Vector3 endscale)
    {

    }
}
