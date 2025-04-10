using UnityEngine;

namespace WordDetective.Core
{
    /// <summary>
    /// Базовый класс для конфигурации игровых режимов
    /// </summary>
    public abstract class GameModeConfig : ScriptableObject
    {
        [Header("Общие настройки")]
        [Tooltip("Название режима")]
        public string ModeName;
        
        [Tooltip("Описание режима")]
        [TextArea(3, 10)]
        public string Description;
        
        [Tooltip("Иконка режима")]
        public Sprite Icon;
        
        [Header("Игровые настройки")]
        [Tooltip("Начальное время в секундах")]
        public float InitialTime = 300f;
        
        [Tooltip("Минимальная длина слова")]
        public int MinWordLength = 3;
        
        [Tooltip("Максимальная длина слова")]
        public int MaxWordLength = 15;
        
        [Tooltip("Очки за правильное слово")]
        public int PointsPerWord = 10;
        
        [Tooltip("Штраф за неправильное слово")]
        public int PenaltyPerWord = 5;
        
        [Header("Настройки подсказок")]
        [Tooltip("Количество доступных подсказок")]
        public int AvailableHints = 3;
        
        [Tooltip("Стоимость подсказки в монетах")]
        public int HintCost = 50;
        
        [Header("Настройки наград")]
        [Tooltip("Монеты за завершение уровня")]
        public int CoinsForCompletion = 100;
        
        [Tooltip("Монеты за достижение высокого счета")]
        public int CoinsForHighScore = 200;
        
        [Tooltip("Порог для высокого счета")]
        public int HighScoreThreshold = 500;
    }
} 