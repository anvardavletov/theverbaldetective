using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using TMPro;

[System.Serializable]
public class IAPProduct
{
    public string id;
    public string title;
    public string description;
    public string price;
    public ProductType type;
}

public class IAPSystem : MonoBehaviour, IStoreListener
{
    [Header("Настройки IAP")]
    [SerializeField] private bool enableIAP = true;
    [SerializeField] private List<IAPProduct> products;
    
    [Header("UI элементы")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI hintsText;
    
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;
    private static IAPSystem instance;
    
    public static IAPSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<IAPSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("IAPSystem");
                    instance = go.AddComponent<IAPSystem>();
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
        
        if (enableIAP)
        {
            InitializePurchasing();
        }
    }
    
    private void InitializePurchasing()
    {
        if (IsInitialized())
            return;
            
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        
        foreach (var product in products)
        {
            builder.AddProduct(product.id, product.type);
        }
        
        UnityPurchasing.Initialize(this, builder);
    }
    
    private bool IsInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }
    
    public void BuyProduct(string productId)
    {
        if (!IsInitialized())
        {
            Debug.LogError("Система покупок не инициализирована");
            return;
        }
        
        Product product = storeController.products.WithID(productId);
        
        if (product != null && product.availableToPurchase)
        {
            Debug.Log($"Покупка продукта: {product.definition.id}");
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.LogError("Продукт недоступен для покупки");
        }
    }
    
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
        Debug.Log("Система покупок инициализирована");
        
        // Обновление UI
        UpdateUI();
    }
    
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Ошибка инициализации системы покупок: {error}");
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"Обработка покупки: {args.purchasedProduct.definition.id}");
        
        // Обработка различных типов покупок
        switch (args.purchasedProduct.definition.id)
        {
            case "coins_small":
                GameManager.Instance.AddCoins(100);
                break;
            case "coins_medium":
                GameManager.Instance.AddCoins(500);
                break;
            case "coins_large":
                GameManager.Instance.AddCoins(1000);
                break;
            case "hints_pack":
                GameManager.Instance.AddHints(5);
                break;
            case "remove_ads":
                GameManager.Instance.SetAdsEnabled(false);
                break;
        }
        
        UpdateUI();
        return PurchaseProcessingResult.Complete;
    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Ошибка покупки {product.definition.id}: {failureReason}");
    }
    
    public void ShowShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
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
    
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.LogError("Система покупок не инициализирована");
            return;
        }
        
        if (Application.platform == RuntimePlatform.IPhonePlayer || 
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("Восстановление покупок...");
            var appleExtensions = storeExtensionProvider.GetExtension<IAppleExtensions>();
            appleExtensions.RestoreTransactions((result) => {
                Debug.Log($"Результат восстановления: {result}");
            });
        }
        else
        {
            Debug.Log("Восстановление покупок не поддерживается на этой платформе");
        }
    }
} 