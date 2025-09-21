using GameCore.Health;
using Player.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enemy;
using System.Diagnostics;
using Zenject;

namespace Enemy
{
    public class EnemyHealth : ObjectHealth
    {
        [SerializeField] private WaitForSeconds _tick = new WaitForSeconds(2f);
        [SerializeField] private float _enemyDamage;

        private IEnumerator StartBurn(float damage)
        {
           if (gameObject.activeSelf == false)
                yield break;

            float burnDamage = damage / 3f;

            if (burnDamage < 1)
                burnDamage = 1;

            float roundDamage = Mathf.Round(burnDamage);

            for (int i = 0; i < 5; i++)
            {
                TakeDamage(roundDamage);
                yield return _tick;
            }
            
        }
        public void Burn(float damage)
        {
            StartCoroutine(StartBurn(2f));
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }

    }
}

