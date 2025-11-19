using System.Collections;
using UnityEngine;
using GameCore.Pool;
using Zenject;

namespace Player.Weapon.Suriken
{
    public class Suriken : Projectile
    {
        [SerializeField] private Transform _sprite;
        private Transform _startPoint;
        private SurikenWeapon _surikenWeapon;
        private ObjectPool _objectPool; 
        private float _returnSpeed = 3f;
        private float _returnTimer;
        private bool _isReturning;
        private Vector3 _direction; 
        private float _speed;  

        protected override void OnEnable()
        {
            base.OnEnable();
            Damage = _surikenWeapon._Damage;  
            _isReturning = false;
        }

        private void Start()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                _startPoint = playerObject.transform;
            }
        }

        public void Initialize(Vector3 direction, float speed, float duration, ObjectPool pool)
        {
            _direction = direction;
            _speed = speed;
            _returnTimer = 2f;  
            _objectPool = pool;
            Timer = new WaitForSeconds(duration);  
        }

        private void Update()
        {
            transform.Rotate(0, 0, 500f * Time.deltaTime);
            if (!_isReturning)
            {
                transform.position += _direction * (_speed * Time.deltaTime);
                _returnTimer -= Time.deltaTime;
                if (_returnTimer <= 0)
                {
                    _isReturning = true;
                }
            }
            else
            {
                Vector3 returnDirection = (_startPoint.position - transform.position).normalized;
                transform.position += returnDirection * _returnSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, _startPoint.position) < 0.5f)
                {
                    _objectPool.ReturnToPool(gameObject);
                }
            }
        }

        public void StartReturn()
        {
            _isReturning = true;
        }

        [Inject]
        private void Construct(SurikenWeapon surikenWeapon)
        {
            _surikenWeapon = surikenWeapon;
        }
    }
}