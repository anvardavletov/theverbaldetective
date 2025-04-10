using NUnit.Framework;
using UnityEngine;
using WordDetective.Systems;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class LocalizationSystemTests
    {
        private LocalizationSystem localizationSystem;
        private GameSettings testSettings;
        
        [SetUp]
        public void Setup()
        {
            localizationSystem = LocalizationSystem.Instance;
            testSettings = new GameSettings
            {
                language = "ru"
            };
        }
        
        [TearDown]
        public void Teardown()
        {
            // Cleanup any resources if needed
        }
        
        [Test]
        public void Initialize_WithSettings_SetsCorrectLanguage()
        {
            // Act
            localizationSystem.Initialize(testSettings);
            
            // Assert
            Assert.AreEqual(testSettings.language, localizationSystem.CurrentLanguage);
        }
        
        [Test]
        public void GetLocalizedText_WithValidKey_ReturnsText()
        {
            // Arrange
            localizationSystem.Initialize(testSettings);
            string key = "test_key";
            
            // Act
            string text = localizationSystem.GetLocalizedText(key);
            
            // Assert
            Assert.IsNotNull(text);
            Assert.IsNotEmpty(text);
        }
        
        [Test]
        public void GetLocalizedText_WithInvalidKey_ReturnsKey()
        {
            // Arrange
            localizationSystem.Initialize(testSettings);
            string key = "invalid_key";
            
            // Act
            string text = localizationSystem.GetLocalizedText(key);
            
            // Assert
            Assert.AreEqual(key, text);
        }
        
        [Test]
        public void SetLanguage_UpdatesLanguage()
        {
            // Arrange
            localizationSystem.Initialize(testSettings);
            string newLanguage = "en";
            
            // Act
            localizationSystem.SetLanguage(newLanguage);
            
            // Assert
            Assert.AreEqual(newLanguage, localizationSystem.CurrentLanguage);
        }
        
        [Test]
        public void SetLanguage_WithInvalidLanguage_KeepsCurrentLanguage()
        {
            // Arrange
            localizationSystem.Initialize(testSettings);
            string currentLanguage = localizationSystem.CurrentLanguage;
            string invalidLanguage = "invalid";
            
            // Act
            localizationSystem.SetLanguage(invalidLanguage);
            
            // Assert
            Assert.AreEqual(currentLanguage, localizationSystem.CurrentLanguage);
        }
        
        [Test]
        public void GetAvailableLanguages_ReturnsLanguages()
        {
            // Arrange
            localizationSystem.Initialize(testSettings);
            
            // Act
            List<string> languages = localizationSystem.GetAvailableLanguages();
            
            // Assert
            Assert.IsNotNull(languages);
            Assert.IsNotEmpty(languages);
            Assert.Contains("ru", languages);
            Assert.Contains("en", languages);
        }
        
        [Test]
        public void OnLanguageChanged_TriggersEvent()
        {
            // Arrange
            localizationSystem.Initialize(testSettings);
            bool eventTriggered = false;
            localizationSystem.OnLanguageChanged += () => eventTriggered = true;
            
            // Act
            localizationSystem.SetLanguage("en");
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
    }
} 