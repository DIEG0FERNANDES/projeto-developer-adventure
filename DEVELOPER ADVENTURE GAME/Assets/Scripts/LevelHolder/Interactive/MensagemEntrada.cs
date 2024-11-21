using UnityEngine;
using TMPro;

public class MensagemEntradaFase2 : MonoBehaviour, ISignTrigger
{
	[SerializeField] [TextArea] private string textoModified;

	public bool EvaluateCondition(CoinManager cm)
	{
		return true;
	}

	public void UpdateDisplayText(TextMeshProUGUI displayText, bool conditionMet)
	{
		displayText.text = textoModified.Replace( "\\n", "\n" );
	}
}
