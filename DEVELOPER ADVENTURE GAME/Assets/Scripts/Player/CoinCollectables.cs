using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectables : MonoBehaviour
{
	[Header("Referencia ao CoinManager")]
	[SerializeField]
	private CoinManager cm;

	[Header("Componente do Player")]
	[SerializeField]
	private Animator _animator;

	public RuntimeAnimatorController _defaultSkin;
	public RuntimeAnimatorController _greenSkin;
	public RuntimeAnimatorController _redSkin;
	public RuntimeAnimatorController _cyanSkin;
	public RuntimeAnimatorController _magentaSkin;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	public int GetCoinCount()
	{
		return cm.GetCoinCount();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Coin"))
		{
			if (_animator.runtimeAnimatorController == _greenSkin)
			{
				Destroy(other.gameObject);
				cm.coinCount += 2;
			}
			else if (_animator.runtimeAnimatorController == _redSkin)
			{
				Destroy(other.gameObject);
				cm.coinCount -= 2;
			}
			else if (_animator.runtimeAnimatorController == _magentaSkin)
			{
				Destroy(other.gameObject);
				cm.coinCount *= 2;
			}
			else if (_animator.runtimeAnimatorController == _cyanSkin)
			{
				Destroy(other.gameObject);
				cm.coinCount /= 2;
			}
			else if (_animator.runtimeAnimatorController == _defaultSkin)
			{
				Destroy(other.gameObject);
			}
			else
			{
				Destroy(other.gameObject);
			}
		}
		if (other.gameObject.CompareTag("Plus"))
		{
			Destroy(other.gameObject);
			_animator.runtimeAnimatorController = _greenSkin;
			ResyncAnimatorParams(); // Resincronizar parâmetros do Animator
		}
		if (other.gameObject.CompareTag("Minus"))
		{
			Destroy(other.gameObject);
			_animator.runtimeAnimatorController = _redSkin;
			ResyncAnimatorParams(); // Resincronizar parâmetros do Animator
		}
		if (other.gameObject.CompareTag("Multiply"))
		{
			Destroy(other.gameObject);
			_animator.runtimeAnimatorController = _magentaSkin;
			ResyncAnimatorParams(); // Resincronizar parâmetros do Animator
		}
		if (other.gameObject.CompareTag("Divide"))
		{
			Destroy(other.gameObject);
			_animator.runtimeAnimatorController = _cyanSkin;
			ResyncAnimatorParams(); // Resincronizar parâmetros do Animator
		}
		if (other.gameObject.CompareTag("Clear"))
		{
			Destroy(other.gameObject);
			_animator.runtimeAnimatorController = _defaultSkin;
			ResyncAnimatorParams(); // Resincronizar parâmetros do Animator
		}
	}

	private void ResyncAnimatorParams()
	{
		_animator.SetFloat("InputX", 0);
		_animator.SetFloat("InputY", 0);
		_animator.SetBool("isWalking", false);
		_animator.SetFloat("LastInputX", 0);
		_animator.SetFloat("LastInputY", 0);
	}
}
