using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    Vector2 bulletDirection;
    private IObjectPool<Bullet> objectPool;
    public IObjectPool<Bullet> ObjectPool {set => objectPool = value;}
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool OnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.x >= 0 && screenPoint.x <= 1;
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(3f));
    }

    private IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay); 
        
        Rigidbody2D buffer_rb = GetComponent<Rigidbody2D>();
        buffer_rb.velocity = new Vector3(0f, 0f, 0f);
        buffer_rb.angularVelocity = 0f;
        
        objectPool.Release(this);
    }

    public void setDirection(Vector2 direction)
    {
        bulletDirection = direction;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(objectPool != null)
        {
            objectPool.Release(this); 
        }
    }

    void Update()
    {
        if (!OnScreen())
        {
            if(objectPool != null)
            {
                objectPool.Release(this); 
            }
        }

        rb.velocity = bulletDirection * bulletSpeed;
    }
}
