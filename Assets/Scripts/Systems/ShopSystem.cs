using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
    public string id;
    public string title;
    public string description;
    public Sprite icon;
    public int price;
    public ItemType type;
    public int amount;
}

public enum ItemType
{
    Coins,
    Hints,
    PowerUp,
    Theme,
    RemoveAds
}

public class ShopSystem : MonoBehaviour
{
    [Header("Настройки магазина")]
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Transform shopContent;
    
    [Header("UI элементы")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI hintsText;
    [SerializeField] private Button closeButton;
    
    private Dictionary<string, ShopItem> itemsDict;
    private static ShopSystem instance;
    
    public static ShopSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShopSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("ShopSystem");
                    instance = go.AddComponent<ShopSystem>();
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeShop();
    }
    
    private void InitializeShop()
    {
        itemsDict = new Dictionary<string, ShopItem>();
        foreach (var item in shopItems)
        {
            itemsDict[item.id] = item;
        }
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideShop);
        }
        
        UpdateUI();
    }
    
    public void ShowShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
            PopulateShop();
            UpdateUI();
        }
    }
    
    public void HideShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }
    
    private void PopulateShop()
    {
        if (shopContent == null || shopItemPrefab == null)
            return;
            
        // Очистка существующих элементов
        foreach (Transform child in shopContent)
        {
            Destroy(child.gameObject);
        }
        
        // Создание новых элементов
        foreach (var item in shopItems)
        {
            GameObject itemObj = Instantiate(shopItemPrefab, shopContent);
            ShopItemUI itemUI = itemObj.GetComponent<ShopItemUI>();
            
            if (itemUI != null)
            {
                itemUI.Initialize(item, OnItemPurchased);
            }
        }
    }
    
    private void OnItemPurchased(ShopItem item)
    {
        if (GameManager.Instance.GetCoins() >= item.price)
        {
            GameManager.Instance.AddCoins(-item.price);
            
            switch (item.type)
            {
                case ItemType.Coins:
                    GameManager.Instance.AddCoins(item.amount);
                    break;
                case ItemType.Hints:
                    GameManager.Instance.AddHints(item.amount);
                    break;
                case ItemType.PowerUp:
                    GameManager.Instance.AddPowerUp(item.id, item.amount);
                    break;
                case ItemType.Theme:
                    GameManager.Instance.UnlockTheme(item.id);
                    break;
                case ItemType.RemoveAds:
                    GameManager.Instance.SetAdsEnabled(false);
                    break;
            }
            
            // Воспроизведение эффекта покупки
            AudioSystem.Instance.PlaySound("purchase_success");
            VisualEffectSystem.Instance.PlayPurchaseEffect();
            
            UpdateUI();
        }
        else
        {
            // Недостаточно монет
            AudioSystem.Instance.PlaySound("purchase_failed");
            VisualEffectSystem.Instance.ShakeTransform(coinsText.transform);
        }
    }
    
    private void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = $"Монеты: {GameManager.Instance.GetCoins()}";
        }
        
        if (hintsText != null)
        {
            hintsText.text = $"Подсказки: {GameManager.Instance.GetHints()}";
        }
    }
}

// Компонент UI для элемента магазина
public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button purchaseButton;
    
    private ShopItem item;
    private System.Action<ShopItem> onPurchaseCallback;
    
    public void Initialize(ShopItem shopItem, System.Action<ShopItem> callback)
    {
        item = shopItem;
        onPurchaseCallback = callback;
        
        if (iconImage != null && item.icon != null)
        {
            iconImage.sprite = item.icon;
        }
        
        if (titleText != null)
        {
            titleText.text = item.title;
        }
        
        if (descriptionText != null)
        {
            descriptionText.text = item.description;
        }
        
        if (priceText != null)
        {
            priceText.text = $"{item.price} монет";
        }
        
        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(OnPurchaseClicked);
        }
    }
    
    private void OnPurchaseClicked()
    {
        onPurchaseCallback?.Invoke(item);
    }
} 