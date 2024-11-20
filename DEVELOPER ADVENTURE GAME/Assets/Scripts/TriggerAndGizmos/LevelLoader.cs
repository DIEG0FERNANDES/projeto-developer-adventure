using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public CoinManager coinManager;
	public ConditionFailedMessage conditionFailedMessage;
	public Animator doorAnimator;
	public SignTrigger signTrigger;

	private void Start()
	{
		if ( coinManager == null )
		{
			coinManager = FindObjectOfType<CoinManager>();
		}
		if ( conditionFailedMessage == null )
		{
			conditionFailedMessage = FindObjectOfType<ConditionFailedMessage>();
		}
		if ( doorAnimator == null )
		{
			doorAnimator = GameObject.Find( "door" ).GetComponent<Animator>();
		}
		if ( signTrigger == null )
		{
			signTrigger = FindObjectOfType<SignTrigger>();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ( other.CompareTag( "Player" ) )
		{
			if ( gameObject.name == "LevelReset" )
			{
				conditionFailedMessage.ShowMessageAndRestart();
			}
			else if ( gameObject.name == "LevelProgress" )
			{
				if ( signTrigger != null )
				{
					ISignTrigger fase = signTrigger.faseScript as ISignTrigger;
					if ( fase != null )
					{
						bool conditionMet = fase.EvaluateCondition( coinManager );
						if ( conditionMet )
						{
							doorAnimator.SetTrigger( "door_opening" );
						}
						else
						{
							conditionFailedMessage.ShowMessageAndRestart();
						}
					}
					else
					{
						Debug.LogError( "FaseScript não implementa ISignTrigger." );
						conditionFailedMessage.ShowMessageAndRestart();
					}
				}
				else
				{
					Debug.LogError( "SignTrigger não atribuído ou não encontrado." );
					conditionFailedMessage.ShowMessageAndRestart();
				}
			}
		}
	}
}
