using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed;
    public int JumpPower;
    Vector2 vecMove;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        
    }
    public void Jump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }
    }

    public void Moviment(InputAction.CallbackContext value) 
    {
        vecMove = value.ReadValue<Vector2>();
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(vecMove.x * speed, rb.velocity.y);
    }

    void Flip()
    {
        if (vecMove.x < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
        if (vecMove.x > 0.01f) transform.localScale = new Vector3(1, 1, 1);
    }
}
