using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;

namespace WordDetective.Tests
{
    public class GameModeConfigTests
    {
        private TestGameModeConfig config;
        
        [SetUp]
        public void Setup()
        {
            config = ScriptableObject.CreateInstance<TestGameModeConfig>();
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(config);
        }
        
        [Test]
        public void InitialValues_AreSetCorrectly()
        {
            // Assert
            Assert.AreEqual("Тестовый режим", config.ModeName);
            Assert.AreEqual("Описание тестового режима", config.Description);
            Assert.AreEqual(300f, config.InitialTime);
            Assert.AreEqual(3, config.MinWordLength);
            Assert.AreEqual(15, config.MaxWordLength);
            Assert.AreEqual(10, config.PointsPerWord);
            Assert.AreEqual(5, config.PenaltyPerWord);
            Assert.AreEqual(3, config.AvailableHints);
            Assert.AreEqual(50, config.HintCost);
            Assert.AreEqual(100, config.CoinsForCompletion);
            Assert.AreEqual(200, config.CoinsForHighScore);
            Assert.AreEqual(500, config.HighScoreThreshold);
        }
        
        [Test]
        public void ValidateConfig_WithValidValues_ReturnsTrue()
        {
            // Act
            bool isValid = config.ValidateConfig();
            
            // Assert
            Assert.IsTrue(isValid);
        }
        
        [Test]
        public void ValidateConfig_WithInvalidTime_ReturnsFalse()
        {
            // Arrange
            config.InitialTime = -1f;
            
            // Act
            bool isValid = config.ValidateConfig();
            
            // Assert
            Assert.IsFalse(isValid);
        }
        
        [Test]
        public void ValidateConfig_WithInvalidWordLength_ReturnsFalse()
        {
            // Arrange
            config.MinWordLength = 10;
            config.MaxWordLength = 5;
            
            // Act
            bool isValid = config.ValidateConfig();
            
            // Assert
            Assert.IsFalse(isValid);
        }
    }
    
    // Тестовый класс для GameModeConfig
    public class TestGameModeConfig : GameModeConfig
    {
        private void OnEnable()
        {
            ModeName = "Тестовый режим";
            Description = "Описание тестового режима";
            InitialTime = 300f;
            MinWordLength = 3;
            MaxWordLength = 15;
            PointsPerWord = 10;
            PenaltyPerWord = 5;
            AvailableHints = 3;
            HintCost = 50;
            CoinsForCompletion = 100;
            CoinsForHighScore = 200;
            HighScoreThreshold = 500;
        }
        
        public bool ValidateConfig()
        {
            if (InitialTime <= 0) return false;
            if (MinWordLength <= 0 || MaxWordLength <= 0) return false;
            if (MinWordLength > MaxWordLength) return false;
            if (PointsPerWord <= 0) return false;
            if (PenaltyPerWord < 0) return false;
            if (AvailableHints < 0) return false;
            if (HintCost < 0) return false;
            if (CoinsForCompletion < 0) return false;
            if (CoinsForHighScore < 0) return false;
            if (HighScoreThreshold <= 0) return false;
            
            return true;
        }
    }
} 