using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionFailedMessage : MonoBehaviour
{
	public TextMeshProUGUI displayText; // Referência ao TextMeshProUGUI
	public float delayBeforeRestart = 10f; // Delay antes de reiniciar a fase

	public void ShowMessageAndRestart()
	{
		displayText.text = "Você não cumpriu a condição! A fase será reiniciada.";
		displayText.gameObject.SetActive(true);
		StartCoroutine(RestartAfterDelay());
	}

	private IEnumerator RestartAfterDelay()
	{
		yield return new WaitForSeconds(delayBeforeRestart);
		SceneManager.LoadScene(1); // Carrega a cena de índice 1
	}
}