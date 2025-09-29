using Enemy;
using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Transform _targetContainer;
        [SerializeField] private CircleCollider2D _circleCollider;
        private List<EnemyHealth> _enemiesInZone = new List<EnemyHealth>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _auraCoroutine;
        private float _range;

        protected override void Start()
        {
            base.Start();
            SetStats(0);
            Activate();
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                if (!_enemiesInZone.Contains(enemy))
                {
                    _enemiesInZone.Add(enemy);
                }
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var enemy = other.GetComponentInParent<EnemyHealth>();
            _enemiesInZone.Remove(enemy);
            
        }

        private void OnTriggerStay(Collider other)
        {
            var enemy = other.GetComponentInParent<EnemyHealth>();
            for (int i = 0; i < _enemiesInZone.Count; i++)
            {
                _enemiesInZone[i].TakeDamage(Damage);
            }
        }



        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel - 1].Range;
            _targetContainer.transform.localScale = Vector3.one * _range;
            _circleCollider.radius = _range / 4.5f;
            _circleCollider.isTrigger = true;
        }

        private IEnumerator CheckZone()
        {
            while (enabled)
            {
                for (int i = 0; i < _enemiesInZone.Count; i++)
                {
                    _enemiesInZone[i].TakeDamage(Damage);
                }
                yield return _timeBetweenAttack;
            }
        }

        public void Activate()
        {
            SetStats(0);
            _auraCoroutine = StartCoroutine(CheckZone());
        }

        public void Deactivate()
        {
            if (_auraCoroutine != null)
            { 
                StopCoroutine(_auraCoroutine);
            }
                
        }

    }
}

