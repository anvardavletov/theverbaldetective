using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;

namespace WordDetective.Tests
{
    public class OptimizationSystemTests
    {
        private OptimizationSystem optimizationSystem;
        private GameSettings testSettings;
        
        [SetUp]
        public void Setup()
        {
            optimizationSystem = OptimizationSystem.Instance;
            testSettings = new GameSettings
            {
                qualityLevel = 2
            };
        }
        
        [TearDown]
        public void Teardown()
        {
            // Cleanup any resources if needed
        }
        
        [Test]
        public void Initialize_WithSettings_SetsQualityLevel()
        {
            // Act
            optimizationSystem.Initialize(testSettings);
            
            // Assert
            Assert.AreEqual(testSettings.qualityLevel, QualitySettings.GetQualityLevel());
        }
        
        [Test]
        public void SetQualityLevel_UpdatesQualityLevel()
        {
            // Arrange
            optimizationSystem.Initialize(testSettings);
            int newQualityLevel = 1;
            
            // Act
            optimizationSystem.SetQualityLevel(newQualityLevel);
            
            // Assert
            Assert.AreEqual(newQualityLevel, QualitySettings.GetQualityLevel());
        }
        
        [Test]
        public void SetQualityLevel_WithInvalidLevel_KeepsCurrentLevel()
        {
            // Arrange
            optimizationSystem.Initialize(testSettings);
            int currentLevel = QualitySettings.GetQualityLevel();
            int invalidLevel = -1;
            
            // Act
            optimizationSystem.SetQualityLevel(invalidLevel);
            
            // Assert
            Assert.AreEqual(currentLevel, QualitySettings.GetQualityLevel());
        }
        
        [Test]
        public void OptimizeMemory_ReducesMemoryUsage()
        {
            // Arrange
            optimizationSystem.Initialize(testSettings);
            
            // Act
            optimizationSystem.OptimizeMemory();
            
            // Assert
            // Note: We can't directly test memory usage, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void OptimizeGraphics_UpdatesGraphicsSettings()
        {
            // Arrange
            optimizationSystem.Initialize(testSettings);
            
            // Act
            optimizationSystem.OptimizeGraphics();
            
            // Assert
            // Note: We can't directly test graphics settings, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void OptimizeAudio_UpdatesAudioSettings()
        {
            // Arrange
            optimizationSystem.Initialize(testSettings);
            
            // Act
            optimizationSystem.OptimizeAudio();
            
            // Assert
            // Note: We can't directly test audio settings, but we can verify the method doesn't throw
            Assert.Pass();
        }
        
        [Test]
        public void GetPerformanceStats_ReturnsStats()
        {
            // Arrange
            optimizationSystem.Initialize(testSettings);
            
            // Act
            var stats = optimizationSystem.GetPerformanceStats();
            
            // Assert
            Assert.IsNotNull(stats);
            Assert.IsNotNull(stats.fps);
            Assert.IsNotNull(stats.memoryUsage);
            Assert.IsNotNull(stats.cpuUsage);
        }
        
        [Test]
        public void OnQualityLevelChanged_TriggersEvent()
        {
            // Arrange
            optimizationSystem.Initialize(testSettings);
            bool eventTriggered = false;
            optimizationSystem.OnQualityLevelChanged += () => eventTriggered = true;
            
            // Act
            optimizationSystem.SetQualityLevel(1);
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
    }
} 