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

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"AuraWeapon Trigger entered by: {other.gameObject.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                if (!_enemiesInZone.Contains(enemy))
                {
                    _enemiesInZone.Add(enemy);
                    Debug.Log($"Enemy ADDED to _enemiesInZone: {enemy.gameObject.name}, Health: {enemy.CurrentHealth}");
                }
            }
            else
            {
                Debug.Log($"No EnemyHealth component on: {other.gameObject.name}");
                if (other.gameObject.TryGetComponent(out EnemyCollision enemyCollision))
                {
                    Debug.Log($"EnemyCollision FOUND on: {other.gameObject.name}");
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemiesInZone.Remove(enemy);
                Debug.Log($"Enemy REMOVED from _enemiesInZone: {enemy.gameObject.name}");
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel - 1].Range;
            _targetContainer.transform.localScale = Vector3.one * _range;
            _circleCollider.radius = _range / 25f;
            _circleCollider.isTrigger = true;
            Debug.Log($"AuraWeapon: Damage = {_damage}, Range = {_range}, Collider Radius = {(_circleCollider != null ? _circleCollider.radius.ToString() : "null")}");
        }

        private IEnumerator CheckZone()
        {
            while (enabled)
            {
                Debug.Log($"CheckZone: Enemies in zone = {_enemiesInZone.Count}");
                for (int i = 0; i < _enemiesInZone.Count; i++)
                {
                    if (_enemiesInZone[i] != null)
                    {
                        Debug.Log($"Dealing {_damage} damage to {_enemiesInZone[i].gameObject.name}");
                        _enemiesInZone[i].TakeDamage(_damage);
                        // Или используйте Burn: _enemiesInZone[i].Burn(_damage);
                    }
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

