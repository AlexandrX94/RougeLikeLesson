using GameCore;
using GameCore.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Player.Weapon.Trap
{
    public class TrapWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _trapCoroutine;

        private void OnEnable()
        {
            Activate();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }

        private IEnumerator SpawnTrap()
        {
            while(enabled)
            {
                GameObject trap = _objectPool.GetFromPool();
                trap.transform.SetParent(_container);
                trap.transform.position = transform.position;
                yield return _timeBetweenAttack;
            }
        }

        public void Activate()
        {
            StartCoroutine(SpawnTrap());
            SetStats(0);
        }

        public void Deactivate()
        {
            if(_trapCoroutine != null)
            {
                StopCoroutine(_trapCoroutine);
            }
        }
    }
}
