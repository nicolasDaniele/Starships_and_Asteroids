using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15f;
    public float xLimit = 9.5f; // Screen limit


    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        // Destroy when gets out of the screen limits
        if (transform.position.x >= xLimit)
        {
            Destroy(gameObject);
        }
    }
}
