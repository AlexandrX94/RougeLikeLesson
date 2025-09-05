
using System;
using UnityEngine;

namespace GameCore.Health
{
    public abstract class ObjectHealth : MonoBehaviour, IDamageble
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;

        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;

        public virtual void TakeDamage(float damage)
        {
            if (damage <= 0) { throw new ArgumentOutOfRangeException(nameof(damage)); }
            _currentHealth -= damage;
        }

        public void TakeHeal(float heal)
        {
            if (heal <= 0) 
            { 
                throw new ArgumentOutOfRangeException(nameof(heal)); 
            }

            _currentHealth += heal;

            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

        private void OnEnable() =>  _currentHealth = _maxHealth;
        
    }

}