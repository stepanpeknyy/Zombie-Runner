using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource musicTheme;
    public AudioClip[] musicArray;

    private void Awake()
    {
        musicTheme = GetComponent<AudioSource>();
    }
    private void Start()
    {
        musicTheme.clip = musicArray[Random.Range(0, musicArray.Length)];
        musicTheme.Play(musicTheme.clip);
    }
}
