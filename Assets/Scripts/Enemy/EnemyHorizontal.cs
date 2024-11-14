using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [Header("Movement")]
    public float speed = 5f;
    private Vector2 direction;

    void Start()
    {
        SetRandomSpawnPosition();
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
