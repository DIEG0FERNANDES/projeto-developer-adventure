using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2  movimento;

    public bool IsCrouched, IsProne;
    private int tapCoubt = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetMovimento(InputAction.CallbackContext value)
    {
        movimento = value.ReadValue<Vector2>();
    }

    public void SetPular(InputAction.CallbackContext value)
    {
        rb.AddForce(Vector3.up * 100);
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(movimento.x, 0, movimento.y) * Time.fixedDeltaTime * 3000);
    }

    public void Crouch_Prone(InputAction.CallbackContext value)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            IsCrouched = true;

            tapCoubt += 1;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            IsCrouched = false;

            IsProne = false;
        }
    }
}
