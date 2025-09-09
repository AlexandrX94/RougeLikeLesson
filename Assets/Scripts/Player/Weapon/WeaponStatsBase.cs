using UnityEngine;

namespace Player
{
    public class WeaponStatsBase
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _duration;
        [SerializeField] private float _range;
        [SerializeField] private float _speed;
        [SerializeField] private float _timeBetweenAttack;
    }
}