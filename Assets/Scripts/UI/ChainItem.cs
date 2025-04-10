using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class ChainItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemBorder;
    [SerializeField] private float appearDuration = 0.3f;
    [SerializeField] private float highlightDuration = 0.2f;
    [SerializeField] private float highlightScale = 1.2f;
    
    private Vector3 originalScale;
    private Color originalTextColor;
    private Color originalBackgroundColor;
    private Color originalBorderColor;
    
    private void Awake()
    {
        if (itemText == null)
        {
            itemText = GetComponentInChildren<TextMeshProUGUI>();
        }
        
        if (itemBackground == null)
        {
            itemBackground = GetComponent<Image>();
        }
        
        originalScale = transform.localScale;
        
        if (itemText != null)
        {
            originalTextColor = itemText.color;
        }
        
        if (itemBackground != null)
        {
            originalBackgroundColor = itemBackground.color;
        }
        
        if (itemBorder != null)
        {
            originalBorderColor = itemBorder.color;
        }
        
        // Начальное состояние
        transform.localScale = Vector3.zero;
        if (itemBorder != null)
        {
            itemBorder.color = new Color(originalBorderColor.r, originalBorderColor.g, originalBorderColor.b, 0f);
        }
    }
    
    public void Initialize(string word, bool isRareTerm)
    {
        if (itemText != null)
        {
            itemText.text = word;
            itemText.color = isRareTerm ? Color.yellow : originalTextColor;
        }
        
        // Анимация появления
        transform.DOScale(originalScale, appearDuration)
            .SetEase(Ease.OutBack);
            
        if (itemBorder != null)
        {
            itemBorder.DOFade(1f, appearDuration)
                .SetEase(Ease.OutQuad);
        }
    }
    
    public void Highlight()
    {
        // Анимация подсветки
        transform.DOPunchScale(Vector3.one * highlightScale, highlightDuration)
            .SetEase(Ease.OutBack);
            
        if (itemText != null)
        {
            itemText.DOColor(Color.green, highlightDuration)
                .SetEase(Ease.OutQuad);
        }
        
        if (itemBackground != null)
        {
            itemBackground.DOColor(new Color(0.2f, 0.8f, 0.2f, 0.3f), highlightDuration)
                .SetEase(Ease.OutQuad);
        }
        
        if (itemBorder != null)
        {
            itemBorder.DOColor(Color.green, highlightDuration)
                .SetEase(Ease.OutQuad);
        }
    }
    
    public void SetCompleted()
    {
        if (itemText != null)
        {
            itemText.color = Color.green;
        }
        
        if (itemBackground != null)
        {
            itemBackground.color = new Color(0.2f, 0.8f, 0.2f, 0.3f);
        }
        
        if (itemBorder != null)
        {
            itemBorder.color = Color.green;
        }
    }
    
    public void SetInteractable(bool interactable)
    {
        // Изменяем прозрачность при отключении
        if (!interactable)
        {
            if (itemText != null)
            {
                itemText.DOFade(0.5f, 0.3f);
            }
            
            if (itemBackground != null)
            {
                itemBackground.DOFade(0.5f, 0.3f);
            }
            
            if (itemBorder != null)
            {
                itemBorder.DOFade(0.5f, 0.3f);
            }
        }
        else
        {
            if (itemText != null)
            {
                itemText.DOFade(1f, 0.3f);
            }
            
            if (itemBackground != null)
            {
                itemBackground.DOFade(1f, 0.3f);
            }
            
            if (itemBorder != null)
            {
                itemBorder.DOFade(1f, 0.3f);
            }
        }
    }
} 