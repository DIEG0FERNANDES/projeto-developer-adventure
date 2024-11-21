using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasesManager : MonoBehaviour
{
	public GameObject[] phasePrefabs; 

	private GameObject currentPhase;

	private void Start()
	{
		LoadPhase(0); 
	}

	public void LoadPhase(int phaseIndex)
	{
		if (currentPhase != null)
		{
			Destroy(currentPhase); 
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
		LoadPhase(nextPhaseIndex);
	}
}
