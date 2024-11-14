using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        moveDirection = new Vector2(xInput, yInput).normalized;
        
        Vector2 targetVelocity = new Vector2(
            moveDirection.x * maxSpeed.x,
            moveDirection.y * maxSpeed.y
        );
        
        float acceleration = (moveDirection != Vector2.zero) ? 
            moveVelocity.magnitude - GetFriction().magnitude * Time.deltaTime : 
            GetFriction().magnitude * Time.deltaTime;
        
        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, acceleration);
        
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x), 
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );
        
        if(rb.velocity.magnitude < stopClamp.magnitude) {
            rb.velocity = Vector2.zero;
        }
    }

    public Vector2 GetFriction()
    {
        return (moveDirection != Vector2.zero) ? moveFriction : stopFriction; 
    }

    public void MoveBound()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(rb.position);

        pos.x = Mathf.Clamp(pos.x, 0.019f, 0.981f);
        pos.y = Mathf.Clamp(pos.y, 0, 0.9375f);
        // pos.x = Mathf.Clamp01(pos.x);
        // pos.y = Mathf.Clamp01(pos.y);
        
        rb.position = Camera.main.ViewportToWorldPoint(pos);
    }

    public bool IsMoving()
    {
        return rb.velocity.sqrMagnitude > 0.01f;
    }
    
    public void Update()
    {
        Move(); 
        MoveBound();
    }
}
