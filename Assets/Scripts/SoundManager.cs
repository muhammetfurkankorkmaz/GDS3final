using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SoundManager is empty!!!");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] AudioSource audioSourcePrefab;

    public void PlaySoundEffect(AudioClip _audioClip, float _volume)
    {
        AudioSource currentAudioSource = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);

        currentAudioSource.clip = _audioClip;

        currentAudioSource.volume = _volume;

        currentAudioSource.Play();

        float clipLength = currentAudioSource.clip.length;

        Destroy(currentAudioSource.gameObject, clipLength);
    }
   
}//Class