using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    public float Speed = 4f;
    public float JumpForce;
  

    public Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Jump();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rig.velocity = new Vector2(Speed, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rig.velocity = new Vector2(-Speed, 0);
        }
        else
        {
            rig.velocity = new Vector2(0, 0);
        }

    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
    
}
