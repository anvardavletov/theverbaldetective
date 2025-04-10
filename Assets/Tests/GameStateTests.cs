using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class GameStateTests
    {
        private GameState gameState;
        
        [SetUp]
        public void Setup()
        {
            gameState = new GameState();
        }
        
        [TearDown]
        public void Teardown()
        {
            gameState = null;
        }
        
        [Test]
        public void Initialize_SetsDefaultValues()
        {
            // Act
            gameState.Initialize();
            
            // Assert
            Assert.AreEqual(0, gameState.Score);
            Assert.AreEqual(0, gameState.Coins);
            Assert.AreEqual(3, gameState.Hints);
            Assert.AreEqual(GameMode.None, gameState.CurrentMode);
            Assert.AreEqual(GameStatus.MainMenu, gameState.CurrentStatus);
            Assert.IsFalse(gameState.IsPaused);
            Assert.IsFalse(gameState.IsGameOver);
            Assert.IsNotNull(gameState.WordHistory);
            Assert.AreEqual(0, gameState.WordHistory.Count);
            Assert.IsNotNull(gameState.Achievements);
            Assert.AreEqual(0, gameState.Achievements.Count);
        }
        
        [Test]
        public void SetScore_UpdatesScore()
        {
            // Arrange
            gameState.Initialize();
            int newScore = 100;
            
            // Act
            gameState.SetScore(newScore);
            
            // Assert
            Assert.AreEqual(newScore, gameState.Score);
        }
        
        [Test]
        public void AddCoins_UpdatesCoins()
        {
            // Arrange
            gameState.Initialize();
            int initialCoins = gameState.Coins;
            int coinsToAdd = 50;
            
            // Act
            gameState.AddCoins(coinsToAdd);
            
            // Assert
            Assert.AreEqual(initialCoins + coinsToAdd, gameState.Coins);
        }
        
        [Test]
        public void SpendCoins_UpdatesCoins()
        {
            // Arrange
            gameState.Initialize();
            gameState.AddCoins(100);
            int coinsToSpend = 30;
            
            // Act
            gameState.SpendCoins(coinsToSpend);
            
            // Assert
            Assert.AreEqual(70, gameState.Coins);
        }
        
        [Test]
        public void SpendCoins_WithInsufficientCoins_DoesNotUpdateCoins()
        {
            // Arrange
            gameState.Initialize();
            int initialCoins = gameState.Coins;
            int coinsToSpend = 100;
            
            // Act
            gameState.SpendCoins(coinsToSpend);
            
            // Assert
            Assert.AreEqual(initialCoins, gameState.Coins);
        }
        
        [Test]
        public void AddHint_UpdatesHints()
        {
            // Arrange
            gameState.Initialize();
            int initialHints = gameState.Hints;
            
            // Act
            gameState.AddHint();
            
            // Assert
            Assert.AreEqual(initialHints + 1, gameState.Hints);
        }
        
        [Test]
        public void UseHint_UpdatesHints()
        {
            // Arrange
            gameState.Initialize();
            gameState.AddHint();
            int initialHints = gameState.Hints;
            
            // Act
            gameState.UseHint();
            
            // Assert
            Assert.AreEqual(initialHints - 1, gameState.Hints);
        }
        
        [Test]
        public void UseHint_WithNoHints_DoesNotUpdateHints()
        {
            // Arrange
            gameState.Initialize();
            int initialHints = gameState.Hints;
            
            // Act
            gameState.UseHint();
            
            // Assert
            Assert.AreEqual(initialHints, gameState.Hints);
        }
        
        [Test]
        public void SetGameMode_UpdatesCurrentMode()
        {
            // Arrange
            gameState.Initialize();
            GameMode newMode = GameMode.MysteryStory;
            
            // Act
            gameState.SetGameMode(newMode);
            
            // Assert
            Assert.AreEqual(newMode, gameState.CurrentMode);
        }
        
        [Test]
        public void SetGameStatus_UpdatesCurrentStatus()
        {
            // Arrange
            gameState.Initialize();
            GameStatus newStatus = GameStatus.Playing;
            
            // Act
            gameState.SetGameStatus(newStatus);
            
            // Assert
            Assert.AreEqual(newStatus, gameState.CurrentStatus);
        }
        
        [Test]
        public void SetPaused_UpdatesIsPaused()
        {
            // Arrange
            gameState.Initialize();
            
            // Act
            gameState.SetPaused(true);
            
            // Assert
            Assert.IsTrue(gameState.IsPaused);
        }
        
        [Test]
        public void SetGameOver_UpdatesIsGameOver()
        {
            // Arrange
            gameState.Initialize();
            
            // Act
            gameState.SetGameOver(true);
            
            // Assert
            Assert.IsTrue(gameState.IsGameOver);
        }
        
        [Test]
        public void AddWordToHistory_UpdatesWordHistory()
        {
            // Arrange
            gameState.Initialize();
            string word = "тест";
            
            // Act
            gameState.AddWordToHistory(word);
            
            // Assert
            Assert.AreEqual(1, gameState.WordHistory.Count);
            Assert.AreEqual(word, gameState.WordHistory[0]);
        }
        
        [Test]
        public void ClearWordHistory_ClearsHistory()
        {
            // Arrange
            gameState.Initialize();
            gameState.AddWordToHistory("тест1");
            gameState.AddWordToHistory("тест2");
            
            // Act
            gameState.ClearWordHistory();
            
            // Assert
            Assert.AreEqual(0, gameState.WordHistory.Count);
        }
        
        [Test]
        public void AddAchievement_UpdatesAchievements()
        {
            // Arrange
            gameState.Initialize();
            string achievement = "Достижение";
            
            // Act
            gameState.AddAchievement(achievement);
            
            // Assert
            Assert.AreEqual(1, gameState.Achievements.Count);
            Assert.AreEqual(achievement, gameState.Achievements[0]);
        }
        
        [Test]
        public void ClearAchievements_ClearsAchievements()
        {
            // Arrange
            gameState.Initialize();
            gameState.AddAchievement("Достижение1");
            gameState.AddAchievement("Достижение2");
            
            // Act
            gameState.ClearAchievements();
            
            // Assert
            Assert.AreEqual(0, gameState.Achievements.Count);
        }
        
        [Test]
        public void Reset_ResetsAllValues()
        {
            // Arrange
            gameState.Initialize();
            gameState.SetScore(100);
            gameState.AddCoins(50);
            gameState.AddHint();
            gameState.SetGameMode(GameMode.MysteryStory);
            gameState.SetGameStatus(GameStatus.Playing);
            gameState.SetPaused(true);
            gameState.SetGameOver(true);
            gameState.AddWordToHistory("тест");
            gameState.AddAchievement("Достижение");
            
            // Act
            gameState.Reset();
            
            // Assert
            Assert.AreEqual(0, gameState.Score);
            Assert.AreEqual(0, gameState.Coins);
            Assert.AreEqual(3, gameState.Hints);
            Assert.AreEqual(GameMode.None, gameState.CurrentMode);
            Assert.AreEqual(GameStatus.MainMenu, gameState.CurrentStatus);
            Assert.IsFalse(gameState.IsPaused);
            Assert.IsFalse(gameState.IsGameOver);
            Assert.AreEqual(0, gameState.WordHistory.Count);
            Assert.AreEqual(0, gameState.Achievements.Count);
        }
    }
} 