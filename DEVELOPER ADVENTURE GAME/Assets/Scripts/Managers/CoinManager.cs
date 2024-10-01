using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
	public int coinCount;
	public TMP_Text coinText;

	// Start is called before the first frame update
	void Start() { }

	public int GetCoinCount()
	{
		return coinCount;
	}

	// Update is called once per frame
	void Update()
	{
		coinText.text = " : " + coinCount.ToString();
	}
}
