using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    public int level = 1;

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
}

