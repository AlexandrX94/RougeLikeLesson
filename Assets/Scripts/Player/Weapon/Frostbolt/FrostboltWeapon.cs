using System.Collections;
using System.Collections.Generic;
using GameCore.Pool;
using Player.Weapon;
using UnityEngine;
using GameCore;
using Enemy;

namespace Player.Weapon.Frostbolt
{
    public class FrostboltWeapon: BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private List<Transform> _shootPoints = new List<Transform>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _frostboltCoroutine;
        private float _duration;
        private float _speed;
        private Vector3 _direction;

        public float Duration => _duration;
        public float Speed => _speed;
        public Vector3 Direction => _direction;

        private void OnEnable()
        {
            Activate();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
        }

        override protected void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _duration = WeaponStats[CurrentLevel - 1].Duration;
            _speed = WeaponStats[CurrentLevel - 1].Speed;
        }

        private IEnumerator StartThrowFrostbolt()
        {
            while (enabled)
            {
                for (int i = 0; i < _shootPoints.Count; i++)
                {
                    _direction = (_shootPoints[i].position - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

                    GameObject frozenbolt = _objectPool.GetFromPool();
                    frozenbolt.transform.SetParent(_container);
                    frozenbolt.transform.position = _shootPoints[i].position;
                    frozenbolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                }
                yield return _timeBetweenAttack;
            }
        }


        public void Activate()
        {
            SetStats(0);
            _frostboltCoroutine = StartCoroutine(StartThrowFrostbolt());
        }

        public void Deactivate()
        {
            if (_frostboltCoroutine != null) StopCoroutine(_frostboltCoroutine);
        }
    }
}
    

