using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [Header("SFX")]
    public AudioSource[] soundEffects;
    
    [Header("OST")] 
    public AudioSource[] ost;
    
    
    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(int sound)
    {
        soundEffects[sound].Stop();
        soundEffects[sound].Play();
    }
    
    public void StopSounds()
    {
        foreach (var fx in soundEffects)
        {
            if (fx.isPlaying)
            {
                fx.Stop();
            }
        }
        
    }
    
}
