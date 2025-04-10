using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;

namespace WordDetective.Tests
{
    public class GameConstantsTests
    {
        [Test]
        public void GameModes_ContainsAllModes()
        {
            // Assert
            Assert.AreEqual(4, System.Enum.GetValues(typeof(GameMode)).Length);
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameMode), GameMode.None));
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameMode), GameMode.MysteryStory));
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameMode), GameMode.Photofit));
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameMode), GameMode.CrimeScene));
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameMode), GameMode.Expertise));
        }
        
        [Test]
        public void GameStatuses_ContainsAllStatuses()
        {
            // Assert
            Assert.AreEqual(4, System.Enum.GetValues(typeof(GameStatus)).Length);
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameStatus), GameStatus.MainMenu));
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameStatus), GameStatus.Playing));
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameStatus), GameStatus.Paused));
            Assert.IsTrue(System.Enum.IsDefined(typeof(GameStatus), GameStatus.GameOver));
        }
        
        [Test]
        public void DefaultValues_AreCorrect()
        {
            // Assert
            Assert.AreEqual(3, GameConstants.DEFAULT_HINTS);
            Assert.AreEqual(0, GameConstants.DEFAULT_COINS);
            Assert.AreEqual(0, GameConstants.DEFAULT_SCORE);
            Assert.AreEqual(60f, GameConstants.DEFAULT_GAME_TIME);
            Assert.AreEqual(1f, GameConstants.DEFAULT_MUSIC_VOLUME);
            Assert.AreEqual(1f, GameConstants.DEFAULT_SFX_VOLUME);
            Assert.AreEqual("ru", GameConstants.DEFAULT_LANGUAGE);
            Assert.AreEqual(2, GameConstants.DEFAULT_QUALITY_LEVEL);
        }
        
        [Test]
        public void MinMaxValues_AreCorrect()
        {
            // Assert
            Assert.AreEqual(0f, GameConstants.MIN_VOLUME);
            Assert.AreEqual(1f, GameConstants.MAX_VOLUME);
            Assert.AreEqual(0, GameConstants.MIN_QUALITY_LEVEL);
            Assert.AreEqual(3, GameConstants.MAX_QUALITY_LEVEL);
            Assert.AreEqual(0, GameConstants.MIN_HINTS);
            Assert.AreEqual(5, GameConstants.MAX_HINTS);
            Assert.AreEqual(0, GameConstants.MIN_COINS);
            Assert.AreEqual(9999, GameConstants.MAX_COINS);
        }
        
        [Test]
        public void GameSettings_AreCorrect()
        {
            // Assert
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_ADS);
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_IAP);
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_NOTIFICATIONS);
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_TUTORIAL);
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_SOUND);
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_MUSIC);
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_VIBRATION);
            Assert.IsTrue(GameConstants.DEFAULT_ENABLE_DEBUG);
        }
        
        [Test]
        public void FilePaths_AreCorrect()
        {
            // Assert
            Assert.AreEqual("GameSettings.json", GameConstants.SETTINGS_FILE);
            Assert.AreEqual("GameState.json", GameConstants.STATE_FILE);
            Assert.AreEqual("HighScores.json", GameConstants.HIGH_SCORES_FILE);
        }
        
        [Test]
        public void SceneNames_AreCorrect()
        {
            // Assert
            Assert.AreEqual("MainMenu", GameConstants.MAIN_MENU_SCENE);
            Assert.AreEqual("Game", GameConstants.GAME_SCENE);
            Assert.AreEqual("Store", GameConstants.STORE_SCENE);
            Assert.AreEqual("Settings", GameConstants.SETTINGS_SCENE);
        }
        
        [Test]
        public void AudioClips_AreCorrect()
        {
            // Assert
            Assert.AreEqual("Click", GameConstants.CLICK_SOUND);
            Assert.AreEqual("Success", GameConstants.SUCCESS_SOUND);
            Assert.AreEqual("Failure", GameConstants.FAILURE_SOUND);
            Assert.AreEqual("Achievement", GameConstants.ACHIEVEMENT_SOUND);
            Assert.AreEqual("Background", GameConstants.BACKGROUND_MUSIC);
        }
        
        [Test]
        public void Animations_AreCorrect()
        {
            // Assert
            Assert.AreEqual("FadeIn", GameConstants.FADE_IN_ANIM);
            Assert.AreEqual("FadeOut", GameConstants.FADE_OUT_ANIM);
            Assert.AreEqual("Show", GameConstants.SHOW_ANIM);
            Assert.AreEqual("Hide", GameConstants.HIDE_ANIM);
        }
        
        [Test]
        public void Tags_AreCorrect()
        {
            // Assert
            Assert.AreEqual("Player", GameConstants.PLAYER_TAG);
            Assert.AreEqual("Enemy", GameConstants.ENEMY_TAG);
            Assert.AreEqual("Collectible", GameConstants.COLLECTIBLE_TAG);
            Assert.AreEqual("Obstacle", GameConstants.OBSTACLE_TAG);
        }
        
        [Test]
        public void Layers_AreCorrect()
        {
            // Assert
            Assert.AreEqual("Default", GameConstants.DEFAULT_LAYER);
            Assert.AreEqual("UI", GameConstants.UI_LAYER);
            Assert.AreEqual("Background", GameConstants.BACKGROUND_LAYER);
            Assert.AreEqual("Foreground", GameConstants.FOREGROUND_LAYER);
        }
    }
} 