using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public GameController game;
    public float shootTime = 0.3f;

    bool canShoot;
    AudioSource shotAudio;
    public AudioClip[] shotClips = new AudioClip[3];

    private void Start()
    {
        shotAudio = GetComponent<AudioSource>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawns bullet
        if (Input.GetButtonDown("Fire1") && canShoot &&
            (game.state == GameController.GameStates.PLAY || 
            game.state == GameController.GameStates.BOSSFIGHT))
        {
            // Randomize sfx clip
            int randClip = Random.Range(0, 3);
            shotAudio.clip = shotClips[randClip];
            shotAudio.Play();
            Instantiate(bullet, transform.position, Quaternion.identity);
            canShoot = false;
            Invoke("EnableShoot", shootTime);
        }
    }

    void EnableShoot()
    {
        canShoot = true;
    }
}
