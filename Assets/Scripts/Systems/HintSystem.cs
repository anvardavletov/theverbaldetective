using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class HintSystem : MonoBehaviour
{
    [Header("Настройки подсказок")]
    [SerializeField] private int maxHintsPerLevel = 3;
    [SerializeField] private float hintCooldown = 30f;
    
    [Header("UI элементы")]
    [SerializeField] private TextMeshProUGUI hintsRemainingText;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private GameObject hintPanel;
    
    private Dictionary<string, List<string>> levelHints;
    private int currentHintsRemaining;
    private float lastHintTime;
    private string currentLevel;
    
    private void Start()
    {
        InitializeHints();
    }
    
    private void InitializeHints()
    {
        levelHints = new Dictionary<string, List<string>>();
        currentHintsRemaining = maxHintsPerLevel;
        lastHintTime = -hintCooldown;
        
        // Загрузка подсказок для каждого уровня
        LoadHintsForLevel("crime_scene", new List<string> {
            "Обратите внимание на следы на полу",
            "Проверьте окна на наличие следов взлома",
            "Изучите предметы на столе"
        });
        
        LoadHintsForLevel("expertise", new List<string> {
            "Используйте специальные термины",
            "Обратите внимание на детали",
            "Проверьте связи между предметами"
        });
        
        UpdateUI();
    }
    
    private void LoadHintsForLevel(string levelId, List<string> hints)
    {
        levelHints[levelId] = new List<string>(hints);
    }
    
    public bool CanUseHint()
    {
        return currentHintsRemaining > 0 && Time.time - lastHintTime >= hintCooldown;
    }
    
    public string GetHint()
    {
        if (!CanUseHint() || !levelHints.ContainsKey(currentLevel))
            return null;
            
        var hints = levelHints[currentLevel];
        if (hints.Count == 0)
            return null;
            
        int hintIndex = Random.Range(0, hints.Count);
        string hint = hints[hintIndex];
        hints.RemoveAt(hintIndex);
        
        currentHintsRemaining--;
        lastHintTime = Time.time;
        
        UpdateUI();
        ShowHint(hint);
        
        return hint;
    }
    
    private void ShowHint(string hint)
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(true);
            if (hintText != null)
            {
                hintText.text = hint;
            }
        }
    }
    
    public void SetCurrentLevel(string levelId)
    {
        currentLevel = levelId;
        currentHintsRemaining = maxHintsPerLevel;
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (hintsRemainingText != null)
        {
            hintsRemainingText.text = $"Подсказки: {currentHintsRemaining}/{maxHintsPerLevel}";
        }
    }
    
    public void HideHintPanel()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }
    }
} 