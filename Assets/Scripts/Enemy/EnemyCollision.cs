using Player;
using Player.Weapon;
using UnityEngine;
using Enemy;

namespace Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger)
            {
                if (other.gameObject.TryGetComponent(out PlayerHealth player))
                {
                    player.TakeDamage(_damage);
                    gameObject.SetActive(false);
                }
                
                return;
            }

        }
        
    }

}