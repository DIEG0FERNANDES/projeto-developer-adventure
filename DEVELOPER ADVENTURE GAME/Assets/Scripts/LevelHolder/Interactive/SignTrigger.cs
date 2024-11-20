using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public interface ISignTrigger
{
	bool EvaluateCondition(CoinManager cm);
	void UpdateDisplayText(TextMeshProUGUI displayText, bool conditionMet);
}

public class SignTrigger : MonoBehaviour
{
	public TextMeshProUGUI displayText;
	public GameObject panel;
	public GameObject door;
	public CoinManager cm;
	public MonoBehaviour faseScript;

	private CanvasGroup textCanvasGroup;
	private CanvasGroup panelCanvasGroup;

	private const float FADE_DURATION = 1f;

	private void Start()
	{
		InitializeCanvasGroups();
		door.SetActive( false );
		cm = FindObjectOfType<CoinManager>();

		if ( cm == null )
		{
			Debug.LogError( "CoinManager não encontrado na cena." );
		}

		SetupConditions();
	}

	private void InitializeCanvasGroups()
	{
		textCanvasGroup = displayText.GetComponent<CanvasGroup>();
		panelCanvasGroup = panel.GetComponent<CanvasGroup>();

		if ( textCanvasGroup == null || panelCanvasGroup == null )
		{
			Debug.LogError( "CanvasGroup não encontrado no displayText ou panel." );
			return;
		}

		SetCanvasGroupState( textCanvasGroup, false );
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
			ISignTrigger fase = faseScript as ISignTrigger;
			if ( fase == null )
			{
				Debug.LogError( "FaseScript não implementa ISignTrigger." );
				return;
			}

			bool conditionMet = fase.EvaluateCondition( cm );
			door.SetActive( conditionMet );
			fase.UpdateDisplayText( displayText, conditionMet );
			DisplaySign( conditionMet );
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

	protected virtual void SetupConditions() { }

	private void DisplaySign(bool conditionMet)
	{
		displayText.gameObject.SetActive( true );
		panel.SetActive( true );
		StartCoroutine( FadeIn( textCanvasGroup ) );
		StartCoroutine( FadeIn( panelCanvasGroup ) );
	}
}
