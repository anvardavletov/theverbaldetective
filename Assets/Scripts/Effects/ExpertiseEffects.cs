using UnityEngine;
using DG.Tweening;

public class ExpertiseEffects : MonoBehaviour
{
    [Header("Микроскоп")]
    [SerializeField] private GameObject microscopeObject;
    [SerializeField] private Transform microscopeLens;
    [SerializeField] private float lensMoveDuration = 0.5f;
    [SerializeField] private float lensMoveDistance = 0.2f;
    [SerializeField] private float lensRotateDuration = 1f;
    [SerializeField] private float lensRotateAngle = 15f;
    
    [Header("Частицы")]
    [SerializeField] private ParticleSystem rareTermEffect;
    [SerializeField] private ParticleSystem chainCompleteEffect;
    [SerializeField] private ParticleSystem completionEffect;
    
    [Header("Звуки")]
    [SerializeField] private AudioClip rareTermSound;
    [SerializeField] private AudioClip chainCompleteSound;
    [SerializeField] private AudioClip completionSound;
    [SerializeField] private AudioClip microscopeMoveSound;
    [SerializeField] private AudioClip microscopeRotateSound;
    
    private AudioSource audioSource;
    private Vector3 originalLensPosition;
    private Quaternion originalLensRotation;
    
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        
        if (microscopeLens != null)
        {
            originalLensPosition = microscopeLens.localPosition;
            originalLensRotation = microscopeLens.localRotation;
        }
    }
    
    public void Initialize()
    {
        // Инициализация микроскопа
        if (microscopeObject != null)
        {
            microscopeObject.SetActive(true);
        }
        
        if (microscopeLens != null)
        {
            microscopeLens.localPosition = originalLensPosition;
            microscopeLens.localRotation = originalLensRotation;
        }
        
        // Останавливаем все эффекты
        if (rareTermEffect != null)
        {
            rareTermEffect.Stop();
        }
        
        if (chainCompleteEffect != null)
        {
            chainCompleteEffect.Stop();
        }
        
        if (completionEffect != null)
        {
            completionEffect.Stop();
        }
    }
    
    public void PlayRareTermEffect(Vector3 position)
    {
        if (rareTermEffect != null)
        {
            rareTermEffect.transform.position = position;
            rareTermEffect.Play();
        }
        
        if (rareTermSound != null)
        {
            AudioSource.PlayClipAtPoint(rareTermSound, position, 0.5f);
        }
    }
    
    public void PlayChainCompleteEffect(Vector3 position)
    {
        if (chainCompleteEffect != null)
        {
            chainCompleteEffect.transform.position = position;
            chainCompleteEffect.Play();
        }
        
        if (chainCompleteSound != null)
        {
            AudioSource.PlayClipAtPoint(chainCompleteSound, position, 0.3f);
        }
    }
    
    public void PlayCompletionEffect(Vector3 position)
    {
        if (completionEffect != null)
        {
            completionEffect.transform.position = position;
            completionEffect.Play();
        }
        
        if (completionSound != null)
        {
            AudioSource.PlayClipAtPoint(completionSound, position, 0.5f);
        }
    }
    
    public void MoveMicroscopeLens(Vector3 targetPosition)
    {
        if (microscopeLens != null)
        {
            microscopeLens.DOLocalMove(targetPosition, lensMoveDuration)
                .SetEase(Ease.OutQuad);
                
            if (microscopeMoveSound != null)
            {
                audioSource.PlayOneShot(microscopeMoveSound);
            }
        }
    }
    
    public void RotateMicroscopeLens(float angle)
    {
        if (microscopeLens != null)
        {
            microscopeLens.DOLocalRotate(new Vector3(0f, 0f, angle), lensRotateDuration)
                .SetEase(Ease.OutQuad);
                
            if (microscopeRotateSound != null)
            {
                audioSource.PlayOneShot(microscopeRotateSound);
            }
        }
    }
    
    public void ResetMicroscopeLens()
    {
        if (microscopeLens != null)
        {
            microscopeLens.DOLocalMove(originalLensPosition, lensMoveDuration)
                .SetEase(Ease.OutQuad);
                
            microscopeLens.DOLocalRotate(originalLensRotation.eulerAngles, lensRotateDuration)
                .SetEase(Ease.OutQuad);
        }
    }
    
    public void ShakeMicroscope()
    {
        if (microscopeObject != null)
        {
            microscopeObject.transform.DOShakePosition(0.3f, 0.1f, 10, 90f, false, true)
                .SetEase(Ease.OutQuad);
        }
    }
    
    private void OnDestroy()
    {
        // Останавливаем все анимации при уничтожении объекта
        if (microscopeLens != null)
        {
            microscopeLens.DOKill();
        }
        
        if (microscopeObject != null)
        {
            microscopeObject.transform.DOKill();
        }
    }
} 