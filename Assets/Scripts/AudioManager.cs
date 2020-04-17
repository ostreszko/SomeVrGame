using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public List<Sound> soundList;
    public List<Sound> GothicDeadSounds;
    System.Random rand;
    private void Awake()
    {
        foreach (Sound s in soundList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in GothicDeadSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Play("BackgroundMusic");
        rand = new System.Random();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(soundList.ToArray(), sound => sound.name == name );
        if (s == null)
        {
            Debug.LogWarning("There is no sound with" + name + " name");
            return;
        }
        s.source.Play();
    }

    public void PlayGothicDeathSound()
    {
        Sound s = GothicDeadSounds[rand.Next(GothicDeadSounds.Count)];
        if (s == null)
        {
            Debug.LogWarning("There is no sound with" + name + " name");
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(soundList.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("There is no sound with" + name + " name");
            return;
        }
        s.source.Pause();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(soundList.ToArray(), sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("There is no sound with" + name + " name");
            return false;
        }
        return s.source.isPlaying;
    }
}
