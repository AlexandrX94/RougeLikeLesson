using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _freezeTimer;
    [SerializeField] private Animator _animator;
    private Vector3 _direction;
    private PlayerMovement _playerTransform;
    private WaitForSeconds _checkTime = new WaitForSeconds(3f);
    private Coroutine _distanceToHide;

     [Inject] private void Construct(PlayerMovement playerTransform)
    {
        _playerTransform = playerTransform;
    }
    
    private void OnEnable()
    {
        _distanceToHide = StartCoroutine(CheckDistanceToHide());
    }

    private void Update()
    {
        EnemyAnimation();
        EnemyMove();
    }

    private void EnemyMove()
    {
        _direction = (_playerTransform.transform.position - transform.position).normalized;
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private void EnemyAnimation()
    {
        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
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
