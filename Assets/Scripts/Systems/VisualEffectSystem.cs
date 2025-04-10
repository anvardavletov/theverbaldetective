using UnityEngine;
using System.Collections;
using DG.Tweening;

public class VisualEffectSystem : MonoBehaviour
{
    [Header("Настройки эффектов")]
    [SerializeField] private float wordFoundScale = 1.2f;
    [SerializeField] private float wordFoundDuration = 0.3f;
    [SerializeField] private float hintPulseScale = 1.1f;
    [SerializeField] private float hintPulseDuration = 0.5f;
    [SerializeField] private float achievementPopupDuration = 0.5f;
    
    [Header("Префабы эффектов")]
    [SerializeField] private GameObject wordFoundEffect;
    [SerializeField] private GameObject hintEffect;
    [SerializeField] private GameObject achievementEffect;
    [SerializeField] private GameObject levelCompleteEffect;
    
    private void Start()
    {
        // Инициализация системы эффектов
    }
    
    public void PlayWordFoundEffect(Transform wordTransform)
    {
        if (wordTransform != null)
        {
            // Анимация масштабирования
            wordTransform.DOScale(wordFoundScale, wordFoundDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(() => {
                    wordTransform.DOScale(1f, wordFoundDuration * 0.5f)
                        .SetEase(Ease.InOutQuad);
                });
            
            // Создание визуального эффекта
            if (wordFoundEffect != null)
            {
                GameObject effect = Instantiate(wordFoundEffect, wordTransform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
        }
    }
    
    public void PlayHintEffect(Transform hintTransform)
    {
        if (hintTransform != null)
        {
            // Пульсирующая анимация
            hintTransform.DOScale(hintPulseScale, hintPulseDuration)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
            
            // Создание визуального эффекта
            if (hintEffect != null)
            {
                GameObject effect = Instantiate(hintEffect, hintTransform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
        }
    }
    
    public void PlayAchievementEffect(Transform achievementTransform)
    {
        if (achievementTransform != null)
        {
            // Анимация появления
            achievementTransform.localScale = Vector3.zero;
            achievementTransform.DOScale(1f, achievementPopupDuration)
                .SetEase(Ease.OutBack);
            
            // Создание визуального эффекта
            if (achievementEffect != null)
            {
                GameObject effect = Instantiate(achievementEffect, achievementTransform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }
        }
    }
    
    public void PlayLevelCompleteEffect(Transform levelTransform)
    {
        if (levelTransform != null)
        {
            // Анимация завершения уровня
            levelTransform.DOScale(1.2f, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => {
                    levelTransform.DOScale(1f, 0.3f)
                        .SetEase(Ease.InOutQuad);
                });
            
            // Создание визуального эффекта
            if (levelCompleteEffect != null)
            {
                GameObject effect = Instantiate(levelCompleteEffect, levelTransform.position, Quaternion.identity);
                Destroy(effect, 3f);
            }
        }
    }
    
    public void ShakeTransform(Transform targetTransform, float duration = 0.3f, float strength = 1f)
    {
        if (targetTransform != null)
        {
            targetTransform.DOShakePosition(duration, strength)
                .SetEase(Ease.OutQuad);
        }
    }
    
    public void FadeIn(CanvasGroup canvasGroup, float duration = 0.5f)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, duration)
                .SetEase(Ease.InOutQuad);
        }
    }
    
    public void FadeOut(CanvasGroup canvasGroup, float duration = 0.5f)
    {
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(0f, duration)
                .SetEase(Ease.InOutQuad);
        }
    }
    
    private void OnDestroy()
    {
        // Очистка всех активных анимаций
        DOTween.KillAll();
    }
} 