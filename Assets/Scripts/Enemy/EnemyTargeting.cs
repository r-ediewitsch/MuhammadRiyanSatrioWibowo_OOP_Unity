using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    [Header("Movement")]
    public float speed = 5f;

    private Transform player; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        SetRandomSpawnPosition(); 
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void SetRandomSpawnPosition()
    {
        bool spawnOnLeft = Random.Range(0, 2) == 0;

        float minY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
        float randomY = Random.Range(minY, maxY);

        if (spawnOnLeft)
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x - 1, randomY);
        }
        else
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x + 1, randomY);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); 
        }
    }
}
