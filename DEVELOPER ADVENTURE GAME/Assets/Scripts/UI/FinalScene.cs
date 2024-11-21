using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{
	[Header("Referencia ao Scene escolhido")]
	[SerializeField]private string sceneName;
	private void Start()
	{
		LoadScene();
		StartCoroutine( LoadSceneWithDelay() );
	}
	IEnumerator LoadSceneWithDelay()
	{
		yield return new WaitForSeconds(5 );
		SceneManager.LoadScene(sceneName);
	}

	private void LoadScene()
	{
		if ( Input.GetKey( KeyCode.Escape ) )
		{
			SceneManager.LoadScene( sceneName );
		}
	}
}
