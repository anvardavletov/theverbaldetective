using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpertiseUIManager : MonoBehaviour
{
    [Header("Field Info")]
    [SerializeField] private TextMeshProUGUI fieldNameText;
    [SerializeField] private TextMeshProUGUI fieldDescriptionText;
    [SerializeField] private TextMeshProUGUI requiredWordsText;
    [SerializeField] private Image fieldIcon;

    [Header("Progress")]
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI foundWordsText;

    [Header("Hints")]
    [SerializeField] private TextMeshProUGUI hintsText;
    [SerializeField] private Button hintButton;

    [Header("Results")]
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TextMeshProUGUI expertTitleText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        if (resultsPanel != null)
        {
            resultsPanel.SetActive(false);
        }
    }

    public void UpdateFieldInfo(string fieldName, string description, string requiredWords, Sprite icon)
    {
        if (fieldNameText != null) fieldNameText.text = fieldName;
        if (fieldDescriptionText != null) fieldDescriptionText.text = description;
        if (requiredWordsText != null) requiredWordsText.text = requiredWords;
        if (fieldIcon != null && icon != null) fieldIcon.sprite = icon;
    }

    public void UpdateProgress(int completedFields, int totalFields)
    {
        if (progressText != null)
        {
            progressText.text = $"{completedFields}/{totalFields}";
        }

        if (progressSlider != null)
        {
            progressSlider.value = (float)completedFields / totalFields;
        }
    }

    public void UpdateTimer(float timeRemaining)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void UpdateFoundWords(int foundWords, int totalWords)
    {
        if (foundWordsText != null)
        {
            foundWordsText.text = $"Найдено слов: {foundWords}/{totalWords}";
        }
    }

    public void UpdateHints(int remainingHints)
    {
        if (hintsText != null)
        {
            hintsText.text = $"Подсказки: {remainingHints}";
        }

        if (hintButton != null)
        {
            hintButton.interactable = remainingHints > 0;
        }
    }

    public void ShowResults(string expertTitle, string reward, int score)
    {
        if (resultsPanel != null)
        {
            resultsPanel.SetActive(true);
        }

        if (expertTitleText != null)
        {
            expertTitleText.text = expertTitle;
        }

        if (rewardText != null)
        {
            rewardText.text = reward;
        }

        if (scoreText != null)
        {
            scoreText.text = $"Очки: {score}";
        }
    }
} 