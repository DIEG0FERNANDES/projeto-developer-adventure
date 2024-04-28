using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    public Rigidbody2D rb;
    bool isFacingRight = true;
    // Movimento do Personagem
    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    

    // Pulo do personagem
    [Header("Jump")]
    float jumpPower = 10f;

    // GroundChecker do player
    [Header("GroundChecker")]
    public Transform groundCheckPos;
    public UnityEngine.Vector2 groundCheckSize = new UnityEngine.Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    // gravidade do personagem
    [Header("GravityChecker")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;
    bool isGrounded;
    //WallChecker
    [Header("WallChecker")]
    public Transform WallCheckPos;
    public UnityEngine.Vector2 WallCheckSize = new UnityEngine.Vector2(0.5f, 0.05f);
    public LayerMask WallLayer;

    [Header("WallMoviment")]
    public float wallSlideSpeed = 2;
    public bool iswallSliding;

    //wall jumping
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public UnityEngine.Vector2 wallJumpPower = new UnityEngine.Vector2(5f, 10f);


    // Start is called before the first frame update
    // void Start() {
    // }

    // Update is called once per frame
    void Update() {
        
        GroundCheck();
        Gravity();
        GravityWall();
        WallJump();
        cancelWall();
        if (!isWallJumping) {
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x * moveSpeed, rb.velocity.y);
            Flip();
        }
    }
    // referencia a gravidade do personagem caindo rapido no chão para não flutuar
    private void Gravity() {
        if (rb.velocity.y < 0) {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else {
            rb.gravityScale = baseGravity;
        }
    }
    //gravity to walljump
    private void GravityWall() {
        // Not grounded & On a Wall & movement != 0
        if (isGrounded & WallCheck() & horizontalMovement != 0) {
            iswallSliding = true;
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed)); // cops fall rate
        }
        else {
            iswallSliding = false;
        }
    }
    private void WallJump() {
        if (iswallSliding) {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(cancelWall));
        }else if (wallJumpTimer > 0f) {
            wallJumpTimer -= Time.deltaTime;
        }
    }
    private void cancelWall() {
        isWallJumping = false;
    }
    // referencia ao movimento do player com o novo input system
    public void Move(InputAction.CallbackContext context) {
        horizontalMovement = context.ReadValue<UnityEngine.Vector2>().x;
    }
    // referencia ao pulo do player com o novo input system
    public void Jump(InputAction.CallbackContext context) {
        if (GroundCheck()) {
            if (context.performed) {
                rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpPower);
            }
            else if (context.canceled) {
                rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpPower * 0.5f);
            }
        }
        //wall Jump
        if(context.performed && wallJumpTimer > 0f) {
            isWallJumping = true;
            rb.velocity = new UnityEngine.Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;
            //ForceFlip
            if(transform.localScale.x != wallJumpDirection) {
                isFacingRight = !isFacingRight;
                UnityEngine.Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(cancelWall), wallJumpTime + 0, 1f);
        }
    }

    private void Invoke(string v1, float v2, float v3) {
        throw new NotImplementedException();
    }

    // referencia a checagem de ground
    private bool GroundCheck() {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer)) {
            return true;
           
        }
        return false;
    }

    private bool WallCheck() {
        return Physics2D.OverlapBox(WallCheckPos.position, WallCheckSize, 0, WallLayer);
    }
    private void Flip() {
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0) {
            isFacingRight = !isFacingRight;
            UnityEngine.Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    // referencia ao collider do groundchecker de forma scriptada
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(WallCheckPos.position, WallCheckSize);
    }
}
