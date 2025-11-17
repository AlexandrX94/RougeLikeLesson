using Player;
using Player.Weapon;
using Player.Weapon.Bow;
using Player.Weapon.Frostbolt;
using Player.Weapon.Suriken;
using Player.Weapon.Trap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private SurikenWeapon _surikenWeapon;
        [SerializeField] private FrostboltWeapon _frostboltWeapon;
        [SerializeField] private TrapWeapon _trapWeapon;
        [SerializeField] private BowWeapon _bowWeapon;
        [SerializeField] private SwordWeapon _swordWeapon;

        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
            Container.Bind<SurikenWeapon>().FromInstance(_surikenWeapon).AsSingle().NonLazy();
            Container.Bind<FrostboltWeapon>().FromInstance(_frostboltWeapon).AsSingle().NonLazy();
            Container.Bind<TrapWeapon>().FromInstance(_trapWeapon).AsSingle().NonLazy();
            Container.Bind<BowWeapon>().FromInstance(_bowWeapon).AsSingle().NonLazy();
            Container.Bind<SwordWeapon>().FromInstance(_swordWeapon).AsSingle().NonLazy();
        }
    }
}

