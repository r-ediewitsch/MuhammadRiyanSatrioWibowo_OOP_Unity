using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    private Vector2 direction;

    [Header("Boss Weapon")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shootInterval = 2f;
    private IObjectPool<Bullet> objectPool;
    private float shootTimer;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,       
            OnTakeFromPool,     
            OnReturnedToPool,   
            OnDestroyBullet,    
            collectionCheck,    
            defaultCapacity,    
            maxSize             
        );
        
        shootTimer = shootInterval; 
    }

    void Start()
    {
        SetRandomSpawnPosition(); 
        shootTimer = shootInterval; 
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 180); 

        if (!IsOnScreen())
        {
            direction *= -1; 
            ClampPosition();
        }

        shootTimer -= Time.deltaTime; 

        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    private void Shoot()
    {
        Bullet bulletInstance = objectPool.Get();

        if (bulletInstance == null)
            return;

        bulletInstance.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletInstance.setDirection(Vector2.down); 

        bulletInstance.Deactivate();
    }

    Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    void OnTakeFromPool(Bullet bullet)
    {
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

    private void SetRandomSpawnPosition()
    {
        bool spawnOnLeft = Random.Range(0, 2) == 0;

        float minY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).y;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
        float randomY = Random.Range(minY, maxY);

        if (spawnOnLeft)
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x - 1, randomY);
            direction = Vector2.right; 
        }
        else
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x + 1, randomY);
            direction = Vector2.left;
        }
    }

    private void ClampPosition()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        viewportPos.x = Mathf.Clamp(viewportPos.x, -0.1f, 1.1f); 
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }

    private bool IsOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= -0.2 && screenPoint.x <= 1.2;
    }
}
