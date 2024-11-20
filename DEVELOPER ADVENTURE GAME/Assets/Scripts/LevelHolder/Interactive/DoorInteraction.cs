using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Door : MonoBehaviour
{
	[Header( "Referencia da cena a ser carregada na interação" )]
#if UNITY_EDITOR
	public SceneAsset sceneAsset;
#endif
	public string sceneName;

	[Header( "Referencia do Text Mesh Pro que será exibido na tela" )]
	public TextMeshProUGUI promptText;

	// Referencias as animações e aproximação na porta
	private bool isNearDoor = false;
	private Animator animator;

	private void Start()
	{
		if ( promptText != null )
		{
			promptText.gameObject.SetActive( false );
		}
		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		if ( isNearDoor )
		{
			if ( Input.GetButtonDown( "Interact" ) )
			{
				OnInteract();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			isNearDoor = true;
			if ( promptText != null )
			{
				UpdatePromptText();
				promptText.gameObject.SetActive( true );
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			isNearDoor = false;
			if ( promptText != null )
			{
				promptText.gameObject.SetActive( false );
			}
		}
	}

	private void UpdatePromptText()
	{
		promptText.text = "Pressione C ou A (Gamepad)";
	}

	private void OnInteract()
	{
		Debug.Log( "Interact está funcionando" );
		if ( isNearDoor )
		{
			LoadScene();
		}
	}

	private void LoadScene()
	{
	#if UNITY_EDITOR
		if ( sceneAsset != null )
		{
			string scenePath = AssetDatabase.GetAssetPath( sceneAsset );
			string sceneNFA = System.IO.Path.GetFileNameWithoutExtension( scenePath );
			Debug.Log( "Carregando Cena no editor : sceneNFA" );
			try
			{
				SceneManager.LoadScene( sceneNFA );
				Debug.Log( "Cena Carregada" );
			}
			catch ( System.Exception e )
			{
				Debug.LogError( "Erro ao carregar cena: {e.Message}" );
			}
		}
		else
		{
			Debug.LogError( "SceneAsset não atribuido" );
		}
	#endif
	}
}
