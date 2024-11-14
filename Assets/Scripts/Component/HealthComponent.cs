using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    float health {set; get;}

    void Awake()
    {
        health = maxHealth;
    }

    public void Subtract(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
