using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource SFX_Source;

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    SoundFile GetSound(AudioData _audioType, string _name)
    {
        List<SoundFile> temp = new List<SoundFile>(_audioType.audioList);

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].name == _name)
            {
                return temp[i];
            }
        }

        return null;
    }

    public void PlaySFX(AudioData _audioData, string _name)
    {
        SoundFile sound = GetSound(_audioData, _name);
        if (sound != null)
        {
            SFX_Source.volume = sound.volume;
            SFX_Source.PlayOneShot(sound.clip);
        }
    }



}
