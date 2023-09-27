using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger instance { get; private set; }

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        //keeps the sound effects same even on going to some other/new level or scene.
        if(instance == null)
        {
            instance = this;
        }
        //destroy duplicate gameobjects
        else if(instance != null && instance != this)       //duplicate object
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound) 
    {
        source.PlayOneShot(_sound);         //plays an audio clip only once
    }
}
