using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player.Weapon;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Frostbolt
{
    public class Frostbolt : Projectile
    {
        private FrostboltWeapon _frostboltWeapon;

        private void Update()
        {
            transform.position += transform.right * (_frostboltWeapon.Speed * Time.deltaTime);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_frostboltWeapon.Duration);
            Damage = _frostboltWeapon._Damage;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);
                enemy.GetComponent<EnemyMovement>().SlowMove();
                gameObject.SetActive(false);
                if (_frostboltWeapon.CurrentLevel >= 5)
                {
                    if(Random.Range(0f, 1f) <= 0.5f)
                    {
                        enemy.GetComponent<EnemyMovement>().FrozenEnemy();
                    }
                    
                }
            }
        }

        [Inject] private void Construct(FrostboltWeapon frostboltWeapon)
        {
            _frostboltWeapon = frostboltWeapon;
        }


    }
}

