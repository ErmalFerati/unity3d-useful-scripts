using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    class Clip
    {
        public enum Type
        {
            Sfx,
            Music
        }

        public string key;
        public AudioClip clip;
        public Type type;
    }

    public static SoundManager instance;

    [SerializeField]
    private List<Clip> clips;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    private int musicIndex;

    private void Awake()
    {
        instance = this;
    }

    public void playSound(string key)
    {
        Clip clip = getClip(key);
        AudioSource source = clip.type == Clip.Type.Sfx ? sfxSource : musicSource;

        source.PlayOneShot(getClip(key).clip);
    }

    private Clip getClip(string key = null, int index = 0)
    {
        if (index != 0)
            return clips[index];

        for (int i = 0; i < clips.Count; i++)
        {
            if(clips[i].key.ToLower() == key.ToLower())
            {
                return clips[i];
            }
        }
        return null;
    }

    public void playMusicOnLoop()
    {
        for (int i = musicIndex; i < clips.Count; i++)
        {
            if (clips[i].type == Clip.Type.Music)
            {
                AudioClip clip = clips[i].clip;
                musicIndex = i + 1;
                musicSource.PlayOneShot(clip);
                Invoke("playMusicOnLoop", clip.length);
                return;
            }

            if(i == clips.Count)
                i = 0;
        }
    }

    public void setMusicVolume(float volume) { musicSource.volume = volume; }
    public void setSfxVolume(float volume) { sfxSource.volume = volume; }
}