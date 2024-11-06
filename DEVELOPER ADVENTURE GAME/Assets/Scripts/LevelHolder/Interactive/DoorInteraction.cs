using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; // Adiciona o namespace necessário
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Door : MonoBehaviour
{
#if UNITY_EDITOR
	public SceneAsset sceneAsset;
#endif
	public TextMeshProUGUI promptText;
	private bool isNearDoor = false;
	private bool hasOpened = false;
	private Animator animator;

	private void Start()
	{
		if (promptText != null)
		{
			promptText.gameObject.SetActive(false);
		}

		animator = GetComponent<Animator>();
		if (animator == null)
		{
			Debug.LogError("Animator component not found on the door object.");
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isNearDoor = true;
			if (!hasOpened && animator != null)
			{
				animator.SetBool("door_opening", true); // Define o parâmetro booleano do Animator
				hasOpened = true;
			}
			if (promptText != null)
			{
				UpdatePromptText();
				promptText.gameObject.SetActive(true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isNearDoor = false;
			if (promptText != null)
			{
				promptText.gameObject.SetActive(false);
			}
		}
	}

	private void Update()
	{
		if (isNearDoor)
		{
			// Verifica se a tecla C do teclado ou o botão sul do gamepad foi pressionado
			if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton0))
			{
				LoadScene();
			}
		}
	}

	private void UpdatePromptText()
	{
		bool isKeyboard = false;
		bool isGamepad = false;
		bool isJoystick = false;

		foreach (var device in InputSystem.devices)
		{
			if (device is Keyboard && device.lastUpdateTime > 0)
			{
				isKeyboard = true;
			}
			if (device is Gamepad && device.lastUpdateTime > 0)
			{
				isGamepad = true;
			}
			if (device is Joystick && device.lastUpdateTime > 0)
			{
				isJoystick = true;
			}
		}

		if (isKeyboard)
		{
			promptText.text = "Pressione C para entrar";
		}
		else if (isGamepad)
		{
			promptText.text = "Pressione A para entrar";
		}
		else if (isJoystick)
		{
			promptText.text = "Pressione A para entrar";
		}
		else
		{
			promptText.text = "Pressione C ou A para entrar"; // Caso nenhum dispositivo específico seja detectado
		}
	}

	private void LoadScene()
	{
#if UNITY_EDITOR
		if (sceneAsset != null)
		{
			string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
			string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
			SceneManager.LoadScene(sceneName);
		}
		else
		{
			Debug.LogError("SceneAsset não está definido.");
		}
#endif
	}
}
