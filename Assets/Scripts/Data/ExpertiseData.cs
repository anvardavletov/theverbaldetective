using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpertiseData
{
    [System.Serializable]
    public class ExpertiseFieldData
    {
        public string fieldName;
        public string description;
        public List<string> validWords;
        public List<string> hints;
        public Sprite fieldIcon;
        public int requiredWords;
        public float timeLimit;
    }

    public List<ExpertiseFieldData> expertiseFields = new List<ExpertiseFieldData>();

    [Header("Expert Titles")]
    public List<string> expertTitles = new List<string>
    {
        "Начинающий эксперт",
        "Опытный эксперт",
        "Ведущий эксперт",
        "Главный эксперт",
        "Легендарный эксперт"
    };

    [Header("Rewards")]
    public List<string> rewards = new List<string>
    {
        "Бронзовая медаль",
        "Серебряная медаль",
        "Золотая медаль",
        "Платиновая медаль",
        "Алмазная медаль"
    };

    private System.Random random = new System.Random();

    public ExpertiseFieldData GetRandomField()
    {
        if (expertiseFields.Count == 0)
            return null;
            
        int index = random.Next(expertiseFields.Count);
        return expertiseFields[index];
    }

    public string GetExpertTitle(int completedFields)
    {
        int titleIndex = Mathf.Min(completedFields / 2, expertTitles.Count - 1);
        return expertTitles[titleIndex];
    }

    public string GetReward(int completedFields)
    {
        int rewardIndex = Mathf.Min(completedFields / 2, rewards.Count - 1);
        return rewards[rewardIndex];
    }

    public bool IsWordValid(string word, ExpertiseFieldData field)
    {
        if (field == null || field.validWords == null)
            return false;
            
        return field.validWords.Contains(word.ToLower());
    }

    public List<string> GetAvailableHints(ExpertiseFieldData field)
    {
        if (field == null || field.hints == null)
            return new List<string>();
            
        return new List<string>(field.hints);
    }
} 