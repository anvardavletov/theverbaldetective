using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class ExpertiseInputManager : MonoBehaviour
{
    [Header("UI элементы")]
    [SerializeField] private TMP_InputField wordInputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private TextMeshProUGUI availableWordsText;
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private GameObject suggestionPanel;
    
    [Header("Настройки")]
    [SerializeField] private int maxInputLength = 20;
    [SerializeField] private float suggestionShowDelay = 0.5f;
    [SerializeField] private int maxSuggestions = 5;
    [SerializeField] private Color validWordColor = Color.green;
    [SerializeField] private Color invalidWordColor = Color.red;
    
    [Header("Анимация")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeStrength = 5f;
    [SerializeField] private int shakeVibrato = 10;
    [SerializeField] private float shakeRandomness = 90f;
    
    private ExpertiseMode expertiseMode;
    private List<string> currentSuggestions = new List<string>();
    private bool isProcessingInput = false;
    private Sequence currentAnimation;
    
    private void Awake()
    {
        if (wordInputField == null)
        {
            wordInputField = GetComponentInChildren<TMP_InputField>();
        }
        
        if (submitButton == null)
        {
            submitButton = GetComponentInChildren<Button>();
        }
        
        // Находим ExpertiseMode
        expertiseMode = FindObjectOfType<ExpertiseMode>();
        
        // Настраиваем обработчики событий
        SetupEventHandlers();
    }
    
    private void SetupEventHandlers()
    {
        if (wordInputField != null)
        {
            wordInputField.onValueChanged.AddListener(OnInputValueChanged);
            wordInputField.onSubmit.AddListener(OnInputSubmit);
            wordInputField.onEndEdit.AddListener(OnInputEndEdit);
            
            // Ограничиваем длину ввода
            wordInputField.characterLimit = maxInputLength;
        }
        
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitButtonClick);
        }
    }
    
    private void OnInputValueChanged(string value)
    {
        // Обновляем подсказки при вводе
        UpdateSuggestions(value);
        
        // Обновляем текст доступных слов
        UpdateAvailableWordsText();
    }
    
    private void OnInputSubmit(string value)
    {
        ProcessInput(value);
    }
    
    private void OnInputEndEdit(string value)
    {
        // Если ввод завершен по Enter, обрабатываем его
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ProcessInput(value);
        }
    }
    
    private void OnSubmitButtonClick()
    {
        if (wordInputField != null)
        {
            ProcessInput(wordInputField.text);
        }
    }
    
    private void ProcessInput(string input)
    {
        if (isProcessingInput || string.IsNullOrEmpty(input) || expertiseMode == null)
        {
            return;
        }
        
        isProcessingInput = true;
        
        // Очищаем поле ввода
        if (wordInputField != null)
        {
            wordInputField.text = "";
        }
        
        // Отправляем слово в ExpertiseMode
        expertiseMode.ProcessWord(input);
        
        // Сбрасываем флаг обработки
        isProcessingInput = false;
        
        // Обновляем UI
        UpdateAvailableWordsText();
    }
    
    private void UpdateSuggestions(string input)
    {
        if (string.IsNullOrEmpty(input) || expertiseMode == null)
        {
            HideSuggestions();
            return;
        }
        
        // Получаем текущее поле экспертизы
        var currentField = expertiseMode.GetCurrentField();
        if (currentField == null)
        {
            HideSuggestions();
            return;
        }
        
        // Очищаем текущие подсказки
        currentSuggestions.Clear();
        
        // Ищем подходящие слова
        string lowerInput = input.ToLower();
        
        // Проверяем обычные термины
        foreach (var term in currentField.commonTerms)
        {
            if (term.ToLower().Contains(lowerInput) && !currentSuggestions.Contains(term))
            {
                currentSuggestions.Add(term);
                if (currentSuggestions.Count >= maxSuggestions)
                {
                    break;
                }
            }
        }
        
        // Проверяем редкие термины
        if (currentSuggestions.Count < maxSuggestions)
        {
            foreach (var term in currentField.rareTerms)
            {
                if (term.ToLower().Contains(lowerInput) && !currentSuggestions.Contains(term))
                {
                    currentSuggestions.Add(term);
                    if (currentSuggestions.Count >= maxSuggestions)
                    {
                        break;
                    }
                }
            }
        }
        
        // Показываем подсказки
        ShowSuggestions();
    }
    
    private void ShowSuggestions()
    {
        if (suggestionPanel == null || currentSuggestions.Count == 0)
        {
            HideSuggestions();
            return;
        }
        
        // Показываем панель подсказок
        suggestionPanel.SetActive(true);
        
        // Обновляем текст подсказок
        if (availableWordsText != null)
        {
            string suggestionsText = "Возможные слова:\n";
            foreach (var suggestion in currentSuggestions)
            {
                suggestionsText += $"• {suggestion}\n";
            }
            availableWordsText.text = suggestionsText;
        }
        
        // Анимация появления
        suggestionPanel.transform.localScale = Vector3.zero;
        suggestionPanel.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }
    
    private void HideSuggestions()
    {
        if (suggestionPanel != null)
        {
            // Анимация скрытия
            suggestionPanel.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack)
                .OnComplete(() => {
                    suggestionPanel.SetActive(false);
                });
        }
    }
    
    private void UpdateAvailableWordsText()
    {
        if (availableWordsText == null || expertiseMode == null)
        {
            return;
        }
        
        // Получаем текущее поле экспертизы
        var currentField = expertiseMode.GetCurrentField();
        if (currentField == null)
        {
            availableWordsText.text = "Выберите поле экспертизы";
            return;
        }
        
        // Обновляем текст с доступными словами
        string text = $"Поле: {currentField.fieldName}\n";
        text += $"Обычные термины: {currentField.commonTerms.Count}\n";
        text += $"Редкие термины: {currentField.rareTerms.Count}\n";
        text += $"Цепочки: {currentField.wordChains.Count}";
        
        availableWordsText.text = text;
    }
    
    public void ShowInputPanel()
    {
        if (inputPanel != null)
        {
            inputPanel.SetActive(true);
            inputPanel.transform.localScale = Vector3.zero;
            inputPanel.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }
    }
    
    public void HideInputPanel()
    {
        if (inputPanel != null)
        {
            inputPanel.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack)
                .OnComplete(() => {
                    inputPanel.SetActive(false);
                });
        }
    }
    
    public void ShakeInputField()
    {
        if (wordInputField != null)
        {
            // Останавливаем текущую анимацию
            if (currentAnimation != null && currentAnimation.IsPlaying())
            {
                currentAnimation.Kill();
            }
            
            // Создаем новую анимацию тряски
            currentAnimation = DOTween.Sequence();
            currentAnimation.Append(wordInputField.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, false, true));
            currentAnimation.Join(wordInputField.textComponent.DOColor(invalidWordColor, shakeDuration * 0.5f)
                .SetLoops(2, LoopType.Yoyo));
        }
    }
    
    public void ShowValidWordEffect()
    {
        if (wordInputField != null)
        {
            // Останавливаем текущую анимацию
            if (currentAnimation != null && currentAnimation.IsPlaying())
            {
                currentAnimation.Kill();
            }
            
            // Создаем анимацию для валидного слова
            currentAnimation = DOTween.Sequence();
            currentAnimation.Append(wordInputField.textComponent.DOColor(validWordColor, 0.3f));
            currentAnimation.Append(wordInputField.textComponent.DOColor(Color.white, 0.3f));
        }
    }
    
    public void SetHint(string hint)
    {
        if (hintText != null)
        {
            hintText.text = hint;
            
            // Анимация появления подсказки
            hintText.transform.localScale = Vector3.zero;
            hintText.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }
    }
    
    private void OnDestroy()
    {
        // Останавливаем все анимации
        if (currentAnimation != null)
        {
            currentAnimation.Kill();
        }
    }
} 