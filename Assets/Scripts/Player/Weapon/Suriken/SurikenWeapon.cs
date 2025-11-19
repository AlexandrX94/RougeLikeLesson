using GameCore;
using GameCore.Pool;
using System.Collections;
using UnityEngine;

namespace Player.Weapon.Suriken
{
    public class SurikenWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _startPosition;  

        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _surikenCoroutine;
        private Coroutine _returnCoroutine;  
        private float _duration;
        private float _speed;
        private float _range;
        private float _returnSpeed;

        private void OnEnable()
        {
            Activate();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
        }

        public void Activate()
        {
            SetStats(0);
            _surikenCoroutine = StartCoroutine(SpawnSuriken());
            if (CurrentLevel >= 5)
            {
                _returnCoroutine = StartCoroutine(ReturnCycle());
            }
        }

        public void Deactivate()
        {
            if (_surikenCoroutine != null)
                StopCoroutine(_surikenCoroutine);
            if (_returnCoroutine != null)
                StopCoroutine(_returnCoroutine);
        }

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
            _returnSpeed = WeaponStats[CurrentLevel - 1].Speed;  
        }

        private IEnumerator SpawnSuriken()
        {
            while (enabled)
            {
                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _range, _layerMask);
                if (enemiesInRange.Length > 0)
                {
                    Vector3 targetPosition = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform.position;
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    GameObject surikenGO = _objectPool.GetFromPool();
                    surikenGO.transform.SetParent(_container);
                    surikenGO.transform.position = transform.position;
                    surikenGO.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    
                    Suriken suriken = surikenGO.GetComponent<Suriken>();
                    if (suriken != null)
                    {
                        suriken.Initialize(direction, _speed, _duration, _objectPool);
                    }

                    yield return _timeBetweenAttack;
                }
                else
                {
                    yield return _timeBetweenAttack;
                }
            }
        }

        private IEnumerator ReturnCycle()
        {
            while (enabled)
            {
                ReturnAllSurikens();
                yield return new WaitForSeconds(3f);
                
            }
        }

        private void ReturnAllSurikens()
        {
            Suriken[] activeSurikens = _container.GetComponentsInChildren<Suriken>();
            foreach (Suriken suriken in activeSurikens)
            {
                suriken.StartReturn(); 
            }
        }
    }
}