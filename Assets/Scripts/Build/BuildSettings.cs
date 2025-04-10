using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildSettings : MonoBehaviour
{
    [Header("Настройки сборки")]
    public string productName = "Словесный Детектив";
    public string companyName = "Your Company";
    public string version = "1.0.0";
    public string bundleIdentifier = "com.yourcompany.worddetective";
    
    [Header("Настройки платформ")]
    public bool buildAndroid = true;
    public bool buildIOS = true;
    public bool buildWindows = true;
    public bool buildMacOS = true;
    public bool buildLinux = true;
    
    [Header("Настройки оптимизации")]
    public bool enableIL2CPP = true;
    public bool enableStripEngineCode = true;
    public bool enableStripManagedCode = true;
    public bool enableDevelopmentBuild = false;
    
    [Header("Настройки рекламы")]
    public bool enableAdMob = true;
    public string adMobAppId = "ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy";
    public string adMobBannerId = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
    public string adMobInterstitialId = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
    public string adMobRewardedId = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyy";
    
    [Header("Настройки IAP")]
    public bool enableIAP = true;
    public string[] productIds = new string[]
    {
        "coins_small",
        "coins_medium",
        "coins_large",
        "hints_small",
        "hints_medium",
        "hints_large",
        "remove_ads"
    };
    
    [Header("Настройки аналитики")]
    public bool enableAnalytics = true;
    public string analyticsKey = "your-analytics-key";
    
    [Header("Настройки социальных сетей")]
    public bool enableSocialSharing = true;
    public string facebookAppId = "your-facebook-app-id";
    public string twitterApiKey = "your-twitter-api-key";
    
    [Header("Настройки безопасности")]
    public bool enableEncryption = true;
    public string encryptionKey = "your-encryption-key";
    public bool enableAntiCheat = true;
    
    [Header("Настройки локализации")]
    public string[] supportedLanguages = new string[]
    {
        "ru",
        "en"
    };
    
    [Header("Настройки тестирования")]
    public bool enableTestMode = false;
    public bool enableDebugLog = false;
    public bool enablePerformanceProfiling = false;
    
    private void Start()
    {
        if (Application.isEditor)
        {
            Debug.Log("BuildSettings is running in editor mode");
            return;
        }
        
        ApplySettings();
    }
    
    private void ApplySettings()
    {
        // Применяем основные настройки
        Application.productName = productName;
        Application.companyName = companyName;
        Application.version = version;
        
        // Применяем настройки рекламы
        if (enableAdMob)
        {
            // Инициализация AdMob
            AdSystem.Instance.Initialize(adMobAppId);
        }
        
        // Применяем настройки IAP
        if (enableIAP)
        {
            // Инициализация IAP
            IAPSystem.Instance.InitializePurchasing();
        }
        
        // Применяем настройки аналитики
        if (enableAnalytics)
        {
            // Инициализация аналитики
            // Analytics.Initialize(analyticsKey);
        }
        
        // Применяем настройки социальных сетей
        if (enableSocialSharing)
        {
            // Инициализация социальных сетей
            // Facebook.Initialize(facebookAppId);
            // Twitter.Initialize(twitterApiKey);
        }
        
        // Применяем настройки безопасности
        if (enableEncryption)
        {
            // Инициализация шифрования
            // Encryption.Initialize(encryptionKey);
        }
        
        // Применяем настройки локализации
        LocalizationSystem.Instance.SetAvailableLanguages(supportedLanguages);
        
        // Применяем настройки тестирования
        if (enableTestMode)
        {
            DebugSystem.Instance.EnableTestMode();
        }
        
        if (enableDebugLog)
        {
            DebugSystem.Instance.EnableDebugLog();
        }
        
        if (enablePerformanceProfiling)
        {
            OptimizationSystem.Instance.EnableProfiling();
        }
    }
    
    #if UNITY_EDITOR
    [MenuItem("Словесный Детектив/Настройки сборки")]
    public static void ShowBuildSettings()
    {
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Scripts/Build/BuildSettings.asset");
    }
    
    [MenuItem("Словесный Детектив/Собрать для Android")]
    public static void BuildAndroid()
    {
        BuildSettings settings = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Scripts/Build/BuildSettings.asset");
        if (settings != null && settings.buildAndroid)
        {
            BuildPipeline.BuildPlayer(
                GetScenePaths(),
                "Build/Android/WordDetective.apk",
                BuildTarget.Android,
                GetBuildOptions()
            );
        }
    }
    
    [MenuItem("Словесный Детектив/Собрать для iOS")]
    public static void BuildIOS()
    {
        BuildSettings settings = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Scripts/Build/BuildSettings.asset");
        if (settings != null && settings.buildIOS)
        {
            BuildPipeline.BuildPlayer(
                GetScenePaths(),
                "Build/iOS",
                BuildTarget.iOS,
                GetBuildOptions()
            );
        }
    }
    
    [MenuItem("Словесный Детектив/Собрать для Windows")]
    public static void BuildWindows()
    {
        BuildSettings settings = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Scripts/Build/BuildSettings.asset");
        if (settings != null && settings.buildWindows)
        {
            BuildPipeline.BuildPlayer(
                GetScenePaths(),
                "Build/Windows/WordDetective.exe",
                BuildTarget.StandaloneWindows64,
                GetBuildOptions()
            );
        }
    }
    
    [MenuItem("Словесный Детектив/Собрать для macOS")]
    public static void BuildMacOS()
    {
        BuildSettings settings = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Scripts/Build/BuildSettings.asset");
        if (settings != null && settings.buildMacOS)
        {
            BuildPipeline.BuildPlayer(
                GetScenePaths(),
                "Build/macOS/WordDetective.app",
                BuildTarget.StandaloneOSX,
                GetBuildOptions()
            );
        }
    }
    
    [MenuItem("Словесный Детектив/Собрать для Linux")]
    public static void BuildLinux()
    {
        BuildSettings settings = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Scripts/Build/BuildSettings.asset");
        if (settings != null && settings.buildLinux)
        {
            BuildPipeline.BuildPlayer(
                GetScenePaths(),
                "Build/Linux/WordDetective",
                BuildTarget.StandaloneLinux64,
                GetBuildOptions()
            );
        }
    }
    
    private static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }
    
    private static BuildOptions GetBuildOptions()
    {
        BuildSettings settings = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Scripts/Build/BuildSettings.asset");
        BuildOptions options = BuildOptions.None;
        
        if (settings.enableDevelopmentBuild)
            options |= BuildOptions.Development;
            
        if (settings.enableIL2CPP)
            options |= BuildOptions.IL2CPP;
            
        return options;
    }
    #endif
} 