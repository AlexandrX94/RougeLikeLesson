using GameCore.Health;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : ObjectHealth
    {
        private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
        private float _regenarationValue = 1f;

        private IEnumerator Regeneration()
        {
            while (enabled)
            {
                TakeHeal(_regenarationValue);
                yield return _regenerationInterval;
            }
        }

        private void Start()
        {
            StartCoroutine(Regeneration());
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if(CurrentHealth <= 0)
            {
                Debug.Log("Игрок умер");
            }

        }
    }
}