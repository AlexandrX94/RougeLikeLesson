using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Suriken
{
    public class Suriken : Projectile
    {
        [SerializeField] private Transform _sprite;
        [SerializeField] private Transform _startPoint;
        private Rigidbody2D _rb2D;
        private SurikenWeapon _surikenWeapon;
        private float _returnSpeed = 3f;
        private float _returnTimer = 3f;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_surikenWeapon.Duration);
            Damage = _surikenWeapon._Damage;
        }

        private void Start()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                _startPoint = playerObject.transform;
            }
        }

        private void Update()
        {
            _returnTimer -= Time.deltaTime;
            transform.Rotate(0, 0, 500f * Time.deltaTime);
            transform.position += _surikenWeapon.Direction * (_surikenWeapon.Speed * Time.deltaTime);

            if (_returnTimer <= 0 && _surikenWeapon.CurrentLevel >= 5)
            {
                transform.position += (_surikenWeapon.transform.position - transform.position).normalized;
            }
            
        }


        [Inject] private void Construct(SurikenWeapon surikenWeapon) => _surikenWeapon = surikenWeapon;
    }
}
