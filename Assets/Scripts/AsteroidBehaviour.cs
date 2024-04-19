using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private float forceX = 500f;
    [SerializeField] private float minForceY = 200f;
    [SerializeField] private float maxForceY = 500f;
    [SerializeField] protected float torque = 100f;
    [SerializeField] protected float yLimit = -6f;
    [SerializeField] private int minNumberOfPieces = 2;
    [SerializeField] private int maxNumberOfPieces = 6;
    [SerializeField] private GameObject asteroidPiece;
    [SerializeField] protected AudioClip[] destroyClips = new AudioClip[2];

    protected Rigidbody2D rb2d;
    private AudioSource destroyAudio;

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

    void Update()
    {
        if (transform.position.y <= yLimit)
        {
            PoolsManager.Instance.Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // A SOUND MANAGER SHOULD DO THIS
            //int randClip = Random.Range(0, 2);
            //destroyAudio.clip = destroyClips[randClip];
            //destroyAudio.Play();

            // A SCORE MANAGER SHOULD DO THIS
            GameController.score += 100;

            // SHOULD DESTROY BULLET HERE?
            PoolsManager.Instance.Destroy(collision.gameObject);

            HandleBeingShot();
        }
    }

    protected virtual void HandleBeingShot()
    {
        SplitIntoPieces();
        DestroySelf();
    }

    private void SplitIntoPieces()
    {
        int numberOfAsteroidPieces = Random.Range(minNumberOfPieces, maxNumberOfPieces);
        for (int i = 0; i <= numberOfAsteroidPieces; i++)
        {
            PoolsManager.Instance.Instantiate(asteroidPiece, transform.position, Quaternion.identity);
        }
    }

    protected virtual void DestroySelf()
    {
        PoolsManager.Instance.Destroy(gameObject);
    }
}
