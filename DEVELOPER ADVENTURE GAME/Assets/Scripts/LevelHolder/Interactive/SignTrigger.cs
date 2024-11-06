using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignTrigger : MonoBehaviour
{
	public TextMeshProUGUI displayText;
	public GameObject panel;
	public GameObject door;
	public CoinManager cm;
	private CanvasGroup textCanvasGroup;
	private CanvasGroup panelCanvasGroup;
	private const float FADE_DURATION = 1f;
	private void Start()
	{
		InitializeCanvasGroups();
		door.SetActive( false );
		cm = FindObjectOfType<CoinManager>();
	}
	private void InitializeCanvasGroups()
	{
		textCanvasGroup = displayText.GetComponent<CanvasGroup>();
		SetCanvasGroupState( textCanvasGroup, false );
		panelCanvasGroup = panel.GetComponent<CanvasGroup>();
		SetCanvasGroupState( panelCanvasGroup, false );
	}
	private void SetCanvasGroupState(CanvasGroup canvasGroup, bool active)
	{
		canvasGroup.alpha = active ? 1 : 0;
		canvasGroup.gameObject.SetActive( active );
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			bool hasEnoughCoins = cm.GetCoinCount() == 2; door.SetActive( hasEnoughCoins );
			displayText.text = hasEnoughCoins ? "se (CoinCount == 2)\n{\n Debug.Log(\"Vá para Cima!\");\n}\nsenao\n{\n Debug.Log(\"Vá para Baixo!\");\n}" : "se (CoinCount != 2)\n{\n Debug.Log(\"Vá para Baixo!\");\n}\nsenao\n{\n Debug.Log(\"Vá para Cima!\");\n}"; Debug.Log( hasEnoughCoins ? "Vá para Cima!" : "Vá para Baixo!" );
			displayText.gameObject.SetActive( true );
			panel.SetActive( true );
			StartCoroutine( FadeIn( textCanvasGroup ) );
			StartCoroutine( FadeIn( panelCanvasGroup ) );
		}
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			StartCoroutine( FadeOut( textCanvasGroup ) );
			StartCoroutine( FadeOut( panelCanvasGroup ) );
		}
	}
	private IEnumerator FadeIn(CanvasGroup canvasGroup)
	{
		yield return Fade( canvasGroup, 0, 1 );
	}
	private IEnumerator FadeOut(CanvasGroup canvasGroup)
	{
		yield return Fade( canvasGroup, 1, 0 );
		canvasGroup.gameObject.SetActive( false );
	}
	private IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
	{
		float elapsed = 0f;
		canvasGroup.alpha = startAlpha;
		while ( elapsed < FADE_DURATION )
		{
			elapsed += Time.deltaTime;
			canvasGroup.alpha = Mathf.Lerp( startAlpha, endAlpha, elapsed / FADE_DURATION );
			yield return null;
		}
		canvasGroup.alpha = endAlpha;
	}
}
