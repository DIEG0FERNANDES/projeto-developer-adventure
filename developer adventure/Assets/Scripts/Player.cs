using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 2.0f;
    private bool isJumping = false;
    private bool isCrouching = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1;
        }

        // Movimentação
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb.AddForce(movement * speed);

        // Correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15.0f;
        }
        else
        {
            speed = 10.0f;
        }

        // Pular
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        // Agachar
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
