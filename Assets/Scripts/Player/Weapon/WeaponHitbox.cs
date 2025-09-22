using Enemy;
using UnityEngine;

namespace Player.Weapon
{
    public class WeaponHitbox : MonoBehaviour
    {
        private BaseWeapon _owner;

        public void SetOwner(BaseWeapon owner)
        {
            _owner = owner;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_owner == null)
                return;

            var enemy = other.GetComponentInParent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(_owner.GetCurrentDamage());
            }
        }
    }
}


