using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using WordDetective.GameModes;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class PhotofitModeTests
    {
        private GameObject gameObject;
        private PhotofitMode photofitMode;
        private Text timerText;
        private Transform featuresContainer;
        private GameModeConfig testConfig;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            photofitMode = gameObject.AddComponent<PhotofitMode>();
            
            // Создаем UI элементы
            var timerObj = new GameObject("TimerText");
            timerText = timerObj.AddComponent<Text>();
            photofitMode.timerText = timerText;
            
            var containerObj = new GameObject("FeaturesContainer");
            featuresContainer = containerObj.transform;
            photofitMode.featuresContainer = featuresContainer;
            
            // Создаем тестовую конфигурацию
            testConfig = ScriptableObject.CreateInstance<GameModeConfig>();
            testConfig.InitialTime = 300f;
            testConfig.MinWordLength = 3;
            testConfig.MaxWordLength = 15;
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
            UnityEngine.Object.DestroyImmediate(testConfig);
        }
        
        [Test]
        public void InitializeGame_SetsUpInitialState()
        {
            // Act
            photofitMode.InitializeGame(testConfig);
            
            // Assert
            Assert.IsTrue(photofitMode.IsGameActive);
            Assert.AreEqual(testConfig.InitialTime, photofitMode.currentTime);
            Assert.AreEqual(0, photofitMode.completedFeatures);
        }
        
        [Test]
        public void UpdateTimer_DecreasesTime()
        {
            // Arrange
            photofitMode.InitializeGame(testConfig);
            float initialTime = photofitMode.currentTime;
            
            // Act
            photofitMode.UpdateTimer(1f);
            
            // Assert
            Assert.IsTrue(photofitMode.currentTime < initialTime);
        }
        
        [Test]
        public void ProcessWord_WithValidFeature_CompletesFeature()
        {
            // Arrange
            photofitMode.InitializeGame(testConfig);
            int initialCompleted = photofitMode.completedFeatures;
            
            // Act
            photofitMode.ProcessWord("тест");
            
            // Assert
            Assert.AreEqual(initialCompleted + 1, photofitMode.completedFeatures);
        }
        
        [Test]
        public void ProcessWord_WithInvalidWord_DoesNotCompleteFeature()
        {
            // Arrange
            photofitMode.InitializeGame(testConfig);
            int initialCompleted = photofitMode.completedFeatures;
            
            // Act
            photofitMode.ProcessWord("неправильное");
            
            // Assert
            Assert.AreEqual(initialCompleted, photofitMode.completedFeatures);
        }
        
        [Test]
        public void OnTimeUp_EndsGame()
        {
            // Arrange
            photofitMode.InitializeGame(testConfig);
            bool gameEnded = false;
            photofitMode.OnGameComplete += () => gameEnded = true;
            
            // Act
            photofitMode.UpdateTimer(testConfig.InitialTime + 1f);
            
            // Assert
            Assert.IsTrue(gameEnded);
            Assert.IsFalse(photofitMode.IsGameActive);
        }
        
        [Test]
        public void OnAllFeaturesCompleted_EndsGame()
        {
            // Arrange
            photofitMode.InitializeGame(testConfig);
            bool gameEnded = false;
            photofitMode.OnGameComplete += () => gameEnded = true;
            
            // Act
            for (int i = 0; i < photofitMode.featuresToComplete; i++)
            {
                photofitMode.ProcessWord("тест");
            }
            
            // Assert
            Assert.IsTrue(gameEnded);
            Assert.IsFalse(photofitMode.IsGameActive);
        }
    }
} 