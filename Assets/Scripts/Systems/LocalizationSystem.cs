using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class LocalizationSystem : MonoBehaviour
{
    [System.Serializable]
    public class LanguageData
    {
        public string languageCode;
        public string languageName;
        public TextAsset localizationFile;
    }
    
    [Header("Настройки локализации")]
    [SerializeField] private List<LanguageData> availableLanguages;
    [SerializeField] private string defaultLanguage = "ru";
    
    private Dictionary<string, Dictionary<string, string>> localizationData;
    private string currentLanguage;
    private static LocalizationSystem instance;
    
    public static LocalizationSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LocalizationSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("LocalizationSystem");
                    instance = go.AddComponent<LocalizationSystem>();
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
        
        InitializeLocalization();
    }
    
    private void InitializeLocalization()
    {
        localizationData = new Dictionary<string, Dictionary<string, string>>();
        
        foreach (var language in availableLanguages)
        {
            LoadLanguageData(language.languageCode, language.localizationFile);
        }
        
        // Загрузка сохраненного языка или использование языка по умолчанию
        currentLanguage = PlayerPrefs.GetString("Language", defaultLanguage);
        if (!localizationData.ContainsKey(currentLanguage))
        {
            currentLanguage = defaultLanguage;
        }
    }
    
    private void LoadLanguageData(string languageCode, TextAsset localizationFile)
    {
        if (localizationFile == null)
            return;
            
        Dictionary<string, string> languageDict = new Dictionary<string, string>();
        string[] lines = localizationFile.text.Split('\n');
        
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                continue;
                
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                string key = parts[0].Trim();
                string value = parts[1].Trim();
                languageDict[key] = value;
            }
        }
        
        localizationData[languageCode] = languageDict;
    }
    
    public string GetLocalizedText(string key)
    {
        if (localizationData.TryGetValue(currentLanguage, out Dictionary<string, string> languageDict))
        {
            if (languageDict.TryGetValue(key, out string value))
            {
                return value;
            }
        }
        
        return key; // Возвращаем ключ, если перевод не найден
    }
    
    public void SetLanguage(string languageCode)
    {
        if (localizationData.ContainsKey(languageCode))
        {
            currentLanguage = languageCode;
            PlayerPrefs.SetString("Language", languageCode);
            PlayerPrefs.Save();
            
            // Обновление всех текстовых компонентов
            UpdateAllTextComponents();
        }
    }
    
    public List<string> GetAvailableLanguages()
    {
        List<string> languages = new List<string>();
        foreach (var language in availableLanguages)
        {
            languages.Add(language.languageCode);
        }
        return languages;
    }
    
    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }
    
    private void UpdateAllTextComponents()
    {
        // Обновление всех TMP_Text компонентов с тегом LocalizedText
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            LocalizedText localizedText = text.GetComponent<LocalizedText>();
            if (localizedText != null)
            {
                localizedText.UpdateText();
            }
        }
    }
}

// Компонент для автоматического обновления текста
public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string textKey;
    private TMP_Text textComponent;
    
    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        if (textComponent == null)
        {
            textComponent = GetComponentInChildren<TMP_Text>();
        }
    }
    
    private void Start()
    {
        UpdateText();
    }
    
    public void UpdateText()
    {
        if (textComponent != null && !string.IsNullOrEmpty(textKey))
        {
            textComponent.text = LocalizationSystem.Instance.GetLocalizedText(textKey);
        }
    }
} 