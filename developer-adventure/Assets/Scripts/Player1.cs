using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    
    private float moveSpeed; // Deixei privado para ficar mais facil deu mexer no Script BY:Diego
    private float jumpForce; // Deixei este privado tambem pelo mesmo motivo
    public bool isGround;

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

        moveSpeed = 2f; // Velocidade definida por padr�o para anima��o Walk
        jumpForce = 50f; // Altura definida por padr�o para anima��o jump

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        // moveLeft = Input.GetKey(KeyCode.LeftArrow); implementa��o antiga
        // moveRight = Input.GetKey(KeyCode.RightArrow); implementa��o antiga
        moveHorizontal = Input.GetAxisRaw("Horizontal"); //m�todo de importa��o teclas teclados,joystick etc..

        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));// serve para ativar a anima��o walk
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
