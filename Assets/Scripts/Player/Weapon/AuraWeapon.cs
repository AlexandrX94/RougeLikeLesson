using Enemy;
using GameCore;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        private List<EnemyMovement> _enemyMovements = new List<EnemyMovement>();

        protected override void Start()
        {
            base.Start();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            SetStats(0);
            Activate();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null && !_enemiesInZone.Contains(enemy))
            {
                _enemiesInZone.Add(enemy);

                var enemyMovement = other.GetComponent<EnemyMovement>();
                if (enemyMovement != null && CurrentLevel >= 5)
                {
                    _enemyMovements.Add(enemyMovement);
                    enemyMovement.SlowMove(0.5f); 
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null && _enemiesInZone.Contains(enemy))
            {
                _enemiesInZone.Remove(enemy);
                var enemyMovement = other.GetComponent<EnemyMovement>();
                if (enemyMovement != null && _enemyMovements.Contains(enemyMovement))
                {
                    _enemyMovements.Remove(enemyMovement);
                    enemyMovement.RestoreSpeed(); 
                }
            }
        }

        
        protected override void SetStats(int value)
        {
            base.SetStats(value);

            if (_damage <= 0f)  
            {
                if (WeaponStats != null && WeaponStats.Count > value && WeaponStats[value] != null)
                {
                    _damage = WeaponStats[value].Damage;  
                }
            }

            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel - 1].Range;

            if (_targetContainer != null)
            {
                _targetContainer.localScale = Vector3.one * _range;
            }
            if (_circleCollider != null)
            {
                _circleCollider.radius = _range / 4.5f;
                _circleCollider.isTrigger = true;
            }

        }
        

        private IEnumerator CheckZone()
        {
            while (enabled)
            {
                int hitCount = 0;
                for (int i = _enemiesInZone.Count - 1; i >= 0; i--)
                {
                    _enemiesInZone[i].TakeDamage(Damage);
                    hitCount++;
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
                _auraCoroutine = null;
            }
                
        }

    }
}

