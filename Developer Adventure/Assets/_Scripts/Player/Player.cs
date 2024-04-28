using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    public Rigidbody2D rb;
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

    // Start is called before the first frame update
    // void Start() {
    // }

    // Update is called once per frame
    void Update() {
        rb.velocity = new UnityEngine.Vector2(rb.velocity.x * moveSpeed, rb.velocity.y);
        GroundCheck();
        Gravity();
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
    }
    // referencia a checagem de ground
    private bool GroundCheck() {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer)) {
            return true;
        }
        return false;
    }
    // referencia ao collider do groundchecker de forma scriptada
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);

    }
}
