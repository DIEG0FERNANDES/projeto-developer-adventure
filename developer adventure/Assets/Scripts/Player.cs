using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D rbPlayer;
    private void Awake() {
        rbPlayer = GetComponent<Rigidbody2D>();
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
}
