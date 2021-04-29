using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float forceX = 500f;
    public float minForceY = 200f;
    public float maxForceY = 500f;
    public float torque = 100f;
    public float yLimit = -6f;
    public int numberOfAsteroidPieces;
    public GameObject asteroidPiece;
    public AudioClip[] destroyClips = new AudioClip[2];

    Rigidbody2D rb2d;
    AudioSource destroyAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        destroyAudio = GetComponent<AudioSource>();
        float randForceX = Random.Range(-forceX, forceX);
        float forceY = Random.Range(minForceY, maxForceY);
        float randTorque = Random.Range(-torque, torque);
        rb2d.AddForce(new Vector2(randForceX, -forceY));
        rb2d.AddTorque(randTorque);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= yLimit) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Randomize sfx clip
            int randClip = Random.Range(0, 2);
            destroyAudio.clip = destroyClips[randClip];
            destroyAudio.Play();

            // When shot, split into 2-5 asteroid pieces
            numberOfAsteroidPieces = Random.Range(2, 6);
            for (int i = 0; i <= numberOfAsteroidPieces; i++)
            {
                Instantiate(asteroidPiece, transform.position, Quaternion.identity);
            }

            GameController.score += 100;
            Destroy(gameObject, 2f);
            Destroy(collision.gameObject);
        }
    }
}
