using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using WordDetective.GameModes;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class MysteryStoryModeTests
    {
        private GameObject gameObject;
        private MysteryStoryMode mysteryMode;
        private Text storyText;
        private Text timerText;
        private Transform choicesContainer;
        private GameModeConfig testConfig;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            mysteryMode = gameObject.AddComponent<MysteryStoryMode>();
            
            // Создаем UI элементы
            var storyObj = new GameObject("StoryText");
            storyText = storyObj.AddComponent<Text>();
            mysteryMode.storyText = storyText;
            
            var timerObj = new GameObject("TimerText");
            timerText = timerObj.AddComponent<Text>();
            mysteryMode.timerText = timerText;
            
            var containerObj = new GameObject("ChoicesContainer");
            choicesContainer = containerObj.transform;
            mysteryMode.choicesContainer = choicesContainer;
            
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
            mysteryMode.InitializeGame(testConfig);
            
            // Assert
            Assert.IsTrue(mysteryMode.IsGameActive);
            Assert.AreEqual(testConfig.InitialTime, mysteryMode.currentTime);
            Assert.AreEqual(0, mysteryMode.currentStoryIndex);
        }
        
        [Test]
        public void UpdateTimer_DecreasesTime()
        {
            // Arrange
            mysteryMode.InitializeGame(testConfig);
            float initialTime = mysteryMode.currentTime;
            
            // Act
            mysteryMode.UpdateTimer(1f);
            
            // Assert
            Assert.IsTrue(mysteryMode.currentTime < initialTime);
        }
        
        [Test]
        public void ProcessChoice_WithValidChoice_AdvancesStory()
        {
            // Arrange
            mysteryMode.InitializeGame(testConfig);
            int initialIndex = mysteryMode.currentStoryIndex;
            
            // Act
            mysteryMode.ProcessChoice(0);
            
            // Assert
            Assert.AreEqual(initialIndex + 1, mysteryMode.currentStoryIndex);
        }
        
        [Test]
        public void ProcessChoice_WithInvalidChoice_DoesNotAdvanceStory()
        {
            // Arrange
            mysteryMode.InitializeGame(testConfig);
            int initialIndex = mysteryMode.currentStoryIndex;
            
            // Act
            mysteryMode.ProcessChoice(-1);
            
            // Assert
            Assert.AreEqual(initialIndex, mysteryMode.currentStoryIndex);
        }
        
        [Test]
        public void OnTimeUp_EndsGame()
        {
            // Arrange
            mysteryMode.InitializeGame(testConfig);
            bool gameEnded = false;
            mysteryMode.OnGameComplete += () => gameEnded = true;
            
            // Act
            mysteryMode.UpdateTimer(testConfig.InitialTime + 1f);
            
            // Assert
            Assert.IsTrue(gameEnded);
            Assert.IsFalse(mysteryMode.IsGameActive);
        }
        
        [Test]
        public void OnStoryComplete_EndsGame()
        {
            // Arrange
            mysteryMode.InitializeGame(testConfig);
            bool gameEnded = false;
            mysteryMode.OnGameComplete += () => gameEnded = true;
            
            // Act
            for (int i = 0; i < mysteryMode.storyData.storyNodes.Count; i++)
            {
                mysteryMode.ProcessChoice(0);
            }
            
            // Assert
            Assert.IsTrue(gameEnded);
            Assert.IsFalse(mysteryMode.IsGameActive);
        }
    }
} 