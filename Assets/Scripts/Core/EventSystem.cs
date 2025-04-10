using UnityEngine;
using System;
using System.Collections.Generic;

namespace WordDetective.Core
{
    /// <summary>
    /// Централизованная система событий для связи между системами
    /// </summary>
    public class EventSystem : MonoBehaviour
    {
        private static EventSystem _instance;
        public static EventSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("EventSystem");
                    _instance = go.AddComponent<EventSystem>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }
        
        // Словарь для хранения событий
        private Dictionary<string, Action<object>> events = new Dictionary<string, Action<object>>();
        
        /// <summary>
        /// Подписка на событие
        /// </summary>
        public void Subscribe(string eventName, Action<object> callback)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = null;
            }
            
            events[eventName] += callback;
        }
        
        /// <summary>
        /// Отписка от события
        /// </summary>
        public void Unsubscribe(string eventName, Action<object> callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName] -= callback;
            }
        }
        
        /// <summary>
        /// Вызов события
        /// </summary>
        public void TriggerEvent(string eventName, object data = null)
        {
            if (events.ContainsKey(eventName) && events[eventName] != null)
            {
                events[eventName].Invoke(data);
            }
        }
        
        /// <summary>
        /// Очистка всех событий
        /// </summary>
        public void ClearAllEvents()
        {
            events.Clear();
        }
        
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
    
    /// <summary>
    /// Статические константы для имен событий
    /// </summary>
    public static class GameEvents
    {
        // События игрового процесса
        public const string GAME_STARTED = "GameStarted";
        public const string GAME_PAUSED = "GamePaused";
        public const string GAME_RESUMED = "GameResumed";
        public const string GAME_COMPLETED = "GameCompleted";
        public const string GAME_FAILED = "GameFailed";
        
        // События режимов
        public const string MYSTERY_STORY_STARTED = "MysteryStoryStarted";
        public const string PHOTOFIT_STARTED = "PhotofitStarted";
        public const string CRIME_SCENE_STARTED = "CrimeSceneStarted";
        public const string EXPERTISE_STARTED = "ExpertiseStarted";
        
        // События UI
        public const string UI_MENU_OPENED = "UIMenuOpened";
        public const string UI_MENU_CLOSED = "UIMenuClosed";
        public const string UI_DIALOG_SHOWN = "UIDialogShown";
        public const string UI_DIALOG_CLOSED = "UIDialogClosed";
        
        // События прогресса
        public const string LEVEL_COMPLETED = "LevelCompleted";
        public const string LEVEL_FAILED = "LevelFailed";
        public const string SCORE_UPDATED = "ScoreUpdated";
        public const string COINS_EARNED = "CoinsEarned";
        public const string COINS_SPENT = "CoinsSpent";
        public const string HINT_USED = "HintUsed";
        public const string ACHIEVEMENT_UNLOCKED = "AchievementUnlocked";
        
        // События монетизации
        public const string AD_SHOWN = "AdShown";
        public const string AD_FAILED = "AdFailed";
        public const string PURCHASE_COMPLETED = "PurchaseCompleted";
        public const string PURCHASE_FAILED = "PurchaseFailed";
        
        // События сохранения
        public const string GAME_SAVED = "GameSaved";
        public const string GAME_LOADED = "GameLoaded";
        
        // События локализации
        public const string LANGUAGE_CHANGED = "LanguageChanged";
    }
} 