using TMPro;
using UnityEngine;

public class Fase1SignTrigger : MonoBehaviour, ISignTrigger
{
	public bool EvaluateCondition(CoinManager cm)
	{
		return true;
	}

	public void UpdateDisplayText(TextMeshProUGUI displayText, bool conditionMet)
	{
		displayText.text = "Você acabou de ver Estrutura sequencial, variáveis, tipos e operações aritméticas. \n Vá pela porta!";
	}
}
