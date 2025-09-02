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
    private PlayerMovement _playerMovement;
    private WaitForSeconds _checkTime = new WaitForSeconds(3f);
    private Coroutine _distanceToHide;

    private void OnEnable()
    {
        _distanceToHide = StartCoroutine(CheckDistanceToHide());
    }

    private void OnDisable()
    {
        StopCoroutine(_distanceToHide);
    }

    private void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        _direction = (_playerMovement.transform.position - transform.position).normalized;
        transform.position += _direction * (_speed * Time.deltaTime);
        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
    }


    private IEnumerator CheckDistanceToHide()
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.position, _playerMovement.transform.position);
            if (distance > 20f) gameObject.SetActive(false);
            yield return _checkTime;
        }
    }

    [Inject] private void Construct(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

}
