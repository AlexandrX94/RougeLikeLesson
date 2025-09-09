using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WeaponStats
    {
        [SerializeField] private float _speed, _damage, _range, _timeBetweenAttack, _duration;
        public float speed => _speed;
        public float damage => _damage;
        public float range => _range;
        public float timeBetweenAttack => _timeBetweenAttack;
        public float duration => _duration;
    }
}

