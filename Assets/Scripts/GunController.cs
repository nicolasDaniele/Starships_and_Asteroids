using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootTime = 0.3f;

    bool canShoot;
    AudioSource shotAudio;
    public AudioClip[] shotClips = new AudioClip[3];

    private void OnEnable()
    {
        ShipBehaviour.OnShoot += Shoot;
    }

    private void Start()
    {
        shotAudio = GetComponent<AudioSource>();
        canShoot = true;
    }


    private void Shoot()
    {
        if(!canShoot)
        {
            return;
        }

        // HANDLE SFX PLAYING

        PoolsManager.Instance.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // CHECK COOLDOWN
        canShoot = false;
        //Invoke("EnableShoot", shootTime);
        StartCoroutine(nameof(EnableShoot));
    }

    private IEnumerator EnableShoot()
    {
        new WaitForSeconds(shootTime);
        canShoot = true;

        yield return null;
    }
}
