using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WordManager : MonoBehaviour
{
    public static WordManager Instance { get; private set; }

    [SerializeField] private TextAsset dictionaryFile; // Файл со словарем
    private HashSet<string> validWords;
    private List<char> availableLetters;
    private string currentSentence;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionary()
    {
        validWords = new HashSet<string>();
        if (dictionaryFile != null)
        {
            string[] words = dictionaryFile.text.Split('\n');
            foreach (string word in words)
            {
                validWords.Add(word.Trim().ToLower());
            }
        }
    }

    public void SetCurrentSentence(string sentence)
    {
        currentSentence = sentence.ToLower();
        availableLetters = currentSentence.Where(c => char.IsLetter(c)).ToList();
    }

    public bool IsValidWord(string word)
    {
        if (string.IsNullOrEmpty(word)) return false;
        
        word = word.ToLower();
        if (!validWords.Contains(word)) return false;

        // Проверка, что все буквы слова доступны в текущем предложении
        var wordLetters = word.ToList();
        var availableLettersCopy = new List<char>(availableLetters);

        foreach (char letter in wordLetters)
        {
            int index = availableLettersCopy.IndexOf(letter);
            if (index == -1) return false;
            availableLettersCopy.RemoveAt(index);
        }

        return true;
    }

    public List<char> GetAvailableLetters()
    {
        return new List<char>(availableLetters);
    }

    public void RemoveUsedLetters(string word)
    {
        foreach (char letter in word)
        {
            availableLetters.Remove(letter);
        }
    }

    public void ResetAvailableLetters()
    {
        availableLetters = currentSentence.Where(c => char.IsLetter(c)).ToList();
    }

    public bool AreAllLettersUsed()
    {
        return availableLetters.Count == 0;
    }
} 