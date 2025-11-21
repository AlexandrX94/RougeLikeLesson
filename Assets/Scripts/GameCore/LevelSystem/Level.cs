using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    [Serializable]
    public class Level : IActivate
    {
        [SerializeField] private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

        public void Activate()
        {
            for(int i = 0; i < _enemySpawners.Count; i++ )
            {
                _enemySpawners[i].Activate();
            }
        }

        public void Deactivate()
        {
            for (int i = 0; i < _enemySpawners.Count; i++)
            {
                _enemySpawners[i].Deactivate();
            }
        }
    }
}

