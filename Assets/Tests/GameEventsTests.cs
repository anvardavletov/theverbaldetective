using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;
using System;

namespace WordDetective.Tests
{
    public class GameEventsTests
    {
        private GameEvents gameEvents;
        
        [SetUp]
        public void Setup()
        {
            gameEvents = new GameEvents();
        }
        
        [TearDown]
        public void Teardown()
        {
            gameEvents = null;
        }
        
        [Test]
        public void OnGameStart_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            gameEvents.OnGameStart += () => eventTriggered = true;
            
            // Act
            gameEvents.TriggerGameStart();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnGameOver_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            int finalScore = 0;
            gameEvents.OnGameOver += (score) => 
            {
                eventTriggered = true;
                finalScore = score;
            };
            
            // Act
            gameEvents.TriggerGameOver(100);
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(100, finalScore);
        }
        
        [Test]
        public void OnScoreChanged_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            int newScore = 0;
            gameEvents.OnScoreChanged += (score) => 
            {
                eventTriggered = true;
                newScore = score;
            };
            
            // Act
            gameEvents.TriggerScoreChanged(50);
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(50, newScore);
        }
        
        [Test]
        public void OnCoinsChanged_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            int newCoins = 0;
            gameEvents.OnCoinsChanged += (coins) => 
            {
                eventTriggered = true;
                newCoins = coins;
            };
            
            // Act
            gameEvents.TriggerCoinsChanged(30);
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(30, newCoins);
        }
        
        [Test]
        public void OnHintsChanged_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            int newHints = 0;
            gameEvents.OnHintsChanged += (hints) => 
            {
                eventTriggered = true;
                newHints = hints;
            };
            
            // Act
            gameEvents.TriggerHintsChanged(2);
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(2, newHints);
        }
        
        [Test]
        public void OnWordSubmitted_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            string submittedWord = "";
            gameEvents.OnWordSubmitted += (word) => 
            {
                eventTriggered = true;
                submittedWord = word;
            };
            
            // Act
            gameEvents.TriggerWordSubmitted("тест");
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("тест", submittedWord);
        }
        
        [Test]
        public void OnWordAccepted_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            string acceptedWord = "";
            gameEvents.OnWordAccepted += (word) => 
            {
                eventTriggered = true;
                acceptedWord = word;
            };
            
            // Act
            gameEvents.TriggerWordAccepted("тест");
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("тест", acceptedWord);
        }
        
        [Test]
        public void OnWordRejected_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            string rejectedWord = "";
            gameEvents.OnWordRejected += (word) => 
            {
                eventTriggered = true;
                rejectedWord = word;
            };
            
            // Act
            gameEvents.TriggerWordRejected("тест");
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("тест", rejectedWord);
        }
        
        [Test]
        public void OnAchievementUnlocked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            string achievement = "";
            gameEvents.OnAchievementUnlocked += (achievementId) => 
            {
                eventTriggered = true;
                achievement = achievementId;
            };
            
            // Act
            gameEvents.TriggerAchievementUnlocked("достижение");
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("достижение", achievement);
        }
        
        [Test]
        public void OnGamePaused_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            gameEvents.OnGamePaused += () => eventTriggered = true;
            
            // Act
            gameEvents.TriggerGamePaused();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnGameResumed_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            gameEvents.OnGameResumed += () => eventTriggered = true;
            
            // Act
            gameEvents.TriggerGameResumed();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnSettingsChanged_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            GameSettings settings = null;
            gameEvents.OnSettingsChanged += (newSettings) => 
            {
                eventTriggered = true;
                settings = newSettings;
            };
            
            // Act
            var newSettings = new GameSettings();
            gameEvents.TriggerSettingsChanged(newSettings);
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(newSettings, settings);
        }
        
        [Test]
        public void OnLanguageChanged_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            string newLanguage = "";
            gameEvents.OnLanguageChanged += (language) => 
            {
                eventTriggered = true;
                newLanguage = language;
            };
            
            // Act
            gameEvents.TriggerLanguageChanged("en");
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual("en", newLanguage);
        }
        
        [Test]
        public void OnQualityLevelChanged_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            int newLevel = -1;
            gameEvents.OnQualityLevelChanged += (level) => 
            {
                eventTriggered = true;
                newLevel = level;
            };
            
            // Act
            gameEvents.TriggerQualityLevelChanged(1);
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(1, newLevel);
        }
        
        [Test]
        public void OnVolumeChanged_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            float newVolume = -1f;
            gameEvents.OnVolumeChanged += (volume) => 
            {
                eventTriggered = true;
                newVolume = volume;
            };
            
            // Act
            gameEvents.TriggerVolumeChanged(0.5f);
            
            // Assert
            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(0.5f, newVolume);
        }
    }
} 