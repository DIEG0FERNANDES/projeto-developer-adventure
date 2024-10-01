using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public CoinManager coinManager;
	public ConditionFailedMessage conditionFailedMessage;
	public Animator doorAnimator;

	private void Start()
	{
		if (coinManager == null)
		{
			coinManager = FindObjectOfType<CoinManager>();
		}
		if (conditionFailedMessage == null)
		{
			conditionFailedMessage = FindObjectOfType<ConditionFailedMessage>();
		}
		if (doorAnimator == null)
		{
			doorAnimator = GameObject.Find("door").GetComponent<Animator>();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (gameObject.name == "LevelReset")
			{
				conditionFailedMessage.ShowMessageAndRestart();
			}
			else if (gameObject.name == "LevelProgress")
			{
				if (coinManager.GetCoinCount() != 2)
				{
					conditionFailedMessage.ShowMessageAndRestart();
				}
				else
				{
					Debug.Log("Pronto para avançar para a próxima fase.");
					doorAnimator.SetTrigger("door_opening");
				}
			}
		}
	}
}
