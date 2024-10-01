using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasesManager : MonoBehaviour
{
	public GameObject[] phasePrefabs; // Arraste seus prefabs de fase aqui no Inspector

	private GameObject currentPhase;

	private void Start()
	{
		LoadPhase(0); // Carrega a primeira fase (fase1)
	}

	public void LoadPhase(int phaseIndex)
	{
		if (currentPhase != null)
		{
			Destroy(currentPhase); // Remove a fase atual
		}

		if (phaseIndex < phasePrefabs.Length)
		{
			currentPhase = Instantiate(
				phasePrefabs[phaseIndex],
				transform.position,
				Quaternion.identity
			);
		}
	}

	public void CompletePhase(int nextPhaseIndex)
	{
		LoadPhase(nextPhaseIndex); // Carrega a próxima fase
	}
}
