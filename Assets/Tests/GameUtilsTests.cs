using NUnit.Framework;
using UnityEngine;
using WordDetective.Core;
using System.Collections.Generic;

namespace WordDetective.Tests
{
    public class GameUtilsTests
    {
        [Test]
        public void FormatTime_FormatsCorrectly()
        {
            // Assert
            Assert.AreEqual("00:00", GameUtils.FormatTime(0f));
            Assert.AreEqual("00:01", GameUtils.FormatTime(1f));
            Assert.AreEqual("00:59", GameUtils.FormatTime(59f));
            Assert.AreEqual("01:00", GameUtils.FormatTime(60f));
            Assert.AreEqual("01:01", GameUtils.FormatTime(61f));
            Assert.AreEqual("59:59", GameUtils.FormatTime(3599f));
            Assert.AreEqual("01:00:00", GameUtils.FormatTime(3600f));
        }
        
        [Test]
        public void FormatScore_FormatsCorrectly()
        {
            // Assert
            Assert.AreEqual("0", GameUtils.FormatScore(0));
            Assert.AreEqual("100", GameUtils.FormatScore(100));
            Assert.AreEqual("1,000", GameUtils.FormatScore(1000));
            Assert.AreEqual("10,000", GameUtils.FormatScore(10000));
            Assert.AreEqual("100,000", GameUtils.FormatScore(100000));
            Assert.AreEqual("1,000,000", GameUtils.FormatScore(1000000));
        }
        
        [Test]
        public void FormatCoins_FormatsCorrectly()
        {
            // Assert
            Assert.AreEqual("0", GameUtils.FormatCoins(0));
            Assert.AreEqual("100", GameUtils.FormatCoins(100));
            Assert.AreEqual("1,000", GameUtils.FormatCoins(1000));
            Assert.AreEqual("10,000", GameUtils.FormatCoins(10000));
            Assert.AreEqual("100,000", GameUtils.FormatCoins(100000));
            Assert.AreEqual("1,000,000", GameUtils.FormatCoins(1000000));
        }
        
        [Test]
        public void ClampValue_ClampsCorrectly()
        {
            // Assert
            Assert.AreEqual(0f, GameUtils.ClampValue(-1f, 0f, 1f));
            Assert.AreEqual(0.5f, GameUtils.ClampValue(0.5f, 0f, 1f));
            Assert.AreEqual(1f, GameUtils.ClampValue(2f, 0f, 1f));
            
            Assert.AreEqual(0, GameUtils.ClampValue(-1, 0, 10));
            Assert.AreEqual(5, GameUtils.ClampValue(5, 0, 10));
            Assert.AreEqual(10, GameUtils.ClampValue(11, 0, 10));
        }
        
        [Test]
        public void LerpValue_LerpsCorrectly()
        {
            // Assert
            Assert.AreEqual(0f, GameUtils.LerpValue(0f, 1f, 0f));
            Assert.AreEqual(0.5f, GameUtils.LerpValue(0f, 1f, 0.5f));
            Assert.AreEqual(1f, GameUtils.LerpValue(0f, 1f, 1f));
            
            Assert.AreEqual(0, GameUtils.LerpValue(0, 10, 0f));
            Assert.AreEqual(5, GameUtils.LerpValue(0, 10, 0.5f));
            Assert.AreEqual(10, GameUtils.LerpValue(0, 10, 1f));
        }
        
        [Test]
        public void RandomRange_ReturnsValueInRange()
        {
            // Act & Assert
            for (int i = 0; i < 100; i++)
            {
                float value = GameUtils.RandomRange(0f, 1f);
                Assert.IsTrue(value >= 0f && value <= 1f);
                
                int intValue = GameUtils.RandomRange(0, 10);
                Assert.IsTrue(intValue >= 0 && intValue <= 10);
            }
        }
        
        [Test]
        public void ShuffleList_ShufflesCorrectly()
        {
            // Arrange
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            List<int> originalList = new List<int>(list);
            
            // Act
            GameUtils.ShuffleList(list);
            
            // Assert
            Assert.AreEqual(originalList.Count, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
            Assert.IsTrue(list.Contains(3));
            Assert.IsTrue(list.Contains(4));
            Assert.IsTrue(list.Contains(5));
        }
        
        [Test]
        public void GetRandomItem_ReturnsItemFromList()
        {
            // Arrange
            List<string> list = new List<string> { "один", "два", "три" };
            
            // Act & Assert
            for (int i = 0; i < 100; i++)
            {
                string item = GameUtils.GetRandomItem(list);
                Assert.IsTrue(list.Contains(item));
            }
        }
        
        [Test]
        public void IsValidWord_ValidatesCorrectly()
        {
            // Assert
            Assert.IsTrue(GameUtils.IsValidWord("тест"));
            Assert.IsTrue(GameUtils.IsValidWord("слово"));
            Assert.IsFalse(GameUtils.IsValidWord(""));
            Assert.IsFalse(GameUtils.IsValidWord(" "));
            Assert.IsFalse(GameUtils.IsValidWord("123"));
            Assert.IsFalse(GameUtils.IsValidWord("test"));
        }
        
        [Test]
        public void CalculateScore_CalculatesCorrectly()
        {
            // Assert
            Assert.AreEqual(0, GameUtils.CalculateScore(""));
            Assert.AreEqual(4, GameUtils.CalculateScore("тест"));
            Assert.AreEqual(10, GameUtils.CalculateScore("слово"));
            Assert.AreEqual(0, GameUtils.CalculateScore("123"));
            Assert.AreEqual(0, GameUtils.CalculateScore("test"));
        }
        
        [Test]
        public void GetWordDifficulty_ReturnsCorrectDifficulty()
        {
            // Assert
            Assert.AreEqual(WordDifficulty.Easy, GameUtils.GetWordDifficulty("тест"));
            Assert.AreEqual(WordDifficulty.Medium, GameUtils.GetWordDifficulty("слово"));
            Assert.AreEqual(WordDifficulty.Hard, GameUtils.GetWordDifficulty("сложное"));
            Assert.AreEqual(WordDifficulty.Easy, GameUtils.GetWordDifficulty(""));
        }
        
        [Test]
        public void GetTimeBonus_CalculatesCorrectly()
        {
            // Assert
            Assert.AreEqual(100, GameUtils.GetTimeBonus(60f, 60f));
            Assert.AreEqual(50, GameUtils.GetTimeBonus(30f, 60f));
            Assert.AreEqual(0, GameUtils.GetTimeBonus(0f, 60f));
        }
        
        [Test]
        public void GetDifficultyMultiplier_ReturnsCorrectMultiplier()
        {
            // Assert
            Assert.AreEqual(1f, GameUtils.GetDifficultyMultiplier(WordDifficulty.Easy));
            Assert.AreEqual(1.5f, GameUtils.GetDifficultyMultiplier(WordDifficulty.Medium));
            Assert.AreEqual(2f, GameUtils.GetDifficultyMultiplier(WordDifficulty.Hard));
        }
        
        [Test]
        public void GetAchievementProgress_CalculatesCorrectly()
        {
            // Assert
            Assert.AreEqual(0f, GameUtils.GetAchievementProgress(0, 100));
            Assert.AreEqual(0.5f, GameUtils.GetAchievementProgress(50, 100));
            Assert.AreEqual(1f, GameUtils.GetAchievementProgress(100, 100));
        }
    }
} 