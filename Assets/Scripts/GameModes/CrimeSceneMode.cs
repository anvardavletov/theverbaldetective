using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class CrimeSceneMode : MonoBehaviour
{
    [System.Serializable]
    public class EvidenceItem
    {
        public string itemName;
        public string description;
        public List<string> validWords;
        public GameObject visualObject;
        public float timeToDisappear = 60f; // Время в секундах до исчезновения
        public bool isRevealed = false;
        public bool isDiscovered = false;
    }

    [Header("Место преступления")]
    [SerializeField] private Image crimeSceneImage;
    [SerializeField] private List<EvidenceItem> evidenceItems;
    [SerializeField] private float timeLimit = 300f; // 5 минут на уровень
    
    [Header("UI элементы")]
    [SerializeField] private Transform evidenceListContainer;
    [SerializeField] private GameObject evidenceItemPrefab;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private GameObject completionPanel;
    [SerializeField] private TextMeshProUGUI discoveredEvidenceText;
    
    [Header("Эффекты")]
    [SerializeField] private GameObject rainEffect;
    [SerializeField] private GameObject fogEffect;
    [SerializeField] private AudioClip rainSound;
    [SerializeField] private AudioClip thunderSound;
    
    private Dictionary<string, EvidenceItem> evidenceDictionary;
    private float currentTime;
    private bool isCompleted = false;
    private int discoveredEvidenceCount = 0;
    private AudioSource audioSource;
    private List<GameObject> evidenceUIItems = new List<GameObject>();

    private void Start()
    {
        InitializeCrimeScene();
    }

    private void Update()
    {
        if (!isCompleted)
        {
            UpdateTimer();
            CheckEvidenceDisappearance();
        }
    }

    private void InitializeCrimeScene()
    {
        evidenceDictionary = new Dictionary<string, EvidenceItem>();
        currentTime = timeLimit;
        discoveredEvidenceCount = 0;
        evidenceUIItems.Clear();
        
        // Инициализация словаря улик
        foreach (var evidence in evidenceItems)
        {
            evidenceDictionary[evidence.itemName] = evidence;
            evidence.isRevealed = false;
            evidence.isDiscovered = false;
            
            // Скрываем визуальные объекты улик
            if (evidence.visualObject != null)
            {
                evidence.visualObject.SetActive(false);
            }
        }
        
        // Создаем UI элементы для улик
        CreateEvidenceUI();
        
        // Скрываем панель завершения
        if (completionPanel != null)
        {
            completionPanel.SetActive(false);
        }
        
        // Обновляем UI
        UpdateTimerUI();
        UpdateDiscoveredEvidenceUI();
        
        // Настраиваем атмосферные эффекты
        SetupAtmosphere();
    }

    private void CreateEvidenceUI()
    {
        if (evidenceListContainer != null && evidenceItemPrefab != null)
        {
            // Очищаем контейнер
            foreach (Transform child in evidenceListContainer)
            {
                Destroy(child.gameObject);
            }
            
            // Создаем UI элементы для каждой улики
            foreach (var evidence in evidenceItems)
            {
                GameObject itemObj = Instantiate(evidenceItemPrefab, evidenceListContainer);
                TextMeshProUGUI itemText = itemObj.GetComponentInChildren<TextMeshProUGUI>();
                
                if (itemText != null)
                {
                    itemText.text = "???";
                }
                
                evidenceUIItems.Add(itemObj);
            }
        }
    }

    private void SetupAtmosphere()
    {
        // Настраиваем дождь
        if (rainEffect != null)
        {
            rainEffect.SetActive(true);
        }
        
        // Настраиваем туман
        if (fogEffect != null)
        {
            fogEffect.SetActive(true);
        }
        
        // Настраиваем звуки
        audioSource = gameObject.AddComponent<AudioSource>();
        if (rainSound != null)
        {
            audioSource.clip = rainSound;
            audioSource.loop = true;
            audioSource.volume = 0.3f;
            audioSource.Play();
        }
        
        // Случайные раскаты грома
        InvokeRepeating("PlayThunder", 10f, Random.Range(15f, 30f));
    }

    private void PlayThunder()
    {
        if (thunderSound != null)
        {
            AudioSource.PlayClipAtPoint(thunderSound, Camera.main.transform.position, 0.5f);
        }
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        
        if (currentTime <= 0)
        {
            currentTime = 0;
            CompleteCrimeScene(false);
        }
        
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
            
            // Изменяем цвет в зависимости от оставшегося времени
            if (currentTime < timeLimit * 0.3f)
            {
                timerText.color = Color.red;
                timerText.transform.DOShakePosition(0.5f, 5f, 10, 90f);
            }
        }
        
        if (timerSlider != null)
        {
            timerSlider.value = currentTime / timeLimit;
        }
        
        // Обновляем UI в GameManager
        UIManager.Instance.UpdateTimeLimit(currentTime, timeLimit);
    }

    private void CheckEvidenceDisappearance()
    {
        foreach (var evidence in evidenceItems)
        {
            if (!evidence.isDiscovered && !evidence.isRevealed)
            {
                evidence.timeToDisappear -= Time.deltaTime;
                
                if (evidence.timeToDisappear <= 0)
                {
                    // Улика исчезает
                    UIManager.Instance.ShowNotification($"Улика '{evidence.itemName}' исчезла!", "disappear_icon");
                }
            }
        }
    }

    public void ProcessWord(string word)
    {
        if (isCompleted) return;
        
        bool wordMatched = false;
        
        // Проверяем, соответствует ли слово какой-либо улике
        foreach (var evidence in evidenceItems)
        {
            if (!evidence.isDiscovered && evidence.validWords.Contains(word.ToLower()))
            {
                RevealEvidence(evidence);
                wordMatched = true;
                break;
            }
        }
        
        if (wordMatched)
        {
            GameManager.Instance.AddScore(word.Length * 10);
        }
        else
        {
            UIManager.Instance.ShowNotification($"Слово '{word}' не соответствует ни одной улике", "wrong_icon");
        }
        
        // Проверяем, можно ли завершить уровень
        CheckCompletion();
    }

    private void RevealEvidence(EvidenceItem evidence)
    {
        evidence.isRevealed = true;
        evidence.isDiscovered = true;
        discoveredEvidenceCount++;
        
        // Показываем визуальный объект улики
        if (evidence.visualObject != null)
        {
            evidence.visualObject.SetActive(true);
            evidence.visualObject.transform.localScale = Vector3.zero;
            evidence.visualObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
        
        // Обновляем UI
        UpdateEvidenceUI(evidence);
        UpdateDiscoveredEvidenceUI();
        
        // Показываем уведомление
        UIManager.Instance.ShowNotification($"Обнаружена улика: {evidence.itemName}", "evidence_icon");
        
        // Воспроизводим звук обнаружения
        AudioSource.PlayClipAtPoint(thunderSound, Camera.main.transform.position, 0.3f);
    }

    private void UpdateEvidenceUI(EvidenceItem evidence)
    {
        int index = evidenceItems.IndexOf(evidence);
        if (index >= 0 && index < evidenceUIItems.Count)
        {
            TextMeshProUGUI itemText = evidenceUIItems[index].GetComponentInChildren<TextMeshProUGUI>();
            if (itemText != null)
            {
                itemText.text = evidence.itemName;
                itemText.color = Color.green;
            }
        }
    }

    private void UpdateDiscoveredEvidenceUI()
    {
        if (discoveredEvidenceText != null)
        {
            discoveredEvidenceText.text = $"Обнаружено улик: {discoveredEvidenceCount}/{evidenceItems.Count}";
        }
    }

    private void CheckCompletion()
    {
        // Проверяем, можно ли завершить уровень
        if (discoveredEvidenceCount >= evidenceItems.Count)
        {
            CompleteCrimeScene(true);
        }
    }

    private void CompleteCrimeScene(bool success)
    {
        isCompleted = true;
        
        // Останавливаем таймер
        CancelInvoke("PlayThunder");
        
        // Показываем панель завершения
        if (completionPanel != null)
        {
            completionPanel.SetActive(true);
            completionPanel.transform.localScale = Vector3.zero;
            completionPanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        }
        
        if (success)
        {
            // Успешное завершение
            GameManager.Instance.AddScore(1000);
            
            // Добавляем награду
            string rewardName = $"Сувенир с места преступления #{Random.Range(1, 100)}";
            GameManager.Instance.AddCollectedItem(rewardName);
            
            // Показываем уведомление о завершении
            UIManager.Instance.ShowNotification("Место преступления исследовано! Все улики найдены.", "complete_icon");
        }
        else
        {
            // Неудачное завершение
            UIManager.Instance.ShowNotification("Время вышло! Не все улики были найдены.", "timeout_icon");
        }
    }
} 