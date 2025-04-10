using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class AchievementSystemTests
    {
        private AchievementSystem achievementSystem;
        private GameModeConfig testConfig;
        
        [SetUp]
        public void Setup()
        {
            achievementSystem = AchievementSystem.Instance;
            testConfig = ScriptableObject.CreateInstance<GameModeConfig>();
            testConfig.CoinsForCompletion = 100;
            testConfig.CoinsForHighScore = 200;
            testConfig.HighScoreThreshold = 500;
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
            achievementSystem.Initialize(testConfig);
            
            // Assert
            Assert.AreEqual(testConfig.CoinsForCompletion, achievementSystem.CoinsForCompletion);
            Assert.AreEqual(testConfig.CoinsForHighScore, achievementSystem.CoinsForHighScore);
            Assert.AreEqual(testConfig.HighScoreThreshold, achievementSystem.HighScoreThreshold);
        }
        
        [Test]
        public void UnlockAchievement_WithNewAchievement_AddsToUnlockedList()
        {
            // Arrange
            achievementSystem.Initialize(testConfig);
            string achievementId = "test_achievement";
            
            // Act
            achievementSystem.UnlockAchievement(achievementId);
            
            // Assert
            Assert.IsTrue(achievementSystem.IsAchievementUnlocked(achievementId));
        }
        
        [Test]
        public void UnlockAchievement_WithExistingAchievement_DoesNotAddDuplicate()
        {
            // Arrange
            achievementSystem.Initialize(testConfig);
            string achievementId = "test_achievement";
            achievementSystem.UnlockAchievement(achievementId);
            
            // Act
            achievementSystem.UnlockAchievement(achievementId);
            
            // Assert
            Assert.IsTrue(achievementSystem.IsAchievementUnlocked(achievementId));
        }
        
        [Test]
        public void GetAchievementProgress_WithNewAchievement_ReturnsZero()
        {
            // Arrange
            achievementSystem.Initialize(testConfig);
            string achievementId = "test_achievement";
            
            // Act
            float progress = achievementSystem.GetAchievementProgress(achievementId);
            
            // Assert
            Assert.AreEqual(0f, progress);
        }
        
        [Test]
        public void UpdateAchievementProgress_WithValidProgress_UpdatesProgress()
        {
            // Arrange
            achievementSystem.Initialize(testConfig);
            string achievementId = "test_achievement";
            float progress = 0.5f;
            
            // Act
            achievementSystem.UpdateAchievementProgress(achievementId, progress);
            
            // Assert
            Assert.AreEqual(progress, achievementSystem.GetAchievementProgress(achievementId));
        }
        
        [Test]
        public void OnAchievementUnlocked_TriggersEvent()
        {
            // Arrange
            achievementSystem.Initialize(testConfig);
            string achievementId = "test_achievement";
            bool eventTriggered = false;
            achievementSystem.OnAchievementUnlocked += (id) => eventTriggered = true;
            
            // Act
            achievementSystem.UnlockAchievement(achievementId);
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void SaveAchievements_SavesUnlockedAchievements()
        {
            // Arrange
            achievementSystem.Initialize(testConfig);
            string achievementId = "test_achievement";
            achievementSystem.UnlockAchievement(achievementId);
            
            // Act
            achievementSystem.SaveAchievements();
            
            // Assert
            Assert.IsTrue(achievementSystem.IsAchievementUnlocked(achievementId));
        }
        
        [Test]
        public void LoadAchievements_LoadsSavedAchievements()
        {
            // Arrange
            achievementSystem.Initialize(testConfig);
            string achievementId = "test_achievement";
            achievementSystem.UnlockAchievement(achievementId);
            achievementSystem.SaveAchievements();
            
            // Act
            achievementSystem.LoadAchievements();
            
            // Assert
            Assert.IsTrue(achievementSystem.IsAchievementUnlocked(achievementId));
        }
    }
} 