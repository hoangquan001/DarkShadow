using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public static SoundManager instance;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playAudioClip(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);

    }
}
