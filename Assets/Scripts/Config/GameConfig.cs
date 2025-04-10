using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Словесный Детектив/Конфигурация/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Общие настройки")]
    public int startCoins = 100;
    public int startHints = 3;
    public float baseTimePerLevel = 300f;
    public int minWordLength = 4;
    public int maxWordLength = 12;
    
    [Header("Настройки сложности")]
    public float easyTimeMultiplier = 1.5f;
    public float normalTimeMultiplier = 1.0f;
    public float hardTimeMultiplier = 0.7f;
    
    [Header("Награды")]
    public int baseCoinsPerLevel = 10;
    public int baseHintsPerLevel = 1;
    public int streakBonus = 5;
    public int perfectLevelBonus = 20;
    
    [Header("Подсказки")]
    public int hintCost = 5;
    public float hintCooldown = 60f;
    public int maxHintsPerLevel = 3;
    
    [Header("Реклама")]
    public float adCooldown = 180f;
    public int coinsForAd = 50;
    public int hintsForAd = 1;
    
    [Header("Магазин")]
    public int smallCoinPack = 100;
    public int mediumCoinPack = 500;
    public int largeCoinPack = 1000;
    public int smallHintPack = 5;
    public int mediumHintPack = 15;
    public int largeHintPack = 30;
    
    [Header("Достижения")]
    public int wordsForAchievement = 100;
    public int levelsForAchievement = 50;
    public int coinsForAchievement = 1000;
    public int hintsForAchievement = 50;
    
    [Header("Оптимизация")]
    public int maxParticles = 100;
    public int maxAudioSources = 10;
    public float objectPoolSize = 20f;
    public float textureMemoryLimit = 512f;
    
    [Header("Отладка")]
    public bool showDebugInfo = false;
    public bool enableCheats = false;
    public bool logToFile = false;
    public string logFilePath = "GameLog.txt";
} 