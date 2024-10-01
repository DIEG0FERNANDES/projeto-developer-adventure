using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignTrigger : MonoBehaviour
{
	public TextMeshProUGUI displayText;
	private CanvasGroup textCanvasGroup;
	public GameObject panel;
	public GameObject door;
	private CanvasGroup panelCanvasGroup;
	public CoinManager cm;

	private void Start()
	{
		textCanvasGroup = displayText.GetComponent<CanvasGroup>();
		textCanvasGroup.alpha = 0;
		displayText.gameObject.SetActive(false);

		panelCanvasGroup = panel.GetComponent<CanvasGroup>();
		panelCanvasGroup.alpha = 0;
		panel.SetActive(false);
		door.SetActive(false);

		cm = FindObjectOfType<CoinManager>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (cm.GetCoinCount() == 2)
			{
				door.SetActive(true);
				displayText.text =
					"se (CoinCount == 2)\n{\n    Debug.Log(\"Vá para Cima!\");\n}\nsenao\n{\n    Debug.Log(\"Vá para Baixo!\");\n}";
				Debug.Log("Vá para Cima!");
			}
			else
			{
				displayText.text =
					"se (CoinCount != 2)\n{\n    Debug.Log(\"Vá para Baixo!\");\n}\nsenao\n{\n    Debug.Log(\"Vá para Cima!\");\n}";
				Debug.Log("Vá para Baixo!");
			}

			displayText.gameObject.SetActive(true);
			panel.SetActive(true);
			StartCoroutine(FadeIn(textCanvasGroup));
			StartCoroutine(FadeIn(panelCanvasGroup));
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			StartCoroutine(FadeOut(textCanvasGroup));
			StartCoroutine(FadeOut(panelCanvasGroup));
		}
	}

	private IEnumerator FadeIn(CanvasGroup canvasGroup)
	{
		float duration = 1f;
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
		float duration = 1f;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			canvasGroup.alpha = Mathf.Clamp01(1 - (elapsed / duration));
			yield return null;
		}

		canvasGroup.gameObject.SetActive(false);
	}
}
