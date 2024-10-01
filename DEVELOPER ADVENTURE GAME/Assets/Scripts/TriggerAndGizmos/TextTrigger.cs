using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
	public TextMeshProUGUI displayText;
	public Image displayImage;
	private CanvasGroup textCanvasGroup;
	private CanvasGroup imageCanvasGroup;
	private bool hasDisplayed = false;

	private void Start()
	{
		textCanvasGroup = displayText.GetComponent<CanvasGroup>();
		imageCanvasGroup = displayImage.GetComponent<CanvasGroup>();
		textCanvasGroup.alpha = 0; // Começa invisível
		imageCanvasGroup.alpha = 0; // Começa invisível
		displayText.gameObject.SetActive(false);
		displayImage.gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) // Verifica se o objeto que entrou é o jogador
		{
			if (!hasDisplayed) // Verifica se a mensagem já foi exibida
			{
				displayText.text = "USE A SETAS PARA MOVER-SE!";
				displayText.gameObject.SetActive(true); // Ativa o texto
				displayImage.gameObject.SetActive(true); // Ativa a imagem

				// Faz fade in
				StartCoroutine(FadeIn(textCanvasGroup));
				StartCoroutine(FadeIn(imageCanvasGroup));
				hasDisplayed = true; // Marca que já foi exibido
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) // Verifica se o objeto que saiu é o jogador
		{
			// Faz fade out
			StartCoroutine(FadeOut(textCanvasGroup));
			StartCoroutine(FadeOut(imageCanvasGroup));
		}
	}

	private IEnumerator FadeIn(CanvasGroup canvasGroup)
	{
		float duration = 1f; // Duração do fade
		float elapsed = 0f;

		canvasGroup.alpha = 0;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
			yield return null;
		}
	}

	private IEnumerator FadeOut(CanvasGroup canvasGroup)
	{
		float duration = 1f; // Duração do fade
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			canvasGroup.alpha = Mathf.Clamp01(1 - (elapsed / duration));
			yield return null;
		}

		canvasGroup.gameObject.SetActive(false); // Desativa o objeto após o fade out
	}
}
