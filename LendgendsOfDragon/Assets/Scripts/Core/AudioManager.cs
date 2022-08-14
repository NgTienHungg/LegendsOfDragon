using System;
using UnityEngine;

[Serializable]
public class Audio
{
    public string name;
    public bool loop;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [HideInInspector] public AudioSource source;
}

public class AudioManager : Singleton<AudioManager>
{
    public Audio[] musics;
    public Audio[] sounds;

    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        if (!PlayerPrefs.HasKey("OnMusic"))
            PlayerPrefs.SetInt("OnMusic", 1);

        if (!PlayerPrefs.HasKey("OnSound"))
            PlayerPrefs.SetInt("OnSound", 1);

        foreach (var music in musics)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.clip = music.clip;
            music.source.loop = music.loop;
            music.source.volume = music.volume;
        }

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
        }
    }

    private void Start()
    {
        PlayMusic("Background");
    }

    public void PlayMusic(string musicName)
    {
        if (PlayerPrefs.GetInt("OnMusic") == 0)
            return;

        Audio music = Array.Find(musics, music => music.name == musicName);

        if (music != null)
            music.source.Play();
        else
            Debug.Log("Can't find music with name: " + musicName);
    }

    public void PauseMusic(string musicName)
    {
        Audio music = Array.Find(musics, music => music.name == musicName);

        if (music != null && music.source.isPlaying)
            music.source.Pause();
    }

    public void StopMusic(string musicName)
    {
        Audio music = Array.Find(musics, music => music.name == musicName);

        if (music != null)
            music.source.Stop();
    }

    public void PlaySound(string soundName)
    {
        if (PlayerPrefs.GetInt("OnSound") == 0)
            return;

        Audio sound = Array.Find(sounds, sound => sound.name == soundName);

        if (sound != null)
            sound.source.Play();
        else
            Debug.Log("Can't find sound with name:" + soundName);
    }

    public void ContinuePlayMusic()
    {
        PlayMusic("Background");
    }

    public void StopAllMusic()
    {
        foreach (var music in musics)
            StopMusic(music.name);
    }
}