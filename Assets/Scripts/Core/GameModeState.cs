using UnityEngine;
using System;

namespace WordDetective.Core
{
    /// <summary>
    /// Абстрактный класс для управления состояниями игровых режимов
    /// </summary>
    public abstract class GameModeState : MonoBehaviour
    {
        [SerializeField] protected GameModeConfig config;
        
        // События для уведомления о состоянии
        public event Action<float> OnTimerUpdated;
        public event Action<int> OnScoreUpdated;
        public event Action OnGameCompleted;
        public event Action OnGameFailed;
        public event Action OnGamePaused;
        public event Action OnGameResumed;
        
        // Текущее состояние игры
        protected bool isPaused = false;
        protected bool isCompleted = false;
        protected bool isFailed = false;
        
        // Таймер и счет
        protected float currentTime;
        protected int currentScore;
        
        /// <summary>
        /// Инициализация игрового режима
        /// </summary>
        public virtual void Initialize()
        {
            currentTime = config.InitialTime;
            currentScore = 0;
            isPaused = false;
            isCompleted = false;
            isFailed = false;
            
            // Дополнительная инициализация в дочерних классах
        }
        
        /// <summary>
        /// Обновление состояния игры
        /// </summary>
        public virtual void UpdateState()
        {
            if (isPaused || isCompleted || isFailed)
                return;
                
            // Обновление таймера
            currentTime -= Time.deltaTime;
            OnTimerUpdated?.Invoke(currentTime);
            
            // Проверка на окончание времени
            if (currentTime <= 0)
            {
                FailGame();
            }
            
            // Дополнительное обновление в дочерних классах
        }
        
        /// <summary>
        /// Обработка ввода игрока
        /// </summary>
        public virtual void ProcessInput(string input)
        {
            if (isPaused || isCompleted || isFailed)
                return;
                
            // Обработка ввода в дочерних классах
        }
        
        /// <summary>
        /// Пауза игры
        /// </summary>
        public virtual void PauseGame()
        {
            if (isPaused || isCompleted || isFailed)
                return;
                
            isPaused = true;
            OnGamePaused?.Invoke();
        }
        
        /// <summary>
        /// Возобновление игры
        /// </summary>
        public virtual void ResumeGame()
        {
            if (!isPaused || isCompleted || isFailed)
                return;
                
            isPaused = false;
            OnGameResumed?.Invoke();
        }
        
        /// <summary>
        /// Завершение игры с успехом
        /// </summary>
        public virtual void CompleteGame()
        {
            if (isCompleted || isFailed)
                return;
                
            isCompleted = true;
            OnGameCompleted?.Invoke();
        }
        
        /// <summary>
        /// Завершение игры с неудачей
        /// </summary>
        public virtual void FailGame()
        {
            if (isCompleted || isFailed)
                return;
                
            isFailed = true;
            OnGameFailed?.Invoke();
        }
        
        /// <summary>
        /// Обновление счета
        /// </summary>
        protected void UpdateScore(int points)
        {
            currentScore += points;
            OnScoreUpdated?.Invoke(currentScore);
        }
        
        /// <summary>
        /// Получение текущего счета
        /// </summary>
        public int GetScore()
        {
            return currentScore;
        }
        
        /// <summary>
        /// Получение оставшегося времени
        /// </summary>
        public float GetRemainingTime()
        {
            return currentTime;
        }
    }
} 