using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControll : MonoBehaviour
{
    AudioSource audioSource = null;
    [SerializeField] AudioClip BackGroundMusic;
    [SerializeField] AudioClip BattleMusic;
    [SerializeField] AudioClip PinballMusic;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        DontDestroyOnLoad(this);
    }

    public void ChangeBackGroundMusic()
    {
        this.audioSource.clip = BackGroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeBattleMusic()
    {
        this.audioSource.clip = BattleMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangePinballMusic()
    {
        this.audioSource.clip = PinballMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}
