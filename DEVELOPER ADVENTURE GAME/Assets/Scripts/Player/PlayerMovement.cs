using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Referencias")]
	[Header("Walk")]
	[Range(1f, 100f)]
	public float moveSpeed = 12f;

	// Componentes do Player
	private Rigidbody2D _rb;
	private Animator _animator;

	// Variáveis de Movimento
	private Vector2 _moveVelocity;

	// Variáveis para armazenar os últimos valores de Input
	public float lastInputX { get; private set; }
	public float lastInputY { get; private set; }
	public bool isWalking { get; private set; }

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
			lastInputX = _moveVelocity.x;
			lastInputY = _moveVelocity.y;
			isWalking = true;
		}
		else
		{
			_animator.SetBool("isWalking", false);
			isWalking = false;
		}

		_animator.SetFloat("LastInputX", lastInputX);
		_animator.SetFloat("LastInputY", lastInputY);
	}

	private void movePlayer()
	{
		Vector2 movement = new Vector2(_moveVelocity.x, _moveVelocity.y);
		transform.Translate(_moveVelocity * moveSpeed * Time.fixedDeltaTime);
	}
}
