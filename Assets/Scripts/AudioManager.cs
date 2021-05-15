using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audio;
        public float volume;
        [HideInInspector]
        public AudioSource source;

    }
    public List<Sound> sounds;
    // Start is called before the first frame update

    private void Awake()
    {
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.clip = s.audio;
        }
        Play("World");
    }
    public void Play(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        s.source.Play();

    }
    public void Stop(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        s.source.Stop();

    }
}
