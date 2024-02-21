using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int speed;
    float speedMultiplier;
    bool btnPressed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float targetSpeed = speed * speedMultiplier;
        rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
    }
    public void Move(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPressed = true;
            speedMultiplier = 1;
        }
        else if (value.canceled)
        {
            btnPressed = false;
            speedMultiplier = 0;
        }
    }

    public void Jump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPressed = true;
        }
        else if (value.canceled)
        {
            btnPressed = false;
        }
    }
}
