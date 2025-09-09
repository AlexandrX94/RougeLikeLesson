using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class GetRandonSpawnPoint 
    {
        public Vector3 GetRandomPoint(Transform minPos, Transform maxPos)
        {
            Vector3 spawnPoint = Vector3.zero;
            bool verticalSpawn = Random.Range(0f, 1f) > 0.5f;
            if (verticalSpawn)
            {
                spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
                spawnPoint.x = Random.Range(0f, 1f) > 0.5 ? minPos.position.x : maxPos.position.x;
            }
            else
            {
                spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
                spawnPoint.y = Random.Range(0f, 1f) > 0.5 ? minPos.position.y : maxPos.position.y;
            }
            return spawnPoint;
        }

    }
}

