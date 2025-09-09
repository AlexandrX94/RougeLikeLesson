using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GetRandonSpawnPoint>().FromNew().AsSingle().NonLazy();
        }
    }
}

