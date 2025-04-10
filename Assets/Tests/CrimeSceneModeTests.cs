using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using WordDetective.GameModes;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class CrimeSceneModeTests
    {
        private GameObject gameObject;
        private CrimeSceneMode crimeSceneMode;
        private Text timerText;
        private Transform evidenceList;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            crimeSceneMode = gameObject.AddComponent<CrimeSceneMode>();
            
            // Создаем необходимые UI элементы
            var timerObj = new GameObject("TimerText");
            timerText = timerObj.AddComponent<Text>();
            crimeSceneMode.timerText = timerText;
            
            var listObj = new GameObject("EvidenceList");
            evidenceList = listObj.transform;
            crimeSceneMode.evidenceList = evidenceList;
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
        
        [Test]
        public void InitializeGame_SetsUpInitialState()
        {
            // Act
            crimeSceneMode.InitializeGame();
            
            // Assert
            Assert.IsTrue(crimeSceneMode.IsGameActive);
            Assert.AreEqual(crimeSceneMode.initialTime, crimeSceneMode.currentTime);
        }
        
        [Test]
        public void UpdateTimer_DecreasesTime()
        {
            // Arrange
            crimeSceneMode.InitializeGame();
            float initialTime = crimeSceneMode.currentTime;
            
            // Act
            crimeSceneMode.UpdateTimer(1f);
            
            // Assert
            Assert.IsTrue(crimeSceneMode.currentTime < initialTime);
        }
        
        [Test]
        public void ProcessWord_WithValidEvidence_RemovesEvidence()
        {
            // Arrange
            crimeSceneMode.InitializeGame();
            var evidence = new CrimeSceneMode.EvidenceItem
            {
                itemName = "тест",
                description = "Тестовое доказательство",
                validWords = new List<string> { "тест" }
            };
            crimeSceneMode.evidenceItems.Add(evidence);
            
            // Act
            crimeSceneMode.ProcessWord("тест");
            
            // Assert
            Assert.IsFalse(crimeSceneMode.evidenceItems.Contains(evidence));
        }
        
        [Test]
        public void ProcessWord_WithInvalidWord_DoesNotRemoveEvidence()
        {
            // Arrange
            crimeSceneMode.InitializeGame();
            var evidence = new CrimeSceneMode.EvidenceItem
            {
                itemName = "тест",
                description = "Тестовое доказательство",
                validWords = new List<string> { "тест" }
            };
            crimeSceneMode.evidenceItems.Add(evidence);
            
            // Act
            crimeSceneMode.ProcessWord("неправильное");
            
            // Assert
            Assert.IsTrue(crimeSceneMode.evidenceItems.Contains(evidence));
        }
        
        [Test]
        public void OnTimeUp_EndsGame()
        {
            // Arrange
            crimeSceneMode.InitializeGame();
            bool gameEnded = false;
            crimeSceneMode.OnGameComplete += () => gameEnded = true;
            
            // Act
            crimeSceneMode.currentTime = 0;
            crimeSceneMode.UpdateTimer(0);
            
            // Assert
            Assert.IsTrue(gameEnded);
            Assert.IsFalse(crimeSceneMode.IsGameActive);
        }
    }
} 