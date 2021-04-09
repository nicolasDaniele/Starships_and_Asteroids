using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeSpawnerController : MonoBehaviour
{
    public GameObject ship;
    public GameObject bee;
    public float xPos = 9.5f;
    public float minYPos = -4f;
    public float maxYPos = 0f;
    Vector3 beePos;

    // Start is called before the first frame update
    void Start()
    {
        float randYPos = Random.Range(minYPos, maxYPos + 1);
        beePos = new Vector3(xPos, randYPos, 0);
    }
     
    public GameObject spawnEnemyBee()
    {
        return Instantiate(bee, beePos, Quaternion.Euler(0, 0, -90));
    }
}
