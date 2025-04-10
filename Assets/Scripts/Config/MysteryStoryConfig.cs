using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MysteryStoryConfig", menuName = "Словесный Детектив/Конфигурация/MysteryStoryConfig")]
public class MysteryStoryConfig : ScriptableObject
{
    [Header("Настройки истории")]
    public float storyTextSpeed = 0.05f;
    public float choiceDelay = 2f;
    public int minChoicesPerStory = 3;
    public int maxChoicesPerStory = 5;
    
    [Header("Настройки слов")]
    public int minWordsPerChoice = 2;
    public int maxWordsPerChoice = 4;
    public float wordTimeLimit = 30f;
    public int minWordLength = 4;
    public int maxWordLength = 12;
    
    [Header("Настройки наград")]
    public int baseCoinsPerStory = 20;
    public int baseHintsPerStory = 1;
    public int perfectStoryBonus = 50;
    public int wordStreakBonus = 10;
    
    [Header("Настройки сложности")]
    public float easyTimeMultiplier = 1.5f;
    public float normalTimeMultiplier = 1.0f;
    public float hardTimeMultiplier = 0.7f;
    
    [Header("Настройки подсказок")]
    public int hintCost = 5;
    public float hintCooldown = 60f;
    public int maxHintsPerStory = 3;
    
    [Header("Настройки визуальных эффектов")]
    public float textFadeSpeed = 0.5f;
    public float choiceHighlightSpeed = 0.3f;
    public float successEffectDuration = 1f;
    public float failureEffectDuration = 1f;
    
    [Header("Настройки звука")]
    public string typingSound = "typing";
    public string choiceSound = "choice";
    public string successSound = "success";
    public string failureSound = "failure";
    public string storyMusic = "story_theme";
    
    [Header("Настройки локализации")]
    public string storyPrefix = "story_";
    public string choicePrefix = "choice_";
    public string wordPrefix = "word_";
    
    [Header("Настройки сохранения")]
    public string saveKeyPrefix = "mystery_story_";
    public bool autoSave = true;
    public float autoSaveInterval = 300f;
    
    [Header("Настройки отладки")]
    public bool showDebugInfo = false;
    public bool logStoryProgress = false;
    public bool logWordValidation = false;
    public bool logChoiceSelection = false;
} 