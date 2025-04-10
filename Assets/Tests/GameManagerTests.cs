using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;
using System;

namespace WordDetective.Tests
{
    public class GameManagerTests
    {
        private GameObject gameObject;
        private GameManager gameManager;
        private WordManager wordManager;
        private UIManager uiManager;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            gameManager = gameObject.AddComponent<GameManager>();
            wordManager = gameObject.AddComponent<WordManager>();
            uiManager = gameObject.AddComponent<UIManager>();
            
            // Настраиваем ссылки
            gameManager.wordManager = wordManager;
            gameManager.uiManager = uiManager;
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
        
        [Test]
        public void StartGame_InitializesGameState()
        {
            // Act
            gameManager.StartGame();
            
            // Assert
            Assert.IsTrue(gameManager.IsGameActive);
            Assert.AreEqual(0, gameManager.CurrentScore);
        }
        
        [Test]
        public void EndGame_UpdatesGameState()
        {
            // Arrange
            gameManager.StartGame();
            
            // Act
            gameManager.EndGame();
            
            // Assert
            Assert.IsFalse(gameManager.IsGameActive);
        }
        
        [Test]
        public void ProcessWord_WithValidWord_IncreasesScore()
        {
            // Arrange
            gameManager.StartGame();
            string validWord = "тест";
            
            // Act
            gameManager.ProcessWord(validWord);
            
            // Assert
            Assert.IsTrue(gameManager.CurrentScore > 0);
        }
        
        [Test]
        public void ProcessWord_WithInvalidWord_DoesNotIncreaseScore()
        {
            // Arrange
            gameManager.StartGame();
            int initialScore = gameManager.CurrentScore;
            string invalidWord = "xyz";
            
            // Act
            gameManager.ProcessWord(invalidWord);
            
            // Assert
            Assert.AreEqual(initialScore, gameManager.CurrentScore);
        }
        
        [Test]
        public void OnGameOver_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            gameManager.OnGameOver += () => eventTriggered = true;
            
            // Act
            gameManager.EndGame();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
    }
} 