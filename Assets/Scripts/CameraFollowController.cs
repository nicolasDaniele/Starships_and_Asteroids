using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public GameObject ship;
    public float moveTime = 15f;

    // Update is called once per frame
    void Update()
    {
        // Follow the player
        if (GameController.moveCamera && ship != null)
        {
            Vector3 newPos = new Vector3(transform.position.x, 
                                        ship.transform.position.y,
                                        transform.position.z);
            newPos.y = Mathf.Clamp(newPos.y, -5f, -0.5f);
            transform.position = Vector3.Lerp(transform.position, newPos, moveTime);
        }
    }
}
