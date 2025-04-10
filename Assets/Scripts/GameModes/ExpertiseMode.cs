using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class ExpertiseMode : MonoBehaviour
{
    [Header("Данные")]
    [SerializeField] private ExpertiseData expertiseData;
    
    [Header("Требования")]
    [SerializeField] private int requiredChains = 3;
    [SerializeField] private int requiredRareTerms = 2;
    
    [Header("UI элементы")]
    [SerializeField] private Transform fieldButtonsContainer;
    [SerializeField] private GameObject fieldButtonPrefab;
    [SerializeField] private Transform chainContainer;
    [SerializeField] private GameObject chainItemPrefab;
    [SerializeField] private TextMeshProUGUI fieldDescriptionText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private GameObject completionPanel;
    [SerializeField] private TextMeshProUGUI certificateText;
    
    [Header("Эффекты")]
    [SerializeField] private GameObject microscopeEffect;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip rareTermSound;
    [SerializeField] private ParticleSystem chainCompleteEffect;

    [Header("References")]
    [SerializeField] private ExpertiseUIManager uiManager;
    [SerializeField] private ExpertiseInputManager inputManager;

    [Header("Settings")]
    [SerializeField] private float timePerField = 300f; // 5 минут на поле
    [SerializeField] private int fieldsToComplete = 10;

    private Dictionary<string, ExpertiseData.ExpertiseFieldData> fieldDictionary;
    private ExpertiseData.ExpertiseFieldData currentField;
    private List<string> completedChains = new List<string>();
    private List<string> discoveredRareTerms = new List<string>();
    private bool isCompleted = false;
    private AudioSource audioSource;
    private List<GameObject> chainUIItems = new List<GameObject>();
    private Dictionary<string, List<string>> fieldWordChains = new Dictionary<string, List<string>>();
    private float currentTime;
    private int completedFields;
    private List<string> foundWords = new List<string>();
    private List<string> availableHints = new List<string>();

    public UnityEvent onGameComplete;
    public UnityEvent onFieldComplete;
    public UnityEvent onGameOver;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        if (currentField != null)
        {
            UpdateTimer();
        }
    }

    private void InitializeGame()
    {
        if (expertiseData == null)
        {
            Debug.LogError("ExpertiseData не назначен!");
            return;
        }

        fieldDictionary = new Dictionary<string, ExpertiseData.ExpertiseFieldData>();
        completedChains.Clear();
        discoveredRareTerms.Clear();
        chainUIItems.Clear();
        fieldWordChains.Clear();
        
        // Инициализация словаря полей экспертизы
        foreach (var field in expertiseData.fields)
        {
            fieldDictionary[field.fieldName] = field;
            fieldWordChains[field.fieldName] = new List<string>(field.wordChains);
        }
        
        // Создаем кнопки для полей экспертизы
        CreateFieldButtons();
        
        // Скрываем панель завершения
        if (completionPanel != null)
        {
            completionPanel.SetActive(false);
        }
        
        // Настраиваем эффекты
        SetupEffects();
        
        // Обновляем UI
        UpdateProgressUI();

        completedFields = 0;
        currentField = expertiseData.GetRandomField();
        currentTime = timePerField;
        foundWords.Clear();
        availableHints = expertiseData.GetAvailableHints(currentField);

        UpdateUI();
    }

    private void CreateFieldButtons()
    {
        if (fieldButtonsContainer != null && fieldButtonPrefab != null)
        {
            // Очищаем контейнер
            foreach (Transform child in fieldButtonsContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Создаем кнопки для каждого поля экспертизы
            foreach (var field in expertiseData.fields)
            {
                GameObject buttonObj = Instantiate(fieldButtonPrefab, fieldButtonsContainer);
                Button button = buttonObj.GetComponent<Button>();
                TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
                Image buttonImage = buttonObj.GetComponent<Image>();
                
                if (buttonText != null)
                {
                    buttonText.text = field.fieldName;
                }
                
                if (buttonImage != null)
                {
                    buttonImage.color = field.fieldColor;
                }
                
                if (button != null)
                {
                    button.onClick.AddListener(() => SelectField(field));
                }
            }
        }
    }

    private void SetupEffects()
    {
        // Настраиваем микроскоп
        if (microscopeEffect != null)
        {
            microscopeEffect.SetActive(true);
        }
        
        // Настраиваем звуки
        audioSource = gameObject.AddComponent<AudioSource>();
        
        // Настраиваем эффект завершения цепочки
        if (chainCompleteEffect != null)
        {
            chainCompleteEffect.Stop();
        }
    }

    private void SelectField(ExpertiseData.ExpertiseFieldData field)
    {
        currentField = field;
        
        // Обновляем описание поля
        if (fieldDescriptionText != null)
        {
            fieldDescriptionText.text = field.description;
            fieldDescriptionText.color = field.fieldColor;
        }
        
        // Показываем уведомление
        UIManager.Instance.ShowNotification($"Выбрано поле: {field.fieldName}", "field_icon");
        
        // Очищаем цепочки слов
        ClearChainUI();
    }

    private void ClearChainUI()
    {
        if (chainContainer != null)
        {
            // Очищаем контейнер
            foreach (Transform child in chainContainer)
            {
                Destroy(child.gameObject);
            }
            
            chainUIItems.Clear();
        }
    }

    public void ProcessWord(string word)
    {
        if (currentField == null || foundWords.Contains(word))
            return;

        if (expertiseData.IsWordValid(word, currentField))
        {
            foundWords.Add(word);
            uiManager.AddWord(word);

            if (foundWords.Count >= currentField.requiredWords)
            {
                CompleteField();
            }
        }
    }

    public void ShowHint()
    {
        if (availableHints.Count > 0)
        {
            int hintIndex = Random.Range(0, availableHints.Count);
            string hint = availableHints[hintIndex];
            availableHints.RemoveAt(hintIndex);
            uiManager.ShowHints(availableHints);
        }
    }

    private void CompleteField()
    {
        completedFields++;
        onFieldComplete?.Invoke();

        if (completedFields >= fieldsToComplete)
        {
            CompleteGame();
        }
        else
        {
            StartNewField();
        }
    }

    private void StartNewField()
    {
        currentField = expertiseData.GetRandomField();
        currentTime = timePerField;
        foundWords.Clear();
        availableHints = expertiseData.GetAvailableHints(currentField);
        UpdateUI();
    }

    private void CompleteGame()
    {
        string title = expertiseData.GetExpertTitle(completedFields);
        string reward = expertiseData.GetReward(completedFields);
        onGameComplete?.Invoke();
    }

    private void GameOver()
    {
        onGameOver?.Invoke();
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        uiManager.UpdateTimer(currentTime);

        if (currentTime <= 0)
        {
            GameOver();
        }
    }

    private void UpdateUI()
    {
        if (currentField != null)
        {
            uiManager.UpdateFieldInfo(currentField.fieldName, currentField.description, currentField.fieldIcon);
            uiManager.UpdateProgress(completedFields, fieldsToComplete);
            uiManager.UpdateTimer(currentTime);
        }
    }

    private void UpdateProgressUI()
    {
        if (progressText != null)
        {
            progressText.text = $"Цепочки: {completedChains.Count}/{requiredChains}\nРедкие термины: {discoveredRareTerms.Count}/{requiredRareTerms}";
        }
    }

    private void CheckCompletion()
    {
        // Проверяем, можно ли завершить уровень
        if (completedChains.Count >= requiredChains && discoveredRareTerms.Count >= requiredRareTerms)
        {
            CompleteExpertise();
        }
    }

    private void CompleteExpertise()
    {
        isCompleted = true;
        
        // Генерируем сертификат
        string certificateTitle = "Сертификат эксперта";
        string expertTitle = GenerateExpertTitle();
        
        // Показываем панель завершения
        if (completionPanel != null)
        {
            completionPanel.SetActive(true);
            completionPanel.transform.localScale = Vector3.zero;
            completionPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
        
        // Обновляем текст сертификата
        if (certificateText != null)
        {
            certificateText.text = $"{certificateTitle}\n\n{expertTitle}\n\nПоздравляем с завершением экспертизы!";
        }
        
        // Добавляем награду
        string rewardName = $"Сертификат: {expertTitle}";
        GameManager.Instance.AddCollectedItem(rewardName);
        
        // Показываем уведомление о завершении
        UIManager.Instance.ShowNotification($"Экспертиза завершена! Вы получили звание: {expertTitle}", "certificate_icon");
    }

    // Добавляем метод для получения текущего поля экспертизы
    public ExpertiseData.ExpertiseFieldData GetCurrentField()
    {
        return currentField;
    }

    private string GenerateExpertTitle()
    {
        // Генерируем забавный титул эксперта
        string[] prefixes = { "Мастер", "Гуру", "Профессор", "Доктор", "Академик" };
        string[] fields = { "токсикологии", "криминалистики", "баллистики", "трасологии", "дактилоскопии" };
        
        string prefix = prefixes[Random.Range(0, prefixes.Length)];
        string field = fields[Random.Range(0, fields.Length)];
        
        return $"{prefix} {field}";
    }
} 