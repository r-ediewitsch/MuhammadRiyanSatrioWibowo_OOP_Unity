using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxComponent : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    private InvincibilityComponent invincibilityComponent;

    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public void Damage(float damage)
    {
        if (invincibilityComponent == null || !invincibilityComponent.isInvincible)
        {
            healthComponent.Subtract(damage);
        }
    }
    
}
