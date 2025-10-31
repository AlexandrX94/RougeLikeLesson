using GameCore.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Bow
{
    public class Arrow : Projectile
    {
        private BowWeapon _bowWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_bowWeapon.Duration);
            Damage = _bowWeapon._Damage;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if(_bowWeapon.CurrentLevel <= 4)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            transform.position += transform.up * (-1 * _bowWeapon.Speed * Time.deltaTime);
        }

        [Inject] private void Construct(BowWeapon bowWeapon)
        {
            _bowWeapon = bowWeapon;
        }

    }
}