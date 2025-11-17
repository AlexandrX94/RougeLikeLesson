using GameCore;
using GameCore.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player.Weapon
{
    public class SwordWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _edgePoint, _weaponTransform;
        [SerializeField] private Animator _animator;

        private WaitForSeconds _timeBetweenAttack;
        private PlayerMovement _playerMovement;
        private Coroutine _swordCoroutine;
        private Vector3 _direction;
        private float _duration;
        private float _speed;
        private float _damage;

        public float Speed => _speed;
        public float Damage => _damage;

        private void OnEnable()
        {
            Activate();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _damage = WeaponStats[WeaponStats.Count - 1].Damage;
        }

        private void Update()
        {
            _direction = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            _weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        

        public void Activate()
        {
            SetStats(0);
        }

        public void Deactivate()
        {
            
        }
        
        
        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }
    }
}