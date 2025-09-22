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

        private void OnTriggerStay2D(Collider2D other)
        {
            var enemy = other.GetComponentInParent<EnemyHealth>();
            if (enemy != null && !_enemiesInZone.Contains(enemy)) _enemiesInZone.Add(enemy);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var enemy = other.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                _enemiesInZone.Remove(enemy);
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel - 1].Range;
            _targetContainer.transform.localScale = Vector3.one * _range;
            _circleCollider.radius = _range;
            _circleCollider.isTrigger = true;
            // добавлено
            EnsureHitbox(_circleCollider);
        }

        private IEnumerator CheckZone()
        {
            while (enabled)
            {
                for (int i = 0; i < _enemiesInZone.Count; i++)
                {
                    if (_enemiesInZone[i] != null)
                    {
                        _enemiesInZone[i].TakeDamage(_damage);
                    }
                }
                yield return _timeBetweenAttack;
            }
        }

        public void Activate()
        {
            SetStats(0);
            // добавлено
            PopulateEnemiesAlreadyInZone();
            _auraCoroutine = StartCoroutine(CheckZone());
        }

        public void Deactivate()
        {
            if (_auraCoroutine != null)
            { 
                StopCoroutine(_auraCoroutine);
            }
                
        }

        // добавлено
        private void PopulateEnemiesAlreadyInZone()
        {
            if (_circleCollider == null)
                return;

            var results = new Collider2D[32];
            int count = _circleCollider.OverlapCollider(new ContactFilter2D { useTriggers = true, useLayerMask = false }, results);
            for (int i = 0; i < count && i < results.Length; i++)
            {
                var enemy = results[i]?.GetComponentInParent<EnemyHealth>();
                if (enemy != null && !_enemiesInZone.Contains(enemy))
                {
                    _enemiesInZone.Add(enemy);
                }
            }
        }

        // добавлено
        private void EnsureHitbox(Collider2D collider)
        {
            if (collider == null)
                return;

            var hitbox = collider.GetComponent<WeaponHitbox>();
            if (hitbox == null)
            {
                hitbox = collider.gameObject.AddComponent<WeaponHitbox>();
            }
            hitbox.SetOwner(this);
        }
    }
}

