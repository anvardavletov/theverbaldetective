using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;

namespace WordDetective.Tests
{
    public class ObjectPoolTests
    {
        private GameObject gameObject;
        private ObjectPool objectPool;
        private GameObject testPrefab;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            objectPool = gameObject.AddComponent<ObjectPool>();
            
            // Создаем тестовый префаб
            testPrefab = new GameObject("TestPrefab");
            testPrefab.AddComponent<TestComponent>();
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
            UnityEngine.Object.DestroyImmediate(testPrefab);
        }
        
        [Test]
        public void CreatePool_InitializesPoolWithCorrectSize()
        {
            // Arrange
            int poolSize = 5;
            
            // Act
            objectPool.CreatePool(testPrefab, poolSize);
            
            // Assert
            var pool = objectPool.GetPool(testPrefab);
            Assert.AreEqual(poolSize, pool.Count);
        }
        
        [Test]
        public void GetObject_ReturnsActiveObject()
        {
            // Arrange
            objectPool.CreatePool(testPrefab, 1);
            
            // Act
            var obj = objectPool.GetObject(testPrefab);
            
            // Assert
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.activeInHierarchy);
        }
        
        [Test]
        public void ReturnObject_DeactivatesObject()
        {
            // Arrange
            objectPool.CreatePool(testPrefab, 1);
            var obj = objectPool.GetObject(testPrefab);
            
            // Act
            objectPool.ReturnObject(obj);
            
            // Assert
            Assert.IsFalse(obj.activeInHierarchy);
        }
        
        [Test]
        public void GetObject_WhenPoolEmpty_CreatesNewObject()
        {
            // Arrange
            objectPool.CreatePool(testPrefab, 1);
            var obj1 = objectPool.GetObject(testPrefab);
            objectPool.ReturnObject(obj1);
            
            // Act
            var obj2 = objectPool.GetObject(testPrefab);
            
            // Assert
            Assert.IsNotNull(obj2);
            Assert.IsTrue(obj2.activeInHierarchy);
        }
        
        [Test]
        public void ClearAllPools_DestroysAllObjects()
        {
            // Arrange
            objectPool.CreatePool(testPrefab, 2);
            var obj1 = objectPool.GetObject(testPrefab);
            var obj2 = objectPool.GetObject(testPrefab);
            
            // Act
            objectPool.ClearAllPools();
            
            // Assert
            Assert.IsTrue(obj1 == null || !obj1.activeInHierarchy);
            Assert.IsTrue(obj2 == null || !obj2.activeInHierarchy);
        }
    }
    
    // Вспомогательный компонент для тестов
    public class TestComponent : MonoBehaviour
    {
    }
} 