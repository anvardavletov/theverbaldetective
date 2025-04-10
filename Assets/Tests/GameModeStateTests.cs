using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;
using System;

namespace WordDetective.Tests
{
    public class GameModeStateTests
    {
        private GameObject gameObject;
        private TestGameModeState gameModeState;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            gameModeState = gameObject.AddComponent<TestGameModeState>();
            
            // Создание тестового конфига
            var config = ScriptableObject.CreateInstance<TestGameModeConfig>();
            config.InitialTime = 10f;
            gameModeState.SetConfig(config);
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
        
        [Test]
        public void Initialize_ResetsState()
        {
            // Arrange
            gameModeState.currentTime = 5f;
            gameModeState.currentScore = 100;
            gameModeState.isPaused = true;
            gameModeState.isCompleted = true;
            gameModeState.isFailed = true;
            
            // Act
            gameModeState.Initialize();
            
            // Assert
            Assert.AreEqual(10f, gameModeState.currentTime);
            Assert.AreEqual(0, gameModeState.currentScore);
            Assert.IsFalse(gameModeState.isPaused);
            Assert.IsFalse(gameModeState.isCompleted);
            Assert.IsFalse(gameModeState.isFailed);
        }
        
        [Test]
        public void UpdateState_DecreasesTime()
        {
            // Arrange
            gameModeState.Initialize();
            float initialTime = gameModeState.currentTime;
            
            // Act
            gameModeState.UpdateState();
            
            // Assert
            Assert.Less(gameModeState.currentTime, initialTime);
        }
        
        [Test]
        public void UpdateState_WhenTimeZero_FailsGame()
        {
            // Arrange
            gameModeState.Initialize();
            gameModeState.currentTime = 0f;
            bool eventTriggered = false;
            gameModeState.OnGameFailed += () => eventTriggered = true;
            
            // Act
            gameModeState.UpdateState();
            
            // Assert
            Assert.IsTrue(gameModeState.isFailed);
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void PauseGame_SetsPausedState()
        {
            // Arrange
            gameModeState.Initialize();
            bool eventTriggered = false;
            gameModeState.OnGamePaused += () => eventTriggered = true;
            
            // Act
            gameModeState.PauseGame();
            
            // Assert
            Assert.IsTrue(gameModeState.isPaused);
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void ResumeGame_ClearsPausedState()
        {
            // Arrange
            gameModeState.Initialize();
            gameModeState.PauseGame();
            bool eventTriggered = false;
            gameModeState.OnGameResumed += () => eventTriggered = true;
            
            // Act
            gameModeState.ResumeGame();
            
            // Assert
            Assert.IsFalse(gameModeState.isPaused);
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void CompleteGame_SetsCompletedState()
        {
            // Arrange
            gameModeState.Initialize();
            bool eventTriggered = false;
            gameModeState.OnGameCompleted += () => eventTriggered = true;
            
            // Act
            gameModeState.CompleteGame();
            
            // Assert
            Assert.IsTrue(gameModeState.isCompleted);
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void FailGame_SetsFailedState()
        {
            // Arrange
            gameModeState.Initialize();
            bool eventTriggered = false;
            gameModeState.OnGameFailed += () => eventTriggered = true;
            
            // Act
            gameModeState.FailGame();
            
            // Assert
            Assert.IsTrue(gameModeState.isFailed);
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void UpdateScore_IncreasesScore()
        {
            // Arrange
            gameModeState.Initialize();
            int initialScore = gameModeState.currentScore;
            bool eventTriggered = false;
            int eventScore = 0;
            gameModeState.OnScoreUpdated += (score) => { eventTriggered = true; eventScore = score; };
            
            // Act
            gameModeState.TestUpdateScore(10);
            
            // Assert
            Assert.AreEqual(initialScore + 10, gameModeState.currentScore);
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(initialScore + 10, eventScore);
        }
    }
    
    // Тестовый класс для GameModeState
    public class TestGameModeState : GameModeState
    {
        public void SetConfig(GameModeConfig config)
        {
            this.config = config;
        }
        
        public void TestUpdateScore(int points)
        {
            UpdateScore(points);
        }
    }
    
    // Тестовый класс для GameModeConfig
    public class TestGameModeConfig : GameModeConfig
    {
    }
} 