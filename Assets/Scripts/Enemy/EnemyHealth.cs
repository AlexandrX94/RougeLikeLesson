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
        [SerializeField] private AudioSource _audioSource;

        private IEnumerator StartBurn(float damage) 
        {
            if (gameObject.activeSelf == false || damage <= 0)
                yield break;

            float burnDamage = damage / 3f;
            if (burnDamage < 1) burnDamage = 1;
            float roundDamage = Mathf.Round(burnDamage);

            for (int i = 0; i < 5; i++)
            {
                TakeDamage(roundDamage); 
                yield return _tick;
            }
        }

        public void Burn(float damage)
        {
            StartCoroutine(StartBurn(damage));
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            _audioSource.Play();
            if (CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }


    }
}

