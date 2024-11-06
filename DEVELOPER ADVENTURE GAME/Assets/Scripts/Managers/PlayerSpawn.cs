using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
	[Header( "O collider onde o jogador deve surgir" )]
	public Collider2D spawnArea;
	[Header( "O objeto do jogador" )]
	public GameObject player;

	void Start()
	{
		// Verifica se o objeto do jogador e o collider de spawn foram definidos
		if ( player != null && spawnArea != null )
		{
			SpawnPlayer();
		}
		else
		{
			Debug.LogError( "Player ou spawnArea não foram definidos." );
		}
	}

	void SpawnPlayer()
	{
		// Define a posição do jogador no centro do collider de spawn
		Vector3 spawnPosition = GetCenterPositionInCollider( spawnArea );
		player.transform.position = spawnPosition;
	}

	Vector3 GetCenterPositionInCollider(Collider2D collider)
	{
		// Pega o centro do collider
		Vector3 center = collider.bounds.center;
		return new Vector3( center.x, center.y, 0 ); // Z é definido como 0 pois estamos usando 2D
	}
}
