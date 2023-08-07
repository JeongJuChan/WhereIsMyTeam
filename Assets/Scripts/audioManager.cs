using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm;
    void Start()
    {
        audioSource.clip = bgm;
        audioSource.Play();
    }

}
