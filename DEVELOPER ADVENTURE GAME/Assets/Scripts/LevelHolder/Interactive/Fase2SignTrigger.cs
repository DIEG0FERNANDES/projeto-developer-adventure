using TMPro;
using UnityEngine;

public class Fase2SignTrigger : MonoBehaviour, ISignTrigger
{
	[SerializeField] private TextMeshProUGUI displayText;
	[SerializeField] private GameObject door;

	private void Start()
	{
		if ( door != null )
		{
			door.SetActive( false );
		}
	}

	public int GetCoinCount(CoinManager cm)
	{
		return cm.GetCoinCount();
	}

	public bool EvaluateCondition(CoinManager cm)
	{
		bool conditionMet = cm.coinCount == 6;
		Debug.Log( conditionMet ? "Acontece" : "Você falhou a condição" );
		return conditionMet;
	}

	public void UpdateDisplayText(TextMeshProUGUI displayText, bool conditionMet)
	{
		displayText.text = conditionMet
			? "Você acabou de ver uma Introdução a Lógica Condicional.\nAgora vá pelo caminho à direita do Computador, caso contrário, você terá que fazer tudo do começo."
			: "Condição não alcançada.";
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			CoinManager cm = other.GetComponent<CoinManager>();
			if ( cm != null )
			{
				bool conditionMet = EvaluateCondition( cm );
				UpdateDisplayText( displayText, conditionMet );

				if ( conditionMet && door != null )
				{
					door.SetActive( true );
				}
			}
		}
	}
}
