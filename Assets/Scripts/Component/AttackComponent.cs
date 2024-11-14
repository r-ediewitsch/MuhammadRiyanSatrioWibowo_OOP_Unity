using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [Header("Attack Settings")]
    
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag)) 
            return;

        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
        if (invincibility != null)
        {
            invincibility.StartInvincibility();
        }

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            hitbox.Damage(damage);
        }
    }
}
