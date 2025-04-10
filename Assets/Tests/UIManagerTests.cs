using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using WordDetective.Systems;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class UIManagerTests
    {
        private GameObject gameObject;
        private UIManager uiManager;
        private Text scoreText;
        private Text timerText;
        private Text coinsText;
        private Text hintsText;
        private Transform mainMenu;
        private Transform gameMenu;
        private Transform pauseMenu;
        private Transform gameOverMenu;
        private Transform storeMenu;
        private Transform settingsMenu;
        
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
            uiManager = gameObject.AddComponent<UIManager>();
            
            // Создаем UI элементы
            var scoreObj = new GameObject("ScoreText");
            scoreText = scoreObj.AddComponent<Text>();
            uiManager.scoreText = scoreText;
            
            var timerObj = new GameObject("TimerText");
            timerText = timerObj.AddComponent<Text>();
            uiManager.timerText = timerText;
            
            var coinsObj = new GameObject("CoinsText");
            coinsText = coinsObj.AddComponent<Text>();
            uiManager.coinsText = coinsText;
            
            var hintsObj = new GameObject("HintsText");
            hintsText = hintsObj.AddComponent<Text>();
            uiManager.hintsText = hintsText;
            
            // Создаем меню
            var mainMenuObj = new GameObject("MainMenu");
            mainMenu = mainMenuObj.transform;
            uiManager.mainMenu = mainMenu;
            
            var gameMenuObj = new GameObject("GameMenu");
            gameMenu = gameMenuObj.transform;
            uiManager.gameMenu = gameMenu;
            
            var pauseMenuObj = new GameObject("PauseMenu");
            pauseMenu = pauseMenuObj.transform;
            uiManager.pauseMenu = pauseMenu;
            
            var gameOverMenuObj = new GameObject("GameOverMenu");
            gameOverMenu = gameOverMenuObj.transform;
            uiManager.gameOverMenu = gameOverMenu;
            
            var storeMenuObj = new GameObject("StoreMenu");
            storeMenu = storeMenuObj.transform;
            uiManager.storeMenu = storeMenu;
            
            var settingsMenuObj = new GameObject("SettingsMenu");
            settingsMenu = settingsMenuObj.transform;
            uiManager.settingsMenu = settingsMenu;
        }
        
        [TearDown]
        public void Teardown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }
        
        [Test]
        public void Initialize_WithSettings_SetsCorrectValues()
        {
            // Arrange
            var settings = new GameSettings
            {
                musicVolume = 0.5f,
                sfxVolume = 0.7f,
                language = "ru",
                qualityLevel = 2
            };
            
            // Act
            uiManager.Initialize(settings);
            
            // Assert
            Assert.IsTrue(mainMenu.gameObject.activeSelf);
            Assert.IsFalse(gameMenu.gameObject.activeSelf);
            Assert.IsFalse(pauseMenu.gameObject.activeSelf);
            Assert.IsFalse(gameOverMenu.gameObject.activeSelf);
            Assert.IsFalse(storeMenu.gameObject.activeSelf);
            Assert.IsFalse(settingsMenu.gameObject.activeSelf);
        }
        
        [Test]
        public void ShowMainMenu_ShowsMainMenu()
        {
            // Act
            uiManager.ShowMainMenu();
            
            // Assert
            Assert.IsTrue(mainMenu.gameObject.activeSelf);
            Assert.IsFalse(gameMenu.gameObject.activeSelf);
            Assert.IsFalse(pauseMenu.gameObject.activeSelf);
            Assert.IsFalse(gameOverMenu.gameObject.activeSelf);
            Assert.IsFalse(storeMenu.gameObject.activeSelf);
            Assert.IsFalse(settingsMenu.gameObject.activeSelf);
        }
        
        [Test]
        public void ShowGameMenu_ShowsGameMenu()
        {
            // Act
            uiManager.ShowGameMenu();
            
            // Assert
            Assert.IsFalse(mainMenu.gameObject.activeSelf);
            Assert.IsTrue(gameMenu.gameObject.activeSelf);
            Assert.IsFalse(pauseMenu.gameObject.activeSelf);
            Assert.IsFalse(gameOverMenu.gameObject.activeSelf);
            Assert.IsFalse(storeMenu.gameObject.activeSelf);
            Assert.IsFalse(settingsMenu.gameObject.activeSelf);
        }
        
        [Test]
        public void ShowPauseMenu_ShowsPauseMenu()
        {
            // Act
            uiManager.ShowPauseMenu();
            
            // Assert
            Assert.IsFalse(mainMenu.gameObject.activeSelf);
            Assert.IsFalse(gameMenu.gameObject.activeSelf);
            Assert.IsTrue(pauseMenu.gameObject.activeSelf);
            Assert.IsFalse(gameOverMenu.gameObject.activeSelf);
            Assert.IsFalse(storeMenu.gameObject.activeSelf);
            Assert.IsFalse(settingsMenu.gameObject.activeSelf);
        }
        
        [Test]
        public void ShowGameOverMenu_ShowsGameOverMenu()
        {
            // Act
            uiManager.ShowGameOverMenu();
            
            // Assert
            Assert.IsFalse(mainMenu.gameObject.activeSelf);
            Assert.IsFalse(gameMenu.gameObject.activeSelf);
            Assert.IsFalse(pauseMenu.gameObject.activeSelf);
            Assert.IsTrue(gameOverMenu.gameObject.activeSelf);
            Assert.IsFalse(storeMenu.gameObject.activeSelf);
            Assert.IsFalse(settingsMenu.gameObject.activeSelf);
        }
        
        [Test]
        public void ShowStoreMenu_ShowsStoreMenu()
        {
            // Act
            uiManager.ShowStoreMenu();
            
            // Assert
            Assert.IsFalse(mainMenu.gameObject.activeSelf);
            Assert.IsFalse(gameMenu.gameObject.activeSelf);
            Assert.IsFalse(pauseMenu.gameObject.activeSelf);
            Assert.IsFalse(gameOverMenu.gameObject.activeSelf);
            Assert.IsTrue(storeMenu.gameObject.activeSelf);
            Assert.IsFalse(settingsMenu.gameObject.activeSelf);
        }
        
        [Test]
        public void ShowSettingsMenu_ShowsSettingsMenu()
        {
            // Act
            uiManager.ShowSettingsMenu();
            
            // Assert
            Assert.IsFalse(mainMenu.gameObject.activeSelf);
            Assert.IsFalse(gameMenu.gameObject.activeSelf);
            Assert.IsFalse(pauseMenu.gameObject.activeSelf);
            Assert.IsFalse(gameOverMenu.gameObject.activeSelf);
            Assert.IsFalse(storeMenu.gameObject.activeSelf);
            Assert.IsTrue(settingsMenu.gameObject.activeSelf);
        }
        
        [Test]
        public void UpdateScore_UpdatesScoreText()
        {
            // Arrange
            int score = 100;
            
            // Act
            uiManager.UpdateScore(score);
            
            // Assert
            Assert.AreEqual(score.ToString(), scoreText.text);
        }
        
        [Test]
        public void UpdateTimer_UpdatesTimerText()
        {
            // Arrange
            float time = 60f;
            
            // Act
            uiManager.UpdateTimer(time);
            
            // Assert
            Assert.AreEqual("01:00", timerText.text);
        }
        
        [Test]
        public void UpdateCoins_UpdatesCoinsText()
        {
            // Arrange
            int coins = 50;
            
            // Act
            uiManager.UpdateCoins(coins);
            
            // Assert
            Assert.AreEqual(coins.ToString(), coinsText.text);
        }
        
        [Test]
        public void UpdateHints_UpdatesHintsText()
        {
            // Arrange
            int hints = 3;
            
            // Act
            uiManager.UpdateHints(hints);
            
            // Assert
            Assert.AreEqual(hints.ToString(), hintsText.text);
        }
        
        [Test]
        public void OnPlayButtonClicked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            uiManager.OnPlayButtonClicked += () => eventTriggered = true;
            
            // Act
            uiManager.OnPlayButtonClick();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnPauseButtonClicked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            uiManager.OnPauseButtonClicked += () => eventTriggered = true;
            
            // Act
            uiManager.OnPauseButtonClick();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnResumeButtonClicked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            uiManager.OnResumeButtonClicked += () => eventTriggered = true;
            
            // Act
            uiManager.OnResumeButtonClick();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnRestartButtonClicked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            uiManager.OnRestartButtonClicked += () => eventTriggered = true;
            
            // Act
            uiManager.OnRestartButtonClick();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnMainMenuButtonClicked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            uiManager.OnMainMenuButtonClicked += () => eventTriggered = true;
            
            // Act
            uiManager.OnMainMenuButtonClick();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnStoreButtonClicked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            uiManager.OnStoreButtonClicked += () => eventTriggered = true;
            
            // Act
            uiManager.OnStoreButtonClick();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
        
        [Test]
        public void OnSettingsButtonClicked_TriggersEvent()
        {
            // Arrange
            bool eventTriggered = false;
            uiManager.OnSettingsButtonClicked += () => eventTriggered = true;
            
            // Act
            uiManager.OnSettingsButtonClick();
            
            // Assert
            Assert.IsTrue(eventTriggered);
        }
    }
} 