using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVertical : Enemy
{
    [Header("Movement")]
    public float speed = 5f;
    private Vector2 direction = Vector2.up;

    void Start()
    {
        SetRandomSpawnPosition();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 180);

        if(IsOutOfScreen())
        {
            transform.position = new Vector2(transform.position.x, Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y + 1);
        }
    }

    private void SetRandomSpawnPosition()
    {
        float minX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        float randomX = Random.Range(minX, maxX);

        transform.position = new Vector2(randomX, Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y + 1);
    }

    private bool IsOutOfScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.y < -0.2f; 
    }
}
