using GameCore;
using GameCore.Pool;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour, IActivate
    {
        [SerializeField] private float _timeToSpawn;
        [SerializeField] private Transform _minPos, _maxPos;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private ObjectPool _enemyPool;
        private PlayerMovement _playerMovement;
        private WaitForSeconds _interval;
        private Coroutine _spawnCoroutine;
        private GetRandonSpawnPoint _randomSpawnPoint;


        public void Activate()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        public void Deactivate()
        {
            if (_spawnCoroutine != null) 
            {
                StopCoroutine(_spawnCoroutine);
            }
        }

        private IEnumerator Spawn()
        {
            while (enabled)
            {
                transform.position = _playerMovement.transform.position;
                GameObject enemy = _enemyPool.GetFromPool();
                enemy.transform.SetParent(_enemyContainer);
                enemy.transform.position = _randomSpawnPoint.GetRandomPoint(_minPos, _maxPos);
                yield return _interval;
            }
        }

        private void Start()
        {
            _interval = new WaitForSeconds(_timeToSpawn);
        }

        [Inject] private void Construct (GetRandonSpawnPoint getRandonSpawnPoint, PlayerMovement playerMovement)
        {
            _randomSpawnPoint = getRandonSpawnPoint;
            _playerMovement = playerMovement;
        }

    }
}

