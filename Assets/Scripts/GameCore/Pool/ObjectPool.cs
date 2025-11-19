using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace GameCore.Pool
{
    public class ObjectPool : MonoBehaviour, IFactory<GameObject>
    {
        [SerializeField] private GameObject _prefab;
        private List<GameObject> _objectPool = new List<GameObject>();
        private DiContainer _diContainer;
        private Queue<GameObject> _pool = new Queue<GameObject>();  
        private Transform _poolParent;

        public GameObject Create()
        {
            GameObject newObject = _diContainer.InstantiatePrefab(_prefab);
            newObject.SetActive(false);
            _objectPool.Add(newObject);
            return newObject;
        }

        public GameObject GetFromPool()
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                if(_objectPool[i].activeInHierarchy) continue;
                _objectPool[i].SetActive(true);
                return _objectPool[i];
            }
            GameObject newObject = Create();
            newObject.SetActive(true);
            return newObject;
            
        }

        public void ReturnToPool(GameObject obj)
        {
            if (obj == null) return;

            obj.SetActive(false);
            obj.transform.SetParent(_poolParent);  // Вернём под родителя пула
            obj.transform.localPosition = Vector3.zero;  // Сброс позиции (опционально)
            obj.transform.localRotation = Quaternion.identity;  // Сброс ротации (опционально)

            _pool.Enqueue(obj);
        }

        [Inject] private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        

    }
}

