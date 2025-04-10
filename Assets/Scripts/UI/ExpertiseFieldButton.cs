using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class ExpertiseFieldButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image highlightImage;
    [SerializeField] private float highlightDuration = 0.3f;
    [SerializeField] private float highlightScale = 1.1f;
    
    private Button button;
    private Vector3 originalScale;
    private Color originalColor;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        originalScale = transform.localScale;
        originalColor = buttonImage.color;
        
        // Добавляем обработчики событий
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        // Анимация нажатия
        transform.DOScale(originalScale * highlightScale, highlightDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() => {
                transform.DOScale(originalScale, highlightDuration)
                    .SetEase(Ease.OutBack);
            });
        
        // Подсветка
        if (highlightImage != null)
        {
            highlightImage.DOFade(1f, highlightDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => {
                    highlightImage.DOFade(0f, highlightDuration)
                        .SetEase(Ease.InQuad);
                });
        }
    }
    
    public void SetFieldData(string fieldName, Color fieldColor)
    {
        if (buttonText != null)
        {
            buttonText.text = fieldName;
        }
        
        if (buttonImage != null)
        {
            buttonImage.color = fieldColor;
            originalColor = fieldColor;
        }
    }
    
    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
        
        // Изменяем прозрачность при отключении
        if (!interactable)
        {
            buttonImage.DOFade(0.5f, 0.3f);
            if (buttonText != null)
            {
                buttonText.DOFade(0.5f, 0.3f);
            }
        }
        else
        {
            buttonImage.DOFade(1f, 0.3f);
            if (buttonText != null)
            {
                buttonText.DOFade(1f, 0.3f);
            }
        }
    }
} 