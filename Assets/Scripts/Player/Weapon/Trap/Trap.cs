using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Trap
{
    public class Trap : Projectile
    {
        [SerializeField] private CircleCollider2D _collider;
        private WaitForSeconds _checkInterval = new WaitForSeconds(3f);
        private PlayerHealth _playerHealth;
        private TrapWeapon _trapWeapon;

        protected override void OnEnable()
        {
            Damage = _trapWeapon._Damage;
            _collider.enabled = false;
            StartCoroutine(CheckDistance());

        }

        public void ActivateTrap()
        {
            _collider.enabled = true;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);
                if (enemy.gameObject.activeSelf)
                {
                    enemy.Burn(Damage);
                }
                gameObject.SetActive(false);
            }
        }

        private IEnumerator CheckDistance()
        {
            while(enabled)
            {
                if (Vector3.Distance(transform.position, _playerHealth.gameObject.transform.position) > 15f)
                {
                    gameObject.SetActive(false); 
                }

                yield return _checkInterval;
            }
        }

        [Inject] private void Construct(PlayerHealth playerHealth, TrapWeapon trapWeapon)
        {
            _playerHealth = playerHealth;
            _trapWeapon = trapWeapon;
        }
    }
}
