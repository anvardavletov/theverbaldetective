using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Main UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI currentSentenceText;
    [SerializeField] private TextMeshProUGUI currentWordText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Button submitWordButton;
    [SerializeField] private Button resetWordButton;
    [SerializeField] private Slider timeSlider;

    [Header("Game Mode UI")]
    [SerializeField] private GameObject mysteryStoryUI;
    [SerializeField] private GameObject photoRobotUI;
    [SerializeField] private GameObject crimeSceneUI;
    [SerializeField] private GameObject expertiseUI;

    [Header("Story Choice UI")]
    [SerializeField] private GameObject storyChoicePanel;
    [SerializeField] private Button[] storyChoiceButtons;
    [SerializeField] private TextMeshProUGUI[] storyChoiceTexts;

    [Header("Notification UI")]
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private Image notificationIcon;
    [SerializeField] private float notificationDuration = 3f;

    [Header("Collection UI")]
    [SerializeField] private GameObject collectionPanel;
    [SerializeField] private Transform collectionGrid;
    [SerializeField] private GameObject collectionItemPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeUI()
    {
        // Скрываем все панели при старте
        notificationPanel?.SetActive(false);
        storyChoicePanel?.SetActive(false);
        collectionPanel?.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Очки: {score}";
            // Анимация обновления очков
            scoreText.transform.DOPunchScale(Vector3.one * 1.2f, 0.3f);
        }
    }

    public void UpdateLevel(int level)
    {
        if (levelText != null)
        {
            levelText.text = $"Уровень: {level}";
        }
    }

    public void UpdateCurrentSentence(string sentence)
    {
        if (currentSentenceText != null)
        {
            currentSentenceText.text = sentence;
            // Эффект печатающей машинки
            currentSentenceText.DOText(sentence, 1f);
        }
    }

    public void UpdateCurrentWord(string word)
    {
        if (currentWordText != null)
        {
            currentWordText.text = word;
        }
    }

    public void ShowRankUpNotification(string newRank)
    {
        ShowNotification($"Поздравляем! Новое звание: {newRank}", "rank_icon");
        if (rankText != null)
        {
            rankText.text = newRank;
            rankText.transform.DOPunchScale(Vector3.one * 1.5f, 0.5f);
        }
    }

    public void ShowCollectionNotification(string item)
    {
        ShowNotification($"Получен новый предмет: {item}", "collection_icon");
        UpdateCollectionUI();
    }

    private void ShowNotification(string message, string iconName)
    {
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(true);
            notificationText.text = message;
            // Загрузка иконки из ресурсов
            Sprite icon = Resources.Load<Sprite>($"Icons/{iconName}");
            if (icon != null)
                notificationIcon.sprite = icon;

            // Анимация появления
            notificationPanel.transform.localScale = Vector3.zero;
            notificationPanel.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);

            // Автоматическое скрытие
            DOVirtual.DelayedCall(notificationDuration, () => {
                notificationPanel.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack)
                    .OnComplete(() => notificationPanel.SetActive(false));
            });
        }
    }

    public void ShowStoryChoices(List<string> choices, UnityAction<int> onChoiceSelected)
    {
        if (storyChoicePanel != null && storyChoiceButtons != null && choices != null)
        {
            storyChoicePanel.SetActive(true);
            
            for (int i = 0; i < storyChoiceButtons.Length && i < choices.Count; i++)
            {
                int choiceIndex = i;
                storyChoiceTexts[i].text = choices[i];
                storyChoiceButtons[i].onClick.RemoveAllListeners();
                storyChoiceButtons[i].onClick.AddListener(() => {
                    onChoiceSelected?.Invoke(choiceIndex);
                    storyChoicePanel.SetActive(false);
                });
            }
        }
    }

    public void UpdateTimeLimit(float currentTime, float maxTime)
    {
        if (timeSlider != null)
        {
            timeSlider.value = currentTime / maxTime;
            // Изменение цвета слайдера в зависимости от оставшегося времени
            if (currentTime / maxTime < 0.3f)
            {
                timeSlider.transform.DOShakePosition(0.5f, 5f, 10, 90f);
            }
        }
    }

    private void UpdateCollectionUI()
    {
        if (collectionPanel != null && collectionGrid != null)
        {
            // Обновление сетки коллекции
            // Реализация зависит от конкретной структуры UI
        }
    }

    public void ShowGameModeUI(GameManager.GameMode mode)
    {
        // Скрываем все UI режимов
        mysteryStoryUI?.SetActive(false);
        photoRobotUI?.SetActive(false);
        crimeSceneUI?.SetActive(false);
        expertiseUI?.SetActive(false);

        // Показываем UI выбранного режима с анимацией
        GameObject targetUI = null;
        switch (mode)
        {
            case GameManager.GameMode.MysteryStory:
                targetUI = mysteryStoryUI;
                break;
            case GameManager.GameMode.PhotoRobot:
                targetUI = photoRobotUI;
                break;
            case GameManager.GameMode.CrimeScene:
                targetUI = crimeSceneUI;
                break;
            case GameManager.GameMode.Expertise:
                targetUI = expertiseUI;
                break;
        }

        if (targetUI != null)
        {
            targetUI.SetActive(true);
            targetUI.transform.localScale = Vector3.zero;
            targetUI.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
    }

    public void SetSubmitButtonInteractable(bool interactable)
    {
        if (submitWordButton != null)
        {
            submitWordButton.interactable = interactable;
        }
    }

    public void SetResetButtonInteractable(bool interactable)
    {
        if (resetWordButton != null)
        {
            resetWordButton.interactable = interactable;
        }
    }
} 