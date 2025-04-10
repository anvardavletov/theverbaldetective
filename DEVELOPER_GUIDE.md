# Техническая документация "Словесный Детектив"

## Содержание
1. [Архитектура проекта](#архитектура-проекта)
2. [Основные системы](#основные-системы)
3. [Игровые режимы](#игровые-режимы)
4. [UI система](#ui-система)
5. [Локализация](#локализация)
6. [Монетизация](#монетизация)
7. [Оптимизация](#оптимизация)
8. [Тестирование](#тестирование)

## Архитектура проекта

### Структура папок
```
Assets/
├── Scripts/
│   ├── Core/
│   ├── GameModes/
│   ├── Systems/
│   ├── UI/
│   └── Utils/
├── Resources/
│   ├── Localization/
│   ├── Prefabs/
│   └── Configs/
├── Scenes/
└── Tests/
```

### Основные компоненты
- **GameManager** - управление игровым процессом
- **UIManager** - управление интерфейсом
- **SaveSystem** - система сохранений
- **LocalizationSystem** - система локализации
- **AdSystem** - система рекламы
- **IAPSystem** - система внутриигровых покупок

## Основные системы

### GameManager
```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameState CurrentState { get; private set; }
    public GameMode CurrentMode { get; private set; }
    
    public void StartGame(GameMode mode)
    public void PauseGame()
    public void ResumeGame()
    public void EndGame()
}
```

### SaveSystem
```csharp
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    
    public void SaveGame(GameData data)
    public GameData LoadGame()
    public void DeleteSave()
}
```

### LocalizationSystem
```csharp
public class LocalizationSystem : MonoBehaviour
{
    public static LocalizationSystem Instance { get; private set; }
    
    public string CurrentLanguage { get; private set; }
    public void SetLanguage(string language)
    public string GetLocalizedText(string key)
}
```

## Игровые режимы

### MysteryStoryMode
```csharp
public class MysteryStoryMode : MonoBehaviour
{
    [SerializeField] private MysteryStoryConfig config;
    [SerializeField] private UIManager uiManager;
    
    private void InitializeGame()
    private void ProcessChoice(string choice)
    private void UpdateStory()
}
```

### PhotofitMode
```csharp
public class PhotofitMode : MonoBehaviour
{
    [SerializeField] private PhotofitConfig config;
    [SerializeField] private UIManager uiManager;
    
    private void InitializeGame()
    private void ProcessWord(string word)
    private void UpdatePortrait()
}
```

### CrimeSceneMode
```csharp
public class CrimeSceneMode : MonoBehaviour
{
    [SerializeField] private CrimeSceneConfig config;
    [SerializeField] private UIManager uiManager;
    
    private void InitializeGame()
    private void ProcessEvidence(string evidence)
    private void UpdateScene()
}
```

### ExpertiseMode
```csharp
public class ExpertiseMode : MonoBehaviour
{
    [SerializeField] private ExpertiseConfig config;
    [SerializeField] private UIManager uiManager;
    
    private void InitializeGame()
    private void ProcessField(string field)
    private void UpdateReport()
}
```

## UI система

### Основные компоненты
- **MainMenuUI** - главное меню
- **GameplayUI** - игровой интерфейс
- **SettingsUI** - меню настроек
- **ShopUI** - магазин
- **TutorialUI** - обучение

### Пример реализации
```csharp
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingsButton;
    
    private void InitializeUI()
    private void OnPlayClicked()
    private void OnShopClicked()
    private void OnSettingsClicked()
}
```

## Локализация

### Структура файлов
```
Resources/Localization/
├── ru.json
└── en.json
```

### Формат JSON
```json
{
    "common": {
        "play": "Играть",
        "shop": "Магазин",
        "settings": "Настройки"
    },
    "modes": {
        "mystery_story": "Загадочная История",
        "photofit": "Фоторобот",
        "crime_scene": "Место Преступления",
        "expertise": "Экспертиза"
    }
}
```

## Монетизация

### AdSystem
```csharp
public class AdSystem : MonoBehaviour
{
    public static AdSystem Instance { get; private set; }
    
    public void ShowBannerAd()
    public void ShowInterstitialAd()
    public void ShowRewardedAd()
}
```

### IAPSystem
```csharp
public class IAPSystem : MonoBehaviour, IStoreListener
{
    public static IAPSystem Instance { get; private set; }
    
    public void InitializePurchasing()
    public void BuyProduct(string productId)
    public void RestorePurchases()
}
```

## Оптимизация

### Рекомендации
1. Использовать пулинг объектов
2. Кэшировать компоненты
3. Оптимизировать UI
4. Использовать LOD для моделей
5. Оптимизировать текстуры

### Пример пулинга
```csharp
public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    
    public GameObject GetObject()
    public void ReturnObject(GameObject obj)
}
```

## Тестирование

### Unit тесты
```csharp
public class GameManagerTests
{
    [Test]
    public void StartGame_ValidMode_StateChanged()
    {
        // Arrange
        var gameManager = new GameManager();
        
        // Act
        gameManager.StartGame(GameMode.MysteryStory);
        
        // Assert
        Assert.AreEqual(GameState.Playing, gameManager.CurrentState);
    }
}
```

### PlayMode тесты
```csharp
public class GameplayTests
{
    [UnityTest]
    public IEnumerator ProcessWord_ValidWord_ScoreIncreased()
    {
        // Arrange
        var gameplay = new GameObject().AddComponent<GameplayManager>();
        
        // Act
        gameplay.ProcessWord("test");
        yield return null;
        
        // Assert
        Assert.Greater(gameplay.CurrentScore, 0);
    }
}
```

## Сборка проекта

### Настройки сборки
```csharp
public class BuildSettings : MonoBehaviour
{
    public void BuildAndroid()
    public void BuildIOS()
    public void BuildWindows()
    public void BuildMacOS()
    public void BuildLinux()
}
```

### Параметры сборки
- **Android**: 
  - Minimum API Level: 21
  - Target API Level: 30
  - Build System: Gradle
- **iOS**:
  - Target iOS Version: 13.0
  - Architecture: ARM64
- **Windows**:
  - Target Platform: x86_64
  - Graphics API: DirectX 11
- **macOS**:
  - Target macOS Version: 10.14
  - Architecture: x86_64
- **Linux**:
  - Target Platform: x86_64
  - Graphics API: OpenGL 