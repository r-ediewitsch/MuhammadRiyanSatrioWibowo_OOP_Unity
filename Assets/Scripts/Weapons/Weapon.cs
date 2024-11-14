using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;


    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;


    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    void FixedUpdate() 
    {
        if(Time.time > timer && objectPool != null)
        {
            Bullet bulletInstance = objectPool.Get();

            if(bulletInstance == null)
                return;

            bulletInstance.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bulletInstance.setDirection(Vector2.up);
            
            bulletInstance.Deactivate();

            timer = Time.time + shootIntervalInSeconds;
        }
    }

    Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, parentTransform);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    void OnTakeFromPool(Bullet bullet)
    {
        if(bullet == null)
            return;
            
        bullet.gameObject.SetActive(true);
    }

    void OnReturnedToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

}
