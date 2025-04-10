using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ExpertiseCompletionPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panelRect;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI expertTitleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private ParticleSystem completionEffect;
    
    [Header("Анимация")]
    [SerializeField] private float showDuration = 0.5f;
    [SerializeField] private float hideDuration = 0.3f;
    [SerializeField] private Vector2 showOffset = new Vector2(0f, 100f);
    
    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        if (panelRect == null)
        {
            panelRect = GetComponent<RectTransform>();
        }
        
        // Начальное состояние
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        
        if (panelRect != null)
        {
            panelRect.anchoredPosition = showOffset;
        }
        
        // Добавляем обработчик кнопки
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClick);
        }
    }
    
    public void Show(string title, string expertTitle, string description)
    {
        // Обновляем тексты
        if (titleText != null)
        {
            titleText.text = title;
        }
        
        if (expertTitleText != null)
        {
            expertTitleText.text = expertTitle;
        }
        
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }
        
        // Показываем панель
        gameObject.SetActive(true);
        
        // Анимация появления
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(1f, showDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                });
        }
        
        if (panelRect != null)
        {
            panelRect.DOAnchorPos(Vector2.zero, showDuration)
                .SetEase(Ease.OutBack);
        }
        
        // Запускаем эффект завершения
        if (completionEffect != null)
        {
            completionEffect.Play();
        }
    }
    
    public void Hide()
    {
        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            
            canvasGroup.DOFade(0f, hideDuration)
                .SetEase(Ease.InQuad)
                .OnComplete(() => {
                    gameObject.SetActive(false);
                });
        }
        
        if (panelRect != null)
        {
            panelRect.DOAnchorPos(showOffset, hideDuration)
                .SetEase(Ease.InBack);
        }
    }
    
    private void OnContinueClick()
    {
        Hide();
        // Здесь можно добавить дополнительную логику при нажатии кнопки продолжения
    }
    
    public void SetBackgroundColor(Color color)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = color;
        }
    }
} 