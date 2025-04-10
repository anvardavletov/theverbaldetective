using UnityEngine;
using System.Collections.Generic;

namespace WordDetective.Core
{
    /// <summary>
    /// Система кэширования компонентов для оптимизации
    /// </summary>
    public class ComponentCache : MonoBehaviour
    {
        // Словарь для хранения кэшированных компонентов
        private Dictionary<string, Component> cachedComponents = new Dictionary<string, Component>();
        
        /// <summary>
        /// Получение компонента из кэша или создание нового
        /// </summary>
        public T GetComponent<T>() where T : Component
        {
            string key = typeof(T).Name;
            
            if (cachedComponents.ContainsKey(key))
            {
                return cachedComponents[key] as T;
            }
            
            T component = base.GetComponent<T>();
            
            if (component != null)
            {
                cachedComponents[key] = component;
            }
            
            return component;
        }
        
        /// <summary>
        /// Очистка кэша компонентов
        /// </summary>
        public void ClearCache()
        {
            cachedComponents.Clear();
        }
    }
    
    /// <summary>
    /// Расширение для MonoBehaviour для добавления кэширования компонентов
    /// </summary>
    public static class ComponentCacheExtension
    {
        /// <summary>
        /// Получение компонента с кэшированием
        /// </summary>
        public static T GetCachedComponent<T>(this MonoBehaviour behaviour) where T : Component
        {
            ComponentCache cache = behaviour.GetComponent<ComponentCache>();
            
            if (cache == null)
            {
                cache = behaviour.gameObject.AddComponent<ComponentCache>();
            }
            
            return cache.GetComponent<T>();
        }
    }
} 