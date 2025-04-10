using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class HintSystemTests
    {
        private HintSystem hintSystem;
        private GameModeConfig testConfig;
        
        [SetUp]
        public void Setup()
        {
            hintSystem = HintSystem.Instance;
            testConfig = ScriptableObject.CreateInstance<GameModeConfig>();
            testConfig.AvailableHints = 3;
            testConfig.HintCost = 50;
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(testConfig);
        }
        
        [Test]
        public void Initialize_WithConfig_SetsCorrectValues()
        {
            // Act
            hintSystem.Initialize(testConfig);
            
            // Assert
            Assert.AreEqual(testConfig.AvailableHints, hintSystem.AvailableHints);
            Assert.AreEqual(testConfig.HintCost, hintSystem.HintCost);
        }
        
        [Test]
        public void UseHint_WithAvailableHints_DecreasesHintCount()
        {
            // Arrange
            hintSystem.Initialize(testConfig);
            int initialHints = hintSystem.AvailableHints;
            
            // Act
            hintSystem.UseHint();
            
            // Assert
            Assert.AreEqual(initialHints - 1, hintSystem.AvailableHints);
        }
        
        [Test]
        public void UseHint_WithNoHints_DoesNotDecreaseHintCount()
        {
            // Arrange
            hintSystem.Initialize(testConfig);
            hintSystem.AvailableHints = 0;
            
            // Act
            hintSystem.UseHint();
            
            // Assert
            Assert.AreEqual(0, hintSystem.AvailableHints);
        }
        
        [Test]
        public void AddHint_IncreasesHintCount()
        {
            // Arrange
            hintSystem.Initialize(testConfig);
            int initialHints = hintSystem.AvailableHints;
            
            // Act
            hintSystem.AddHint();
            
            // Assert
            Assert.AreEqual(initialHints + 1, hintSystem.AvailableHints);
        }
        
        [Test]
        public void GetHint_WithAvailableHints_ReturnsHint()
        {
            // Arrange
            hintSystem.Initialize(testConfig);
            
            // Act
            string hint = hintSystem.GetHint();
            
            // Assert
            Assert.IsNotNull(hint);
            Assert.IsNotEmpty(hint);
        }
        
        [Test]
        public void GetHint_WithNoHints_ReturnsEmptyString()
        {
            // Arrange
            hintSystem.Initialize(testConfig);
            hintSystem.AvailableHints = 0;
            
            // Act
            string hint = hintSystem.GetHint();
            
            // Assert
            Assert.IsEmpty(hint);
        }
        
        [Test]
        public void OnHintUsed_TriggersEvent()
        {
            // Arrange
            hintSystem.Initialize(testConfig);
            bool eventTriggered = false;
            hintSystem.OnHintUsed += () => eventTriggered = true;
            
            // Act
            hintSystem.UseHint();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
    }
} 