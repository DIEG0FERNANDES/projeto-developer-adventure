using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	public TextMeshProUGUI promptText;
	private bool isNearDoor = false;

	private void Start()
	{
		promptText.gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isNearDoor = true;
			promptText.text = "Pressione C para entrar";
			promptText.gameObject.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			isNearDoor = false;
			promptText.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (isNearDoor)
		{
			// Verifica se a tecla C do teclado ou o botão sul do gamepad foi pressionado
			if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton0))
			{
				SceneManager.LoadScene(2);
			}
		}
	}
}
