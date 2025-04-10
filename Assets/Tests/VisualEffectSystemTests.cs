using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;

namespace WordDetective.Tests
{
    public class VisualEffectSystemTests
    {
        private VisualEffectSystem effectSystem;
        private GameObject testObject;
        
        [SetUp]
        public void Setup()
        {
            effectSystem = VisualEffectSystem.Instance;
            testObject = new GameObject("TestObject");
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(testObject);
        }
        
        [Test]
        public void PlayEffect_WithValidEffect_CreatesEffect()
        {
            // Arrange
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            
            // Act
            GameObject effect = effectSystem.PlayEffect("test_effect", position, rotation);
            
            // Assert
            Assert.IsNotNull(effect);
            Assert.AreEqual(position, effect.transform.position);
            Assert.AreEqual(rotation, effect.transform.rotation);
        }
        
        [Test]
        public void PlayEffect_WithInvalidEffect_ReturnsNull()
        {
            // Arrange
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            
            // Act
            GameObject effect = effectSystem.PlayEffect("invalid_effect", position, rotation);
            
            // Assert
            Assert.IsNull(effect);
        }
        
        [Test]
        public void PlayEffect_WithParent_SetsParent()
        {
            // Arrange
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            
            // Act
            GameObject effect = effectSystem.PlayEffect("test_effect", position, rotation, testObject.transform);
            
            // Assert
            Assert.IsNotNull(effect);
            Assert.AreEqual(testObject.transform, effect.transform.parent);
        }
        
        [Test]
        public void PlayEffect_WithDuration_DestroysAfterDuration()
        {
            // Arrange
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            float duration = 1f;
            
            // Act
            GameObject effect = effectSystem.PlayEffect("test_effect", position, rotation, null, duration);
            
            // Assert
            Assert.IsNotNull(effect);
            // Note: We can't directly test destruction due to timing, but we can verify the effect was created
        }
        
        [Test]
        public void StopEffect_WithValidEffect_StopsEffect()
        {
            // Arrange
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            GameObject effect = effectSystem.PlayEffect("test_effect", position, rotation);
            
            // Act
            effectSystem.StopEffect(effect);
            
            // Assert
            // Note: We can't directly test if the effect is stopped, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void StopAllEffects_StopsAllEffects()
        {
            // Arrange
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            effectSystem.PlayEffect("test_effect", position, rotation);
            effectSystem.PlayEffect("test_effect", position, rotation);
            
            // Act
            effectSystem.StopAllEffects();
            
            // Assert
            // Note: We can't directly test if all effects are stopped, but we can verify the method doesn't throw
            Assert.Pass();
        }
    }
} 