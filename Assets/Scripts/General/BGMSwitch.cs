using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwitch : MonoBehaviour
{
    [SerializeField] AudioClip MainMenu;
    [SerializeField] AudioClip InGame;
    [SerializeField] AudioSource audioSource;

    AudioClip current;


    private void Awake()
    {
        current = MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSong()
    {
        if (current == MainMenu)
        {
            audioSource.Stop();
            audioSource.clip = InGame;
            current = InGame;
            audioSource.Play();
        } else
        {
            audioSource.Stop();
            audioSource.clip = MainMenu;
            current = MainMenu;
            audioSource.Play();
        }
    }

}
