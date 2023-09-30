using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger instance { get; private set; }
    public AudioClip pauseAudio;
    public AudioClip levelAudio;
    private AudioSource source;

    /* Sound Manager plays the background music for the current scene.
       The Class has two serialized audio clip feilds pauseAudio for the pause screen and
       levelAudio for current levels background music. 

       Provides API for enabling the different audio clips.
    */
    private void Awake()
    {
        source = GetComponent<AudioSource>();

        //keeps the sound effects same even on going to some other/new level or scene.
        if(instance == null)
        {
            instance = this;
            source = GetComponent<AudioSource>();
        }
        //destroy duplicate gameobjects
        else if(instance != null && instance != this)       //duplicate object
            Destroy(gameObject);
    }

    //Default plays the levelAudio Clip
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

    // API to Play levelAudio Clip
    public void PlayLevelAudio()
    {
        source.clip = levelAudio;
        source.Play();
    }

    // API to Play pauseAudio Clip
    public void PlayPauseAudio()
    {
        source.clip = pauseAudio;
        source.Play();
    }
}
