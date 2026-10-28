using UnityEngine;

public class BallController : MonoBehaviour
{
    // Brzina loptice
    public float speed = 30f;

    void Start()
    {
        // Pokretanje loptice prema desnoj strani na poèetku igre
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // 1. Provjera udarca u LIJEVI reket
        if (col.gameObject.name == "RacketLeft")
        {
            // Izraèunaj y (1 = vrh, 0 = sredina, -1 = dno reketa)
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            // Smjer ide desno (x = 1) i pod kutem y
            Vector2 direction = new Vector2(1, y).normalized;

            // Zadržava stalnu brzinu bez usporavanja
            GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }

        // 2. Provjera udarca u DESNI reket
        if (col.gameObject.name == "RacketRight")
        {
            // Izraèunaj y (1 = vrh, 0 = sredina, -1 = dno reketa)
            float y = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

            // Smjer ide lijevo (x = -1) i pod kutem y
            Vector2 direction = new Vector2(-1, y).normalized;

            // Zadržava stalnu brzinu bez usporavanja
            GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
        }
    }

    // Pomoæna metoda za izraèun kuta odbijanja
    private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketHeight)
    {
        // ballPosition.y - racketPosition.y daje relativnu visinu udarca
        return (ballPosition.y - racketPosition.y) / racketHeight;
    }
}