using Enemy;
using GameCore.Health;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : ObjectHealth
    {
        public Action OnHealthChange;

        private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
        private WaitForSeconds _burnTick = new WaitForSeconds(2f);
        private float _regenarationValue = 1f;
        private float _burnValue = 5f;

        private IEnumerator Regeneration()
        {
            while (enabled)
            {
                TakeHeal(_regenarationValue);
                OnHealthChange?.Invoke();
                yield return _regenerationInterval;
            }
        }

        private IEnumerator StartBurn()
        {
            for (int i = 0; i < 5; i++)
            {
                TakeDamage(_burnValue);
                yield return _burnTick;
            }

                OnHealthChange?.Invoke();
        }

        private void Start()
        {
            StartCoroutine(Regeneration());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyCollision enemyCollision))
            {
                StartCoroutine(StartBurn());
            }
        }

        public void Heal(float value)
        {
            TakeHeal(value);
            OnHealthChange?.Invoke();
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            OnHealthChange?.Invoke();

            if (CurrentHealth <= 0)
            {
                Debug.Log("Игрок умер");
            }

        }
    }
}