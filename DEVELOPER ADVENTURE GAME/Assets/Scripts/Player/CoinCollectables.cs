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
    [SerializeField]
    private PlayerMovement _playerMovement;

    public RuntimeAnimatorController _defaultSkin;
    public RuntimeAnimatorController _greenSkin;
    public RuntimeAnimatorController _redSkin;
    public RuntimeAnimatorController _cyanSkin;
    public RuntimeAnimatorController _magentaSkin;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
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
            StartCoroutine(ResyncAnimatorParams()); 
        }
        if (other.gameObject.CompareTag("Minus"))
        {
            Destroy(other.gameObject);
            _animator.runtimeAnimatorController = _redSkin;
            StartCoroutine(ResyncAnimatorParams()); 
        }
        if (other.gameObject.CompareTag("Multiply"))
        {
            Destroy(other.gameObject);
            _animator.runtimeAnimatorController = _magentaSkin;
            StartCoroutine(ResyncAnimatorParams()); 
        }
        if (other.gameObject.CompareTag("Divide"))
        {
            Destroy(other.gameObject);
            _animator.runtimeAnimatorController = _cyanSkin;
            StartCoroutine(ResyncAnimatorParams()); 
        }
        if (other.gameObject.CompareTag("Clear"))
        {
            Destroy(other.gameObject);
            _animator.runtimeAnimatorController = _defaultSkin;
            StartCoroutine(ResyncAnimatorParams()); 
        }
    }

    private IEnumerator ResyncAnimatorParams()
    {
        yield return null; 

        if (_playerMovement != null)
        {
            _animator.SetFloat("InputX", _playerMovement.lastInputX);
            _animator.SetFloat("InputY", _playerMovement.lastInputY);
            _animator.SetBool("isWalking", _playerMovement.isWalking);
            _animator.SetFloat("LastInputX", _playerMovement.lastInputX);
            _animator.SetFloat("LastInputY", _playerMovement.lastInputY);
        }
    }
}
