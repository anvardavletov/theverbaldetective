using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ExpertiseUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fieldNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Image fieldIcon;
    [SerializeField] private Transform hintsContainer;
    [SerializeField] private GameObject hintPrefab;
    [SerializeField] private Transform wordsContainer;
    [SerializeField] private GameObject wordPrefab;

    private List<GameObject> activeHints = new List<GameObject>();
    private List<GameObject> activeWords = new List<GameObject>();

    public void UpdateFieldInfo(string fieldName, string description, Sprite icon)
    {
        if (fieldNameText != null)
            fieldNameText.text = fieldName;
        
        if (descriptionText != null)
            descriptionText.text = description;
        
        if (fieldIcon != null)
            fieldIcon.sprite = icon;
    }

    public void UpdateTimer(float remainingTime)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void UpdateProgress(int completedWords, int requiredWords)
    {
        if (progressText != null)
        {
            progressText.text = $"Прогресс: {completedWords}/{requiredWords}";
        }
    }

    public void ShowHints(List<string> hints)
    {
        ClearHints();
        
        foreach (var hint in hints)
        {
            if (hintPrefab != null && hintsContainer != null)
            {
                GameObject hintObj = Instantiate(hintPrefab, hintsContainer);
                TextMeshProUGUI hintText = hintObj.GetComponentInChildren<TextMeshProUGUI>();
                if (hintText != null)
                {
                    hintText.text = hint;
                }
                activeHints.Add(hintObj);
            }
        }
    }

    public void AddWord(string word)
    {
        if (wordPrefab != null && wordsContainer != null)
        {
            GameObject wordObj = Instantiate(wordPrefab, wordsContainer);
            TextMeshProUGUI wordText = wordObj.GetComponentInChildren<TextMeshProUGUI>();
            if (wordText != null)
            {
                wordText.text = word;
            }
            activeWords.Add(wordObj);
        }
    }

    private void ClearHints()
    {
        foreach (var hint in activeHints)
        {
            Destroy(hint);
        }
        activeHints.Clear();
    }

    private void ClearWords()
    {
        foreach (var word in activeWords)
        {
            Destroy(word);
        }
        activeWords.Clear();
    }

    private void OnDestroy()
    {
        ClearHints();
        ClearWords();
    }
} 