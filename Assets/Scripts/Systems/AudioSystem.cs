using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SoundEffect
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
}

public class AudioSystem : MonoBehaviour
{
    [Header("Настройки аудио")]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 0.7f;
    [SerializeField] private float sfxVolume = 1f;
    
    [Header("Звуковые эффекты")]
    [SerializeField] private List<SoundEffect> soundEffects;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    private Dictionary<string, SoundEffect> soundEffectsDict;
    private static AudioSystem instance;
    
    public static AudioSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("AudioSystem");
                    instance = go.AddComponent<AudioSystem>();
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeAudio();
    }
    
    private void InitializeAudio()
    {
        soundEffectsDict = new Dictionary<string, SoundEffect>();
        foreach (var effect in soundEffects)
        {
            soundEffectsDict[effect.name] = effect;
        }
        
        if (musicSource == null)
        {
            GameObject musicObj = new GameObject("MusicSource");
            musicObj.transform.SetParent(transform);
            musicSource = musicObj.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
        
        if (sfxSource == null)
        {
            GameObject sfxObj = new GameObject("SFXSource");
            sfxObj.transform.SetParent(transform);
            sfxSource = sfxObj.AddComponent<AudioSource>();
        }
        
        UpdateVolumes();
    }
    
    public void PlaySound(string soundName)
    {
        if (soundEffectsDict.TryGetValue(soundName, out SoundEffect effect))
        {
            sfxSource.pitch = effect.pitch;
            sfxSource.PlayOneShot(effect.clip, effect.volume * sfxVolume * masterVolume);
        }
    }
    
    public void PlayMusic(AudioClip musicClip, bool loop = true)
    {
        if (musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = loop;
            musicSource.volume = musicVolume * masterVolume;
            musicSource.Play();
        }
    }
    
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }
    
    private void UpdateVolumes()
    {
        if (musicSource != null)
        {
            musicSource.volume = musicVolume * masterVolume;
        }
        
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume * masterVolume;
        }
    }
    
    public void PauseAll()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
        
        if (sfxSource != null)
        {
            sfxSource.Pause();
        }
    }
    
    public void ResumeAll()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
        
        if (sfxSource != null)
        {
            sfxSource.UnPause();
        }
    }
} 