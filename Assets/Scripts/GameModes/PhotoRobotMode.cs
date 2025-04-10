using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class PhotoRobotMode : MonoBehaviour
{
    [System.Serializable]
    public class FeatureCategory
    {
        public string categoryName;
        public List<string> validWords;
        public List<string> wrongWords;
        public GameObject featureObject;
        public float revealProgress = 0f;
    }

    [Header("Фоторобот")]
    [SerializeField] private Image photoRobotImage;
    [SerializeField] private Image targetImage;
    [SerializeField] private float revealSpeed = 0.5f;
    [SerializeField] private float wrongWordPenalty = 0.2f;
    
    [Header("Категории признаков")]
    [SerializeField] private List<FeatureCategory> featureCategories;
    
    [Header("UI элементы")]
    [SerializeField] private Transform categoryButtonsContainer;
    [SerializeField] private GameObject categoryButtonPrefab;
    [SerializeField] private TextMeshProUGUI similarityText;
    [SerializeField] private GameObject completionPanel;
    
    private Dictionary<string, FeatureCategory> categoryDictionary;
    private float overallSimilarity = 0f;
    private bool isCompleted = false;
    private int wordsUsed = 0;
    private int maxWords = 15;

    private void Start()
    {
        InitializePhotoRobot();
    }

    private void InitializePhotoRobot()
    {
        categoryDictionary = new Dictionary<string, FeatureCategory>();
        foreach (var category in featureCategories)
        {
            categoryDictionary[category.categoryName] = category;
            CreateCategoryButton(category);
            
            // Изначально скрываем все признаки
            if (category.featureObject != null)
            {
                var canvasGroup = category.featureObject.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                    canvasGroup = category.featureObject.AddComponent<CanvasGroup>();
                canvasGroup.alpha = 0f;
            }
        }
        
        // Скрываем целевое изображение
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false);
        }
        
        // Скрываем панель завершения
        if (completionPanel != null)
        {
            completionPanel.SetActive(false);
        }
        
        // Обновляем UI
        UpdateSimilarityUI();
        UpdateWordsRemainingUI();
    }

    private void CreateCategoryButton(FeatureCategory category)
    {
        if (categoryButtonPrefab != null && categoryButtonsContainer != null)
        {
            GameObject buttonObj = Instantiate(categoryButtonPrefab, categoryButtonsContainer);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            
            if (buttonText != null)
            {
                buttonText.text = category.categoryName;
            }
            
            if (button != null)
            {
                button.onClick.AddListener(() => ShowCategoryWords(category));
            }
        }
    }

    private void ShowCategoryWords(FeatureCategory category)
    {
        // Показываем список слов для выбранной категории
        string message = $"Слова для категории '{category.categoryName}':\n";
        foreach (var word in category.validWords)
        {
            message += $"- {word}\n";
        }
        
        UIManager.Instance.ShowNotification(message, "words_icon");
    }

    public void ProcessWord(string word, string categoryName)
    {
        if (isCompleted || wordsUsed >= maxWords) return;
        
        if (categoryDictionary.TryGetValue(categoryName, out FeatureCategory category))
        {
            wordsUsed++;
            UpdateWordsRemainingUI();
            
            if (category.validWords.Contains(word.ToLower()))
            {
                // Правильное слово - раскрываем признак
                RevealFeature(category, true);
                GameManager.Instance.AddScore(word.Length * 10);
            }
            else if (category.wrongWords.Contains(word.ToLower()))
            {
                // Неправильное слово - искажаем признак
                RevealFeature(category, false);
                GameManager.Instance.AddScore(word.Length * 5);
            }
            else
            {
                // Слово не относится к категории
                UIManager.Instance.ShowNotification($"Слово '{word}' не подходит для категории '{categoryName}'", "wrong_icon");
            }
            
            // Проверяем, можно ли завершить фоторобот
            CheckCompletion();
        }
    }

    private void RevealFeature(FeatureCategory category, bool isCorrect)
    {
        if (category.featureObject != null)
        {
            var canvasGroup = category.featureObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = category.featureObject.AddComponent<CanvasGroup>();
            
            // Анимируем появление признака
            float targetAlpha = isCorrect ? 1f : 0.5f;
            canvasGroup.DOFade(targetAlpha, revealSpeed).SetEase(Ease.OutQuad);
            
            // Обновляем прогресс раскрытия
            category.revealProgress = isCorrect ? 
                Mathf.Min(category.revealProgress + 0.25f, 1f) : 
                Mathf.Max(category.revealProgress - wrongWordPenalty, 0f);
                
            // Обновляем общее сходство
            UpdateOverallSimilarity();
        }
    }

    private void UpdateOverallSimilarity()
    {
        float totalProgress = 0f;
        foreach (var category in featureCategories)
        {
            totalProgress += category.revealProgress;
        }
        
        overallSimilarity = totalProgress / featureCategories.Count;
        UpdateSimilarityUI();
    }

    private void UpdateSimilarityUI()
    {
        if (similarityText != null)
        {
            similarityText.text = $"Сходство: {Mathf.RoundToInt(overallSimilarity * 100)}%";
            
            // Изменяем цвет в зависимости от сходства
            Color textColor = Color.Lerp(Color.red, Color.green, overallSimilarity);
            similarityText.color = textColor;
        }
    }

    private void UpdateWordsRemainingUI()
    {
        UIManager.Instance.ShowNotification($"Осталось слов: {maxWords - wordsUsed}", "words_icon");
    }

    private void CheckCompletion()
    {
        // Проверяем, можно ли завершить фоторобот
        if (wordsUsed >= maxWords || overallSimilarity >= 0.8f)
        {
            CompletePhotoRobot();
        }
    }

    private void CompletePhotoRobot()
    {
        isCompleted = true;
        
        // Показываем целевое изображение
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(true);
            targetImage.DOFade(1f, 1f).From(0f);
        }
        
        // Показываем панель завершения
        if (completionPanel != null)
        {
            completionPanel.SetActive(true);
            completionPanel.transform.localScale = Vector3.zero;
            completionPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
        
        // Добавляем награду
        string rewardName = $"Фоторобот #{Random.Range(1, 100)}";
        GameManager.Instance.AddCollectedItem(rewardName);
        
        // Показываем уведомление о завершении
        UIManager.Instance.ShowNotification($"Фоторобот завершен! Сходство: {Mathf.RoundToInt(overallSimilarity * 100)}%", "complete_icon");
    }
} 