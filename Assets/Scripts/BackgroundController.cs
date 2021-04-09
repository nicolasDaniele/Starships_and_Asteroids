using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float speed = 2f;
    public GameController game;

    // Update is called once per frame
    void Update()
    {
        if (game.state == GameController.GameStates.PLAY)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
