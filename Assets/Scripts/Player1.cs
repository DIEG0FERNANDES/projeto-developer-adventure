using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    
    private float moveSpeed; // Deixei privado para ficar mais facil deu mexer no Script BY:Diego
    private float jumpForce; // Deixei este privado tambem pelo mesmo motivo
    public bool isGround;
    public float Speed;
    public float RunSpeed;
    public float NormalSpeed;
    public bool IsRunning;
    private Vector2 crouchingSize;
    private Vector2 standingSize;
    private Vector2 crouchingOffset;
    private new BoxCollider2D collider;
    private Sprite crouching;
    private Sprite standing;
    private SpriteRenderer sprite;

    private Animator animate;

    private bool moveLeft;
    private bool moveRight;
    private float moveHorizontal;

    private Rigidbody2D rig;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        animate = gameObject.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        moveSpeed = 2f; // Velocidade definida por padr�o para anima��o Walk
        jumpForce = 50f; // Altura definida por padr�o para anima��o jump

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        // moveLeft = Input.GetKey(KeyCode.LeftArrow); implementaçao antiga
        // moveRight = Input.GetKey(KeyCode.RightArrow); implementação antiga
        moveHorizontal = Input.GetAxisRaw("Horizontal"); //método de importação teclas teclados,joystick etc..

        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));// serve para ativar a animação walk
    }
    private void FixedUpdate()
    {

        /**if (moveRight){
            transform.position += new Vector3(1 * moveSpeed * Time.deltaTime, 0, 0);
            sr.flipX = false;
        }
        else if (moveLeft) {
            transform.position += new Vector3(-1 * moveSpeed * Time.deltaTime, 0, 0);
            sr.flipX = true;
        }*/ //implementa��o de movimento antigo

        rig.velocity = new Vector2(moveHorizontal * moveSpeed, rig.velocity.y);

        if (moveHorizontal > 0.1f)
        {
            sr.flipX = false;
        }
        else if (moveHorizontal < -0.1f)
        {
            sr.flipX = true;
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift)) {
            IsRunning = true;
            Speed = RunSpeed;
            print("Running");
        }
        else
        {
            IsRunning = false;
            Speed = NormalSpeed;
            print("Not Running");
        }
        crouchingSize = new Vector2(1, 0.5f);
        crouchingOffset = new Vector2(0, -0.25f);
        standingSize = new Vector2(1, 1);
        crouchingOffset = Vector2.zero;

        //Start crouch
        if (Input.GetButtonDown("Crouch"))
        {
            sprite.sprite = crouching;
            collider.size = crouchingSize;
        }

        //Stop crouch
        if (Input.GetButtonUp("Crouch"))
        {
            sprite.sprite = standing;
            collider.size = standingSize;

        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            rig.AddForce(transform.up * jumpForce);
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGround = true;
        }
    }
    
}
