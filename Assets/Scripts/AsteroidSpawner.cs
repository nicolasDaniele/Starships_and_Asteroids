using System.Collections.Generic;
using UnityEngine;
using MEC;

public class AsteroidSpawner : SingletonMonoBehaviour<AsteroidSpawner>
{
    [SerializeField] private float minSpawningTime = 1f;
    [SerializeField] private float maxSpawningTime = 3f;
    [SerializeField] private float minXSpaningPosition = 0f;
    [SerializeField] private float maxXSpaningPosition = 8f;
    [SerializeField] private float ySpawningPosition = 6f;
    [SerializeField] private GameObject asteroidPrefab;

    private CoroutineHandle spawningHandle;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        spawningHandle = Timing.RunCoroutine(SpawnAsteroid());
    }

    private IEnumerator<float> SpawnAsteroid()
    {
        while (true)
        {
            if(asteroidPrefab == null)
            {
                Debug.LogError("AsteroidSpawner: asteroidPrefab is null");
                Timing.KillCoroutines(spawningHandle);
                yield return Timing.WaitForOneFrame;
            }

            if (PoolsManager.Instance == null)
            {
                Debug.LogWarning("AsteroidSpawner: PoolsManager.Instance is null");
                yield return Timing.WaitForOneFrame;
            }

            Vector2 spawningPosition = new Vector2(Random.Range(minXSpaningPosition, maxXSpaningPosition), ySpawningPosition);
            PoolsManager.Instance.Instantiate(asteroidPrefab, spawningPosition, Quaternion.identity);
            float spawningInterval = Random.Range(minSpawningTime, maxSpawningTime);
            yield return Timing.WaitForSeconds(spawningInterval);
        }
    }

    private void OnDisable()
    {
        Timing.KillCoroutines(spawningHandle);
    }
}