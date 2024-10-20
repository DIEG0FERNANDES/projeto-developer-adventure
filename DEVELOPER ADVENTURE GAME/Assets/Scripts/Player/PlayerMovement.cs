using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Configurações de Movimento do Player")]
	[SerializeField]
	private float moveSpeed = 5f;
	private Rigidbody2D rb;
	private Vector2 moveInput;
	private Animator animator;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update() { }

	private void FixedUpdate()
	{
		rb.velocity = moveInput * moveSpeed;
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
		animator.SetFloat("InputX", moveInput.x);
		animator.SetFloat("InputY", moveInput.y);

		if (moveInput.magnitude > 0)
		{
			animator.SetBool("isWalking", true);
			animator.SetFloat("LastInputX", moveInput.x);
			animator.SetFloat("LastInputY", moveInput.y);
		}
		else
		{
			animator.SetBool("isWalking", false);
			animator.SetFloat("LastInputX", animator.GetFloat("LastInputX"));
			animator.SetFloat("LastInputY", animator.GetFloat("LastInputY"));
		}
	}
}
