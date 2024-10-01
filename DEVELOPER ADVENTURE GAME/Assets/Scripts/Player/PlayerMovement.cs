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

		// Verifica se o personagem está se movendo
		if (moveInput.magnitude > 0)
		{
			animator.SetBool("isWalking", true);
			// Atualiza os últimos inputs enquanto se move
			animator.SetFloat("LastInputX", moveInput.x);
			animator.SetFloat("LastInputY", moveInput.y);
		}
		else
		{
			animator.SetBool("isWalking", false);
			// Atualiza os últimos inputs quando para
			// Isso mantém os valores corretos para idle
			animator.SetFloat("LastInputX", animator.GetFloat("LastInputX"));
			animator.SetFloat("LastInputY", animator.GetFloat("LastInputY"));
		}
	}
}
