using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	[Header( "Nome da cena a ser carregada" )]
	[SerializeField] private string sceneName;

	[Header( "Referência do TextMeshPro que será exibido na tela" )]
	[SerializeField] private TextMeshProUGUI promptText;

	private bool isNearDoor = false;
	private Animator animator;
	private Inputs inputs;

	private void Awake()
	{
		inputs = new Inputs();
	}

	private void OnEnable()
	{
		inputs.Player.Interact.performed += OnInteract;
		inputs.Enable();
	}

	private void OnDisable()
	{
		inputs.Player.Interact.performed -= OnInteract;
		inputs.Disable();
	}

	private void Start()
	{
		SetPromptTextVisibility( false );
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			isNearDoor = true;
			SetPromptTextVisibility( true );
			UpdatePromptText( "Pressione C ou A (Gamepad)" );
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			isNearDoor = false;
			SetPromptTextVisibility( false );
		}
	}

	private void OnInteract(InputAction.CallbackContext context)
	{
		if ( isNearDoor )
		{
			LoadScene();
		}
	}

	private void SetPromptTextVisibility(bool isVisible)
	{
		if ( promptText != null )
		{
			promptText.gameObject.SetActive( isVisible );
		}
	}

	private void UpdatePromptText(string text)
	{
		if ( promptText != null )
		{
			promptText.text = text;
		}
	}

	private void LoadScene()
	{
		if ( !string.IsNullOrEmpty( sceneName ) )
		{
			try
			{
				SceneManager.LoadScene( sceneName );
				Debug.Log( $"Cena Carregada: {sceneName}" );
			}
			catch ( System.Exception e )
			{
				Debug.LogError( $"Erro ao carregar cena: {e.Message}" );
			}
		}
		else
		{
			Debug.LogError( "Nome da Cena não atribuído" );
		}
	}
}
