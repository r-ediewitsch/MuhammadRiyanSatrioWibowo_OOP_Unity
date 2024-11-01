using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    PlayerMovement playerMovement;
    Animator animator;
    
    void Awake()
    {
        if(Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        GameObject engineEffect = GameObject.Find("EngineEffect");
        if(engineEffect != null) {
            animator = engineEffect.GetComponent<Animator>();
        }
        else {
            Debug.LogError("EngineEffect not found!");
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
}
