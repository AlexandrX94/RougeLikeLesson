using Enemy;
using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapon
{
    public class FireballWeapon : BaseWeapon, IActivate
    {


        [Header("Single")]
        [SerializeField] private SpriteRenderer _spriteRenderer1X;
        [SerializeField] private Collider2D _collider1X;
        [SerializeField] private Transform _transformSprite1X, _targetContainer1X;

        [Header("Double")]
        [SerializeField] private List<SpriteRenderer> _spriteRenderer2X = new List<SpriteRenderer>();
        [SerializeField] private List<Collider2D> _colleder2X = new List<Collider2D>();
        [SerializeField] private List<Transform> _transformSprite2X = new List<Transform>();
        [SerializeField] private Transform _targetContainer2X;

        private WaitForSeconds _duration, _interval, _timeBetweenAtack;
        private float _rotationSpeed, _range;
        private Coroutine _attackCoroutine;

        protected override void Start()
        {
            base.Start();
            SetStats(0);
            SetupWeapon();
            Activate();
        }

        private void Update()
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }

        public override void LevelUp()
        {
            base.LevelUp();
            SetupWeapon();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _rotationSpeed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].Duration);
            _timeBetweenAtack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }

        private void SetupWeapon()
        {
            if (CurrentLevel < 4)
            {
                _targetContainer1X.gameObject.SetActive(true);
                _targetContainer2X.gameObject.SetActive(false);
                _transformSprite1X.localPosition = new Vector3(_range, 0, 0);

                //_collider1X.offset = new Vector2(_range, 0);
                _collider1X.isTrigger = true;

            }
            else
            {
                _targetContainer1X.gameObject.SetActive(false);
                _targetContainer2X.gameObject.SetActive(true);

                for (int i = 0; i < _colleder2X.Count; i++)
                {
                    _colleder2X[i].gameObject.SetActive(true);
                    _colleder2X[i].isTrigger = true;
                }
                _transformSprite2X[0].localPosition = new Vector2(_range, 0);
                _transformSprite2X[1].localPosition = new Vector2(-_range, 0);
                //_colleder2X[0].offset = new Vector2(_range, 0);
                //_colleder2X[1].offset = new Vector2(-_range, 0);
            }

        }

        private IEnumerator WeaponLifeCycle()
        {
            while (enabled)
            {
                if (CurrentLevel < 4)
                {
                    _spriteRenderer1X.enabled = !_spriteRenderer1X.enabled;
                    _collider1X.enabled = !_collider1X.enabled;
                }
                else
                {
                    for (int i = 0; i < _spriteRenderer2X.Count; i++)
                    {
                        _spriteRenderer2X[i].enabled = !_spriteRenderer2X[i].enabled;
                        _colleder2X[i].enabled = !_colleder2X[i].enabled;
                    }
                }

                _interval = _spriteRenderer1X.enabled || _spriteRenderer2X[0].enabled ? _duration : _timeBetweenAtack;
                yield return _interval;
            }

        }

        public void Activate()
        {
            _attackCoroutine = StartCoroutine(WeaponLifeCycle());
        }

        public void Deactivate()
        {
            if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
        }
    }

}