using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] float speed = 4f;

    [SerializeField] float jumpForce = 15f;
    [SerializeField] bool isJump;
    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rbPlayer;
    private void Awake() {
        rbPlayer = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue);

        if (Input.GetButtonDown("Jump") && isGrounded()) 
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && isGrounded()) 
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.x * 0.5f);
        }
    }
    private void FixedUpdate() {
        Move();
    }
    void Move() {
        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.velocity = new Vector2(xMove * speed, rbPlayer.velocity.y);

        if (xMove > 0) {
            transform.eulerAngles = new Vector2(0,0);
        }
        else if(xMove < 0) {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private bool isGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
