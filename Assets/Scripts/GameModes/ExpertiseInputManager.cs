using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpertiseInputManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField wordInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button hintButton;

    [Header("Settings")]
    [SerializeField] private float minWordLength = 3;
    [SerializeField] private float maxWordLength = 20;

    private ExpertiseMode expertiseMode;

    private void Start()
    {
        expertiseMode = GetComponent<ExpertiseMode>();
        InitializeUI();
    }

    private void InitializeUI()
    {
        if (wordInput != null)
        {
            wordInput.onValueChanged.AddListener(OnInputValueChanged);
            wordInput.onSubmit.AddListener(OnSubmit);
        }

        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmit);
            submitButton.interactable = false;
        }

        if (hintButton != null)
        {
            hintButton.onClick.AddListener(OnHintButtonClicked);
        }
    }

    private void OnInputValueChanged(string value)
    {
        if (submitButton != null)
        {
            submitButton.interactable = IsValidInput(value);
        }
    }

    private void OnSubmit(string value)
    {
        if (IsValidInput(value))
        {
            expertiseMode.ProcessWord(value);
            wordInput.text = "";
            wordInput.ActivateInputField();
        }
    }

    private void OnHintButtonClicked()
    {
        expertiseMode.ShowHint();
    }

    private bool IsValidInput(string input)
    {
        return !string.IsNullOrEmpty(input) && 
               input.Length >= minWordLength && 
               input.Length <= maxWordLength;
    }

    private void OnDestroy()
    {
        if (wordInput != null)
        {
            wordInput.onValueChanged.RemoveListener(OnInputValueChanged);
            wordInput.onSubmit.RemoveListener(OnSubmit);
        }

        if (submitButton != null)
        {
            submitButton.onClick.RemoveListener(OnSubmit);
        }

        if (hintButton != null)
        {
            hintButton.onClick.RemoveListener(OnHintButtonClicked);
        }
    }
} 