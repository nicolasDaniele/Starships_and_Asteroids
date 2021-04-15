using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPieceController : MonoBehaviour
{
    public float torque = 100f;
    public float yLimit = 6f;
    public float xLimit = 9.5f;
    public float force = 300f;
    public AudioClip[] explodeClips = new AudioClip[2];

    Rigidbody2D rb2d;
    SpriteRenderer sprt;
    BoxCollider2D boxCol;
    ParticleSystem explosion;
    AudioSource explodeAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
        explosion = GetComponentInChildren<ParticleSystem>();
        boxCol = GetComponent<BoxCollider2D>();
        explodeAudio = GetComponent<AudioSource>();
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
            // Randomize sfx clip
            int randClip = Random.Range(0, 2);
            explodeAudio.clip = explodeClips[randClip];
            explodeAudio.Play();
            GameController.score += 50;
            Destroy(collision.gameObject);
            sprt.enabled = false;
            boxCol.enabled = false;
            explosion.Play();
            Destroy(gameObject, 1f);
        }
    }

}
