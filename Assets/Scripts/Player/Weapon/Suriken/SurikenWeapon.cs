using GameCore;
using GameCore.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Player.Weapon.Suriken
{
    public class SurikenWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _startPosition;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _surikenCoroutine;
        private float _duration;
        private float _speed;
        private float _range;
        private float _returnDelay = 3f;
        private float _timer;
        private float _returnSpeed;
        private Vector3 _direction;
        private Rigidbody2D _rigidbody;
        [SerializeField] private Timer _returnTimer;

        public float Duration => _duration;
        public float Speed => _speed;
        public Vector3 Direction => _direction;

        private void OnEnable()
        {
            Activate();
            _returnSpeed = _speed;
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
        }

        public void Activate()
        {
            SetStats(0);
            _surikenCoroutine = StartCoroutine(SpawnSuriken());
        }

        public void Deactivate()
        {
            if (_surikenCoroutine != null) 
            StopAllCoroutines();
        }


        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = WeaponStats[CurrentLevel - 1].Duration;

        }
        

        private IEnumerator SpawnSuriken()
        {
            while(enabled)
            {
                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _range, _layerMask);
                if(enemiesInRange.Length > 0 )
                {
                    Vector3 targetPosition = enemiesInRange[UnityEngine.Random.Range(0, enemiesInRange.Length)].transform.position;
                    _direction = (targetPosition - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    GameObject suriken = _objectPool.GetFromPool();
                    suriken.transform.SetParent(_container);
                    suriken.transform.position = transform.position;
                    suriken.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                    if (CurrentLevel >= 5)
                    {
                        
                    }

                    yield return _timeBetweenAttack;

                }
                else
                {
                    yield return _timeBetweenAttack;
                }
            }

        }

        private IEnumerator Return()
        {
            
            yield return new WaitForSeconds(_returnDelay);

            for (int i = 3; _returnDelay <= 0; i--)
            {
                Vector3 returnDirection = (_startPosition.position - transform.position).normalized;
                yield return new WaitForSeconds(_returnDelay);
            }

        }
    }
}
