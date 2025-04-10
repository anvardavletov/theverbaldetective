using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;

namespace WordDetective.Tests
{
    public class GameSettingsTests
    {
        private GameSettings settings;
        
        [SetUp]
        public void Setup()
        {
            settings = new GameSettings();
        }
        
        [TearDown]
        public void Teardown()
        {
            settings = null;
        }
        
        [Test]
        public void Initialize_SetsDefaultValues()
        {
            // Act
            settings.Initialize();
            
            // Assert
            Assert.AreEqual(1f, settings.musicVolume);
            Assert.AreEqual(1f, settings.sfxVolume);
            Assert.AreEqual("ru", settings.language);
            Assert.AreEqual(2, settings.qualityLevel);
            Assert.IsTrue(settings.enableAds);
            Assert.IsTrue(settings.enableIAP);
            Assert.IsTrue(settings.enableNotifications);
            Assert.IsTrue(settings.enableTutorial);
            Assert.IsTrue(settings.enableSound);
            Assert.IsTrue(settings.enableMusic);
            Assert.IsTrue(settings.enableVibration);
            Assert.IsTrue(settings.enableDebug);
        }
        
        [Test]
        public void SetMusicVolume_UpdatesVolume()
        {
            // Arrange
            settings.Initialize();
            float newVolume = 0.5f;
            
            // Act
            settings.SetMusicVolume(newVolume);
            
            // Assert
            Assert.AreEqual(newVolume, settings.musicVolume);
        }
        
        [Test]
        public void SetMusicVolume_WithInvalidValue_ClampsValue()
        {
            // Arrange
            settings.Initialize();
            
            // Act & Assert
            settings.SetMusicVolume(-1f);
            Assert.AreEqual(0f, settings.musicVolume);
            
            settings.SetMusicVolume(2f);
            Assert.AreEqual(1f, settings.musicVolume);
        }
        
        [Test]
        public void SetSFXVolume_UpdatesVolume()
        {
            // Arrange
            settings.Initialize();
            float newVolume = 0.7f;
            
            // Act
            settings.SetSFXVolume(newVolume);
            
            // Assert
            Assert.AreEqual(newVolume, settings.sfxVolume);
        }
        
        [Test]
        public void SetSFXVolume_WithInvalidValue_ClampsValue()
        {
            // Arrange
            settings.Initialize();
            
            // Act & Assert
            settings.SetSFXVolume(-1f);
            Assert.AreEqual(0f, settings.sfxVolume);
            
            settings.SetSFXVolume(2f);
            Assert.AreEqual(1f, settings.sfxVolume);
        }
        
        [Test]
        public void SetLanguage_UpdatesLanguage()
        {
            // Arrange
            settings.Initialize();
            string newLanguage = "en";
            
            // Act
            settings.SetLanguage(newLanguage);
            
            // Assert
            Assert.AreEqual(newLanguage, settings.language);
        }
        
        [Test]
        public void SetQualityLevel_UpdatesQualityLevel()
        {
            // Arrange
            settings.Initialize();
            int newLevel = 1;
            
            // Act
            settings.SetQualityLevel(newLevel);
            
            // Assert
            Assert.AreEqual(newLevel, settings.qualityLevel);
        }
        
        [Test]
        public void SetQualityLevel_WithInvalidValue_ClampsValue()
        {
            // Arrange
            settings.Initialize();
            
            // Act & Assert
            settings.SetQualityLevel(-1);
            Assert.AreEqual(0, settings.qualityLevel);
            
            settings.SetQualityLevel(4);
            Assert.AreEqual(3, settings.qualityLevel);
        }
        
        [Test]
        public void SetEnableAds_UpdatesEnableAds()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableAds(false);
            
            // Assert
            Assert.IsFalse(settings.enableAds);
        }
        
        [Test]
        public void SetEnableIAP_UpdatesEnableIAP()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableIAP(false);
            
            // Assert
            Assert.IsFalse(settings.enableIAP);
        }
        
        [Test]
        public void SetEnableNotifications_UpdatesEnableNotifications()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableNotifications(false);
            
            // Assert
            Assert.IsFalse(settings.enableNotifications);
        }
        
        [Test]
        public void SetEnableTutorial_UpdatesEnableTutorial()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableTutorial(false);
            
            // Assert
            Assert.IsFalse(settings.enableTutorial);
        }
        
        [Test]
        public void SetEnableSound_UpdatesEnableSound()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableSound(false);
            
            // Assert
            Assert.IsFalse(settings.enableSound);
        }
        
        [Test]
        public void SetEnableMusic_UpdatesEnableMusic()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableMusic(false);
            
            // Assert
            Assert.IsFalse(settings.enableMusic);
        }
        
        [Test]
        public void SetEnableVibration_UpdatesEnableVibration()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableVibration(false);
            
            // Assert
            Assert.IsFalse(settings.enableVibration);
        }
        
        [Test]
        public void SetEnableDebug_UpdatesEnableDebug()
        {
            // Arrange
            settings.Initialize();
            
            // Act
            settings.SetEnableDebug(false);
            
            // Assert
            Assert.IsFalse(settings.enableDebug);
        }
        
        [Test]
        public void Reset_ResetsAllValues()
        {
            // Arrange
            settings.Initialize();
            settings.SetMusicVolume(0.5f);
            settings.SetSFXVolume(0.7f);
            settings.SetLanguage("en");
            settings.SetQualityLevel(1);
            settings.SetEnableAds(false);
            settings.SetEnableIAP(false);
            settings.SetEnableNotifications(false);
            settings.SetEnableTutorial(false);
            settings.SetEnableSound(false);
            settings.SetEnableMusic(false);
            settings.SetEnableVibration(false);
            settings.SetEnableDebug(false);
            
            // Act
            settings.Reset();
            
            // Assert
            Assert.AreEqual(1f, settings.musicVolume);
            Assert.AreEqual(1f, settings.sfxVolume);
            Assert.AreEqual("ru", settings.language);
            Assert.AreEqual(2, settings.qualityLevel);
            Assert.IsTrue(settings.enableAds);
            Assert.IsTrue(settings.enableIAP);
            Assert.IsTrue(settings.enableNotifications);
            Assert.IsTrue(settings.enableTutorial);
            Assert.IsTrue(settings.enableSound);
            Assert.IsTrue(settings.enableMusic);
            Assert.IsTrue(settings.enableVibration);
            Assert.IsTrue(settings.enableDebug);
        }
    }
} 