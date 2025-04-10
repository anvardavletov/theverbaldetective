using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class GameProgress
{
    public int currentLevel;
    public int totalScore;
    public List<string> unlockedLevels;
    public Dictionary<string, int> levelScores;
    public Dictionary<string, bool> levelCompleted;
    public List<string> collectedItems;
    public DateTime lastSaveTime;
}

public class SaveSystem : MonoBehaviour
{
    private const string SAVE_FOLDER = "Saves";
    private const string SAVE_FILE = "game_progress.json";
    
    private GameProgress currentProgress;
    private string savePath;
    
    private void Awake()
    {
        InitializeSaveSystem();
    }
    
    private void InitializeSaveSystem()
    {
        string persistentDataPath = Application.persistentDataPath;
        string saveFolderPath = Path.Combine(persistentDataPath, SAVE_FOLDER);
        
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }
        
        savePath = Path.Combine(saveFolderPath, SAVE_FILE);
        LoadProgress();
    }
    
    public void SaveProgress()
    {
        if (currentProgress == null)
        {
            currentProgress = new GameProgress();
        }
        
        currentProgress.lastSaveTime = DateTime.Now;
        
        string json = JsonUtility.ToJson(currentProgress, true);
        File.WriteAllText(savePath, json);
    }
    
    public void LoadProgress()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentProgress = JsonUtility.FromJson<GameProgress>(json);
        }
        else
        {
            currentProgress = new GameProgress
            {
                currentLevel = 1,
                totalScore = 0,
                unlockedLevels = new List<string> { "level_1" },
                levelScores = new Dictionary<string, int>(),
                levelCompleted = new Dictionary<string, bool>(),
                collectedItems = new List<string>(),
                lastSaveTime = DateTime.Now
            };
        }
    }
    
    public void UpdateLevelProgress(string levelId, int score, bool completed)
    {
        if (currentProgress.levelScores.ContainsKey(levelId))
        {
            currentProgress.levelScores[levelId] = Mathf.Max(currentProgress.levelScores[levelId], score);
        }
        else
        {
            currentProgress.levelScores[levelId] = score;
        }
        
        currentProgress.levelCompleted[levelId] = completed;
        currentProgress.totalScore += score;
        
        if (completed && !currentProgress.unlockedLevels.Contains(levelId))
        {
            currentProgress.unlockedLevels.Add(levelId);
        }
        
        SaveProgress();
    }
    
    public void AddCollectedItem(string itemId)
    {
        if (!currentProgress.collectedItems.Contains(itemId))
        {
            currentProgress.collectedItems.Add(itemId);
            SaveProgress();
        }
    }
    
    public bool IsLevelUnlocked(string levelId)
    {
        return currentProgress.unlockedLevels.Contains(levelId);
    }
    
    public int GetLevelScore(string levelId)
    {
        return currentProgress.levelScores.ContainsKey(levelId) ? currentProgress.levelScores[levelId] : 0;
    }
    
    public bool IsLevelCompleted(string levelId)
    {
        return currentProgress.levelCompleted.ContainsKey(levelId) && currentProgress.levelCompleted[levelId];
    }
    
    public List<string> GetCollectedItems()
    {
        return new List<string>(currentProgress.collectedItems);
    }
    
    public int GetTotalScore()
    {
        return currentProgress.totalScore;
    }
    
    public DateTime GetLastSaveTime()
    {
        return currentProgress.lastSaveTime;
    }
    
    public void ResetProgress()
    {
        currentProgress = new GameProgress
        {
            currentLevel = 1,
            totalScore = 0,
            unlockedLevels = new List<string> { "level_1" },
            levelScores = new Dictionary<string, int>(),
            levelCompleted = new Dictionary<string, bool>(),
            collectedItems = new List<string>(),
            lastSaveTime = DateTime.Now
        };
        
        SaveProgress();
    }
} 