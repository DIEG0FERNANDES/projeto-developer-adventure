using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private CircleCollider2D circleCollider2D;
    [SerializeField] private LayerMask groundLayer;
    [Range(0, 10f)] [SerializeField] private float speed = 0f;

    float horizontal = 0f;
    float lastJumpY = 0f;

    private bool isFacingRight = true, jump = false,
                 jumpHeld = false, crouchHeld = false,
                 isUnderPlatform = false;

    [Range(0, 5f)] [SerializeField] private float fallLongMult = 0.85f;
    [Range(0, 5f)] [SerializeField] private float fallShortMult = 1.55f;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;

        if (isOnGround() && horizontal.Equals(0) && (crouchHeld || isUnderPlatform))
            GetComponent<Animator>().Play("CharacterCrouchIdle");
        else if (isOnGround() && (horizontal > 0 || horizontal < 0) && (crouchHeld || isUnderPlatform))
            GetComponent<Animator>().Play("CharacterCrouch");
        else if (isOnGround() && horizontal.Equals(0))
            GetComponent<Animator>().Play("CharacterIdle");
        else if (isOnGround() && (horizontal > 0 || horizontal < 0))
            GetComponent<Animator>().Play("CharacterWalk");

        if (isOnGround() && !crouchHeld && !isUnderPlatform && Input.GetButtonDown("Jump")) jump = true;
        crouchHeld = (isOnGround() && Input.GetButton("Crouch")) ? true : false;
        jumpHeld = (!isOnGround() && !crouchHeld && !isUnderPlatform && Input.GetButton("Jump")) ? true : false;

        if (!isOnGround())
        {
            if (lastJumpY < transform.position.y)
            {
                lastJumpY = transform.position.y;
                GetComponent<Animator>().Play("CharacterJump");
            }
            else if (lastJumpY > transform.position.y)
            {
                GetComponent<Animator>().Play("CharacterFall");
            }
        }

    }

    void FixedUpdate()
    {
        float moveFactor = horizontal * Time.fixedDeltaTime;

        // Movement...
        rigidBody2D.velocity = new Vector2(moveFactor * 10f, rigidBody2D.velocity.y);

        // Flipping sprite according to movement direction...
        if (moveFactor > 0 && !isFacingRight) flipSprite();
        else if (moveFactor < 0 && isFacingRight) flipSprite();

        // Jumping...
        if (jump)
        {
            float jumpvel = 2f;
            rigidBody2D.velocity = Vector2.up * jumpvel;
            jump = false;
        }

        // Jumping High...
        if (jumpHeld && rigidBody2D.velocity.y > 0)
        {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallLongMult - 1) * Time.fixedDeltaTime;
        }
        // Jumping Low...
        else if (!jumpHeld && rigidBody2D.velocity.y > 0)
        {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallShortMult - 1) * Time.fixedDeltaTime;
        }

        // Crouching...
        GetComponent<BoxCollider2D>().isTrigger = (crouchHeld || isUnderPlatform) ? true : false;
    }

    private void flipSprite()
    {
        isFacingRight = !isFacingRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    private bool isOnGround()
    {
        RaycastHit2D hit = Physics2D.CircleCast(circleCollider2D.bounds.center, circleCollider2D.radius, Vector2.down, 0.1f, groundLayer);
        if (hit && !lastJumpY.Equals(0)) lastJumpY = 0;
        return hit.collider != null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
            isUnderPlatform = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
            isUnderPlatform = false;

    }
}