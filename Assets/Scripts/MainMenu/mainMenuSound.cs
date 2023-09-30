using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuSound : MonoBehaviour
{
    public static mainMenuSound instance { get; private set; }

    public AudioClip waitAudio;
    public AudioClip levelAudio;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        //keeps the sound effects same even on going to some other/new level or scene.
        if (instance == null)
        {
            instance = this;
            source = GetComponent<AudioSource>();
        }
        //destroy duplicate gameobjects
        else if (instance != null && instance != this)       //duplicate object
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayLevelAudio();
    }
    public void PlaySound(AudioClip _sound)
    {
        if (_sound != null)
        {
            source.PlayOneShot(_sound);         //plays an audio clip only once
        }
    }

    public void PlayLevelAudio()
    {
        source.clip = levelAudio;
        source.Play();
    }

    public void PlayWaitAudio()
    {
        source.clip = waitAudio;
        source.Play();
    }
}
