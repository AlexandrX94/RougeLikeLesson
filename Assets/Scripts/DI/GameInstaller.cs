using GameCore;
using GameCore.IU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DamageTextSpawner _damageTextSpawner;

        public override void InstallBindings()
        {
            Container.Bind<GetRandonSpawnPoint>().FromNew().AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
        }
    }
}

