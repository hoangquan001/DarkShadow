using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip magicFireClip;
    [SerializeField] AudioClip dashClip;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public  void AttackAudio()
    {
        audioSource.PlayOneShot(attackClip);
    }
    public void DashAudio()
    {
        audioSource.PlayOneShot(dashClip);
    }

    public void MagicFireAudio()
    {
        audioSource.PlayOneShot(magicFireClip);
    }
}
