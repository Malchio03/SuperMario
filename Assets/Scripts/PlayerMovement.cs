using UnityEngine;

// Questo script gestisce il movimento orizzontale del giocatore in un gioco 2D utilizzando Unity.
// Utilizza un componente Rigidbody2D per controllare il movimento fisico dell'oggetto del giocatore.
// Inoltre, tiene conto dei bordi della telecamera per limitare il movimento del giocatore.

public class PlayerMovement : MonoBehaviour
{
    // Proprietà per la telecamera
    private new Camera camera;
    // metto new perche esiste gia rigidbody e potrebbe causare problemi
    private new Rigidbody2D rigidbody;
    private Collider2D capsuleCollider;


    // Vettore per tenere traccia della velocità del giocatore
    private Vector2 velocity;

    // Variabile per l'asse di input orizzontale
    private float inputAxis;

    // Velocità di movimento del giocatore
    public float moveSpeed = 8f;
    // Indica altezza massima 
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;  // tempo animazione salto
    // parabola (calcola la forza di salto)
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    //aggiunto ora
    public bool falling => velocity.y < 0f && !grounded;


    // Metodo chiamato all'inizializzazione dello script
    private void Awake() {
        // Ottiene il componente Rigidbody2D collegato all'oggetto del giocatore
        rigidbody = GetComponent<Rigidbody2D>();

        // Ottiene la camera principale
        camera = Camera.main;

        capsuleCollider = GetComponent<Collider2D>(); //aggiunto ora
    }

    private void OnEnable() // aggiunto ora
    {
        rigidbody.isKinematic = false;
        capsuleCollider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()    // aggiunt ora
    {
        rigidbody.isKinematic = true;
        capsuleCollider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    // Metodo chiamato una volta per frame
    private void Update() {
        // Gestisce il movimento orizzontale del giocatore
        HorizontalMovement();

        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded) {
            GroundedMovement();
        }

        ApplyGravity();
    }

    // Metodo per gestire il movimento orizzontale del giocatore
    private void HorizontalMovement() {
        // Ottiene il valore dell'asse orizzontale dall'input (ad es. tasti freccia o A/D)
        inputAxis = Input.GetAxis("Horizontal");

        // Calcola la nuova velocità del giocatore utilizzando MoveTowards per una transizione fluida
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0f;
        }

        if (velocity.x > 0f) {
            transform.eulerAngles = Vector3.zero;
        } else if(velocity.x < 0f){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    
    private void GroundedMovement() {
        velocity.y = Mathf.Max(velocity.y, 0f);
        //jumping = false   potrebbe dare problemi, meglio evitarla
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump")) {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity() {
        // Controlla se Mario sta cadendo o se il pulsante di salto non è premuto
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");

        // Moltiplicatore per aumentare la velocità di caduta se Mario sta cadendo
        float multiplier = falling ? 2f : 1f;

        // Aggiungi la gravità moltiplicata per il moltiplicatore e il deltaTime per la fisica
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate() {
        // Ottiene la posizione corrente del Rigidbody2D
        Vector2 position = rigidbody.position;

        // Aggiorna la posizione del giocatore aggiungendo la velocità calcolata
        position += velocity * Time.fixedDeltaTime;

        // Converte i bordi dello schermo in coordinate del mondo
        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Limita la posizione del giocatore ai bordi della telecamera
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        //position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        // Muove il Rigidbody2D alla nuova posizione calcolata
        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                // velocity.y = 10f;
                jumping = true;
            }
        } else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUP")) {

            if (transform.DotTest(collision.transform, Vector2.up))
            {               // Controlla se il giocatore sta saltando verso il basso e tocca un ostacolo
                velocity.y = 0f;
            }
        }
    }
}
