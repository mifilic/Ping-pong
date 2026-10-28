using UnityEngine;

public class BallController : MonoBehaviour
{
    // Poèetna brzina loptice i stopa kojom se ubrzava
    public float baseSpeed = 60f;
    public float speedIncreaseRate = 1f; // Koliko se brzina poveæava svake sekunde
    private float currentSpeed;

    // Varijable za bodove igraèa
    public int scoreLeft = 0;
    public int scoreRight = 0;

    private Transform racketLeft;
    private Transform racketRight;
    private Rigidbody2D rb;

    void Start()
    {
        scoreLeft = 0;
        scoreRight = 0;
        rb = GetComponent<Rigidbody2D>();

        // Pronaði rekete u sceni
        GameObject rLeft = GameObject.Find("RacketLeft");
        GameObject rRight = GameObject.Find("RacketRight");

        if (rLeft != null) racketLeft = rLeft.transform;
        if (rRight != null) racketRight = rRight.transform;

        ResetBall();
    }

    void Update()
    {
        // 1. Postepeno poveæavaj brzinu loptice s vremenom
        currentSpeed += speedIncreaseRate * Time.deltaTime;

        // Održavaj smjer loptice, ali primjenjuj novu, veæu brzinu
        if (rb.linearVelocity != Vector2.zero)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
        }

        // 2. Provjera golova (ako loptica ode iza reketa)
        if (racketLeft != null && racketRight != null)
        {
            if (transform.position.x < racketLeft.position.x - 1f)
            {
                scoreRight++;
                Debug.Log("Gol za desnog igraèa! Rezultat: " + scoreLeft + " : " + scoreRight);
                ResetBall();
            }

            if (transform.position.x > racketRight.position.x + 1f)
            {
                scoreLeft++;
                Debug.Log("Gol za lijevog igraèa! Rezultat: " + scoreLeft + " : " + scoreRight);
                ResetBall();
            }
        }
    }

    void ResetBall()
    {
        // Vrati lopticu u centar
        transform.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        // Resetiraj brzinu na poèetnu vrijednost za novu rundu
        currentSpeed = baseSpeed;

        // Pošalji lopticu nasumièno lijevo ili desno
        float randomDirection = Random.Range(0, 2) == 0 ? 1f : -1f;
        rb.linearVelocity = new Vector2(randomDirection, 0) * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "RacketLeft")
        {
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 direction = new Vector2(1, y).normalized;
            rb.linearVelocity = direction * currentSpeed;
        }

        if (col.gameObject.name == "RacketRight")
        {
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 direction = new Vector2(-1, y).normalized;
            rb.linearVelocity = direction * currentSpeed;
        }
    }

    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketHeight)
    {
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }
}