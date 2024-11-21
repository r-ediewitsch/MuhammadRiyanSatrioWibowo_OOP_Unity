using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    public int level = 1;

    private Transform playerTransform;
    private Rigidbody2D rb;
    public CombatManager combatManager;
    public EnemySpawner spawner;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    void OnDestroy()
    {
        if (combatManager != null)
        {
            combatManager.RegisterEnemyDeath();
            spawner.RegisterEnemyDeath();
        }
    }
}

