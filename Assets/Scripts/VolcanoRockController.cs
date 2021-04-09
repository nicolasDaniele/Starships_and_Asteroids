using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoRockController : MonoBehaviour
{
    public float yLimit = 6f; // Screen limit
    public float force = 6f;
    Rigidbody2D rb2d;


    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Stage0").transform); // Set background as parent
        rb2d = GetComponent<Rigidbody2D>();
        float randTorque = Random.Range(-5, 6);
        rb2d.AddTorque(randTorque);
        rb2d.AddForce(new Vector2(0, force), ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // Destroy when exceeds screen limit
        if(transform.position.y >= yLimit)
        {
            Destroy(gameObject);
        }
    }
}
