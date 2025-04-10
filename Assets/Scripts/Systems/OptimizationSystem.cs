using UnityEngine;
using System.Collections.Generic;

public class OptimizationSystem : MonoBehaviour
{
    [Header("Настройки оптимизации")]
    [SerializeField] private bool enableObjectPooling = true;
    [SerializeField] private bool enableLODSystem = true;
    [SerializeField] private bool enableOcclusionCulling = true;
    
    [Header("Настройки пула объектов")]
    [SerializeField] private int initialPoolSize = 20;
    [SerializeField] private int maxPoolSize = 100;
    
    private Dictionary<string, Queue<GameObject>> objectPools;
    private Dictionary<string, GameObject> poolPrefabs;
    private static OptimizationSystem instance;
    
    public static OptimizationSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<OptimizationSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("OptimizationSystem");
                    instance = go.AddComponent<OptimizationSystem>();
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeOptimization();
    }
    
    private void InitializeOptimization()
    {
        if (enableObjectPooling)
        {
            InitializeObjectPools();
        }
        
        if (enableLODSystem)
        {
            SetupLODSystem();
        }
        
        if (enableOcclusionCulling)
        {
            SetupOcclusionCulling();
        }
    }
    
    private void InitializeObjectPools()
    {
        objectPools = new Dictionary<string, Queue<GameObject>>();
        poolPrefabs = new Dictionary<string, GameObject>();
    }
    
    public void RegisterPoolObject(string poolId, GameObject prefab)
    {
        if (!enableObjectPooling)
            return;
            
        if (!poolPrefabs.ContainsKey(poolId))
        {
            poolPrefabs[poolId] = prefab;
            objectPools[poolId] = new Queue<GameObject>();
            
            // Предварительное создание объектов
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreatePoolObject(poolId);
            }
        }
    }
    
    private GameObject CreatePoolObject(string poolId)
    {
        if (poolPrefabs.TryGetValue(poolId, out GameObject prefab))
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            return obj;
        }
        return null;
    }
    
    public GameObject GetPooledObject(string poolId)
    {
        if (!enableObjectPooling || !objectPools.ContainsKey(poolId))
            return null;
            
        Queue<GameObject> pool = objectPools[poolId];
        
        if (pool.Count == 0)
        {
            if (pool.Count < maxPoolSize)
            {
                return CreatePoolObject(poolId);
            }
            return null;
        }
        
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    
    public void ReturnToPool(string poolId, GameObject obj)
    {
        if (!enableObjectPooling || !objectPools.ContainsKey(poolId))
            return;
            
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        objectPools[poolId].Enqueue(obj);
    }
    
    private void SetupLODSystem()
    {
        // Настройка системы LOD для всех объектов с компонентом LODGroup
        LODGroup[] lodGroups = FindObjectsOfType<LODGroup>();
        foreach (LODGroup lodGroup in lodGroups)
        {
            OptimizeLODGroup(lodGroup);
        }
    }
    
    private void OptimizeLODGroup(LODGroup lodGroup)
    {
        LOD[] lods = lodGroup.GetLODs();
        for (int i = 0; i < lods.Length; i++)
        {
            // Настройка расстояний для каждого уровня детализации
            float distance = Mathf.Pow(2, i) * 10f;
            lods[i].screenRelativeTransitionHeight = 1f / distance;
        }
        lodGroup.SetLODs(lods);
    }
    
    private void SetupOcclusionCulling()
    {
        // Настройка окклюзии для камеры
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.useOcclusionCulling = true;
        }
    }
    
    public void OptimizeScene()
    {
        // Оптимизация сцены
        OptimizeLighting();
        OptimizeTextures();
        OptimizeMeshes();
    }
    
    private void OptimizeLighting()
    {
        // Оптимизация освещения
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            if (light.type == LightType.Point || light.type == LightType.Spot)
            {
                light.renderMode = LightRenderMode.ForcePixel;
            }
        }
    }
    
    private void OptimizeTextures()
    {
        // Оптимизация текстур
        Texture[] textures = Resources.FindObjectsOfTypeAll<Texture>();
        foreach (Texture texture in textures)
        {
            if (texture != null)
            {
                texture.anisoLevel = 1;
            }
        }
    }
    
    private void OptimizeMeshes()
    {
        // Оптимизация мешей
        MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh != null)
            {
                meshFilter.sharedMesh.RecalculateBounds();
            }
        }
    }
    
    private void OnDestroy()
    {
        // Очистка пулов объектов
        if (objectPools != null)
        {
            foreach (var pool in objectPools.Values)
            {
                while (pool.Count > 0)
                {
                    GameObject obj = pool.Dequeue();
                    if (obj != null)
                    {
                        Destroy(obj);
                    }
                }
            }
        }
    }
} 