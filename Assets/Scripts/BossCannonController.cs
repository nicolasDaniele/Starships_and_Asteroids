﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannonController : MonoBehaviour
{
    public float minRot = 0f;
    public float maxRot = 20f;
    public float rotationSpeed = 0.4f;
    public float shootForce = 7f;
    public GameObject bullet;
    public GameController game;
    public AudioClip[] shotClips = new AudioClip[4];

    Animator anim;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        anim.enabled = false;
    }

    private void Update()
    {
        if (game.state != GameController.GameStates.BOSSFIGHT)
        {
            // Cancel animations and stuff in END mode
            CancelInvoke();
            StopCoroutine("RotateCannon");
            anim.enabled = false;
        }
    }

    void ShootAnimation()
    {
        if (game.state == GameController.GameStates.BOSSFIGHT)
        {
            anim.enabled = true;
        }
    }

    public void StartRotationPatern()
    {
        Invoke("ShootAnimation", 0);
        StartCoroutine("RotateCannon");
    }
    void Shoot()
    {
        // Randomize sfx clip
        int randClip = Random.Range(0, 4);
        source.clip = shotClips[randClip];
        source.Play();
        // Spawns bullet
        Instantiate(bullet, transform.position +
                          new Vector3(-2f, 0.05f, 0),
                          transform.localRotation);
    }


    // Cannon Rotation
    IEnumerator RotateCannon()
    {
        while (true)
        {
            while (transform.localEulerAngles.z >= minRot)
            {
                transform.localEulerAngles -= new Vector3(0, 0, rotationSpeed);
                yield return new WaitForSeconds(0.01f);
            }
            
             while (transform.localEulerAngles.z <= maxRot)
             {
                transform.localEulerAngles += new Vector3(0, 0, rotationSpeed);
                yield return new WaitForSeconds(0.01f);
             }
        }
    }
}
