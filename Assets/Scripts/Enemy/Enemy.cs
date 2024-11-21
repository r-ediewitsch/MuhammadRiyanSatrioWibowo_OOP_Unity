using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    public int level = 1;

    public CombatManager combatManager;
    private Transform playerTransform;
    private Rigidbody2D rb;

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
        combatManager.RegisterEnemyDeath();
    }
}

