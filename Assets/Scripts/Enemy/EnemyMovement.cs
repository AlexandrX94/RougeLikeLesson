using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Player.Weapon;
using System;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] float _freezeTimer;
    [SerializeField] private Animator _animator;
    private Vector3 _direction;
    private PlayerMovement _playerTransform;
    private WaitForSeconds _checkTime = new WaitForSeconds(3f);
    private WaitForSeconds _freeze;
    private Coroutine _distanceToHide;
    private float _slowSpeed;
    private AuraWeapon _auraWeapon; 
    private float _originalSpeed; 
    private bool _isSlowed; 


     [Inject] private void Construct(PlayerMovement playerTransform)
     {
         _playerTransform = playerTransform;
     }

    private void Start()
    {
        _freeze = new WaitForSeconds(_freezeTimer);
        _originalSpeed = Speed;
        _isSlowed = false;
    }

    private void OnEnable()
    {
        _distanceToHide = StartCoroutine(CheckDistanceToHide());
    }
    private void OnDisable()
    {
        if (_distanceToHide != null)
        {
            StopCoroutine(_distanceToHide);
        }
    }

    private void Update()
    {
        EnemyAnimation();
        EnemyMove();
    }

    public void EnemyMove()
    {
        _direction = (_playerTransform.transform.position - transform.position).normalized;
        transform.position += _direction * (Speed * Time.deltaTime);
    }

        
    public void SlowMove(float slowFactor = 0.5f)
    {
        if (!_isSlowed)
        {
            Speed = _originalSpeed * slowFactor; 
            _isSlowed = true;
        }
    }

    public IEnumerator RestoreSpeed()
    {
        if (_isSlowed == true)
        {
            _freezeTimer -= Time.deltaTime;
            yield return _freezeTimer;
            _isSlowed = false;
            Speed = _originalSpeed;
        }
    }

    private void OnChangeSpeed()
    {
        if (_isSlowed == true)
        {
            RestoreSpeed();
        }
        else
        {
            SlowMove();
        }
    }


    private void EnemyAnimation()
    {
        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
    }

    private IEnumerator StartToBeFrozen()
    {
        _originalSpeed = Speed;
        Speed = 0f;

        yield return _freeze;

        Speed = _originalSpeed;
    }

    private IEnumerator StartFreeze()
    {
        _originalSpeed = Speed;
        Speed /= 2f; 

        yield return _freeze; 

        Speed = _originalSpeed; 
    }
    public void FreezeEnemy()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(StartFreeze());
        }
    }

    public void FrozenEnemy()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(StartToBeFrozen());
        }
    }

    private IEnumerator CheckDistanceToHide()
    {
        while (enabled)
        {
            float distance = Vector3.Distance(transform.position, _playerTransform.transform.position);
            if (distance > 20f) gameObject.SetActive(false);
            yield return _checkTime;
        }
    }

}
