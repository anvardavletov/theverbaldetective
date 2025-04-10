using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class WordManagerTests
    {
        private WordManager wordManager;
        private GameModeConfig testConfig;
        
        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject();
            wordManager = gameObject.AddComponent<WordManager>();
            
            testConfig = ScriptableObject.CreateInstance<GameModeConfig>();
            testConfig.MinWordLength = 3;
            testConfig.MaxWordLength = 15;
            testConfig.PointsPerWord = 10;
            testConfig.PenaltyPerWord = 5;
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(wordManager.gameObject);
            UnityEngine.Object.DestroyImmediate(testConfig);
        }
        
        [Test]
        public void ValidateWord_WithValidWord_ReturnsTrue()
        {
            // Arrange
            string validWord = "тест";
            
            // Act
            bool isValid = wordManager.ValidateWord(validWord, testConfig);
            
            // Assert
            Assert.IsTrue(isValid);
        }
        
        [Test]
        public void ValidateWord_WithTooShortWord_ReturnsFalse()
        {
            // Arrange
            string shortWord = "те";
            
            // Act
            bool isValid = wordManager.ValidateWord(shortWord, testConfig);
            
            // Assert
            Assert.IsFalse(isValid);
        }
        
        [Test]
        public void ValidateWord_WithTooLongWord_ReturnsFalse()
        {
            // Arrange
            string longWord = "оченьдлинноеслово";
            
            // Act
            bool isValid = wordManager.ValidateWord(longWord, testConfig);
            
            // Assert
            Assert.IsFalse(isValid);
        }
        
        [Test]
        public void CalculateWordScore_WithValidWord_ReturnsCorrectScore()
        {
            // Arrange
            string validWord = "тест";
            
            // Act
            int score = wordManager.CalculateWordScore(validWord, testConfig);
            
            // Assert
            Assert.AreEqual(testConfig.PointsPerWord, score);
        }
        
        [Test]
        public void CalculateWordScore_WithInvalidWord_ReturnsPenalty()
        {
            // Arrange
            string invalidWord = "те";
            
            // Act
            int score = wordManager.CalculateWordScore(invalidWord, testConfig);
            
            // Assert
            Assert.AreEqual(-testConfig.PenaltyPerWord, score);
        }
        
        [Test]
        public void GetRandomWord_ReturnsWordWithinLengthLimits()
        {
            // Act
            string word = wordManager.GetRandomWord(testConfig);
            
            // Assert
            Assert.IsNotNull(word);
            Assert.GreaterOrEqual(word.Length, testConfig.MinWordLength);
            Assert.LessOrEqual(word.Length, testConfig.MaxWordLength);
        }
        
        [Test]
        public void GetRandomWord_WithCustomDictionary_ReturnsWordFromDictionary()
        {
            // Arrange
            var customDictionary = new List<string> { "тест", "слово", "игра" };
            wordManager.SetCustomDictionary(customDictionary);
            
            // Act
            string word = wordManager.GetRandomWord(testConfig);
            
            // Assert
            Assert.IsNotNull(word);
            Assert.Contains(word, customDictionary);
        }
    }
} 