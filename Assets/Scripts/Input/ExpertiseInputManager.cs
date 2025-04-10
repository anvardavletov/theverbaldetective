using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpertiseInputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private ExpertiseMode expertiseMode;

    private void Start()
    {
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitButtonClicked);
        }

        if (inputField != null)
        {
            inputField.onSubmit.AddListener(OnInputSubmitted);
            inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
    }

    private void OnDestroy()
    {
        if (submitButton != null)
        {
            submitButton.onClick.RemoveListener(OnSubmitButtonClicked);
        }

        if (inputField != null)
        {
            inputField.onSubmit.RemoveListener(OnInputSubmitted);
            inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        }
    }

    private void OnSubmitButtonClicked()
    {
        ProcessInput(inputField.text);
    }

    private void OnInputSubmitted(string input)
    {
        ProcessInput(input);
    }

    private void OnInputValueChanged(string input)
    {
        // Можно добавить дополнительную валидацию при вводе
        submitButton.interactable = !string.IsNullOrEmpty(input);
    }

    private void ProcessInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return;

        var currentField = expertiseMode.GetCurrentField();
        if (currentField != null)
        {
            if (currentField.validWords.Contains(input.ToLower()))
            {
                expertiseMode.ProcessValidWord(input);
                inputField.text = string.Empty;
            }
            else
            {
                expertiseMode.ProcessInvalidWord(input);
            }
        }
    }
} 