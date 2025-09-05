using GameCore.Health;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : ObjectHealth
    {
        public Action OnHealthChange;

        private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
        private float _regenarationValue = 1f;
         

        private IEnumerator Regeneration()
        {
            while (enabled)
            {
                TakeHeal(_regenarationValue);
                OnHealthChange?.Invoke();
                yield return _regenerationInterval;
            }
        }

        private void Start()
        {
            StartCoroutine(Regeneration());
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