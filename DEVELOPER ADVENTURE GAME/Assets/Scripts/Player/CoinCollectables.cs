using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectables : MonoBehaviour
{
	public CoinManager cm;

	public SpriteRenderer color;

	void Start()
	{
		color = GetComponent<SpriteRenderer>();
	}

	public int GetCoinCount()
	{
		return cm.GetCoinCount();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Coin"))
		{
			if (color.material.color == Color.green)
			{
				Destroy(other.gameObject);
				cm.coinCount += 2;
				Debug.Log("moeda acrescentada");
			}
			else if (color.material.color == Color.red)
			{
				Destroy(other.gameObject);
				cm.coinCount -= 2;
				Debug.Log("moeda diminuida");
			}
			else if (color.material.color == Color.magenta)
			{
				Destroy(other.gameObject);
				cm.coinCount *= 2;
				Debug.Log("moeda multiplicada");
			}
			else if (color.material.color == Color.cyan)
			{
				Destroy(other.gameObject);
				cm.coinCount /= 2;
				Debug.Log("moeda dividida");
			}
			else if (color.material.color == Color.gray)
			{
				Destroy(other.gameObject);
				Debug.Log("acontede nada com as moedas");
			}
			else
			{
				Destroy(other.gameObject);
				Debug.Log("moeda destruida, acontece nada");
			}
		}

		if (other.gameObject.CompareTag("Plus"))
		{
			Destroy(other.gameObject);
			color.material.color = Color.green;
			Debug.Log("Operador Mais");
		}

		if (other.gameObject.CompareTag("Minus"))
		{
			Destroy(other.gameObject);
			color.material.color = Color.red;
			Debug.Log("Operador Menos");
		}
		if (other.gameObject.CompareTag("Multiply"))
		{
			Destroy(other.gameObject);
			color.material.color = Color.magenta;
			Debug.Log("Operador de Multiplicação");
		}
		if (other.gameObject.CompareTag("Divide"))
		{
			Destroy(other.gameObject);
			color.material.color = Color.cyan;
			Debug.Log("Operador de Divisão");
		}
		if (other.gameObject.CompareTag("Clear"))
		{
			Destroy(other.gameObject);
			color.material.color = Color.gray;
			Debug.Log("Operador Removido");
		}
	}
}
