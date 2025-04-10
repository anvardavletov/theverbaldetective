using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [Header("Настройки звука")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    
    [Header("Настройки графики")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    
    [Header("Настройки игры")]
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private Toggle tutorialToggle;
    
    [Header("Язык")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    
    private void Start()
    {
        InitializeUI();
        LoadSettings();
    }
    
    private void InitializeUI()
    {
        // Инициализация слайдеров звука
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
        
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
        
        // Инициализация переключателей звука
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        }
        
        if (sfxToggle != null)
        {
            sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
        }
        
        // Инициализация настроек графики
        if (qualityDropdown != null)
        {
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(QualitySettings.names.ToList());
            qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        }
        
        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        }
        
        // Инициализация настроек игры
        if (difficultyDropdown != null)
        {
            difficultyDropdown.ClearOptions();
            difficultyDropdown.AddOptions(new List<string> { "Легкий", "Средний", "Сложный" });
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        }
        
        if (tutorialToggle != null)
        {
            tutorialToggle.onValueChanged.AddListener(OnTutorialChanged);
        }
        
        // Инициализация выбора языка
        if (languageDropdown != null)
        {
            languageDropdown.ClearOptions();
            languageDropdown.AddOptions(LocalizationSystem.Instance.GetAvailableLanguages());
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }
    }
    
    private void LoadSettings()
    {
        // Загрузка настроек звука
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = AudioSystem.Instance.GetMusicVolume();
            
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = AudioSystem.Instance.GetSFXVolume();
            
        if (musicToggle != null)
            musicToggle.isOn = AudioSystem.Instance.IsMusicEnabled();
            
        if (sfxToggle != null)
            sfxToggle.isOn = AudioSystem.Instance.IsSFXEnabled();
            
        // Загрузка настроек графики
        if (qualityDropdown != null)
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;
            
        // Загрузка настроек игры
        if (difficultyDropdown != null)
            difficultyDropdown.value = GameManager.Instance.GetDifficultyLevel();
            
        if (tutorialToggle != null)
            tutorialToggle.isOn = GameManager.Instance.IsTutorialEnabled();
            
        // Загрузка языка
        if (languageDropdown != null)
            languageDropdown.value = LocalizationSystem.Instance.GetCurrentLanguageIndex();
    }
    
    private void OnMusicVolumeChanged(float value)
    {
        AudioSystem.Instance.SetMusicVolume(value);
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnSFXVolumeChanged(float value)
    {
        AudioSystem.Instance.SetSFXVolume(value);
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnMusicToggleChanged(bool value)
    {
        AudioSystem.Instance.SetMusicEnabled(value);
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnSFXToggleChanged(bool value)
    {
        AudioSystem.Instance.SetSFXEnabled(value);
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnQualityChanged(int index)
    {
        QualitySettings.SetQualityLevel(index);
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnFullscreenChanged(bool value)
    {
        Screen.fullScreen = value;
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnDifficultyChanged(int index)
    {
        GameManager.Instance.SetDifficultyLevel(index);
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnTutorialChanged(bool value)
    {
        GameManager.Instance.SetTutorialEnabled(value);
        SaveSystem.Instance.SaveSettings();
    }
    
    private void OnLanguageChanged(int index)
    {
        LocalizationSystem.Instance.SetLanguage(index);
        SaveSystem.Instance.SaveSettings();
    }
} 