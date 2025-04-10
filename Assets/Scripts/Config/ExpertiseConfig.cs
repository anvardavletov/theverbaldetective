using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ExpertiseConfig", menuName = "Словесный Детектив/Конфигурация/ExpertiseConfig")]
public class ExpertiseConfig : ScriptableObject
{
    [Header("Настройки экспертизы")]
    public int minFields = 3;
    public int maxFields = 7;
    public float timePerField = 60f;
    public int minWordLength = 4;
    public int maxWordLength = 12;
    
    [Header("Настройки наград")]
    public int baseCoinsPerExpertise = 30;
    public int baseHintsPerExpertise = 1;
    public int perfectExpertiseBonus = 70;
    public int fieldStreakBonus = 15;
    
    [Header("Настройки сложности")]
    public float easyTimeMultiplier = 1.5f;
    public float normalTimeMultiplier = 1.0f;
    public float hardTimeMultiplier = 0.7f;
    
    [Header("Настройки подсказок")]
    public int hintCost = 5;
    public float hintCooldown = 60f;
    public int maxHintsPerExpertise = 3;
    
    [Header("Настройки визуальных эффектов")]
    public float fieldCompleteSpeed = 0.5f;
    public float successEffectDuration = 1f;
    public float failureEffectDuration = 1f;
    public float fieldHighlightDuration = 0.3f;
    
    [Header("Настройки звука")]
    public string fieldCompleteSound = "field_complete";
    public string successSound = "success";
    public string failureSound = "failure";
    public string expertiseMusic = "expertise_theme";
    
    [Header("Настройки локализации")]
    public string fieldPrefix = "field_";
    public string wordPrefix = "word_";
    public string descriptionPrefix = "description_";
    
    [Header("Настройки сохранения")]
    public string saveKeyPrefix = "expertise_";
    public bool autoSave = true;
    public float autoSaveInterval = 300f;
    
    [Header("Настройки отладки")]
    public bool showDebugInfo = false;
    public bool logFieldChanges = false;
    public bool logWordValidation = false;
    public bool logExpertiseProgress = false;
    
    [Header("Настройки оптимизации")]
    public int maxActiveFields = 5;
    public float fieldUpdateInterval = 0.1f;
    public bool useFieldPooling = true;
    public int fieldPoolSize = 10;
    
    [Header("Настройки UI")]
    public float progressBarSpeed = 0.3f;
    public float timerWarningThreshold = 0.2f;
    public Color normalTimerColor = Color.white;
    public Color warningTimerColor = Color.red;
    public float fieldCompleteAnimationDuration = 0.5f;
    public float fieldCompleteScaleMultiplier = 1.2f;
} 