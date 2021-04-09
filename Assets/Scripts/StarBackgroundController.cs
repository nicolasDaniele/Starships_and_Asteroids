using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBackgroundController : MonoBehaviour
{
    public float xMove = -0.3f;
    public float xOffset = 17.75f;
    public float xLimit = -17.75f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(xMove * Time.deltaTime, 0, 0); 

        if (transform.position.x <= xLimit)
        {
            transform.position =new Vector3(xOffset, 0, 0);
        }
    }
}
