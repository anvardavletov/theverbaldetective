using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class SaveSystemTests
    {
        private SaveSystem saveSystem;
        private GameModeConfig testConfig;
        
        [SetUp]
        public void Setup()
        {
            saveSystem = SaveSystem.Instance;
            testConfig = ScriptableObject.CreateInstance<GameModeConfig>();
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(testConfig);
            PlayerPrefs.DeleteAll();
        }
        
        [Test]
        public void SaveGameState_SavesCorrectData()
        {
            // Arrange
            var gameState = new GameState
            {
                score = 100,
                coins = 50,
                currentLevel = 1,
                unlockedLevels = new List<int> { 1, 2, 3 }
            };
            
            // Act
            saveSystem.SaveGameState(gameState);
            
            // Assert
            var loadedState = saveSystem.LoadGameState();
            Assert.AreEqual(gameState.score, loadedState.score);
            Assert.AreEqual(gameState.coins, loadedState.coins);
            Assert.AreEqual(gameState.currentLevel, loadedState.currentLevel);
            Assert.AreEqual(gameState.unlockedLevels.Count, loadedState.unlockedLevels.Count);
        }
        
        [Test]
        public void LoadGameState_WithNoSave_ReturnsDefaultState()
        {
            // Act
            var loadedState = saveSystem.LoadGameState();
            
            // Assert
            Assert.AreEqual(0, loadedState.score);
            Assert.AreEqual(0, loadedState.coins);
            Assert.AreEqual(1, loadedState.currentLevel);
            Assert.AreEqual(1, loadedState.unlockedLevels.Count);
        }
        
        [Test]
        public void SaveSettings_SavesCorrectData()
        {
            // Arrange
            var settings = new GameSettings
            {
                musicVolume = 0.5f,
                sfxVolume = 0.7f,
                language = "ru",
                qualityLevel = 2
            };
            
            // Act
            saveSystem.SaveSettings(settings);
            
            // Assert
            var loadedSettings = saveSystem.LoadSettings();
            Assert.AreEqual(settings.musicVolume, loadedSettings.musicVolume);
            Assert.AreEqual(settings.sfxVolume, loadedSettings.sfxVolume);
            Assert.AreEqual(settings.language, loadedSettings.language);
            Assert.AreEqual(settings.qualityLevel, loadedSettings.qualityLevel);
        }
        
        [Test]
        public void LoadSettings_WithNoSave_ReturnsDefaultSettings()
        {
            // Act
            var loadedSettings = saveSystem.LoadSettings();
            
            // Assert
            Assert.AreEqual(1f, loadedSettings.musicVolume);
            Assert.AreEqual(1f, loadedSettings.sfxVolume);
            Assert.AreEqual("ru", loadedSettings.language);
            Assert.AreEqual(2, loadedSettings.qualityLevel);
        }
        
        [Test]
        public void SaveHighScore_SavesCorrectData()
        {
            // Arrange
            var highScore = new HighScore
            {
                score = 1000,
                date = System.DateTime.Now.ToString()
            };
            
            // Act
            saveSystem.SaveHighScore(highScore);
            
            // Assert
            var loadedHighScore = saveSystem.LoadHighScore();
            Assert.AreEqual(highScore.score, loadedHighScore.score);
            Assert.AreEqual(highScore.date, loadedHighScore.date);
        }
        
        [Test]
        public void LoadHighScore_WithNoSave_ReturnsDefaultHighScore()
        {
            // Act
            var loadedHighScore = saveSystem.LoadHighScore();
            
            // Assert
            Assert.AreEqual(0, loadedHighScore.score);
            Assert.IsEmpty(loadedHighScore.date);
        }
        
        [Test]
        public void ClearAllData_ClearsAllSavedData()
        {
            // Arrange
            var gameState = new GameState { score = 100 };
            var settings = new GameSettings { musicVolume = 0.5f };
            var highScore = new HighScore { score = 1000 };
            
            saveSystem.SaveGameState(gameState);
            saveSystem.SaveSettings(settings);
            saveSystem.SaveHighScore(highScore);
            
            // Act
            saveSystem.ClearAllData();
            
            // Assert
            var loadedState = saveSystem.LoadGameState();
            var loadedSettings = saveSystem.LoadSettings();
            var loadedHighScore = saveSystem.LoadHighScore();
            
            Assert.AreEqual(0, loadedState.score);
            Assert.AreEqual(1f, loadedSettings.musicVolume);
            Assert.AreEqual(0, loadedHighScore.score);
        }
    }
} 