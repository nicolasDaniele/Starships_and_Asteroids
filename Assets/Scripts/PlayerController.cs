using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float vert;
    float horiz;
    float minX;
    float maxX;
    float minY;
    float maxY;
    public float speed = 10f;

    Camera cam;
    Animator anim;
    Animator camAnim;
    Rigidbody2D rb2d;
    SpriteRenderer sRend;
    AudioSource source;
    public ParticleSystem explosionParticle;
    public GameController game;
    public GameObject boss;
    public GameObject bossCannon;
    public AudioClip[] dieClips = new AudioClip[2];

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sRend = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        cam = Camera.main;
        camAnim = cam.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.state == GameController.GameStates.PLAY ||
            game.state == GameController.GameStates.BOSSFIGHT)
        {
            // Vertical movement
            vert = Input.GetAxis("Vertical");
            transform.position += new Vector3(0, vert * speed * Time.deltaTime, 0);

            // Horizontal movement
            horiz = Input.GetAxis("Horizontal");
            transform.position += new Vector3(horiz * speed * Time.deltaTime, 0, 0);
        }


        // Movement limits (from viewport)
        Vector3 minValues = cam.WorldToViewportPoint(cam.transform.position) - new Vector3(0.4f, 0.4f, 0);
        Vector3 maxValues = cam.WorldToViewportPoint(cam.transform.position) + new Vector3(0, 0.4f, 0);
        minX = cam.ViewportToWorldPoint(minValues).x;
        maxX = cam.ViewportToWorldPoint(maxValues).x;
        minY = cam.ViewportToWorldPoint(minValues).y;
        maxY = cam.ViewportToWorldPoint(maxValues).y;

        // Horizontal movement clamp
        float clampedXPos = Mathf.Clamp(transform.position.x, minX, maxX);
        // Vertical movement clamp
        float clampedYPos = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedXPos, clampedYPos, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If ship collides with enemy, asteroid or background, dies
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid")
            || collision.gameObject.CompareTag("Background"))
        {
            Die();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Camera can move or not when triggers a cameraMover
        if (collision.gameObject.CompareTag("CamMov"))
        {
            GameController.moveCamera = !GameController.moveCamera;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("EnemySpawner"))
        {
            collision.GetComponent<EnemyBeeSpawnerController>().spawnEnemyBee();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("BossTrigger"))
        {
            game.state = GameController.GameStates.BOSSFIGHT;
            boss.GetComponent<BossController>().StartMovePatern();
            bossCannon.GetComponent<BossCannonController>().StartRotationPatern();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("AsteroidsSpawner"))
        {
            game.InvokeRepeating("InstantiateAsteroid", 0.5f, game.asteroidSpawnTime);
            game.canvasTutorial.enabled = false;
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        camAnim.SetTrigger("Shake");
        // Randomize sfx clip
        int randClip = Random.Range(0, 2);
        source.clip = dieClips[randClip];
        source.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Die");
        float randXForce = Random.Range(-4, 0);
        float randYForce = Random.Range(-2, 3);
        float randTorque = Random.Range(-4, 5);
        rb2d.AddForce(new Vector2(randXForce, randYForce), ForceMode2D.Impulse);
        rb2d.AddTorque(randTorque);
        game.state = GameController.GameStates.END;
    }

    void Explode()
    {
        sRend.enabled = false;
        explosionParticle.Play();
        Destroy(gameObject, 1f);
    }
}
