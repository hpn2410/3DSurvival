using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Header("Sound Effect")]
    public AudioSource dropItemSound;
    public AudioSource pickUpItemSound;
    public AudioSource toolSwingSound;
    public AudioSource chopSound;
    public AudioSource rabbitHit;
    public AudioSource playerHit;
    public AudioSource playerDeath;
    public AudioSource bearHit;
    public AudioSource bearDead;
    public AudioSource bossHurt;
    public AudioSource bossDead;
    [Header("Music Effect")]
    public AudioSource startGameBGMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioSource soundToPlay)
    {
        if (!soundToPlay.isPlaying)
            soundToPlay.Play();
    }
}
