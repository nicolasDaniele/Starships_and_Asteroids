using UnityEngine;

public class AsteroidPieceBehaviour : AsteroidBehaviour
{
    [SerializeField] private float xLimit = 9.5f;
    [SerializeField] private float force = 300f;

    private ParticleSystem explosion;

    void Start()
    {
        // SET UP IN PARENT
        rb2d = GetComponent<Rigidbody2D>();
        explosion = GetComponentInChildren<ParticleSystem>();
        float randForceX = Random.Range(-force, force);
        float randForceY = Random.Range(-force, force);
        float randTorque = Random.Range(-torque, torque);
        rb2d.AddForce(new Vector2(randForceX, -randForceY));
        rb2d.AddTorque(randTorque);
    }

    void Update()
    {
        // MAKE A COMPONENT TO DESTROY WHEN GETTING OUT OF THE SCREEN
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (xPos >= xLimit || xPos <= -xLimit || yPos >= yLimit || yPos <= -yLimit)
        {
            Destroy(gameObject);
        }
    }

    protected override void HandleBeingShot()
    {
        explosion.Play();
        DestroySelf();
    }
}