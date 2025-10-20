
using System;
using System.Collections;
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
            float targetHealth = Mathf.Max(0, _currentHealth - damage);
            if (damage <= 0) 
            {
                // <-- Улучшенный warning с стектрейсом (Unity покажет в Console полный вызов)
                Debug.LogWarning($"Invalid damage: {damage} on {gameObject.name} (HP: {_currentHealth}). Called from: {new System.Diagnostics.StackTrace().ToString().Split('\n')[1]}");
                return; // <-- Не throw, чтобы не крашить

                //throw new ArgumentOutOfRangeException(nameof(damage)); 
            }
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