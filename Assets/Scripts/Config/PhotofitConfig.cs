using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PhotofitConfig", menuName = "Словесный Детектив/Конфигурация/PhotofitConfig")]
public class PhotofitConfig : ScriptableObject
{
    [Header("Настройки фоторобота")]
    public int minFeatures = 3;
    public int maxFeatures = 7;
    public float featureChangeTime = 30f;
    public int minWordLength = 4;
    public int maxWordLength = 12;
    
    [Header("Настройки наград")]
    public int baseCoinsPerPhoto = 15;
    public int baseHintsPerPhoto = 1;
    public int perfectPhotoBonus = 40;
    public int featureStreakBonus = 8;
    
    [Header("Настройки сложности")]
    public float easyTimeMultiplier = 1.5f;
    public float normalTimeMultiplier = 1.0f;
    public float hardTimeMultiplier = 0.7f;
    
    [Header("Настройки подсказок")]
    public int hintCost = 5;
    public float hintCooldown = 60f;
    public int maxHintsPerPhoto = 3;
    
    [Header("Настройки визуальных эффектов")]
    public float featureChangeSpeed = 0.5f;
    public float successEffectDuration = 1f;
    public float failureEffectDuration = 1f;
    public float featureHighlightDuration = 0.3f;
    
    [Header("Настройки звука")]
    public string featureChangeSound = "feature_change";
    public string successSound = "success";
    public string failureSound = "failure";
    public string photoMusic = "photo_theme";
    
    [Header("Настройки локализации")]
    public string featurePrefix = "feature_";
    public string wordPrefix = "word_";
    public string descriptionPrefix = "description_";
    
    [Header("Настройки сохранения")]
    public string saveKeyPrefix = "photofit_";
    public bool autoSave = true;
    public float autoSaveInterval = 300f;
    
    [Header("Настройки отладки")]
    public bool showDebugInfo = false;
    public bool logFeatureChanges = false;
    public bool logWordValidation = false;
    public bool logPhotoProgress = false;
    
    [Header("Настройки оптимизации")]
    public int maxActiveFeatures = 5;
    public float featureUpdateInterval = 0.1f;
    public bool useFeaturePooling = true;
    public int featurePoolSize = 10;
} 