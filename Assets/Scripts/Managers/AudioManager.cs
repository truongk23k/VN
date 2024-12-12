using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] public AudioSource musicAudio;
    [SerializeField] public AudioSource musicAudio2;
    [SerializeField] public AudioSource sfx1;
    [SerializeField] public AudioSource sfx2;
    [SerializeField] public AudioSource sfx3;
    [SerializeField] public AudioSource sfx4;
    public List<AudioSource> sfxs;

    [SerializeField] public AudioClip BGMusicClip;
    [SerializeField] public AudioClip[] UIClips;

    // Start is called before the first frame update
    void Start()
    {
        sfxs.Add(sfx1);
        sfxs.Add(sfx2);
        sfxs.Add(sfx3);
        sfxs.Add(sfx4);
        DontDestroyOnLoad(this);
        musicAudio.volume = 0.05f;
        musicAudio2.volume = 0.05f;
        sfx1.volume = 0.5f;
        sfx2.volume = 0.5f;
        sfx3.volume = 0.5f;
        sfx4.volume = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip aClip, float fadeDuration, bool loop)
    {
        float fadeTime = 0;
        if (loop)
        {
            if (!musicAudio.isPlaying)
            {
                musicAudio.clip = aClip;
                musicAudio.Play();
                StartCoroutine(FadeIn(musicAudio));
            }
            else
            {
                musicAudio2.clip = aClip;
                musicAudio2.Play();
                StartCoroutine(FadeIn(musicAudio2));
            }
            
        }
        else
        {
            
            for (int i = 0; i < sfxs.Count; i++)
            {
                if (!sfxs[i].isPlaying)
                {
                    sfxs[i].clip = aClip;
                    sfxs[i].Play();
                    StartCoroutine(FadeIn(sfxs[i]));
                    break;
                }
            }
        }
        IEnumerator FadeIn(AudioSource aSource)
        {
            float defVol = aSource.volume;
            while ((fadeTime / fadeDuration) < defVol)
            {
                fadeTime += Time.deltaTime;
                aSource.volume = fadeTime / fadeDuration;
                yield return null;
            }
            yield break;
        }
    }

    public void ChangeVolumeMusic(bool turnOn)
    {
        if (!turnOn)
        {
            musicAudio.volume = 0;
        }
        else
        {
            musicAudio.volume = 0.3f;
        }
    }
    public void ChangeVolumeSFX(bool turnOn)
    {
        if (!turnOn)
        {
            sfx1.volume = 0;
            sfx2.volume = 0;
            sfx3.volume = 0;
            sfx4.volume = 0;
        }
        else
        {
            sfx1.volume = 0.5f;
            sfx2.volume = 0.5f;
            sfx3.volume = 0.5f;
            sfx4.volume = 0.5f;
        }
    }

    public void StopAll()
    {
        this.musicAudio.Stop();
        this.musicAudio2.Stop();
        this.sfx1.Stop();
        this.sfx2.Stop();
        this.sfx3.Stop();
        this.sfx4.Stop();
    }
}
