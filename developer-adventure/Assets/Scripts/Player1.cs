using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    
    public float Speed = 4f;
    public float JumpForce;
  

    public Rigidbody2D rig;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.RightArrow)){
            rig.velocity = new Vector2(Speed, 0);
            sr.flipX = false;
        }else if (Input.GetKey(KeyCode.LeftArrow)) {
            rig.velocity = new Vector2(-Speed, 0);
            sr.flipX = true;
        }
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = new Vector2(0,JumpForce);
        }
    }
    
}
