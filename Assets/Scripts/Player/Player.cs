using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    PlayerMovement playerMovement;
    Animator animator;
    public Weapon currentWeapon;
    
    void Awake()
    {
        if(Instance != null && Instance != this) 
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        GameObject engineEffect = GameObject.Find("EngineEffect");
        if(engineEffect != null) 
        {
            animator = engineEffect.GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        playerMovement.Move();
    }

    void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }

    void OnDestroy()
    {
        Debug.Log("Player Destroyed: " + this.gameObject.name);
    }
}
