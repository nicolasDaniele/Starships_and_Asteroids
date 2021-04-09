using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoController : MonoBehaviour
{
    Animator anim;
    public float eruptingTime = 2.5f;
    public float xLimit = -9.75f; // Screen limit
    public float shootRockForce = 5f;
    public GameObject volcanoRock;
    public GameObject volcanoExplosion;

    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("Erupt", 5f, eruptingTime);
    }

    private void Update()
    {   
        // Destroy when exceeds screen limits
        if (transform.position.x <= xLimit)
        {
            Destroy(gameObject);
        }
    }

    void Erupt()
    {
        anim.SetTrigger("Erupt");
    }

    void Shoot()
    {
        // Spawns volcano rock
        explosion = Instantiate(volcanoExplosion, transform.position + 
                                new Vector3(0.2f, 1.9f, 0), 
                                Quaternion.identity);
        explosion.transform.SetParent(this.transform);
        Instantiate(volcanoRock, transform.position + new Vector3(0, 1.75f, 0), Quaternion.identity);
    }


    void StopExplosion()
    {
        Destroy(explosion);
    }
}
