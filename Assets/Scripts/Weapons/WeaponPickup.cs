using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;
    public static Weapon currentWeapon;

    void Awake()
    {
        if(weapon == null) 
        {
            weapon = Instantiate(weaponHolder);
        }
    }

    void Start()
    {
        if(weapon != null) 
        {
            TurnVisual(false, weapon);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            if (player.currentWeapon != null)
            {
                TurnVisual(false, player.currentWeapon);  
                player.currentWeapon.transform.SetParent(null); 
            } 

            if (weapon != null)
            {
                TurnVisual(true, weapon);  
                weapon.transform.SetParent(player.transform);  
                weapon.transform.localPosition = new Vector3(0, 0, 1);  
            }

            player.currentWeapon = weapon; 
        }
    }
    }

    void TurnVisual(bool on)
    {
        foreach(var component in weapon.GetComponents<Component>())
        {
            if(component is Behaviour behaviour) 
            {
                behaviour.enabled = on;
            }
        }

        if (weapon != null)
        {
            var renderer = weapon.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = on;
            }
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        foreach(var component in weapon.GetComponents<Component>())
        {
            if(component is Behaviour behaviour) 
            {
                behaviour.enabled = on;
            }
        }

        if (weapon != null)
        {
            var renderer = weapon.GetComponent<Renderer>(); 
            if (renderer != null)
            {
                renderer.enabled = on; 
            }
        }
    }
}