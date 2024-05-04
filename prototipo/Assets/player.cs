using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Movimento")]
    private float horizontal;
    private float speed = 5f;

    [Header("Pulo")]
    private float jumpForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) { 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump")&& rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.05f);
        }
    }

    void FixedUpdate()
    {        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
}
