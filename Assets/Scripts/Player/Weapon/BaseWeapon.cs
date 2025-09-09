using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new List<WeaponStats>();
        [SerializeField] private float _damage;
        private DiContainer _diContainer;
        private int _currentHealt = 1;
        private int _maxHealt = 8;
        public List<WeaponStats> WeaponStats => _weaponStats;
        public float Damage => _damage;
        public int CurrentHealt => _currentHealt;
        public int MaxHealt => _maxHealt;

        private void Awake()
        {
            _diContainer.Inject(this);
        }

        private void Start()
        {
            SetStats(0);
        }

        public virtual void LevelUp()
        {
            if(_currentHealt < _maxHealt)
            {
                _currentHealt++;
            }
            SetStats(_currentHealt - 1);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(_damage / 2f, _damage * 2f);
                enemy.TakeDamage(damage);
            }

        }

        protected virtual void SetStats(int value)
        {
            _damage = _weaponStats[value].damage;
        }

        [Inject] private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
    }
}

