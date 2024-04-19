using UnityEngine;

public class BulletBehaviour: MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float xLimit = 9.5f; // Screen limit

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        // MAKE A COMPONENT FOR THE DESTRUCTION BY SCREEN
        // Destroy when gets out of the screen limits
        if (transform.position.x >= xLimit)
        {
            //Destroy(gameObject);
            PoolsManager.Instance.Destroy(gameObject);
        }
    }

    // HANDLE COLLISIONS
}
