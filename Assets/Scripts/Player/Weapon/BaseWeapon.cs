using Enemy;
using GameCore;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Zenject;


namespace Player.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new List<WeaponStats>();
        //c!!!!
        protected float _damage;
        private DiContainer _diContainer;
        private int _currentLevel = 1;
        private int _maxLevel = 8;
        public List<WeaponStats> WeaponStats => _weaponStats;
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => _maxLevel;
        public float Damage => _damage;


        [Inject] private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        private void Awake()
        {
            _diContainer.Inject(this);
        }

        protected virtual void Start()
        {
            SetStats(0);
        }

        // добавлено
        public float GetCurrentDamage()
        {
            return _damage;
        }

        public virtual void LevelUp()
        {
            if (_currentLevel < _maxLevel)
                _currentLevel++;

            SetStats(_currentLevel - 1);

        }
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (this is AuraWeapon)
            {
                return; 
            }


            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(_damage / 2f, _damage * 2f);
                enemy.TakeDamage(damage);
            }
        }
       
        protected virtual void SetStats(int value)
        {
            if (_weaponStats != null && _weaponStats.Count > value && _weaponStats[value] != null)
            {
                _damage = _weaponStats[value].Damage;
            }
        }

    }
}

