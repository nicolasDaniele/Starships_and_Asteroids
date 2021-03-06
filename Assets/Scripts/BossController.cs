using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 3f;
    public int life = 20;
    public GameController game;
    public GameObject player;
    public ParticleSystem[] partSystems = new ParticleSystem[3];
    public SpriteRenderer cannonSRend;
    public AudioClip[] clipsArr = new AudioClip[4];

    SpriteRenderer sRend;
    AudioSource source;
    Animator camAnim;
    Vector3 minValues;
    Vector3 maxValues;
    Camera cam;
    float minY;
    float maxY;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        camAnim = cam.GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0 && game.state != GameController.GameStates.WIN)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            life--;
            // Randomize sfx clip
            int randClip = Random.Range(1, 4);
            source.clip = clipsArr[randClip];
            source.Play();
            // Disappear for a little while (visual feedback)
            if (sRend != null && cannonSRend != null)
            {
                sRend.enabled = false;
                cannonSRend.enabled = false;
                if (life > 0)
                {
                    Invoke("EnableSpriteRend", 0.2f);
                }
                
            }
        }
    }

    private void Die()
    {
        foreach (ParticleSystem ps in partSystems)
        {
            ps.Play();
        }

        StopCoroutine("StartMovePatern");
        game.state = GameController.GameStates.WIN;
        GameController.score += 1000;
        source.clip = clipsArr[0];
        source.Play();
        //sRend.enabled = false;
        Destroy(GameObject.Find("Boss1Cannon"));
        Destroy(gameObject, 6f);

        if (this != null)
        {
            // ScreenShake
            camAnim.SetTrigger("FinalShake");
        }
    }


    public void StartMovePatern()
    {
        // Movement limits (from cam viewport)
        minValues = cam.WorldToViewportPoint(cam.transform.position) - new Vector3(0.4f, 0.4f, 0);
        maxValues = cam.WorldToViewportPoint(cam.transform.position) + new Vector3(0, 0.4f, 0);
        minY = cam.ViewportToWorldPoint(minValues).y;
        maxY = cam.ViewportToWorldPoint(maxValues).y;

        StartCoroutine("MovePatern");
    }

    void EnableSpriteRend()
    {
        sRend.enabled = true;
        cannonSRend.enabled = true;
    }

    // Move up and down
    IEnumerator MovePatern()
    {
        while (game.state == GameController.GameStates.BOSSFIGHT)
        {
            while (transform.position.y <= maxY)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                yield return new WaitForSeconds(0.01f);
            }

            while (transform.position.y >= minY)
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
