using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Referencias")]
	[Header("Walk")]
	[Range(1f, 100f)]
	public float moveSpeed = 12f;

	//  Componentets do Player
	private Rigidbody2D _rb;
	private Animator _animator;

	// Variaveis de Movimento
	private Vector2 _moveVelocity;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
	}

	private void Update() { }

	private void FixedUpdate()
	{
		movePlayer();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		_moveVelocity = context.ReadValue<Vector2>();
		_animator.SetFloat("InputX", _moveVelocity.x);
		_animator.SetFloat("InputY", _moveVelocity.y);

		if (_moveVelocity.magnitude > 0)
		{
			_animator.SetBool("isWalking", true);
			_animator.SetFloat("LastInputX", _moveVelocity.x);
			_animator.SetFloat("LastInputY", _moveVelocity.y);
		}
		else
		{
			_animator.SetBool("isWalking", false);
			_animator.SetFloat("LastInputX", _animator.GetFloat("LastInputX"));
			_animator.SetFloat("LastInputY", _animator.GetFloat("LastInputY"));
		}
	}

	private void movePlayer()
	{
		Vector2 movement = new Vector2(_moveVelocity.x, _moveVelocity.y);
		transform.Translate(_moveVelocity * moveSpeed * Time.fixedDeltaTime);
		// _rb.velocity = _moveVelocity * moveSpeed;
	}
}
