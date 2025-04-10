using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameMode
    {
        MysteryStory,
        PhotoRobot,
        CrimeScene,
        Expertise
    }

    [System.Serializable]
    public class PlayerProgress
    {
        public string detectiveRank = "Стажёр";
        public int totalCasesSolved;
        public Dictionary<string, bool> unlockedAchievements = new Dictionary<string, bool>();
        public List<string> collectedItems = new List<string>();
    }

    [SerializeField] private GameMode currentGameMode;
    [SerializeField] private int currentLevel;
    [SerializeField] private int playerScore;
    [SerializeField] private PlayerProgress playerProgress = new PlayerProgress();
    
    // Новые поля для расширенных механик
    private Dictionary<string, List<string>> thematicChains = new Dictionary<string, List<string>>();
    private List<string> currentStoryChoices = new List<string>();
    private float timeLimit = 300f; // 5 минут на уровень по умолчанию
    private bool isTimeLimited = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeThematicChains();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeThematicChains()
    {
        // Инициализация тематических цепочек слов
        thematicChains.Add("оружие", new List<string> { "нож", "пистолет", "револьвер" });
        thematicChains.Add("улики", new List<string> { "отпечаток", "кровь", "волос" });
        thematicChains.Add("внешность", new List<string> { "глаза", "волосы", "шрам" });
    }

    public void StartGame(GameMode mode)
    {
        currentGameMode = mode;
        currentLevel = 1;
        playerScore = 0;
        isTimeLimited = false;
        
        // Настройка режима игры
        switch (mode)
        {
            case GameMode.MysteryStory:
                GenerateStoryChoices();
                break;
            case GameMode.PhotoRobot:
                InitializePhotoRobotFeatures();
                break;
            case GameMode.CrimeScene:
                isTimeLimited = true; // Временное ограничение для улик
                break;
            case GameMode.Expertise:
                InitializeExpertiseChains();
                break;
        }
    }

    private void GenerateStoryChoices()
    {
        currentStoryChoices.Clear();
        // Генерация вариантов развития сюжета
        currentStoryChoices.Add("Подозреваемый скрывается в доках");
        currentStoryChoices.Add("Преступник затаился в старом особняке");
        currentStoryChoices.Add("След ведет на заброшенный завод");
    }

    public void AddScore(int points)
    {
        playerScore += points;
        CheckRankProgress();
        UIManager.Instance.UpdateScore(playerScore);
    }

    private void CheckRankProgress()
    {
        // Обновление звания детектива
        if (playerScore >= 1000 && playerProgress.detectiveRank == "Стажёр")
            UpdateDetectiveRank("Младший детектив");
        else if (playerScore >= 5000 && playerProgress.detectiveRank == "Младший детектив")
            UpdateDetectiveRank("Опытный детектив");
        else if (playerScore >= 10000 && playerProgress.detectiveRank == "Опытный детектив")
            UpdateDetectiveRank("Легенда сыска");
    }

    private void UpdateDetectiveRank(string newRank)
    {
        playerProgress.detectiveRank = newRank;
        // Оповещение UI о смене звания
        UIManager.Instance.ShowRankUpNotification(newRank);
    }

    public void AddCollectedItem(string item)
    {
        if (!playerProgress.collectedItems.Contains(item))
        {
            playerProgress.collectedItems.Add(item);
            UIManager.Instance.ShowCollectionNotification(item);
        }
    }

    public bool CheckThematicChain(string word)
    {
        foreach (var chain in thematicChains)
        {
            if (chain.Value.Contains(word.ToLower()))
                return true;
        }
        return false;
    }

    public List<string> GetCurrentStoryChoices()
    {
        return currentStoryChoices;
    }

    public string GetCurrentRank()
    {
        return playerProgress.detectiveRank;
    }

    public bool IsTimeLimited()
    {
        return isTimeLimited;
    }

    public float GetTimeLimit()
    {
        return timeLimit;
    }

    public void NextLevel()
    {
        currentLevel++;
        // Загрузка следующего уровня
    }

    public GameMode GetCurrentGameMode()
    {
        return currentGameMode;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }
} 