using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannonController : MonoBehaviour
{
    public float minRot = 20f;
    public float maxRot = 350f;
    public float rotationSpeed = 3f;
    public float shootingTime = 2f;
    public float shootForce = 7f;
    public GameObject explosion;
    public GameObject bullet;
    public GameController game;

    private GameObject explosionSmoke;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("ShootAnimation", 2, shootingTime);
        //StartCoroutine("RotateCannon");
    }

    void ShootAnimation()
    {
        if (game.state == GameController.GameStates.BOSSFIGHT)
        {
            anim.SetTrigger("Shoot");
        }
    }

    void Shoot()
    {
        // Spawns bullet
        explosionSmoke =  Instantiate(explosion, transform.position + 
                          new Vector3(-2f, 0.05f, 0), 
                          Quaternion.Euler(0, 0, 90));
        explosionSmoke.transform.localScale = new Vector3(0.5f, 1, 1);
        explosionSmoke.transform.SetParent(this.transform);
        Instantiate(bullet, transform.position +
                          new Vector3(-2f, 0.05f, 0),
                          Quaternion.identity);
    }

    void StopExplosion()
    {
        Destroy(explosionSmoke);
    }

    // Cannon Rotation
    IEnumerator RotateCannon()
    {
        while (true)
        {
            while (transform.eulerAngles.z <= maxRot)
            {
                Debug.Log("Rotating. Z = " + transform.eulerAngles.z);
                transform.eulerAngles += new Vector3(0, 0, rotationSpeed);
                yield return new WaitForSeconds(0.01f);
            }
            
             while (transform.rotation.z >= minRot)
             {
                Debug.Log("Rotating. Z = " + transform.eulerAngles.z);
                transform.eulerAngles -= new Vector3(0, 0, rotationSpeed);
                yield return new WaitForSeconds(0.01f);
             }

            Debug.Log("End of rotation. Z = " + transform.eulerAngles.z);
        }
    }
}
