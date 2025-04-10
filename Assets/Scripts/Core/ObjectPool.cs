using UnityEngine;
using System.Collections.Generic;

namespace WordDetective.Core
{
    /// <summary>
    /// Система пулинга объектов для оптимизации
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool _instance;
        public static ObjectPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("ObjectPool");
                    _instance = go.AddComponent<ObjectPool>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }
        
        // Словарь для хранения пулов объектов
        private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
        
        // Словарь для хранения префабов
        private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
        
        /// <summary>
        /// Создание пула объектов
        /// </summary>
        public void CreatePool(GameObject prefab, int initialSize)
        {
            string key = prefab.name;
            
            if (pools.ContainsKey(key))
            {
                Debug.LogWarning($"Пул для {key} уже существует");
                return;
            }
            
            // Создание очереди для пула
            Queue<GameObject> pool = new Queue<GameObject>();
            
            // Создание начальных объектов
            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = CreateNewObject(prefab);
                pool.Enqueue(obj);
            }
            
            // Добавление пула в словарь
            pools[key] = pool;
            prefabs[key] = prefab;
        }
        
        /// <summary>
        /// Получение объекта из пула
        /// </summary>
        public GameObject GetObject(GameObject prefab)
        {
            string key = prefab.name;
            
            if (!pools.ContainsKey(key))
            {
                Debug.LogWarning($"Пул для {key} не существует");
                return null;
            }
            
            Queue<GameObject> pool = pools[key];
            
            // Если пул пуст, создаем новый объект
            if (pool.Count == 0)
            {
                GameObject obj = CreateNewObject(prefab);
                return obj;
            }
            
            // Получение объекта из пула
            GameObject pooledObject = pool.Dequeue();
            pooledObject.SetActive(true);
            
            return pooledObject;
        }
        
        /// <summary>
        /// Возврат объекта в пул
        /// </summary>
        public void ReturnObject(GameObject obj)
        {
            string key = obj.name.Replace("(Clone)", "").Trim();
            
            if (!pools.ContainsKey(key))
            {
                Debug.LogWarning($"Пул для {key} не существует");
                return;
            }
            
            // Деактивация объекта
            obj.SetActive(false);
            
            // Возврат объекта в пул
            pools[key].Enqueue(obj);
        }
        
        /// <summary>
        /// Создание нового объекта
        /// </summary>
        private GameObject CreateNewObject(GameObject prefab)
        {
            GameObject obj = Instantiate(prefab);
            obj.name = prefab.name;
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            
            return obj;
        }
        
        /// <summary>
        /// Очистка всех пулов
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in pools.Values)
            {
                while (pool.Count > 0)
                {
                    GameObject obj = pool.Dequeue();
                    Destroy(obj);
                }
            }
            
            pools.Clear();
            prefabs.Clear();
        }
        
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
} 