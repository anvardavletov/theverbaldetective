using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CrimeSceneConfig", menuName = "Словесный Детектив/Конфигурация/CrimeSceneConfig")]
public class CrimeSceneConfig : ScriptableObject
{
    [Header("Настройки места преступления")]
    public int minEvidenceItems = 3;
    public int maxEvidenceItems = 7;
    public float evidenceDisappearTime = 45f;
    public int minWordLength = 4;
    public int maxWordLength = 12;
    
    [Header("Настройки наград")]
    public int baseCoinsPerScene = 25;
    public int baseHintsPerScene = 1;
    public int perfectSceneBonus = 60;
    public int evidenceStreakBonus = 12;
    
    [Header("Настройки сложности")]
    public float easyTimeMultiplier = 1.5f;
    public float normalTimeMultiplier = 1.0f;
    public float hardTimeMultiplier = 0.7f;
    
    [Header("Настройки подсказок")]
    public int hintCost = 5;
    public float hintCooldown = 60f;
    public int maxHintsPerScene = 3;
    
    [Header("Настройки визуальных эффектов")]
    public float evidenceFadeSpeed = 0.5f;
    public float successEffectDuration = 1f;
    public float failureEffectDuration = 1f;
    public float evidenceHighlightDuration = 0.3f;
    
    [Header("Настройки звука")]
    public string evidenceFoundSound = "evidence_found";
    public string evidenceLostSound = "evidence_lost";
    public string successSound = "success";
    public string failureSound = "failure";
    public string sceneMusic = "scene_theme";
    
    [Header("Настройки локализации")]
    public string evidencePrefix = "evidence_";
    public string wordPrefix = "word_";
    public string descriptionPrefix = "description_";
    
    [Header("Настройки сохранения")]
    public string saveKeyPrefix = "crime_scene_";
    public bool autoSave = true;
    public float autoSaveInterval = 300f;
    
    [Header("Настройки отладки")]
    public bool showDebugInfo = false;
    public bool logEvidenceChanges = false;
    public bool logWordValidation = false;
    public bool logSceneProgress = false;
    
    [Header("Настройки оптимизации")]
    public int maxActiveEvidence = 5;
    public float evidenceUpdateInterval = 0.1f;
    public bool useEvidencePooling = true;
    public int evidencePoolSize = 10;
    
    [Header("Настройки карты")]
    public float mapZoomSpeed = 0.5f;
    public float mapPanSpeed = 0.3f;
    public float mapRotationSpeed = 0.2f;
    public bool enableMapRotation = true;
    public bool enableMapZoom = true;
    public bool enableMapPan = true;
} 