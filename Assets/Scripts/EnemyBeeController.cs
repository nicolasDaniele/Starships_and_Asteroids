using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeController : MonoBehaviour
{
    public float speed = 3.5f;
    public float xLimit = -10f;
    public float attackDistance = 10f;
    public float attackSpeed = 7f;
    public AudioClip[] dieClips = new AudioClip[2];

    ParticleSystem explosion;
    GameObject ship;
    AudioSource dieAudio;
    bool attackMode = false;



    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.Find("Ship");
        dieAudio = GetComponent<AudioSource>();
        explosion = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // If it gets close enough to the player, it chases the player (attack mode)
        if (ship != null &&
      Vector3.Distance(transform.position, ship.transform.position) <= attackDistance)
        {
            attackMode = true;
        }
        if (ship != null && attackMode)
        {
            Vector3 targetPos = ship.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, attackSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

        // Exceeds screen limit
        if (transform.position.x <= xLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When shot, it dies
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Randomize sfx clip
            int randClip = Random.Range(0, 2);
            dieAudio.clip = dieClips[randClip];
            dieAudio.Play();
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            explosion.Play();
            GameController.score += 100;
            Destroy(gameObject, 1f);
        }
    }
}
