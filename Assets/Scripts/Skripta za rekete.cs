using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Brzina pomaka reketa
    public float speed = 30f;

    // Naziv osi ("Vertical" ili "Vertical 2")
    public string axisName = "Vertical";

    void FixedUpdate()
    {
        // Dohvatimo ulaz s tipkovnice (-1, 0 ili 1)
        float vertical = Input.GetAxisRaw(axisName);

        // Pomièemo reket preko fizike - zidovi æe ga automatski zaustaviti
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, vertical) * speed;
    }
}