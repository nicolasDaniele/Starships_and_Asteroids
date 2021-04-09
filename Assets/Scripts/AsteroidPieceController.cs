using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPieceController : MonoBehaviour
{
    public float torque = 100f;
    public float yLimit = 6f;
    public float xLimit = 9.5f;
    public float force = 300f;

    Rigidbody2D rb2d;
    SpriteRenderer sprt;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
        float randForceX = Random.Range(-force, force);
        float randForceY = Random.Range(-force, force);
        float randTorque = Random.Range(-torque, torque);
        rb2d.AddForce(new Vector2(randForceX, -randForceY));
        rb2d.AddTorque(randTorque);
    }

    // Update is called once per frame
    void Update()
    {
        // Screen Limits
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (xPos >= xLimit || xPos <= -xLimit || yPos >= yLimit || yPos <= -yLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When shot, they get destroyed
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameController.score += 50;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
