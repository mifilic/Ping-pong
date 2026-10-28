using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 30f;

    // Varijable za bodove igraèa
    public static int scoreLeft = 0;
    public static int scoreRight = 0;

    void Start()
    {
        ResetBall();
    }

    void Update()
    {
        // Ako loptica ode iza desnog reketa (X > 90), gol za lijevog igraèa
        if (transform.position.x > 84.6f)
        {
            scoreLeft++;
            Debug.Log("Gol za lijevog! Rezultat: " + scoreLeft + " : " + scoreRight);
            ResetBall();
        }

        // Ako loptica ode iza lijevog reketa (X < -90), gol za desnog igraèa
        if (transform.position.x < -84.6f)
        {
            scoreRight++;
            Debug.Log("Gol za desnog! Rezultat: " + scoreLeft + " : " + scoreRight);
            ResetBall();
        }
    }

    void ResetBall()
    {
        // 1. Vrati lopticu u centar ekrana (0, 0)
        transform.position = Vector2.zero;

        // 2. Zaustavi trenutnu brzinu
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        // 3. Pošalji lopticu nasumièno lijevo ili desno
        float randomDirection = Random.Range(0, 2) == 0 ? 1f : -1f;
        rb.linearVelocity = new Vector2(randomDirection, 0) * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Provjera udarca u lijevi reket
        if (col.gameObject.name == "RacketLeft")
        {
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 direction = new Vector2(1, y).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }

        // Provjera udarca u desni reket
        if (col.gameObject.name == "RacketRight")
        {
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 direction = new Vector2(-1, y).normalized;
            GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }
    }

    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketHeight)
    {
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }
}