using UnityEngine;

// Questo script gestisce il side-scrolling della telecamera o di un altro oggetto
// seguendo la posizione del giocatore in un gioco 2D.

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
    }
        
    // importante per la telecamera
    private void LateUpdate() {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }
}
