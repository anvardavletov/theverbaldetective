using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;

namespace WordDetective.Tests
{
    public class ComponentCacheTests
    {
        private GameObject gameObject;
        private ComponentCache componentCache;
        private TestComponent testComponent;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            componentCache = gameObject.AddComponent<ComponentCache>();
            testComponent = gameObject.AddComponent<TestComponent>();
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
        
        [Test]
        public void GetComponent_ReturnsCachedComponent()
        {
            // Act
            var component1 = componentCache.GetComponent<TestComponent>();
            var component2 = componentCache.GetComponent<TestComponent>();
            
            // Assert
            Assert.IsNotNull(component1);
            Assert.AreEqual(component1, component2);
        }
        
        [Test]
        public void GetComponent_WithNewComponent_AddsToCache()
        {
            // Arrange
            var newComponent = gameObject.AddComponent<AnotherTestComponent>();
            
            // Act
            var cachedComponent = componentCache.GetComponent<AnotherTestComponent>();
            
            // Assert
            Assert.IsNotNull(cachedComponent);
            Assert.AreEqual(newComponent, cachedComponent);
        }
        
        [Test]
        public void ClearCache_RemovesAllCachedComponents()
        {
            // Arrange
            var component1 = componentCache.GetComponent<TestComponent>();
            var component2 = gameObject.AddComponent<AnotherTestComponent>();
            var cachedComponent2 = componentCache.GetComponent<AnotherTestComponent>();
            
            // Act
            componentCache.ClearCache();
            
            // Assert
            var newComponent1 = componentCache.GetComponent<TestComponent>();
            var newComponent2 = componentCache.GetComponent<AnotherTestComponent>();
            
            Assert.IsNotNull(newComponent1);
            Assert.IsNotNull(newComponent2);
            Assert.AreNotEqual(component1, newComponent1);
            Assert.AreNotEqual(component2, newComponent2);
        }
    }
    
    // Вспомогательные компоненты для тестов
    public class TestComponent : MonoBehaviour
    {
    }
    
    public class AnotherTestComponent : MonoBehaviour
    {
    }
} 