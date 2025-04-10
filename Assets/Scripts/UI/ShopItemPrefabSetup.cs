using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemPrefabSetup : MonoBehaviour
{
    [Header("UI Компоненты")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Image purchaseButtonImage;
    [SerializeField] private TextMeshProUGUI purchaseButtonText;
    
    [Header("Настройки стиля")]
    [SerializeField] private Color normalBackgroundColor = new Color(1f, 1f, 1f, 0.1f);
    [SerializeField] private Color selectedBackgroundColor = new Color(0.2f, 0.6f, 1f, 0.2f);
    [SerializeField] private Color normalButtonColor = new Color(0.2f, 0.6f, 1f, 1f);
    [SerializeField] private Color disabledButtonColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    
    private ShopItemUI shopItemUI;
    
    private void Awake()
    {
        // Добавляем компонент ShopItemUI, если его нет
        shopItemUI = GetComponent<ShopItemUI>();
        if (shopItemUI == null)
        {
            shopItemUI = gameObject.AddComponent<ShopItemUI>();
        }
        
        // Настраиваем начальные цвета
        if (backgroundImage != null)
        {
            backgroundImage.color = normalBackgroundColor;
        }
        
        if (purchaseButtonImage != null)
        {
            purchaseButtonImage.color = normalButtonColor;
        }
    }
    
    public void SetupReferences()
    {
        // Находим все необходимые компоненты, если они не назначены
        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();
            
        if (iconImage == null)
            iconImage = transform.Find("Icon")?.GetComponent<Image>();
            
        if (titleText == null)
            titleText = transform.Find("Title")?.GetComponent<TextMeshProUGUI>();
            
        if (descriptionText == null)
            descriptionText = transform.Find("Description")?.GetComponent<TextMeshProUGUI>();
            
        if (priceText == null)
            priceText = transform.Find("Price")?.GetComponent<TextMeshProUGUI>();
            
        if (purchaseButton == null)
            purchaseButton = transform.Find("PurchaseButton")?.GetComponent<Button>();
            
        if (purchaseButtonImage == null)
            purchaseButtonImage = purchaseButton?.GetComponent<Image>();
            
        if (purchaseButtonText == null)
            purchaseButtonText = purchaseButton?.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetSelected(bool selected)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = selected ? selectedBackgroundColor : normalBackgroundColor;
        }
    }
    
    public void SetInteractable(bool interactable)
    {
        if (purchaseButton != null)
        {
            purchaseButton.interactable = interactable;
        }
        
        if (purchaseButtonImage != null)
        {
            purchaseButtonImage.color = interactable ? normalButtonColor : disabledButtonColor;
        }
        
        if (purchaseButtonText != null)
        {
            purchaseButtonText.color = interactable ? Color.white : Color.gray;
        }
    }
} 